using System;
using OpenTK;
using raytracer.core.mathematics;

namespace raytracer.core
{
    public class LambertianReflection : BxDF
    {
        /// <summary>
        ///     The inverse of PI.
        /// </summary>
        public const float INV_PI = 1/(float) Math.PI;

        private readonly SampledSpectrum _spectrum;

        public LambertianReflection(SampledSpectrum spectrum) : base(BxDFType.Reflection | BxDFType.Diffuse)
        {
            _spectrum = spectrum ?? SampledSpectrum.Random();
        }

        public override SampledSpectrum F(Vector3 incoming, Vector3 leaving)
        {
            return _spectrum*INV_PI;
        }
    }
}