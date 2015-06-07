using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using raytracer.core;
using raytracer.core.mathematics;

namespace librt3.core.reflection
{
    class FresnelBlend : BxDF
    {
        private MicrofacetDistribution _distribution;

        private SampledSpectrum _rd;

        private SampledSpectrum _rs;

        public FresnelBlend(SampledSpectrum d, SampledSpectrum s, MicrofacetDistribution dist) : base(BxDFType.Reflection | BxDFType.Glossy)
        {
            _distribution = dist;
            _rd = d;
            _rs = s;
        }

        public SampledSpectrum SchlickFresnel(float costheta)
        {
            return _rs + (new SampledSpectrum(1) - _rs)*(float)Math.Pow(1 - costheta, 5);
        }

        public override SampledSpectrum F(Vector3 incoming, Vector3 leaving)
        {
            var diffuse = _rd*(float)(28f/(23*Math.PI))*(new SampledSpectrum(1) - _rs)*
                          (1f - (float) Math.Pow(1 - 0.5*AbsCosTheta(ref incoming), 5))*
                          (1f - (float) Math.Pow(1 - 0.5*AbsCosTheta(ref leaving), 5));
            var half = (incoming + leaving).Normalized();
            var specular = new SampledSpectrum(_distribution.D(ref half)) /
                           (new SampledSpectrum(4 * AbsDot(ref incoming, ref leaving) *
                            Math.Max(AbsCosTheta(ref incoming), AbsCosTheta(ref leaving))) *
                            SchlickFresnel(Vector3.Dot(incoming, half)));
            return diffuse + specular;
        }
    }
}
