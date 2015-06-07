using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace librt3.core
{
    class ScaleTexture<T, TU> : Texture<TU>
    {
        private readonly Texture<T> _tex1;

        private readonly Texture<TU> _tex2;

        public ScaleTexture(Texture<T> t1, Texture<TU> t2)
        {
            _tex1 = t1;
            _tex2 = t2;
        }

        public override TU Evaluate(DifferentialGeometry dg)
        {
            return Operators.Multiply(_tex2.Evaluate(dg), _tex1.Evaluate(dg));
        }
    }
}
