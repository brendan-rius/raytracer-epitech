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
            var incoming = new Vector3();
            var f = bsdf.Sample(leaving, ref incoming, BxDF.BxDFType.Reflection | BxDF.BxDFType.Specular);
            var angle = Math.Abs(Vector3.Dot(incoming.Normalized(), i.NormalVector));
            if (f.IsBlack() || angle == 0f)
                return SampledSpectrum.Black();
            var reflectedRay = new Ray(incoming.Normalized(), i.Point, 0.3f, Ray.DefaultEndValue, ray.Depth + 1);
            var li = renderer.Li(sample, reflectedRay);
            return f*li*angle;
        }
    }
}