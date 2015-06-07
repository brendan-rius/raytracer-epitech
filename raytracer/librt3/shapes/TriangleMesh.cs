using System;
using System.Collections.Generic;
using System.Linq;
using raytracer.core;

namespace raytracer.shapes
{
    public class TriangleMesh : Shape
    {
        private readonly BBox _box;
        private readonly List<Triangle> _triangles;

        public TriangleMesh(List<Triangle> triangles)
        {
            if (triangles.Count == 0)
                throw new Exception();
            _triangles = triangles;
            _box = triangles.ElementAt(0).WorldBound();
            foreach (var t in triangles.Skip(1))
            {
                _box = BBox.Union(_box, t.WorldBound());
            }
        }

        public override void Refine(List<Shape> refined)
        {
            refined.AddRange(_triangles);
        }

        public override BBox WorldBound()
        {
            return _box;
        }

        public override bool CanIntersect()
        {
            return false;
        }

        public override bool Intersect(Ray ray)
        {
            throw new NotImplementedException();
        }

        public override bool TryToIntersect(Ray ray, ref Intersection intersection)
        {
            throw new NotImplementedException();
        }
    }
}