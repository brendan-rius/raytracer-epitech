using OpenTK;

namespace librt3.core
{
    public abstract class MicrofacetDistribution
    {
        public abstract float D(ref Vector3 half);
    }
}