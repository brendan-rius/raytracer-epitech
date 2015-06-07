using System;
using OpenTK;

namespace librt3.core.reflection
{
    internal class Anisotropic : MicrofacetDistribution
    {
        private readonly float _ex;
        private readonly float _ey;

        public Anisotropic(float x, float y)
        {
            _ex = x;
            _ey = y;
            if (_ex > 10000 || float.IsNaN(_ex)) _ex = 10000;
            if (_ey > 10000 || float.IsNaN(_ey)) _ey = 10000;
        }

        public override float D(ref Vector3 half)
        {
            var costhetah = Math.Abs(half.Z);
            var d = 1 - costhetah*costhetah;
            if (d == 0) return 0;
            var e = (_ex*half.X*half.X + _ey*half.Y*half.Y)/d;
            return (float) Math.Sqrt((_ex + 2)*(_ey + 2))*(1/MathHelper.TwoPi)*(float) Math.Pow(costhetah, e);
        }
    }
}