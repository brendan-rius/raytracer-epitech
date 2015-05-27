﻿using OpenTK;
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
            ray = new Ray
            {
                Direction = new Vector3(-Screen.Width/2f + sample.X, Screen.Height/2f - sample.Y, 400).Normalized(),
                Origin = Vector3.Zero,
                Start = Ray.DefaultStartValue,
                End = Ray.DefaultEndValue
            };
            ObjectToWorld.TransformRay(ref ray, out ray);
        }
    }
}