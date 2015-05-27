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
                var lightSpectrum = light.L(point, out incoming);
                spectrum += bsdfAtPoint.F(incoming, leaving)*lightSpectrum*Vector3.Dot(incoming, normalNormalized);
            }
            return spectrum;
        }
    }
}