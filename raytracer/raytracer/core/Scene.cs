using System.Collections.Generic;

namespace raytracer.core
{
    /// <summary>
    ///     A scene holds intersectable elements
    /// </summary>
    public class Scene
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

        public List<Light> Lights
        {
            get { return _lights; }
            set { _lights = value; }
        }

        /// <summary>
        ///     Try to intersect a ray in the scene.
        /// </summary>
        /// <param name="ray">the ray to intersect</param>
        /// <returns>true if the ray has been intersected, false otherwise</returns>
        public bool TryToIntersect(ref Ray ray, out Intersection intersection)
        {
            intersection = new Intersection();
            foreach (var element in Elements)
            {
                if (element.TryToIntersect(ref ray, ref intersection))
                    return true;
            }
            return false;
        }
    }
}