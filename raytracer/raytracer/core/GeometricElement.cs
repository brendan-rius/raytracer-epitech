﻿using raytracer.core.mathematics;

namespace raytracer.core
{
    /// <summary>
    ///     A geometric element
    /// </summary>
    public abstract class GeometricElement
    {
        /// <summary>
        ///     Create a geometric element and set its transformation
        /// </summary>
        /// <param name="worldToObjectTransformation">
        ///     the transformation used to make an object in world space be an
        ///     object in element's space
        /// </param>
        protected GeometricElement(Transformation worldToObjectTransformation = null)
        {
            WorldToObjectTransformation = worldToObjectTransformation ?? Transformation.Identity;
        }

        /// <summary>
        ///     The transformation used to make an object in world space be an
        ///     object in element's space
        /// </summary>
        public Transformation WorldToObjectTransformation { get; set; }

        /// <summary>
        ///     Try to intersect a ray with an element, and get the differential geometry info
        ///     about the intersection
        /// </summary>
        /// <param name="ray">the ray to intersect with the object</param>
        /// <param name="differentialGeometry">the differential geometry element to fill</param>
        /// <returns>either true of the intersection has be done, or false otherwise</returns>
        public abstract bool TryToIntersect(ref Ray ray, out DifferentialGeometry? differentialGeometry);
    }
}