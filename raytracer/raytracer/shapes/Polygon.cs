using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using raytracer.core;

namespace raytracer.shapes
{
    public class Polygon : Shape
    {
        private readonly List<Vector3> Vertices;
        private Vector3 PlaneNormal;

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
            Vector3.Cross(ref v1, ref v2, out PlaneNormal);

            Vertices = vertices;
        }

        protected bool PointInPolygon(ref Vector3 point)
        {
            var intersections = 0;
            float intersect;
            int index;
            Vector3 vertex;
            Vector3 nvertex;

            for (index = 0; index < Vertices.Count - 1; index++)
            {
                vertex = Vertices.ElementAt(index);
                nvertex = Vertices.ElementAt(index + 1);

                if (((vertex.Y <= point.Y) && (nvertex.Y > point.Y))
                    || ((vertex.Y > point.Y) && (nvertex.Y <= point.Y)))
                {
                    intersect = (point.Y - vertex.Y)/(nvertex.Y - vertex.Y);
                    if (point.X < (vertex.X + intersect*(nvertex.X - vertex.X)))
                        intersections++;
                }
            }
            if ((intersections%2) == 0)
                return false;
            return true;
        }

        public override bool TryToIntersect(Ray ray, ref Intersection intersection)
        {
            float denom, num;
            Vector3.Dot(ref PlaneNormal, ref ray.Direction, out denom);
            if (denom == 0)
                return false;
            Vector3.Dot(ref PlaneNormal, ref ray.Origin, out num);
            num *= -1;
            var t = num/denom;
            if (t < ray.Start || t > ray.End) return false;
            var point = ray.PointAtTime(t);
            if (!PointInPolygon(ref point))
                return false;
            intersection.Point = point;
            intersection.NormalVector = PlaneNormal.Normalized();
            return true;
        }

        public override bool Intersect(Ray ray)
        {
            float denom, num;
            Vector3.Dot(ref PlaneNormal, ref ray.Direction, out denom);
            if (denom == 0)
                return false;
            Vector3.Dot(ref PlaneNormal, ref ray.Origin, out num);
            num *= -1;
            var t = num/denom;
            if (t < ray.Start || t > ray.End) return false;
            var point = ray.PointAtTime(t);
            return PointInPolygon(ref point);
        }
    }
}