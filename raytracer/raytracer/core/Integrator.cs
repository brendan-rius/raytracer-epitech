using System;
using OpenTK;
using raytracer.core.mathematics;

namespace raytracer.core
{
    public abstract class Integrator
    {
        /// <summary>
        ///     The max depth for reflection and refraction
        /// </summary>
        public const uint MaxDepth = 5;

        public abstract SampledSpectrum Li(Scene scene, Ray ray, Renderer renderer, Sample sample, ref Intersection i);

        public SampledSpectrum SpecularReflect(Ray ray, Renderer renderer, Sample sample, BSDF bsdf, ref Intersection i)
        {
            var leaving = -ray.Direction;
            Vector3 incoming;
            var f = bsdf.Sample(ref leaving, out incoming, BxDF.BxDFType.Reflection);
            if (f.IsBlack()) return f;
            var reflectedRay = ray.GenerateChild(incoming, i.Point);
            var li = renderer.Li(sample, reflectedRay);
            var angle = Math.Abs(Vector3.Dot(incoming.Normalized(), i.NormalVector));
            return f*li*angle;
        }
    }
}