using raytracer.core;

namespace raytracer.materials
{
    public class TestMaterial : Material
    {
        private readonly float _reflectiveness;
        private readonly SampledSpectrum _spectrum;

        public TestMaterial(float? reflectiveness = null, SampledSpectrum spectrum = null)
        {
            _reflectiveness = reflectiveness ?? StaticRandom.NextFloat();
            _spectrum = spectrum ?? SampledSpectrum.Random()*0.3f;
        }

        public override BSDF GetBSDF(ref Intersection intersection)
        {
            var bsdf = new BSDF(ref intersection);
            //bsdf.AddBxDF(new LambertianReflection(_spectrum));
            bsdf.AddBxDF(new SpecularReflection(new FresnelDielectric(1.23f, 1.0f)));
            return bsdf;
        }
    }
}