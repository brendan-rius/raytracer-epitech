using System.Numerics;
using OpenTK;

namespace raytracer.core
{
    public class BSDF
    {
        public BSDF(BRDF brdf, BTDF btdf)
        {
            BRDF = brdf;
            BTDF = btdf;
        }

        public BRDF BRDF { get; private set; }
        public BTDF BTDF { get; private set; }

        public SampledSpectrum F(Vector3 incoming, Vector3 leaving)
        {
            return BRDF.BidirectionalScattering(incoming, leaving);
        }
    }
}