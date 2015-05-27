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

        public SampledSpectrum L(Vector3 point, out Vector3 incomingVector)
        {
            var direction = point - Position;
            incomingVector = direction.Normalized();
            return Intensity / direction.LengthSquared;
        }

        public SampledSpectrum Intensity { get; private set; }
        public Vector3 Position { get; private set; }
    }
}