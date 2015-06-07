using System;
using System.Collections.Generic;
using raytracer.core.mathematics;

namespace raytracer.core
{
    /// <summary>
    ///     A geometric element
    /// </summary>
    public abstract class Shape : IIntersectable
    {
        public readonly bool ReverseOrientation, TransformSwapsHandedness;

        /// <summary>
        ///     Create a geometric element and set its transformation
        /// </summary>
        /// <param name="worldToObjectTransformation">
        ///     the transformation used to make an object in world space be an
        ///     object in element's space
        /// </param>
        protected Shape(Transformation worldToObjectTransformation = null, bool reverseOrientation = false)
        {
            WorldToObjectTransformation = worldToObjectTransformation ?? Transformation.Identity;
            TransformSwapsHandedness = WorldToObjectTransformation.InverseTransformation.SwapHandedness();
            ReverseOrientation = reverseOrientation;
        }

        /// <summary>
        ///     The transformation used to make an object in world space be an
        ///     object in element's space
        /// </summary>
        public Transformation WorldToObjectTransformation { get; set; }

        public abstract bool TryToIntersect(Ray ray, ref Intersection intersection);
        public abstract bool Intersect(Ray ray);

        /// <summary>
        /// Whether a ray can intersect with this shape.
        /// If false is returned, user must call Refine method.
        /// </summary>
        /// <returns></returns>
        public virtual bool CanIntersect()
        {
            return true;
        }

        /// <summary>
        /// Refines the Shape into intersectable shapes.
        /// </summary>
        /// <param name="refined"></param>
        public virtual void Refine(List<Shape> refined)
        {
            throw new NotImplementedException();
        }

        public abstract BBox WorldBound();
    }
}