using System.Runtime.InteropServices;
using OpenTK;
using raytracer.core;
using raytracer.core.mathematics;

namespace librt3.core.reflection
{
    public class OrenNayar : BxDF
    {
        private float _a;
        private float _b;
        private SampledSpectrum _spectrum;

        public OrenNayar(SampledSpectrum spectrum, float sigma) : base(BxDFType.Reflection | BxDFType.Diffuse)
        {
            var sig = MathHelper.DegreesToRadians(sigma);
            var sigsqrd = sig*sig;
            _a = 1f - (sigsqrd/(2*(sigsqrd + 0.33f)));
            _b = (0.45f*sigsqrd)/(sigsqrd + 0.09f);
            _spectrum = spectrum;
        }

        public override SampledSpectrum F(Vector3 incoming, Vector3 leaving)
        {
            var sinthetai = SinTheta(ref incoming);
            var sinthetao = SinTheta(ref leaving);
            var maxcos = 0f;
            return SampledSpectrum.Black();
        }
    }
}