using OpenTK;
using raytracer.core.mathematics;

namespace raytracer.core
{
    /// <summary>
    ///     Represent a light in a scene
    /// </summary>
    public abstract class Light
    {
        /// <summary>
        ///     Create a light from a transformation
        /// </summary>
        /// <param name="objectToWorld">the light-to-world transformation</param>
        protected Light(Transformation objectToWorld)
        {
            ObjectToWorld = objectToWorld;
        }

        /// <summary>
        ///     The light-to-world transformation
        /// </summary>
        public Transformation ObjectToWorld { get; protected set; }

        /// <summary>
        ///     Return the radiance from the light to a point in a scene and
        ///     instantiate an "inciming vector" (which is a normalized vector coming from light to point)
        ///     and a visibility tester which holds information about if there is any
        ///     object between light an point in the scene.
        /// </summary>
        /// <param name="point">the point</param>
        /// <param name="scene">the scene</param>
        /// <param name="incomingVector">the vector from light to point</param>
        /// <param name="visibilityTester">the visibility tester</param>
        /// <returns>the radiance</returns>
        public abstract SampledSpectrum L(Vector3 point, Scene scene, out Vector3 incomingVector,
            out VisibilityTester visibilityTester);

        /// <summary>
        ///     Return the spectrum of light when ray does not hit anything.
        /// </summary>
        /// <param name="ray">the ray</param>
        /// <returns></returns>
        public virtual SampledSpectrum Le(Ray ray)
        {
            return new SampledSpectrum(1);
        }
    }
}