using OpenTK;
using raytracer.core.mathematics;

namespace raytracer.core
{
    public class SpecularReflection : BxDF
    {
        /// <summary>
        ///     Describe the dielectric/conductor properties on surface
        /// </summary>
        private readonly Fresnel _fresnel;

        /// <summary>
        ///     A scaling spectrum
        /// </summary>
        private readonly SampledSpectrum _spectrum;

        public SpecularReflection(Fresnel fresnel, SampledSpectrum spectrum = null) : base(BxDFType.Reflection)
        {
            _spectrum = spectrum ?? new SampledSpectrum(1);
            _fresnel = fresnel;
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        ///     This function returns nothing since it is nearly impossible to have
        ///     a correct incoming vector (there is only a single incoming vector)
        /// </remarks>
        /// <param name="incoming"></param>
        /// <param name="leaving"></param>
        /// <returns></returns>
        public override SampledSpectrum F(Vector3 incoming, Vector3 leaving)
        {
            return SampledSpectrum.Black();
        }

        /// <summary>
        /// </summary>
        /// <remarks>This function let us choose the incoming vector</remarks>
        /// <param name="leaving"></param>
        /// <param name="incoming"></param>
        /// <returns></returns>
        public override SampledSpectrum Sample(ref Vector3 leaving, out Vector3 incoming)
        {
            /* Since we are in the reflection system, rotating the vector by PI radians
             * around the normal consists of negating its x component and its y component */
            incoming = new Vector3(-leaving.X, -leaving.Y, leaving.Z);
            return _spectrum*_fresnel.Evaluate(CosTheta(ref leaving))/AbsCosTheta(ref incoming);
        }
    }
}