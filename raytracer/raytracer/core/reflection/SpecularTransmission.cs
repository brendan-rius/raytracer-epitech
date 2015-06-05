using System;
using OpenTK;
using raytracer.core.mathematics;

namespace raytracer.core.reflection
{
    public class SpecularTransmission : BxDF
    {
        private readonly FresnelDielectric _fresnel;
        private readonly SampledSpectrum _s;
        private readonly float IndexOfRefractionIncident;
        private readonly float IndexOfRefractionTransmitted;

        public SpecularTransmission(float indexOfRefractionIncident, float indexOfRefractionTransmitted,
            SampledSpectrum s = null) : base(BxDFType.Transmission | BxDFType.Specular)
        {
            _fresnel = new FresnelDielectric(indexOfRefractionIncident, indexOfRefractionTransmitted);
            _s = s ?? SampledSpectrum.Random();
            IndexOfRefractionIncident = indexOfRefractionIncident;
            IndexOfRefractionTransmitted = indexOfRefractionTransmitted;
        }

        public override SampledSpectrum F(Vector3 incoming, Vector3 leaving)
        {
            return SampledSpectrum.Black();
        }

        public override SampledSpectrum Sample(Vector3 leaving, out Vector3 incoming)
        {
            var entering = CosTheta(ref leaving) > 0;
            float ei, et;
            if (entering)
            {
                ei = IndexOfRefractionIncident;
                et = IndexOfRefractionTransmitted;
            }
            else
            {
                et = IndexOfRefractionIncident;
                ei = IndexOfRefractionTransmitted;
            }
            var sinIncidentSquared = SinThetaSquared(ref leaving);
            var eta = ei/et;
            var sinTransmittedSquared = eta*eta*sinIncidentSquared;
            var cosTransmitted = (float) Math.Sqrt(1 - sinTransmittedSquared);
            if (entering)
                cosTransmitted = -cosTransmitted;
            var sinTOversinI = eta;
            incoming = new Vector3(sinTOversinI*-leaving.X, sinTOversinI*-leaving.Y, cosTransmitted);
            if (sinTransmittedSquared >= 1)
                return SampledSpectrum.Black();
            var f = _fresnel.Evaluate(CosTheta(ref leaving));
            return (new SampledSpectrum(1) - f)*_s/AbsCosTheta(ref incoming)*(et*et)/(ei*ei);
        }
    }
}