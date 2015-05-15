using OpenTK;
using raytracer.core;
using raytracer.core.mathematics;

namespace raytracer.primitives
{
    public class Sphere : GeometricElement
    {
        public Sphere(Transformation worldToObjectTransformation = null) : base(worldToObjectTransformation)
        {
        }

        public override bool TryToIntersect(ref Ray ray, out DifferentialGeometry? differentialGeometry)
        {
            Ray rayInObjectWorld;
            WorldToObjectTransformation.TransformRay(ref ray, out rayInObjectWorld);
            float a, b, c;
            Vector3.Dot(ref rayInObjectWorld.Direction, ref rayInObjectWorld.Direction, out a);
            Vector3.Dot(ref rayInObjectWorld.Origin, ref rayInObjectWorld.Direction, out b);
            b += b;
            Vector3.Dot(ref rayInObjectWorld.Origin, ref rayInObjectWorld.Origin, out c);
            c -= 1;

            var d = b*b - 4*a*c;
            differentialGeometry = null;
            return !(d < 0f);
        }
    }
}