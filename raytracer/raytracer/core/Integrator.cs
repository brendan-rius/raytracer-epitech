using System;
using System.Collections.Generic;
using OpenTK;

namespace raytracer.core
{
    public abstract class Integrator
    {
        public abstract SampledSpectrum Li(Scene scene, Ray ray, Renderer renderer, Sample sample, ref Intersection i);

        public SampledSpectrum SpecularReflect(Ray ray, Renderer renderer, Sample sample, ref Intersection i)
        {
            var leaving = -ray.Direction;
            var normalNormalized = i.NormalVector;
            var wi = new Vector3(1, 0, 0);
            var f = SampledSpectrum.Random();//i.GetBSDF(ray).Sample());
            if (f.IsBlack()) return f;
            var reflectedRay = ray.GenerateChild(wi, i.Point);
            return f*renderer.Li(sample, reflectedRay)*Math.Abs(Vector3.Dot(wi, normalNormalized));
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
                    spectrum += bsdfAtPoint.F(incoming, leaving)*lightSpectrum*
                                Math.Abs(Vector3.Dot(incoming, normalNormalized));
            }
            if (ray.Depth + 1 < 5)
            {
                ray.Depth += 1;
                spectrum += SpecularReflect(ray, renderer, sample, ref i);
            }
            return spectrum;
        }
    }
}