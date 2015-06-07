using OpenTK;

namespace librt3.core
{
    internal class PlanarMapping2D : TextureMapping2D
    {
        private readonly float _ds, _dt;
        private Vector3 _vs, _vt;

        public PlanarMapping2D(ref Vector3 v1, ref Vector3 v2, float dds = 0, float ddt = 0)
        {
            _vs = v1;
            _vt = v2;
            _ds = dds;
            _dt = ddt;
        }

        public override void Map(DifferentialGeometry dg, ref float s, ref float t, ref float dsdx, ref float dtdx,
            ref float dsdy,
            ref float dtdy)
        {
            Vector3 vec;
            var zero = new Vector3(0);
            Vector3.Subtract(ref dg.Point, ref zero, out vec);
            s = _ds + Vector3.Dot(vec, _vs);
            t = _dt + Vector3.Dot(vec, _vt);
            Vector3.Dot(ref dg.Dpdx, ref _vs, out dsdx);
            Vector3.Dot(ref dg.Dpdx, ref _vt, out dtdx);
            Vector3.Dot(ref dg.Dpdy, ref _vs, out dsdy);
            Vector3.Dot(ref dg.Dpdy, ref _vt, out dtdy);
        }
    }
}