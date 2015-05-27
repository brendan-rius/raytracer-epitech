using OpenTK;

namespace raytracer.core.mathematics
{
    public abstract class BxDF
    {
        public abstract SampledSpectrum BidirectionalScattering(Vector3 incoming, Vector3 leaving);

        /// <summary>
        ///     Get the reflectance for a "leaving" vector when
        ///     the illumination is constant on the hemisphere.
        /// </summary>
        /// <param name="leaving">the "leaving" vector</param>
        /// <returns></returns>
        public abstract SampledSpectrum Reflectance(Vector3 leaving);

        public static SampledSpectrum DielectricFresnel(float cosi, float cost, SampledSpectrum si, SampledSpectrum st)
        {
            var parallel = ((st*cosi) - (si*cost))/((st*cosi) + (si*cost));
            var perpendicular = ((si*cosi) - (st*cost))/((si*cosi) + (st*cost));
            return (SampledSpectrum) (parallel*parallel + perpendicular*perpendicular);
        }
    }
}