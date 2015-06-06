using raytracer.core.mathematics;

namespace raytracer.core
{
    /// <summary>
    ///     The role of the camera is to see the scene, so that a film can record it.
    ///     Each camera can have its own way of seeing (a camera may have a wider field of view than another one,
    ///     while another camera can create a fish-eye effect for example).
    ///     A camera may also use lens.
    ///     <seealso cref="Film" />
    /// </summary>
    public abstract class Camera
    {
        /// <summary>
        ///     Creates a camera.
        /// </summary>
        /// <param name="screen">the screen of the camera</param>
        /// <param name="objectToWorld">the object-to-world transformation for the camera</param>
        protected Camera(Screen screen, Transformation objectToWorld)
        {
            ObjectToWorld = objectToWorld;
            Screen = screen;
        }

        /// <summary>
        ///     The screen of the camera
        /// </summary>
        public Screen Screen { get; set; }

        /// <summary>
        ///     The transformation used to make an object in camera's space
        ///     be an object in world space
        /// </summary>
        public Transformation ObjectToWorld { get; set; }

        /// <summary>
        ///     Generate a ray from a camera sample
        ///     <seealso cref="Sample" />
        /// </summary>
        /// <param name="sample"></param>
        /// <returns>the generate ray</returns>
        public abstract Ray GenerateRay(Sample sample);
    }
}