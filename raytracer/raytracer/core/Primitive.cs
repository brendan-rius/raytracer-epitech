namespace raytracer.core
{
    public class Primitive : IIntersectable
    {
        public Primitive(Shape shape, Material material)
        {
            Shape = shape;
            Material = material;
        }

        public Shape Shape { get; private set; }
        public Material Material { get; private set; }

        public bool TryToIntersect(ref Ray ray, ref Intersection intersection)
        {
            if (!Shape.TryToIntersect(ref ray, ref intersection)) return false;
            intersection.Primitive = this;
            return true;
        }

        public BSDF GetBSDF(Intersection intersection)
        {
            return Material.GetBSDF(intersection);
        }
    }
}