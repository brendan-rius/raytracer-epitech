using System.Numerics;
using OpenTK;

namespace raytracer.core
{
    public class Light
    {
        public Light(Vector3 position, SampledSpectrum intensity = null)
        {
            Intensity = intensity ?? new SampledSpectrum(3000f);
            Position = position;
        }

        public SampledSpectrum Intensity { get; private set; }
        public Vector3 Position { get; private set; }

        public SampledSpectrum L(Vector3 point, Scene scene, out Vector3 incomingVector, out VisibilityTester visibilityTester)
        {
            var direction = point - Position;
            incomingVector = Vector3.Normalize(direction);
            visibilityTester = new VisibilityTester(Position, point, scene);
            return Intensity/direction.LengthSquared;
        }
    }
}