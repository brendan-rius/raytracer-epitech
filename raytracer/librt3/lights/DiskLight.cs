using System;
using OpenTK;
using raytracer.core;
using raytracer.core.mathematics;

namespace raytracer.lights
{
    public class DiskLight : AreaLight
    {
        /// <summary>
        ///     The normal vector of the disk
        /// </summary>
        private readonly Vector3 _normal;

        /// <summary>
        ///     The radius of the disk
        /// </summary>
        private readonly float _radius;

        public DiskLight(Transformation objectToWorld, float radius = 1, SampledSpectrum spectrum = null)
            : base(objectToWorld, spectrum)
        {
            _radius = radius;
            _normal = ObjectToWorld.TransformNormal(new Vector3(0, -1, 0)).Normalized();
            NSamples = (uint) Math.Max(1, _radius/4f);
        }

        public override SampledSpectrum Sample(ref Vector3 point, Scene scene, out Vector3 incomingVector,
            out VisibilityTester visibilityTester)
        {
            var r1 = StaticRandom.NextFloat()*2 - 1;
            var r2 = StaticRandom.NextFloat()*2 - 1;
            var pointInDiskLocal = new Vector3(r1*_radius, 0, r2*_radius);
            var pointInDiskWorld = ObjectToWorld.TransformPoint(ref pointInDiskLocal);
            incomingVector = point - pointInDiskWorld;
            var cosangle = Vector3.Dot(incomingVector.Normalized(), _normal);
            visibilityTester = new VisibilityTester(pointInDiskWorld, point, scene);
            return Spectrum*cosangle/incomingVector.Length;
        }
    }
} ;