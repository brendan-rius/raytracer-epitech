using raytracer.core.mathematics;

namespace raytracer.core
{
    /// <summary>
    ///     A geometric element
    /// </summary>
    public abstract class Shape : IIntersectable
    {
        /// <summary>
        ///     Create a geometric element and set its transformation
        /// </summary>
        /// <param name="worldToObjectTransformation">
        ///     the transformation used to make an object in world space be an
        ///     object in element's space
        /// </param>
        protected Shape(Transformation worldToObjectTransformation = null)
        {
            WorldToObjectTransformation = worldToObjectTransformation ?? Transformation.Identity;
        }

        /// <summary>
        ///     The transformation used to make an object in world space be an
        ///     object in element's space
        /// </summary>
        public Transformation WorldToObjectTransformation { get; set; }

        public abstract bool TryToIntersect(Ray ray, ref Intersection intersection);

        public abstract bool Intersect(Ray ray);
    }
}