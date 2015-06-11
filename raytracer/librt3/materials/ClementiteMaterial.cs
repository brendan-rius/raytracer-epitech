using OpenTK;
using raytracer.core;
using raytracer.core.reflection;

namespace raytracer.materials
{
    public class ClementiteMaterial : Material
    {
        /// <summary>
        ///     The "transparency"
        /// </summary>
        private readonly float _d;

        /// <summary>
        ///     The type of illumation
        /// </summary>
        private readonly uint _illum;

        /// <summary>
        ///     Ambient lighting
        /// </summary>
        private readonly SampledSpectrum _ka;

        /// <summary>
        ///     Diffuse lighting
        /// </summary>
        private readonly SampledSpectrum _kd;

        /// <summary>
        ///     Specular lighting
        /// </summary>
        private readonly SampledSpectrum _ks;

        /// <summary>
        ///     The specular coefficient
        /// </summary>
        private readonly uint _ns;

        public ClementiteMaterial(Vector3 ka, Vector3 kd, Vector3 ks, uint ns, float d, uint illum)
        {
            _ka = SampledSpectrum.FromRGB(new[] {ka.X, ka.Y, ka.Z});
            _kd = SampledSpectrum.FromRGB(new[] {kd.X, kd.Y, kd.Z});
            _ks = SampledSpectrum.FromRGB(new[] {ks.X, ks.Y, ks.Z});
            _ns = ns;
            _illum = illum;
            _d = d;
        }

        public override BSDF GetBSDF(ref Intersection intersection)
        {
            var bsdf = new BSDF(ref intersection);
            bsdf.AddBxDF(new LambertianReflection(_kd));
            bsdf.AddBxDF(new SpecularReflection(_ns, _ks));
            bsdf.AddBxDF(new SpecularTransmission(1f, 1.14f, _kd));
            return bsdf;
        }
    }
}