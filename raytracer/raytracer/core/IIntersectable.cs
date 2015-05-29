namespace raytracer.core
{
    /// <summary>
    ///     Represent something that can be intersected by a ray
    ///     <seealso cref="Ray" />
    /// </summary>
    public interface IIntersectable
    {
        /// <summary>
        ///     Checks if the ray intersects with the object, and return information
        ///     about the intersection if any.
        /// </summary>
        /// <param name="ray">the ray</param>
        /// <param name="intersection">the intersection structure to fill</param>
        /// <returns>true if there is an intersection, false otherwise</returns>
        bool TryToIntersect(ref Ray ray, ref Intersection intersection);

        /// <summary>
        ///     Checks if a ray intersect with the object, without providing more
        ///     information on the intersection.
        /// </summary>
        /// <param name="ray">the ray</param>
        /// <returns>true if the ray intersects with the object, false otherwise</returns>
        bool Intersect(ref Ray ray);
    }
}