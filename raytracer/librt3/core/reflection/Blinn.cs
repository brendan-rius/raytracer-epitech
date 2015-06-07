using System;
using OpenTK;

namespace librt3.core.reflection
{
    public class Blinn : MicrofacetDistribution
    {
        private readonly float _exponent;

        public Blinn(float e)
        {
            if (e > 10000 || float.IsNaN(e)) e = 10000;
            _exponent = e;
        }

        public override float D(ref Vector3 half)
        {
            var costhetah = Math.Abs(half.Z);
            return (_exponent + 2)*(1/MathHelper.TwoPi)*(float) Math.Pow(costhetah, _exponent);
        }
    }
}