namespace raytracer.core
{
    /// <summary>
    ///     The renderer is the glue between the scene, the camera and the sampler.
    ///     It will generate samples using the sampler, make the camera generate rays
    ///     from those samples, intersect them with the object in the scene, and notify the
    ///     camera's film of the intersections.
    ///     <seealso cref="Scene" />
    ///     <seealso cref="Camera" />
    ///     <seealso cref="Sampler" />
    ///     <seealso cref="Sample" />
    /// </summary>
    public class Renderer
    {
        /// <summary>
        ///     Create a new renderer
        /// </summary>
        /// <param name="scene">the scene to render</param>
        /// <param name="sampler">the sampler</param>
        /// <param name="camera">the camera</param>
        public Renderer(Scene scene, Sampler sampler, Camera camera, Film film)
        {
            Film = film;
            Scene = scene;
            Sampler = sampler;
            Camera = camera;
        }

        /// <summary>
        ///     The film to which the scene will be rendered
        /// </summary>
        public Film Film { get; set; }

        /// <summary>
        ///     The camera used to generate rays
        /// </summary>
        public Camera Camera { get; set; }

        /// <summary>
        ///     THe sampler used to generate samles
        /// </summary>
        public Sampler Sampler { get; set; }

        /// <summary>
        ///     The scene to render
        /// </summary>
        public Scene Scene { get; set; }

        /// <summary>
        ///     Render the scene
        /// </summary>
        public void Render()
        {
            foreach (var sample in Sampler.Samples())
            {
                var ray = Camera.GenerateRay(sample);
                Film.AddSample(sample, Scene.TryToIntersect(ray) ? Spectrum.BlueSpectrum : Spectrum.BlackSpectrum);
            }
        }
    }
}