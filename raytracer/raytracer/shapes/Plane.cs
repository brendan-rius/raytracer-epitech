using OpenTK;
using raytracer.core;
using raytracer.core.mathematics;

namespace raytracer.primitives
{
    public class Plane : Shape
    {
        public Plane(Transformation worldToObjectTransformation = null) : base(worldToObjectTransformation)
        {
        }

        public override bool TryToIntersect(ref Ray ray, ref Intersection intersection)
        {
            var rayInObjectWorld = WorldToObjectTransformation.TransformRay(ref ray);
            if (rayInObjectWorld.Direction.Y == 0f) return false;
            var t = -rayInObjectWorld.Origin.Y/rayInObjectWorld.Direction.Y;
            if (t < rayInObjectWorld.Start || t > rayInObjectWorld.End)
                return false;
            var intersectionPoint = rayInObjectWorld.PointAtTime(t);
            intersection.Point =
                WorldToObjectTransformation.InverseTransformation.TransformPoint(ref intersectionPoint);
            var normal = new Vector3(0, 1, 0);
            intersection.NormalVector = WorldToObjectTransformation.InverseTransformation.TransformVector(ref normal);
            return true;
        }

        public override bool Intersect(ref Ray ray)
        {
            var rayInObjectWorld = WorldToObjectTransformation.TransformRay(ref ray);
            if (rayInObjectWorld.Direction.Y == 0f) return false;
            var t = -rayInObjectWorld.Origin.Y / rayInObjectWorld.Direction.Y;
            return !(t < rayInObjectWorld.Start) && !(t > rayInObjectWorld.End);
        }
    }
}