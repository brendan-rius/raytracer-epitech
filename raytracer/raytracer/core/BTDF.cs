using System;
using OpenTK;
using raytracer.core.mathematics;

namespace raytracer.core
{
    public class BTDF : BxDF
    {
        /// <summary>
        ///     This is the BRDF used to create the BTDF (is any)
        /// </summary>
        private readonly BRDF _sourceBRDF;

        public BTDF()
        {
        }

        /// <summary>
        ///     Creates a BTDF based on a BRDF
        /// </summary>
        /// <param name="brdf"></param>
        public BTDF(BRDF brdf)
        {
            _sourceBRDF = brdf;
        }

        public override SampledSpectrum BidirectionalScattering(Vector3 incoming, Vector3 leaving)
        {
            return _sourceBRDF != null
                ? _sourceBRDF.BidirectionalScattering(OtherHemisphere(incoming), leaving)
                : SampledSpectrum.Random();
        }

        public override SampledSpectrum Reflectance(Vector3 leaving)
        {
            throw new NotImplementedException();
        }

        public static Vector3 OtherHemisphere(Vector3 v)
        {
            v.Z = -v.Z;
            return v;
        }
    }
}