using System;
using OpenTK;
using raytracer.core;
using raytracer.core.mathematics;

namespace librt3.core.reflection
{
    internal class Microfacet : BxDF
    {
        private readonly MicrofacetDistribution _distribution;
        private readonly Fresnel _fresnel;
        private readonly SampledSpectrum _spectrum;

        public Microfacet(SampledSpectrum reflectance, Fresnel f, MicrofacetDistribution d)
            : base(BxDFType.Reflection | BxDFType.Glossy)
        {
            _spectrum = reflectance;
            _distribution = d;
            _fresnel = f;
        }

        public override SampledSpectrum F(Vector3 incoming, Vector3 leaving)
        {
            var cosThetaO = AbsCosTheta(ref leaving);
            var cosThetaI = AbsCosTheta(ref incoming);
            if (cosThetaI == 0 || cosThetaO == 0) return SampledSpectrum.Black();
            var wh = Vector3.Add(incoming, leaving).Normalized();
            var cosThetaH = Vector3.Dot(incoming, leaving);
            var f = _fresnel.Evaluate(cosThetaH);
            return _spectrum*_distribution.D(ref wh)*G(ref incoming, ref leaving, ref wh)*f/(4*cosThetaI*cosThetaO);
        }

        public float G(ref Vector3 leaving, ref Vector3 incoming, ref Vector3 half)
        {
            var NdotWh = AbsCosTheta(ref half);
            var NdotWo = AbsCosTheta(ref leaving);
            var NdotWi = AbsCosTheta(ref incoming);
            var WOdotWh = AbsDot(ref leaving, ref half);
            return Math.Min(1, Math.Min((2*NdotWh*NdotWo/WOdotWh),
                (2*NdotWh*NdotWi/WOdotWh)));
        }
    }
}