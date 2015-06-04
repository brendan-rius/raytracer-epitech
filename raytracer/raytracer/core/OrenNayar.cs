using System;
using OpenTK;
using raytracer.core.mathematics;

namespace raytracer.core
{
    public class OrenNayar : BxDF
    {
        private const float InvPi = (float) (1/Math.PI);
        private readonly SampledSpectrum _reflectance;
        private readonly float A, B;

        public OrenNayar(SampledSpectrum reflectance, float sig) : base(BxDFType.Reflection)
        {
            _reflectance = reflectance;
            var sigma = MathHelper.DegreesToRadians(sig);
            var sigma2 = sigma*sigma;
            A = 1f - (sigma2/(sigma2 + 0.33f));
            B = (0.45F*sigma2)/(sigma2 + 0.09f);
        }

        public override SampledSpectrum F(Vector3 incoming, Vector3 leaving)
        {
            var sinthetaIncoming = SinTheta(ref incoming);
            var sinthetaLeaving = SinTheta(ref leaving);
            var maxcos = 0f;
            if (sinthetaIncoming > 1e-4 && sinthetaLeaving > 1e-4)
            {
                float sinphiIncoming = SinPhi(ref incoming), cosphiIncoming = CosPhi(ref incoming);
                float sinphiLeaving = SinPhi(ref leaving), cosphiLeaving = CosPhi(ref leaving);
                float dcos = cosphiIncoming*cosphiLeaving + sinphiIncoming*sinphiLeaving;
                maxcos = Math.Max(0, dcos);
            }
            float sinalpha, tanbeta;
            if (AbsCosTheta(ref incoming) > AbsCosTheta(ref leaving))
            {
                sinalpha = sinthetaLeaving;
                tanbeta = sinthetaIncoming/AbsCosTheta(ref incoming);
            }
            else
            {
                sinalpha = sinthetaIncoming;
                tanbeta = sinthetaLeaving/AbsCosTheta(ref leaving);
            }
            return _reflectance*InvPi*(A + B*maxcos*sinalpha*tanbeta);
        }
    }
}