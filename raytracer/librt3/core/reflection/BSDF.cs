using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using raytracer.core.mathematics;

namespace raytracer.core
{
    public class BSDF
    {
        private readonly List<BxDF> _bxdfs = new List<BxDF>();
        private readonly Intersection _intersection;
        private readonly Vector3 _nn;
        private readonly Vector3 _sn;
        private readonly Vector3 _tn;

        public BSDF(ref Intersection intersection)
        {
            _intersection = intersection;
            _nn = _intersection.NormalVector;
            _sn = _intersection.PointDifferentialOverU.Normalized();
            Vector3.Cross(ref _nn, ref _sn, out _tn);
        }

        protected Vector3 WorldToLocal(ref Vector3 v)
        {
            return new Vector3(Vector3.Dot(v, _sn), Vector3.Dot(v, _tn), Vector3.Dot(v, _nn));
        }

        protected Vector3 LocalToWorld(ref Vector3 v)
        {
            return new Vector3(
                _sn.X*v.X + _tn.X*v.Y + _nn.X*v.Z,
                _sn.Y*v.X + _tn.Y*v.Y + _nn.Y*v.Z,
                _sn.Z*v.X + _tn.Z*v.Y + _nn.Z*v.Z
                );
        }

        public void AddBxDF(BxDF bxdf)
        {
            _bxdfs.Add(bxdf);
        }

        public SampledSpectrum F(Vector3 incoming, Vector3 leaving, BxDF.BxDFType type)
        {
            var s = SampledSpectrum.Black();
            var incomingLocal = WorldToLocal(ref incoming);
            var leavingLocal = WorldToLocal(ref leaving);
            return _bxdfs.Where(bxdf => type.HasFlag(bxdf.Type))
                .Aggregate(s, (current, bxdf) => current + bxdf.F(incomingLocal, leavingLocal));
        }

        /// <summary>
        ///     Choose an incoming direction on the view from a BxDF matching a type
        ///     and returns its contribution
        /// </summary>
        /// <param name="leaving">the vector leaving the surface</param>
        /// <param name="incoming">the vector arriving at the surface, the function will set it</param>
        /// <param name="type">the type of scattering</param>
        /// <returns></returns>
        public SampledSpectrum Sample(Vector3 leaving, ref Vector3 incoming, BxDF.BxDFType type)
        {
            /* we randomly choose a BxDF that matches the type */
            var matchingBxDFs = _bxdfs.Where(f => type.HasFlag(f.Type)).ToList(); // the list of bxdf matching the type
            if (matchingBxDFs.Count == 0)
                return SampledSpectrum.Black();
            // random selection over matching bxdfs
            var bxdf =
                matchingBxDFs.ElementAt(Math.Min((int) (StaticRandom.NextFloat()*matchingBxDFs.Count), matchingBxDFs.Count - 1));
            var leavingLocal = WorldToLocal(ref leaving);
            Vector3 incomingLocal;
            // we sample the chosen bxdf
            var spectrum = bxdf.Sample(leavingLocal, out incomingLocal);
            // we transform the incoming ray returned by the sampling to world space
            incoming = LocalToWorld(ref incomingLocal);
            // is the reflection is not specular, we add contribution from other bxdfs of the same type
            if (!bxdf.Type.HasFlag(BxDF.BxDFType.Reflection))
                spectrum = _bxdfs.Where(b => type.HasFlag(b.Type))
                    .Aggregate(spectrum, (current, b) => current + b.F(incomingLocal, leavingLocal));
            return spectrum;
        }
    }
}