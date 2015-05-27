using System;

namespace raytracer.core
{
    public abstract class Material
    {
        public abstract BSDF GetBSDF(Intersection intersection);
    }

    public class MatteMaterial : Material
    {
        public override BSDF GetBSDF(Intersection intersection)
        {
            throw new NotImplementedException();
        }
    }
}