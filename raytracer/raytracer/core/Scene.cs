using System.Collections.Generic;
using System.Linq;

namespace raytracer.core
{
    /// <summary>
    ///     A scene holds intersectable elements
    /// </summary>
    public class Scene : IIntersectable
    {
        private List<Primitive> _elements = new List<Primitive>();
        private List<Light> _lights = new List<Light>();
        private Aggregate _aggregator;

        /// <summary>
        ///     The elements of the scene
        /// </summary>
        public List<Primitive> Elements
        {
            get { return _elements; }
            set { _elements = value; }
        }

        /// <summary>
        ///     The lights in the scene
        /// </summary>
        public List<Light> Lights
        {
            get { return _lights; }
            set { _lights = value; }
        }

        public void Initialize()
        {
            _aggregator = new GridAccel(_elements);
        }

        public bool TryToIntersect(Ray ray, ref Intersection intersection)
        {
            intersection.Distance = float.PositiveInfinity;
            foreach (var primitive in _elements)
            {
                var tmp = new Intersection();
                if (!primitive.TryToIntersect(ray, ref tmp)) continue;
                if (tmp.Distance < intersection.Distance)
                    intersection = tmp;
            }
            return intersection.Distance != float.PositiveInfinity;
        }

        public bool Intersect(Ray ray)
        {
            return Elements.Any(element => element.Intersect(ray));
        }
    }
}