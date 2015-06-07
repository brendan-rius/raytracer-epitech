using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace librt3.core
{
    class ConstantTexture<T> : Texture<T>
    {
        private readonly T _value;

        public ConstantTexture(T v)
        {
            _value = v;
        }

        public override T Evaluate(DifferentialGeometry dg)
        {
            return _value;
        }
    }
}
