using raytracer.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raytracer.shapes
{
    public class TriangleMesh : Shape
    {
        private BBox _box;

        private List<Triangle> _triangles;

        TriangleMesh(List<Triangle> triangles)
        {
            if (triangles.Count == 0)
                throw new Exception();
            _triangles = triangles;
            _box = new BBox(triangles.ElementAt(0).Vertices[0], triangles.ElementAt(0).Vertices[1]);
            _box = BBox.Union(_box, triangles.ElementAt(0).Vertices[2]);
            foreach(var t in triangles.Skip(1))
            {
                foreach(var v in t.Vertices)
                {
                    _box = BBox.Union(_box, v);
                }
            }
        }

        public BBox BoundingBox()
        {
            return _box;
        }

        public List<Shape> Refine()
        {
            return _triangles.Cast<Shape>().ToList();
        }

        public bool CanIntersect(Ray ray)
        {
            return false;
        }

        public override bool Intersect(Ray ray)
        {
            return true;
        }

        public override bool TryToIntersect(Ray ray, ref Intersection intersection)
        {
            return true;
        }
    }
}
