using OpenTK;
using raytracer.core;
using raytracer.core.mathematics;

namespace raytracer.lights
{
    /// <summary>
    ///     Represent a light that emits the same amout of light in all directions
    /// </summary>
    public class PointLight : Light
    {
        public PointLight(Transformation lightToWorld, SampledSpectrum spectrum = null) : base(lightToWorld, spectrum)
        {
            var lightPositionInLightSpace = Vector3.Zero;
            Position = lightToWorld.TransformPoint(ref lightPositionInLightSpace);
        }

        /// <summary>
        ///     The position of the light
        /// </summary>
        public Vector3 Position { get; private set; }

        public override SampledSpectrum Sample(ref Vector3 point, Scene scene, out Vector3 incomingVector,
            out VisibilityTester visibilityTester)
        {
            var direction = point - Position;
            incomingVector = direction.Normalized();
            visibilityTester = new VisibilityTester(Position, point, scene);
            return Spectrum/direction.LengthSquared;
        }
    }
}