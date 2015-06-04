using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using raytracer.core;

namespace raytracer.shapes
{
    /// <summary>
    ///     Represents a polygon that can be intersected by light rays.
    /// </summary>
    public class Polygon : Shape
    {
        /// <summary>
        ///     List of the polygon's vertices.
        /// </summary>
        private readonly List<Vector3> _vertices;

        /// <summary>
        ///     The plane normalized vector.
        /// </summary>
        private Vector3 _planeNormal;

        /// <summary>
        ///     Create a olygon from the list of its vertices.
        /// </summary>
        /// <param name="vertices"></param>
        public Polygon(List<Vector3> vertices)
        {
            if (vertices.Count < 3)
                throw new Exception();
            vertices.Add(vertices.ElementAt(0));
            var p1 = vertices.ElementAt(0);
            var p2 = vertices.ElementAt(1);
            var p3 = vertices.ElementAt(2);

            Vector3 v1, v2;
            Vector3.Subtract(ref p2, ref p1, out v1);
            Vector3.Subtract(ref p3, ref p1, out v2);
            Vector3.Cross(ref v1, ref v2, out _planeNormal);

            _vertices = vertices;
        }

        /// <summary>
        ///     Checks whether a point is comprised in a polygon.
        /// </summary>
        /// <param name="point">The checked point</param>
        /// <returns>Whether the point is in the polygon or not</returns>
        protected bool PointInPolygon(ref Vector3 point)
        {
            var intersections = 0;
            float intersect;
            int index;
            Vector3 vertex;
            Vector3 nvertex;

            for (index = 0; index < _vertices.Count - 1; index++)
            {
                vertex = _vertices.ElementAt(index);
                nvertex = _vertices.ElementAt(index + 1);

                if (((vertex.Y <= point.Y) && (nvertex.Y > point.Y))
                    || ((vertex.Y > point.Y) && (nvertex.Y <= point.Y)))
                {
                    intersect = (point.Y - vertex.Y)/(nvertex.Y - vertex.Y);
                    if (point.X < (vertex.X + intersect*(nvertex.X - vertex.X)))
                        intersections++;
                }
            }
            return intersections%2 != 0;
        }

        /// <summary>
        ///     Try to intersect the ray with the polygon.
        ///     Returns true and fill intersection if it intersects, otherwise returns false and doesn't fill intersection.
        /// </summary>
        /// <param name="ray">The ray to intersect with</param>
        /// <param name="intersection">Data about the intersection</param>
        /// <returns>Whether the ray intersected with the polygon</returns>
        public override bool TryToIntersect(Ray ray, ref Intersection intersection)
        {
            float denom, num;
            Vector3.Dot(ref _planeNormal, ref ray.Direction, out denom);
            if (denom == 0)
                return false;
            Vector3.Dot(ref _planeNormal, ref ray.Origin, out num);
            num *= -1;
            var t = num/denom;
            if (t < ray.Start || t > ray.End) return false;
            var point = ray.PointAtTime(t);
            if (!PointInPolygon(ref point))
                return false;
            intersection.Point = point;
            intersection.NormalVector = _planeNormal.Normalized();
            intersection.Distance = (intersection.Point - ray.Origin).Length;
            return true;
        }

        /// <summary>
        ///     Intersect the ray with the polygon
        /// </summary>
        /// <param name="ray">Ray to intersect</param>
        /// <returns>Whether the ray intersected</returns>
        public override bool Intersect(Ray ray)
        {
            float denom, num;
            Vector3.Dot(ref _planeNormal, ref ray.Direction, out denom);
            if (denom == 0)
                return false;
            Vector3.Dot(ref _planeNormal, ref ray.Origin, out num);
            num *= -1;
            var t = num/denom;
            if (t < ray.Start || t > ray.End) return false;
            var point = ray.PointAtTime(t);
            return PointInPolygon(ref point);
        }
    }
}
