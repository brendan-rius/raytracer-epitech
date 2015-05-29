using System.Collections.Generic;

namespace raytracer.core
{
    /// <summary>
    ///     A scene holds intersectable elements
    /// </summary>
    public class Scene : IIntersectable
    {
        private List<Primitive> _elements = new List<Primitive>();
        private List<Light> _lights = new List<Light>();

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

        public bool TryToIntersect(ref Ray ray, ref Intersection intersection)
        {
            foreach (var element in Elements)
            {
                if (element.TryToIntersect(ref ray, ref intersection))
                    return true;
            }
            return false;
        }

        public bool Intersect(ref Ray ray)
        {
            foreach (var element in Elements)
            {
                if (element.Intersect(ref ray))
                    return true;
            }
            return false;
        }
    }
}