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

    public class WhittedIntegrator : Integrator
    {
        public override SampledSpectrum Li(Scene scene, Ray ray, Renderer renderer, Sample sample, ref Intersection i)
        {
            var spectrum = SampledSpectrum.Black();
            var lights = scene.Lights;
            var bsdfAtPoint = i.GetBSDF(ray);
            var leaving = -ray.Direction;
            var point = i.Point;
            var normalNormalized = i.NormalVector;
            foreach (var light in lights)
            {
                Vector3 incoming;
                VisibilityTester visibilityTester;
                var lightSpectrum = light.L(point, scene, out incoming, out visibilityTester);
                // We compute the BSDF value only if the light is not black and it is not occluded. Note that it is important
                // for the occlusion test to be after the test for black spectrum, because checking for intersection is an
                // expansive operation.
                if (!lightSpectrum.IsBlack() && !visibilityTester.Occluded())
                {
                    var angle = Math.Abs(Vector3.Dot(incoming, normalNormalized));
                    var bsdf = bsdfAtPoint.F(incoming, leaving);
                    spectrum += bsdf * lightSpectrum * angle;
                }
            }
            if (ray.Depth + 1 < MaxDepth)
                spectrum += SpecularReflect(ray, renderer, sample, bsdfAtPoint, ref i);
            return spectrum;
        }
    }
}