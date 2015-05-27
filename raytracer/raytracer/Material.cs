using OpenTK;
using OpenTK.Graphics.OpenGL;
using raytracer.core;

namespace raytracer
{
    public abstract class Material
    {
        public abstract BRDF GetBRDF(Vector3 p);
    }
}