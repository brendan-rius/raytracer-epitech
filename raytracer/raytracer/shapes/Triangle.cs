using System;
using OpenTK;
using raytracer.core;

namespace raytracer.shapes
{
    public class Triangle : Shape
    {
        /// <summary>
        ///     Stores the triangle's plane normalized normal vector.
        /// </summary>
        private readonly Vector3 _planeNormal;

        /// <summary>
        ///     Stores the three triangle vertices.
        /// </summary>
        public Vector3[] Vertices { get; private set; }

        /// <summary>
        ///     Creates a new Triangle from 3 vertices
        /// </summary>
        /// <param name="vertices"></param>
        public Triangle(Vector3[] vertices)
        {
            if (vertices.Length != 3)
                throw new Exception();
            Vertices = vertices;

            Vector3 v1, v2, normal;
            Vector3.Subtract(ref vertices[1], ref vertices[0], out v1);
            Vector3.Subtract(ref vertices[2], ref vertices[0], out v2);
            Vector3.Cross(ref v1, ref v2, out normal);
            Vector3.Normalize(ref normal, out _planeNormal);
        }

        /// <summary>
        ///     Returns whether a ray intersects with the triangle.
        /// </summary>
        /// <param name="ray"></param>
        /// <returns></returns>
        public override bool Intersect(Ray ray)
        {
            Vector3 e1, e2, s1;
            float divisor, invDivisor;
            Vector3.Subtract(ref Vertices[1], ref Vertices[0], out e1);
            Vector3.Subtract(ref Vertices[2], ref Vertices[0], out e2);
            Vector3.Cross(ref ray.Direction, ref e2, out s1);
            Vector3.Dot(ref s1, ref e1, out divisor);
            if (divisor == 0)
                return false;
            invDivisor = 1/divisor;

            Vector3 s, s2;
            Vector3.Subtract(ref ray.Origin, ref Vertices[0], out s);
            var b1 = Vector3.Dot(s, s1)*invDivisor;
            if (b1 < 0 || b1 > 1)
                return false;
            Vector3.Cross(ref s, ref e1, out s2);
            var b2 = Vector3.Dot(ray.Direction, s2)*invDivisor;
            if (b2 < 0 || (b1 + b2) > 1)
                return false;
            var t = Vector3.Dot(e2, s2) * invDivisor;
            return !(t < ray.Start) && !(t > ray.End);
        }

        /// <summary>
        ///     Returns whether a ray instersects with the triangle and fill the intersection structure if it does.
        /// </summary>
        /// <param name="ray"></param>
        /// <param name="intersection"></param>
        /// <returns></returns>
        public override bool TryToIntersect(Ray ray, ref Intersection intersection)
        {
            Vector3 e1, e2, s1;
            float divisor, invDivisor;
            Vector3.Subtract(ref Vertices[1], ref Vertices[0], out e1);
            Vector3.Subtract(ref Vertices[2], ref Vertices[0], out e2);
            Vector3.Cross(ref ray.Direction, ref e2, out s1);
            Vector3.Dot(ref s1, ref e1, out divisor);
            if (divisor == 0)
                return false;
            invDivisor = 1/divisor;

            Vector3 s, s2;
            Vector3.Subtract(ref ray.Origin, ref Vertices[0], out s);
            var b1 = Vector3.Dot(s, s1)*invDivisor;
            if (b1 < 0 || b1 > 1)
                return false;
            Vector3.Cross(ref s, ref e1, out s2);
            var b2 = Vector3.Dot(ray.Direction, s2)*invDivisor;
            if (b2 < 0 || (b1 + b2) > 1)
                return false;
            var t = Vector3.Dot(e2, s2)*invDivisor;
            if (t < ray.Start || t > ray.End)
                return false;
            intersection.Point = ray.PointAtTime(t);
            intersection.NormalVector = _planeNormal;
            intersection.Distance = (ray.Origin - intersection.Point).Length;
            return true;
        }

        /// <summary>
        /// Returns the area of the triangle.
        /// </summary>
        /// <returns></returns>
        public float Area()
        {
            return 0.5f * Vector3.Cross(Vertices[1] - Vertices[0], Vertices[2] - Vertices[0]).Length;
        }
    }
}