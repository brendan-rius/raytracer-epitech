﻿using OpenTK;
using raytracer.core;
using raytracer.core.mathematics;

namespace raytracer.primitives
{
    public class Sphere : Shape
    {
        public Sphere(Transformation worldToObjectTransformation = null) : base(worldToObjectTransformation)
        {
        }

        public override bool TryToIntersect(ref Ray ray, ref Intersection intersection)
        {
            Ray rayInObjectWorld;
            WorldToObjectTransformation.TransformRay(ref ray, out rayInObjectWorld);
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
            if (t1 < ray.Start)
            {
                thit = t2;
                if (thit > ray.End) return false;
            }
            var intersectionPoint = rayInObjectWorld.PointAtTime(thit);
            intersection.Point = WorldToObjectTransformation.InverseTransformation.TransformPoint(ref intersectionPoint);
            intersection.NormalVector =
                WorldToObjectTransformation.InverseTransformation.TransformVector(ref intersectionPoint).Normalized();
            return true;
        }

        public override bool Intersect(ref Ray ray)
        {
            Ray rayInObjectWorld;
            WorldToObjectTransformation.TransformRay(ref ray, out rayInObjectWorld);
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
            if (!(t1 < ray.Start)) return true;
            thit = t2;
            return !(thit > ray.End);
        }
    }
}