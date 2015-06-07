namespace librt3.core
{
    public class UVMapping2D : TextureMapping2D
    {
        private readonly float _du;
        private readonly float _dv;
        private readonly float _su;
        private readonly float _sv;

        public UVMapping2D(float ssu, float ssv, float ddu, float ddv)
        {
            _su = ssu;
            _sv = ssv;
            _du = ddu;
            _dv = ddv;
        }

        public override void Map(DifferentialGeometry dg, ref float s, ref float t, ref float dsdx, ref float dtdx,
            ref float dsdy,
            ref float dtdy)
        {
            s = _su*dg.U + _du;
            t = _sv*dg.V + _dv;
            dsdx = _su*dg.dudx;
            dtdx = _sv*dg.dvdx;
            dsdy = _su*dg.dudy;
            dtdy = _sv*dg.dvdy;
        }
    }
}