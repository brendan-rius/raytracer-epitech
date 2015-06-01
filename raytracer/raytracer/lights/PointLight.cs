﻿using OpenTK;
using raytracer.core;
using raytracer.core.mathematics;

namespace raytracer.lights
{
    /// <summary>
    ///     Represent a light that emits the same amout of light in all directions
    /// </summary>
    public class PointLight : Light
    {
        /// <summary>
        ///     Create a point light from a position in space and an intensity
        /// </summary>
        /// <param name="position">the position of the light</param>
        /// <param name="intensity">its intensity</param>
        public PointLight(Transformation lightToWorld, SampledSpectrum intensity = null) : base(lightToWorld)
        {
            Intensity = intensity ?? new SampledSpectrum(1000000f);
            var lightPositionInLightSpace = new Vector3(0, 0, 0);
            Position = lightToWorld.TransformPoint(ref lightPositionInLightSpace);
        }

        /// <summary>
        ///     The intensity of the light
        /// </summary>
        public SampledSpectrum Intensity { get; private set; }

        /// <summary>
        ///     The position of the light
        /// </summary>
        public Vector3 Position { get; private set; }

        public override SampledSpectrum L(Vector3 point, Scene scene, out Vector3 incomingVector,
            out VisibilityTester visibilityTester)
        {
            var direction = point - Position;
            incomingVector = Vector3.Normalize(direction);
            visibilityTester = new VisibilityTester(Position, point, scene);
            return Intensity/direction.LengthSquared;
        }
    }
}