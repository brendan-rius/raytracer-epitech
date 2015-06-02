using raytracer.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace raytracer.shapes
{
    class Triangle : Shape
    {
        /// <summary>
        /// Stores the three triangle vertices.
        /// </summary>
        private readonly Vector3[] Vertices;

        /// <summary>
        /// Stores the triangle's plane normalized normal vector.
        /// </summary>
        private Vector3 PlaneNormal;

        /// <summary>
        /// Creates a new Triangle from 3 vertices
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
            Vector3.Normalize(ref normal, out PlaneNormal);
        }

        /// <summary>
        /// Returns whether a ray intersects with the triangle.
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
            invDivisor = 1 / divisor;
            
            Vector3 s, s2;
            Vector3.Subtract(ref ray.Origin, ref Vertices[0], out s);
            float b1 = Vector3.Dot(s, s1) * invDivisor;
            if (b1 < 0 || b1 > 1)
                return false;
            Vector3.Cross(ref s, ref e1, out s2);
            float b2 = Vector3.Dot(ray.Direction, s2) * invDivisor;
            if (b2 < 0 || (b1 + b2) > 1)
                return false;
            return true;
        }

        /// <summary>
        /// Returns whether a ray instersects with the triangle and fill the intersection structure if it does.
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
            invDivisor = 1 / divisor;

            Vector3 s, s2;
            Vector3.Subtract(ref ray.Origin, ref Vertices[0], out s);
            float b1 = Vector3.Dot(s, s1) * invDivisor;
            if (b1 < 0 || b1 > 1)
                return false;
            Vector3.Cross(ref s, ref e1, out s2);
            float b2 = Vector3.Dot(ray.Direction, s2) * invDivisor;
            if (b2 < 0 || (b1 + b2) > 1)
                return false;
            float t = Vector3.Dot(e2, s2) * invDivisor;
            ray.PointAtTime(t, out intersection.Point);
            intersection.NormalVector = PlaneNormal;
            return true;
        }
    }
}
