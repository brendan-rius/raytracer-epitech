﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using raytracer.core;
using raytracer.core.mathematics;

namespace librt3.core.reflection
{
    class FresnelBlend : BxDF
    {
        private MicrofacetDistribution _distribution;

        private SampledSpectrum _rd;

        private SampledSpectrum _rs;

        public FresnelBlend(SampledSpectrum d, SampledSpectrum s, MicrofacetDistribution dist) : base(BxDFType.Reflection | BxDFType.Glossy)
        {
            _distribution = dist;
            _rd = d;
            _rs = s;
        }

        public override SampledSpectrum F(Vector3 incoming, Vector3 leaving)
        {
            throw new NotImplementedException();
        }
    }
}