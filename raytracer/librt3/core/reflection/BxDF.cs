using System;
using OpenTK;

namespace raytracer.core.mathematics
{
    public abstract class BxDF
    {
        /// <summary>
        ///     Represent the different types a function can be
        /// </summary>
        [Flags]
        public enum BxDFType : uint
        {
            /// <summary>
            ///     The function if reflective
            /// </summary>
            Reflection = (1 << 0),

            /// <summary>
            ///     The function if transmitive
            /// </summary>
            Transmission = (1 << 1),

            /// <summary>
            ///     The reflection/transmission is specular
            /// </summary>
            Specular = (1 << 2),

            /// <summary>
            ///     The reflection/transmission is diffuse
            /// </summary>
            Diffuse = (1 << 3),

            /// <summary>
            ///     The reflection/transmission is diffuse or specular
            /// </summary>
            AllTypes = Diffuse | Specular,

            /// <summary>
            ///     The reflection is diffuse or specular
            /// </summary>
            AllReflection = Reflection | AllTypes,

            /// <summary>
            ///     The transmission is diffuse or specular
            /// </summary>
            AllTransmission = Transmission | AllTypes,

            /// <summary>
            ///     All
            /// </summary>
            All = AllReflection | AllTransmission
        }

        /// <summary>
        ///     The types of the function
        /// </summary>
        public BxDFType Type;

        protected BxDF(BxDFType type)
        {
            Type = type;
        }

        /// <summary>
        ///     Computes the distribution of light coming from a vector and leaving by
        ///     another vector. This function assumes we already know the incoming vector,
        ///     which may not be the case (since there are special cases like specular reflection
        ///     where there is only a single direction reflected into another single direction).
        /// </summary>
        /// <param name="incoming">the incoming vector</param>
        /// <param name="leaving">the leaving vector</param>
        /// <returns>the distribution of light.</returns>
        public abstract SampledSpectrum F(Vector3 incoming, Vector3 leaving);

        /// <summary>
        ///     Computes the distribution of light leaving the surface by a vector,
        ///     without having to pass the incoming vector. Note that the incoming
        ///     vector will be filled.
        /// </summary>
        /// <param name="leaving">the leaving vector</param>
        /// <param name="incoming">the incoming vector</param>
        /// <returns>te distribution of light</returns>
        public virtual SampledSpectrum Sample(Vector3 leaving, out Vector3 incoming)
        {
            incoming = new Vector3(0, 1, 0);
            if (leaving.Z < 0f)
                incoming.Z *= -1;
            return F(leaving, incoming);
        }

        /// <summary>
        ///     Compute the cosine of theta of a vector in the reflection coordinate system
        /// </summary>
        /// <param name="v">the vector</param>
        /// <returns>the cosine of theta</returns>
        protected static float CosTheta(ref Vector3 v)
        {
            return v.Z;
        }

        protected static float AbsCosTheta(ref Vector3 v)
        {
            return Math.Abs(v.Z);
        }

        protected static float SinThetaSquared(ref Vector3 v)
        {
            return 1f - CosTheta(ref v)*CosTheta(ref v);
        }

        protected static float SinTheta(ref Vector3 v)
        {
            return (float) Math.Sqrt(SinThetaSquared(ref v));
        }

        protected static float CosPhi(ref Vector3 v)
        {
            var sintheta = SinTheta(ref v);
            return sintheta == 0f ? 1 : v.X/sintheta;
        }

        protected static float SinPhi(ref Vector3 v)
        {
            var sintheta = SinTheta(ref v);
            return sintheta == 0f ? 0f : v.Y/sintheta;
        }
    }
}