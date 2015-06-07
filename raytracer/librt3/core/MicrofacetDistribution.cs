using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace librt3.core
{
    abstract class MicrofacetDistribution
    {
        public abstract float D(ref Vector3 half);
    }
}
