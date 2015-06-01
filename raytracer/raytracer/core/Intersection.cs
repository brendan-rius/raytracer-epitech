using OpenTK;

namespace raytracer.core
{
    public struct Intersection
    {
        /// <summary>
        ///     The normal vector at the intersection. THis vector
        ///     should be normalized
        /// </summary>
        public Vector3 NormalVector;

        /// <summary>
        ///     The point of the intersection
        /// </summary>
        public Vector3 Point;

        /// <summary>
        ///     The distance of the intersection
        /// </summary>
        public float Distance;

        /// <summary>
        ///     The intersected primitive
        /// </summary>
        public Primitive Primitive { get; set; }

        public BSDF GetBSDF(Ray ray)
        {
            return Primitive.GetBSDF(this);
        }
    }
}