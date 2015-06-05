using raytracer.core;
using raytracer.core.reflection;

namespace raytracer.materials
{
    public class GlassMaterial : Material
    {
        private readonly SpecularTransmission _transmission;

        public GlassMaterial(float refractionIndex1, float refractionIndex2)
        {
            _transmission = new SpecularTransmission(refractionIndex1, refractionIndex2);
        }

        public override BSDF GetBSDF(ref Intersection intersection)
        {
            var bsdf = new BSDF(ref intersection);
            bsdf.AddBxDF(_transmission);
            return bsdf;
        }
    }
}