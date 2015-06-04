using System.Collections.Generic;
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

        protected Vector3 WorldToLocal(Vector3 v)
        {
            return new Vector3(Vector3.Dot(v, _sn), Vector3.Dot(v, _tn), Vector3.Dot(v, _nn));
        }

        protected Vector3 LocalToWorld(Vector3 v)
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

        public SampledSpectrum F(Vector3 incoming, Vector3 leaving)
        {
            var s = SampledSpectrum.Black();
            var incomingLocal = WorldToLocal(incoming);
            var leavingLocal = WorldToLocal(leaving);
            foreach (var bxdf in _bxdfs)
            {
                s += bxdf.F(incomingLocal, leavingLocal);
            }
            return s;
        }

        public SampledSpectrum Sample(ref Vector3 leaving, out Vector3 incoming, BxDF.BxDFType type)
        {
            foreach (var bxdf in _bxdfs)
            {
                if (bxdf.Type.HasFlag(type))
                    return bxdf.Sample(ref leaving, out incoming);
            }
            incoming = Vector3.UnitX;
            return SampledSpectrum.Black();
        }
    }
}