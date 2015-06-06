using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raytracer.core
{
    public abstract class Aggregate : IIntersectable
    {
        public abstract bool TryToIntersect(Ray ray, ref Intersection intersection);

        public abstract bool Intersect(Ray ray);
    }
}
