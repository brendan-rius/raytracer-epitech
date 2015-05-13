using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raytracer
{
    /// <summary>
    /// The renderer is the glue between the scene, the camera and the sampler.
    /// It will generate samples using the sampler, make the camera generate rays
    /// from those samples, intersect them with the object in the scene, and notify the 
    /// camera's film of the intersections.
    /// <seealso cref="Scene"/>
    /// <seealso cref="Camera"/>
    /// <seealso cref="Sampler"/>
    /// <seealso cref="Sample"/>
    /// </summary>
    class Renderer
    {
        protected Scene Scene;
        protected Sampler Sampler;
        protected Camera Camera;

        public Renderer(Scene scene, Sampler sampler, Camera camera)
        {
            this.Scene = scene;
            this.Sampler = sampler;
            this.Camera = camera;
        }
    }
}