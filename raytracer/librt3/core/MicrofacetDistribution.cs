using OpenTK;

namespace librt3.core
{
    internal abstract class MicrofacetDistribution
    {
        public abstract float D(ref Vector3 half);
    }
}