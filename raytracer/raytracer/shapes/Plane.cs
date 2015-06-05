using OpenTK;
using raytracer.core;
using raytracer.core.mathematics;

namespace raytracer.shapes
{
    public class Plane : Shape
    {
        public Plane(Transformation worldToObjectTransformation = null) : base(worldToObjectTransformation)
        {
        }

        public override bool TryToIntersect(Ray ray, ref Intersection intersection)
        {
            var rayInObjectWorld = WorldToObjectTransformation.TransformRay(ray);
            if (rayInObjectWorld.Direction.Y == 0f) return false;
            var t = -rayInObjectWorld.Origin.Y/rayInObjectWorld.Direction.Y;
            if (t < rayInObjectWorld.Start || t > rayInObjectWorld.End)
                return false;
            var intersectionPoint = rayInObjectWorld.PointAtTime(t);
            intersection.Point =
                WorldToObjectTransformation.InverseTransformation.TransformPoint(ref intersectionPoint);
            var normal = new Vector3(0, 1, 0);
            intersection.NormalVector =
                WorldToObjectTransformation.InverseTransformation.TransformNormal(normal).Normalized();
            intersection.Distance = (ray.Origin - intersection.Point).Length;
            intersection.PointDifferentialOverU = new Vector3(1, 0, 0);
            intersection.PointDifferentialOverV = new Vector3(0, 0, 1);
            return true;
        }

        public override bool Intersect(Ray ray)
        {
            var rayInObjectWorld = WorldToObjectTransformation.TransformRay(ray);
            if (rayInObjectWorld.Direction.Y == 0f) return false;
            var t = -rayInObjectWorld.Origin.Y/rayInObjectWorld.Direction.Y;
            return !(t < rayInObjectWorld.Start) && !(t > rayInObjectWorld.End);
        }
    }
}