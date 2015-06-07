using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace librt3.core
{
    class SphericalMapping2D : TextureMapping2D
    {
        public override void Map(DifferentialGeometry dg, ref float s, ref float t, ref float dsdx, ref float dtdx, ref float dsdy,
            ref float dtdy)
        {
            throw new NotImplementedException();
        }
    }
}
