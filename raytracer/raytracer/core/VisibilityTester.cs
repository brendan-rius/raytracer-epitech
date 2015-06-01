using OpenTK;

namespace raytracer.core
{
    /// <summary>
    ///     This class is used to test for visibility between two point in the scene.
    ///     It holds informations about those points, and provide a method to determine
    ///     if the visibility is occluded between those two points.
    /// </summary>
    public class VisibilityTester
    {
        /// <summary>
        ///     The ray
        /// </summary>
        private readonly Ray _ray;

        /// <summary>
        ///     The scene in which to test the visibility
        /// </summary>
        private readonly Scene _scene;

        /// <summary>
        ///     Create a visibility tester between two points in a scene
        /// </summary>
        /// <param name="p1">the first point</param>
        /// <param name="p2">the second point</param>
        /// <param name="scene">the scene</param>
        /// <param name="deltaStart"></param>
        /// <param name="deltaEnd"></param>
        public VisibilityTester(Vector3 p1, Vector3 p2, Scene scene, float deltaStart = 0.5f, float deltaEnd = 0.5f)
        {
            var direction = p2 - p1;
            _scene = scene;
            _ray = new Ray(direction.Normalized(), p1, deltaStart, direction.Length - deltaEnd);
        }

        /// <summary>
        ///     Check if the visibility is occluded
        /// </summary>
        /// <returns>true if the path is occluded, false otherwise</returns>
        public bool Occluded()
        {
            return _scene.Intersect(_ray);
        }
    }
}