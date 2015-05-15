using OpenTK;
using raytracer.core;
using raytracer.core.mathematics;

namespace raytracer.cameras
{
    public class SimpleCamera : Camera
    {
        public SimpleCamera(Screen screen, Transformation objectToWorld) : base(screen, objectToWorld)
        {
        }

        public override void GenerateRay(Sample sample, out Ray ray)
        {
            ray.Direction = new Vector3(-Screen.Width/2f + sample.X, Screen.Height/2f - sample.Y, 400);
            ray.Origin = Vector3.Zero;
            ObjectToWorld.TransformRay(ref ray, out ray);
        }
    }
}