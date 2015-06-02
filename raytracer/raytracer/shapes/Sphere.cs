using OpenTK;
using raytracer.core;
using raytracer.core.mathematics;

namespace raytracer.primitives
{
    public class Sphere : Shape
    {
        public Sphere(Transformation worldToObjectTransformation = null) : base(worldToObjectTransformation)
        {
        }

        public override bool TryToIntersect(Ray ray, ref Intersection intersection)
        {
            var rayInObjectWorld = WorldToObjectTransformation.TransformRay(ray);
            float a, b, c;
            Vector3.Dot(ref rayInObjectWorld.Direction, ref rayInObjectWorld.Direction, out a);
            Vector3.Dot(ref rayInObjectWorld.Origin, ref rayInObjectWorld.Direction, out b);
            b += b;
            Vector3.Dot(ref rayInObjectWorld.Origin, ref rayInObjectWorld.Origin, out c);
            c -= 1;

            float t1, t2;
            if (!Solver.TrySolvePolynomial2(a, b, c, out t1, out t2))
                return false;
            var thit = t1;
            if (t1 < rayInObjectWorld.Start)
            {
                thit = t2;
                if (thit > rayInObjectWorld.End) return false;
            }
            var intersectionPoint = rayInObjectWorld.PointAtTime(thit);
            intersection.Point = WorldToObjectTransformation.InverseTransformation.TransformPoint(ref intersectionPoint);
            intersection.NormalVector =
                WorldToObjectTransformation.InverseTransformation.TransformNormal(intersectionPoint);
            intersection.Distance = (ray.Origin - intersection.Point).Length;
            return true;
        }

        public override bool Intersect(Ray ray)
        {
            var rayInObjectWorld = WorldToObjectTransformation.TransformRay(ray);
            float a, b, c;
            Vector3.Dot(ref rayInObjectWorld.Direction, ref rayInObjectWorld.Direction, out a);
            Vector3.Dot(ref rayInObjectWorld.Origin, ref rayInObjectWorld.Direction, out b);
            b += b;
            Vector3.Dot(ref rayInObjectWorld.Origin, ref rayInObjectWorld.Origin, out c);
            c -= 1;

            float t1, t2;
            if (!Solver.TrySolvePolynomial2(a, b, c, out t1, out t2))
                return false;
            var thit = t1;
            if (!(t1 < rayInObjectWorld.Start)) return true;
            thit = t2;
            return !(thit > rayInObjectWorld.End);
        }
    }
}