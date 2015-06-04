namespace raytracer.core
{
    public abstract class Material
    {
        public abstract BSDF GetBSDF(ref Intersection intersection);
    }
}