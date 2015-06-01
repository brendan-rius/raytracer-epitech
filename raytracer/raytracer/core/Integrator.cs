using System;
using OpenTK;

namespace raytracer.core
{
    public abstract class Integrator
    {
        public abstract SampledSpectrum Li(Scene scene, ref Ray ray, ref Intersection i);
    }

    public class WhittedIntegrator : Integrator
    {
        public override SampledSpectrum Li(Scene scene, ref Ray ray, ref Intersection i)
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
                if (!lightSpectrum.IsBlack())
                    spectrum += bsdfAtPoint.F(incoming, leaving)*lightSpectrum*Math.Abs(Vector3.Dot(incoming, normalNormalized));
            }
            return spectrum;
        }
    }
}