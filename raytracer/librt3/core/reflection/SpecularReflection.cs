using OpenTK;
using raytracer.core.mathematics;

namespace raytracer.core
{
    public class SpecularReflection : BxDF
    {
        /// <summary>
        ///     Describe the dielectric/conductor properties on surface.
        ///     Can be set to null
        /// </summary>
        private readonly Fresnel _fresnel;

        /// <summary>
        ///     The reflectiveness property of the object (if any)
        /// </summary>
        private readonly float _reflectiveness;

        /// <summary>
        ///     A scaling spectrum
        /// </summary>
        private readonly SampledSpectrum _spectrum;

        /// <summary>
        ///     Create a specular reflection model based on Snell's laws.
        /// </summary>
        /// <param name="fresnel">the fresnel properties of the material</param>
        /// <param name="spectrum">a scaling spectrum</param>
        public SpecularReflection(Fresnel fresnel = null, SampledSpectrum spectrum = null)
            : base(BxDFType.Reflection | BxDFType.Specular)
        {
            _spectrum = spectrum ?? new SampledSpectrum(1);
            _fresnel = fresnel;
        }

        /// <summary>
        ///     Creates a specular reflection model based on unrealistic properties
        /// </summary>
        /// <param name="reflectiveness">the reflectiveness (between 0 and 1000)</param>
        /// <param name="spectrum">a scaling spectrum</param>
        public SpecularReflection(float reflectiveness = 1, SampledSpectrum spectrum = null)
            : base(BxDFType.Reflection | BxDFType.Specular)
        {
            _spectrum = spectrum ?? new SampledSpectrum(1);
            _reflectiveness = MathHelper.Clamp(reflectiveness, 0, 1000);
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
        public override SampledSpectrum Sample(Vector3 leaving, out Vector3 incoming)
        {
            /* Since we are in the reflection system, rotating the vector by PI radians
             * around the normal consists of negating its x component and its y component */
            incoming = new Vector3(-leaving.X, -leaving.Y, leaving.Z);
            var abscostheta = AbsCosTheta(ref incoming);

            var reflectedLight = SampledSpectrum.Black();
            if (_fresnel != null)
            {
                var reflectedAmount = _fresnel.Evaluate(CosTheta(ref incoming));
                reflectedLight = _spectrum*reflectedAmount/abscostheta;
            }
            else
            {
                reflectedLight = _spectrum*_reflectiveness/abscostheta;
            }
            return reflectedLight;
        }
    }
}