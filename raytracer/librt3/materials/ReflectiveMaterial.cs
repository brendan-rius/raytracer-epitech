using raytracer.core;

namespace raytracer.materials
{
    public class ReflectiveMaterial : Material
    {
        private readonly SpecularReflection _specular;

        public ReflectiveMaterial(SampledSpectrum spectrum = null, Fresnel fresnel = null)
        {
            _specular = new SpecularReflection(1, spectrum ?? SampledSpectrum.Random());
        }

        public override BSDF GetBSDF(ref Intersection intersection)
        {
            var bsdf = new BSDF(ref intersection);
            bsdf.AddBxDF(_specular);
            return bsdf;
        }
    }
}