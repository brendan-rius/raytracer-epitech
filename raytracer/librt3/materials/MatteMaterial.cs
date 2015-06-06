using raytracer.core;

namespace raytracer.materials
{
    public class MatteMaterial : Material
    {
        private readonly LambertianReflection _lambertianReflection;

        public MatteMaterial(SampledSpectrum spectrum = null)
        {
            _lambertianReflection = new LambertianReflection(spectrum ?? SampledSpectrum.Random());
        }

        public override BSDF GetBSDF(ref Intersection intersection)
        {
            var bsdf = new BSDF(ref intersection);
            bsdf.AddBxDF(_lambertianReflection);
            return bsdf;
        }
    }
}