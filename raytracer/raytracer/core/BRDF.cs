using System;
using OpenTK;
using raytracer.core.mathematics;

namespace raytracer.core
{
    public class BRDF : BxDF
    {
        public override SampledSpectrum BidirectionalScattering(Vector3 incoming, Vector3 leaving)
        {
            return SampledSpectrum.Random();
        }

        public override SampledSpectrum Reflectance(Vector3 leaving)
        {
            throw new NotImplementedException();
        }
    }
}