using System;
using OpenTK;
using raytracer.core;
using raytracer.core.mathematics;

namespace raytracer.integrators
{
    public class WhittedIntegrator : Integrator
    {
        public override SampledSpectrum Li(Scene scene, Ray ray, Renderer renderer, Sample sample, ref Intersection i)
        {
            var spectrum = SampledSpectrum.Black();
            var lights = scene.Lights;
            var bsdfAtPoint = i.GetBSDF();
            // we want to get the radiance coming from the surface to us, but the ray comes from us
            // to the surface
            var leaving = -ray.Direction;
            foreach (var light in lights)
            {
                Vector3 incoming;
                VisibilityTester visibilityTester;
                var lightSpectrum = light.L(i.Point, scene, out incoming, out visibilityTester);
                // We compute the BSDF value only if the light is not black and it is not occluded. Note that it is important
                // for the occlusion test to be after the test for black spectrum, because checking for intersection is an
                // expansive operation.
                if (!lightSpectrum.IsBlack() && !visibilityTester.Occluded())
                {
                    var cosangle = Math.Abs(Vector3.Dot(incoming, i.NormalVector));
                    // we get the light coming to us from transmission + reflection
                    var bsdf = bsdfAtPoint.F(incoming, leaving, BxDF.BxDFType.All);
                    // we scale the light by the incident angle of light on the surface and by the distribution
                    // function from light to us and we add it to the spectrum
                    spectrum += bsdf*lightSpectrum*cosangle;
                }
            }
            if (ray.Depth + 1 < MaxDepth)
                spectrum += SpecularReflect(ray, renderer, sample, bsdfAtPoint, ref i);
            return spectrum;
        }
    }
}