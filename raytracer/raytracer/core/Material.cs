﻿namespace raytracer.core
{
    public abstract class Material
    {
        public abstract BSDF GetBSDF(Intersection intersection);
    }

    public class MatteMaterial : Material
    {
        private readonly LambertianReflection _brdf = new LambertianReflection(SampledSpectrum.Random());

        public override BSDF GetBSDF(Intersection intersection)
        {
            return new BSDF(_brdf, new BTDF());
        }
    }
}