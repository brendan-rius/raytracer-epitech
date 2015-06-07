using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace librt3.core
{
    abstract class Texture<T>
    {
        public abstract T Evaluate(DifferentialGeometry dg);
    }
}
