using System;
using OpenTK;
using raytracer.core;
using raytracer.core.mathematics;

namespace raytracer.integrators
{
    public class DirectLightingIntegrator : Integrator
    {
        /// <summary>
        ///     The strategy used to sample lights
        /// </summary>
        public enum LightSamplingStrategy
        {
            /// <summary>
            ///     A single sample is taken for each light. Best to use it when we already generate
            ///     multiple rays per sample
            /// </summary>
            Single,

            /// <summary>
            ///     Many samples are taken for every light
            /// </summary>
            Multiple
        };

        /// <summary>
        ///     The strategy to use
        /// </summary>
        private readonly LightSamplingStrategy _strategy;

        /// <summary>
        ///     Create a lighting integrator based a on specific lighting strategy
        /// </summary>
        /// <param name="strategy">the strategy used</param>
        public DirectLightingIntegrator(LightSamplingStrategy strategy = LightSamplingStrategy.Multiple)
        {
            _strategy = strategy;
        }

        private SampledSpectrum SampleLights(Scene scene, BSDF bsdf, ref Intersection intersection,
            ref Vector3 leaving)
        {
            var lights = scene.Lights;
            var spectrum = SampledSpectrum.Black();
            foreach (var light in lights)
            {
                var nsamples = _strategy == LightSamplingStrategy.Multiple ? light.NSamples : 1;
                var lightContribution = SampledSpectrum.Black();
                for (var i = 0; i < nsamples; ++i)
                {
                    Vector3 incoming;
                    VisibilityTester visibilityTester;
                    var lightSpectrum = light.Sample(ref intersection.Point, scene, out incoming, out visibilityTester);
                    if (lightSpectrum.IsBlack() || visibilityTester.Occluded()) continue;
                    var cosangle = Math.Abs(Vector3.Dot(incoming, intersection.NormalVector));
                    // we get the light coming to us from transmission + reflection
                    var distribution = bsdf.F(incoming, leaving, BxDF.BxDFType.All);
                    // we scale the light by the incident angle of light on the surface and by the distribution
                    // function from light to us and we add it to the spectrum
                    lightContribution += distribution*lightSpectrum*cosangle;
                }
                spectrum += lightContribution/nsamples;
            }
            return spectrum;
        }

        public override SampledSpectrum Li(Scene scene, Ray ray, Renderer renderer, Sample sample,
            ref Intersection intersection)
        {
            var spectrum = SampledSpectrum.Black();
            var bsdfAtPoint = intersection.GetBSDF();
            var leaving = -ray.Direction;
            spectrum += SampleLights(scene, bsdfAtPoint, ref intersection, ref leaving);
            if (ray.Depth + 1 < MaxDepth)
            {
                spectrum += SpecularTransmit(ray, renderer, sample, bsdfAtPoint, ref intersection);
                spectrum += SpecularReflect(ray, renderer, sample, bsdfAtPoint, ref intersection);
            }
            return spectrum;
        }
    }
}