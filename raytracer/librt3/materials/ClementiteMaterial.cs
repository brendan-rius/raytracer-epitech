using librt3.core.reflection;
using OpenTK;
using raytracer.core;

namespace raytracer.materials
{
    public class ClementiteMaterial : Material
    {
        /// <summary>
        ///     The "transparency"
        /// </summary>
        private float _d;

        /// <summary>
        ///     The type of illumation
        /// </summary>
        private uint _illum;

        /// <summary>
        ///     Ambient lighting
        /// </summary>
        private Vector3 _ka;

        /// <summary>
        ///     Diffuse lighting
        /// </summary>
        private Vector3 _kd;

        /// <summary>
        ///     Specular lighting
        /// </summary>
        private Vector3 _ks;

        /// <summary>
        ///     The specular coefficient
        /// </summary>
        private uint _ns;

        public ClementiteMaterial(Vector3 ka, Vector3 kd, Vector3 ks, uint ns, float d, uint illum)
        {
            _ka = ka;
            _kd = kd;
            _ks = ks;
            _ns = ns;
            _illum = illum;
            _d = d;
        }

        public override BSDF GetBSDF(ref Intersection intersection)
        {
            var bsdf = new BSDF(ref intersection);
            bsdf.AddBxDF(new Microfacet(_ks, null, _ns));
            bsdf.AddBxDF(new LambertianReflection(SampledSpectrum.Random()));
            return bsdf;
        }
    }
}