using OpenTK;
using raytracer.core;
using raytracer.core.mathematics;

namespace raytracer.cameras
{
    public class SimpleCamera : Camera
    {
        /// <summary>
        ///     The focal distance of the camera
        /// </summary>
        public const float FocalDistance = -400;

        public SimpleCamera(Screen screen, Transformation objectToWorld) : base(screen, objectToWorld)
        {
        }

        public override Ray GenerateRay(Sample sample)
        {
            return ObjectToWorld.TransformRay(new Ray(
                new Vector3(-Screen.Width/2f + sample.X, Screen.Height/2f - sample.Y, FocalDistance).Normalized(),
                Vector3.Zero));
        }
    }
}