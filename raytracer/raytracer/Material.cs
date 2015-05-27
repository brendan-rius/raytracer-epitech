using System.Numerics;
using OpenTK;
using raytracer.core;

namespace raytracer
{
    public abstract class Material
    {
        public abstract BRDF GetBRDF(Vector3 p);
    }
}