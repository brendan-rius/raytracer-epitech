using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using OpenTK.Platform.Windows;

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
            return _aggregator.TryToIntersect(ray, ref intersection);
        }

        public bool Intersect(Ray ray)
        {
            return _aggregator.Intersect(ray);
        }
    }
}