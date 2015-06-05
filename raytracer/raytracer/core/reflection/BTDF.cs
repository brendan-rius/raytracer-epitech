using OpenTK;
using raytracer.core.mathematics;

namespace raytracer.core
{
    public class BTDF : BxDF
    {
        /// <summary>
        ///     This is the BRDF used to create the BTDF (if any)
        /// </summary>
        private readonly BRDF _sourceBRDF;

        public BTDF() : base(BxDFType.Transmission)
        {
        }

        /// <summary>
        ///     Creates a BTDF based on a BRDF
        /// </summary>
        /// <param name="brdf"></param>
        public BTDF(BRDF brdf) : base(BxDFType.Transmission)
        {
            _sourceBRDF = brdf;
        }

        public override SampledSpectrum F(Vector3 incoming, Vector3 leaving)
        {
            if (_sourceBRDF != null)
            {
                var incomingInOtherHemisphere = OtherHemisphere(ref incoming);
                return _sourceBRDF.F(incomingInOtherHemisphere, leaving);
            }
            return SampledSpectrum.Random();
        }

        public override SampledSpectrum Sample(Vector3 leaving, out Vector3 incoming)
        {
            incoming = new Vector3(0, 0, 0);
            return SampledSpectrum.Black();
        }

        public static Vector3 OtherHemisphere(ref Vector3 v)
        {
            return new Vector3(v.X, v.Y, -v.Z);
        }
    }
}