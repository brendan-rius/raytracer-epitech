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
        public const float FocalDistance = 400;

        private readonly float _screenLeft;
        private readonly float _screenUp;
        public SimpleCamera(Screen screen, Transformation objectToWorld) : base(screen, objectToWorld)
        {
            _screenLeft = -Screen.Width/2f;
            _screenUp = Screen.Height/2f;
        }

        public override Ray GenerateRay(Sample sample)
        {
            var ray = new Ray(new Vector3(_screenLeft + sample.X, _screenUp - sample.Y, FocalDistance).Normalized(),
                Vector3.Zero);
            return ObjectToWorld.TransformRay(ray);
        }
    }
}