using System;
using OpenTK;
using raytracer.core;
using raytracer.core.mathematics;

namespace librt3.core.reflection
{
    public class OrenNayar : BxDF
    {
        private readonly float _a;
        private readonly float _b;
        private readonly SampledSpectrum _spectrum;

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
            if (sinthetai > 1e-4 && sinthetao > 1e-4)
            {
                var sinphii = SinPhi(ref incoming);
                var cosphii = CosPhi(ref incoming);
                var sinphio = SinPhi(ref leaving);
                var cosphio = CosPhi(ref leaving);
                var dcos = cosphio*cosphii + sinphii*sinphio;
                maxcos = Math.Max(0f, dcos);
            }
            float sinalpha, tanbeta;
            if (AbsCosTheta(ref incoming) > AbsCosTheta(ref leaving))
            {
                sinalpha = sinthetao;
                tanbeta = sinthetai/AbsCosTheta(ref incoming);
            }
            else
            {
                sinalpha = sinthetai;
                tanbeta = sinthetao/AbsCosTheta(ref leaving);
            }
            return _spectrum*(float) (1/Math.PI)*(_a + _b*maxcos*sinalpha*tanbeta);
        }
    }
}