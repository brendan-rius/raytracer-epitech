﻿using raytracer.core;

namespace raytracer.primitives
{
    public class Plane : Shape
    {
        public override bool TryToIntersect(ref Ray ray, ref Intersection intersection)
        {
            var rayInObjectWorld = WorldToObjectTransformation.TransformRay(ref ray);
            if (ray.Direction.Y == 0f) return false;
            var t = -ray.Origin.Y/ray.Direction.Y;
            if (t < ray.Start || t > ray.End)
                return false;
            var intersectionPoint = rayInObjectWorld.PointAtTime(t);
            intersection.Point =
                WorldToObjectTransformation.InverseTransformation.TransformPoint(ref intersectionPoint);
            return true;
        }

        public override bool Intersect(ref Ray ray)
        {
            var rayInObjectWorld = WorldToObjectTransformation.TransformRay(ref ray);
            if (ray.Direction.Y == 0f) return false;
            var t = -ray.Origin.Y / ray.Direction.Y;
            return !(t < ray.Start) && !(t > ray.End);
        }
    }
}