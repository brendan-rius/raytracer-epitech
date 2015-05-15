using OpenTK;

namespace raytracer.core.mathematics
{
    /// <summary>
    ///     Holds the information about an intersection with a <see cref="GeometricElement" />
    /// </summary>
    public struct DifferentialGeometry
    {
        /// <summary>
        ///     Initialize a differential geometry from an intersection point and a normal vector
        /// </summary>
        /// <param name="point">the intersection point</param>
        /// <param name="normalVector">the normal vector</param>
        public DifferentialGeometry(Vector3 point, Vector3 normalVector) : this()
        {
            Point = point;
            NormalVector = normalVector;
        }

        /// <summary>
        ///     The point of the intersection
        /// </summary>
        public Vector3 Point { get; set; }

        /// <summary>
        ///     The normal vector at the intersection
        /// </summary>
        public Vector3 NormalVector { get; set; }
    }
}