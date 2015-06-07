using System;
using OpenTK;
using raytracer.core;
using raytracer.core.mathematics;

namespace raytracer.shapes
{
    public class Triangle : Shape
    {
        private readonly BBox _bbox;

        /// <summary>
        ///     Stores the triangle's plane normalized normal vector.
        /// </summary>
        private readonly Vector3 _planeNormal;

        /// <summary>
        ///     Stores the three triangle vertices.
        /// </summary>
        private readonly Vector3[] _vertices;

        /// <summary>
        ///     Creates a new Triangle from 3 vertices
        /// </summary>
        /// <param name="vertices"></param>
        public Triangle(Vector3[] vertices)
        {
            if (vertices.Length != 3)
                throw new Exception();
            _vertices = vertices;
            _bbox = BBox.Union(new BBox(_vertices[0], _vertices[1]), _vertices[2]);

            Vector3 normal, dp1, dp2;
            Vector3.Subtract(ref vertices[0], ref vertices[2], out dp1);
            Vector3.Subtract(ref vertices[1], ref vertices[2], out dp2);
            Vector3.Cross(ref dp1, ref dp2, out normal);
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
            Vector3.Subtract(ref _vertices[1], ref _vertices[0], out e1);
            Vector3.Subtract(ref _vertices[2], ref _vertices[0], out e2);
            Vector3.Cross(ref ray.Direction, ref e2, out s1);
            Vector3.Dot(ref s1, ref e1, out divisor);
            if (divisor == 0f)
                return false;
            invDivisor = 1/divisor;

            Vector3 s, s2;
            Vector3.Subtract(ref ray.Origin, ref _vertices[0], out s);
            var b1 = Vector3.Dot(s, s1)*invDivisor;
            if (b1 < 0 || b1 > 1)
                return false;
            Vector3.Cross(ref s, ref e1, out s2);
            var b2 = Vector3.Dot(ray.Direction, s2)*invDivisor;
            if (b2 < 0 || (b1 + b2) > 1)
                return false;
            var t = Vector3.Dot(e2, s2)*invDivisor;
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
            Vector3.Subtract(ref _vertices[1], ref _vertices[0], out e1);
            Vector3.Subtract(ref _vertices[2], ref _vertices[0], out e2);
            Vector3.Cross(ref ray.Direction, ref e2, out s1);
            Vector3.Dot(ref s1, ref e1, out divisor);
            if (divisor == 0f)
                return false;
            invDivisor = 1/divisor;

            Vector3 s, s2;
            Vector3.Subtract(ref ray.Origin, ref _vertices[0], out s);
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
            Transformation.CoordinateSystem(ref intersection.NormalVector, out intersection.PointDifferentialOverU,
                out intersection.PointDifferentialOverV);
            return true;
        }

        public override BBox WorldBound()
        {
            return _bbox;
        }

        /// <summary>
        ///     Returns the area of the triangle.
        /// </summary>
        /// <returns></returns>
        public float Area()
        {
            return 0.5f*Vector3.Cross(_vertices[1] - _vertices[0], _vertices[2] - _vertices[0]).Length;
        }
    }
}