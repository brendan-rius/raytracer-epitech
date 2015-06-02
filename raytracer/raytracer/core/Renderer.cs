using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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
        public Renderer(Scene scene, Sampler sampler, Camera camera, Film film, Integrator integrator)
        {
            Film = film;
            Scene = scene;
            Sampler = sampler;
            Camera = camera;
            Integrator = integrator;
        }

        /// <summary>
        ///     The integrator used
        /// </summary>
        public Integrator Integrator { get; set; }

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
        ///     Render the scene.
        ///     <returns>the duration of rendering (in milliseconds)</returns>
        /// </summary>
        public virtual long Render()
        {
            var sw = new Stopwatch();
            sw.Start();
            var samples = Sampler.Samples();
            foreach (var sample in samples)
                Film.AddSample(sample, Li(sample));
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }

        /// <summary>
        ///     Compute radiance for a sample
        /// </summary>
        /// <param name="sample"></param>
        /// <param name="ray"></param>
        /// <returns></returns>
        public SampledSpectrum Li(Sample sample, Ray ray = null)
        {
            ray = ray ?? Camera.GenerateRay(sample);
            var intersection = new Intersection();
            var spectrum = SampledSpectrum.Black();
            if (Scene.TryToIntersect(ray, ref intersection))
                spectrum = Integrator.Li(Scene, ray, this, sample, ref intersection);
            else
                spectrum = Scene.Lights.Aggregate(spectrum, (current, light) => current + light.Le(ray));
            return spectrum;
        }
    }

    /// <summary>
    ///     A threaded renderer use multiple threads to speed up rendering
    /// </summary>
    public class ThreadedRenderer : Renderer
    {
        /// <summary>
        ///     The list of sub-samplers
        /// </summary>
        private readonly List<ThreadedSampler> _samplers;

        /// <summary>
        ///     Create a threaded renderer
        /// </summary>
        /// <param name="scene">the scene to render</param>
        /// <param name="sampler">the multithreaded sampler to use</param>
        /// <param name="camera">the camera</param>
        /// <param name="film">the film</param>
        /// <param name="integrator">the integrator</param>
        /// <param name="nthreads">the number of threads to use</param>
        public ThreadedRenderer(Scene scene, ThreadedSampler sampler, Camera camera, Film film, Integrator integrator,
            uint nthreads = 4) : base(scene, sampler, camera, film, integrator)
        {
            _samplers = sampler.GetSamplers(nthreads);
        }

        /// <summary>
        ///     Render all the samples from a sampler
        /// </summary>
        /// <param name="sampler">the sampler</param>
        protected void RenderSampler(ThreadedSampler sampler)
        {
            var samples = sampler.Samples();
            foreach (var sample in samples)
                Film.AddSample(sample, Li(sample));
        }

        /// <summary>
        ///     Render all the scene asynchronously
        /// </summary>
        /// <returns></returns>
        public async Task<long> RenderAsync()
        {
            var sw = new Stopwatch();
            sw.Start();
            var tasks = _samplers.Select(sampler => Task.Run(() => { RenderSampler(sampler); })).ToList();
            await Task.WhenAll(tasks);
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
    }
}