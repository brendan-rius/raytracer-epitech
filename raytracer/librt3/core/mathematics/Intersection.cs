using OpenTK;

namespace raytracer.core
{
    public struct Intersection
    {
        /// <summary>
        ///     The distance of the intersection
        /// </summary>
        public float Distance;

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
        ///     How the point moves on surface when "u" changes
        /// </summary>
        public Vector3 PointDifferentialOverU;

        /// <summary>
        ///     How the point moves on surface when "v" changes
        /// </summary>
        public Vector3 PointDifferentialOverV;

        /// <summary>
        ///     The intersected primitive
        /// </summary>
        public Primitive Primitive { get; set; }

        public BSDF GetBSDF()
        {
            return Primitive.GetBSDF(ref this);
        }
    }
}