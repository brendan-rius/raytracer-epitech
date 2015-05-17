using raytracer.core;
using raytracer.core.mathematics;

namespace raytracer.primitives
{
    public class Plane : GeometricElement
    {
        public override bool TryToIntersect(ref Ray ray, ref DifferentialGeometry differentialGeometry)
        {
            var rayInObjectWorld = WorldToObjectTransformation.TransformRay(ref ray);
            if (ray.Direction.Y == 0f) return false;
            var t = -ray.Origin.Y/ray.Direction.Y;
            if (t < ray.Start || t > ray.End)
                return false;
            var intersectionPoint = rayInObjectWorld.PointAtTime(t);
            differentialGeometry.Point =
                WorldToObjectTransformation.InverseTransformation.TransformPoint(ref intersectionPoint);
            return true;
        }
    }
}   