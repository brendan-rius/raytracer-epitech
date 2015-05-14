﻿using System.Collections.Generic;
using raytracer.core.mathematics;

namespace raytracer.core
{
    /// <summary>
    ///     A scene holds intersectable elements
    /// </summary>
    public class Scene
    {
        private List<GeometricElement> _elements = new List<GeometricElement>();

        /// <summary>
        ///     The elements of the scene
        /// </summary>
        public List<GeometricElement> Elements
        {
            get { return _elements; }
            set { _elements = value; }
        }

        /// <summary>
        ///     Try to intersect a ray in the scene.
        /// </summary>
        /// <param name="ray">the ray to intersect</param>
        /// <returns>true if the ray has been intersected, false otherwise</returns>
        public bool TryToIntersect(Ray ray)
        {
            foreach (var element in Elements)
            {
                DifferentialGeometry? dg;
                if (element.TryToIntersect(ray, out dg))
                    return true;
            }
            return false;
        }
    }
}