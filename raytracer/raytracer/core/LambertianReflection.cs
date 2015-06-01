﻿using System;
using System.Numerics;
using OpenTK;

namespace raytracer.core
{
    public class LambertianReflection : BRDF
    {
        /// <summary>
        ///     The inverse of PI.
        /// </summary>
        public const float INV_PI = 1/(float) Math.PI;

        private readonly SampledSpectrum _spectrum;

        public LambertianReflection(SampledSpectrum spectrum)
        {
            _spectrum = spectrum;
        }

        public override SampledSpectrum BidirectionalScattering(Vector3 incoming, Vector3 leaving)
        {
            return _spectrum*INV_PI;
        }

        public override SampledSpectrum Reflectance(Vector3 leaving)
        {
            throw new NotImplementedException();
        }
    }
}