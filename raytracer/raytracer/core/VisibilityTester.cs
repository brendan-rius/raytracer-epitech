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
        ///     The first point
        /// </summary>
        private readonly Vector3 _p1;

        /// <summary>
        ///     The second point
        /// </summary>
        private readonly Vector3 _p2;

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
        public VisibilityTester(Vector3 p1, Vector3 p2, Scene scene)
        {
            _p1 = p1;
            _p2 = p2;
            _scene = scene;
        }

        /// <summary>
        ///     Check if the visibility is occluded between the points points
        ///     in the scene.
        /// </summary>
        /// <param name="delta">the offset used to avoid wrong self-intersections</param>
        /// <returns>true if the path is occluded, false otherwise</returns>
        public bool Occluded(float delta = 0.005f)
        {
            var direction = _p2 - _p1;
            var ray = new Ray(direction.Normalized(), _p1, delta, direction.Length - delta);
            return _scene.Intersect(ref ray);
        }
    }
}