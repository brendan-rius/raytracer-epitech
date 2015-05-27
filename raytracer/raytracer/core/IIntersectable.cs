namespace raytracer.core
{
    public interface IIntersectable
    {
        bool TryToIntersect(ref Ray ray, ref Intersection intersection);
    }
}