using raytracer.core.mathematics;

namespace raytracer.core
{
    public abstract class BRDF : BxDF
    {
        protected BRDF() : base(BxDFType.Reflection)
        {
        }
    }
}