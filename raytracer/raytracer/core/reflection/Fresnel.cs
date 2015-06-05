using System;
using System.Diagnostics;

namespace raytracer.core
{
    public abstract class Fresnel
    {
        /// <summary>
        ///     Compute the fresnel reflectance for dielectric materials
        /// </summary>
        /// <param name="cosi">the angle between incoming ray and normal</param>
        /// <param name="cost">the angle between outgoing ray and normal's inverse</param>
        /// <param name="si">the index of refraction of the first medium</param>
        /// <param name="st">the index of refraction of the second medium</param>
        /// <returns>the reflectance</returns>
        public static SampledSpectrum DielectricFresnel(float cosi, float cost, SampledSpectrum si, SampledSpectrum st)
        {
            var parallel = ((st*cosi) - (si*cost))/((st*cosi) + (si*cost));
            var perpendicular = ((si*cosi) - (st*cost))/((si*cosi) + (st*cost));
            if (parallel.HasNaNs() || perpendicular.HasNaNs())
                Debug.WriteLine("");
            return parallel*parallel + perpendicular*perpendicular;
        }

        /// <summary>
        ///     Return the amount of light reflected at the surface
        /// </summary>
        /// <param name="cosi">the cosine of the incident angle</param>
        /// <returns></returns>
        public abstract SampledSpectrum Evaluate(float cosi);
    }

    public class FresnelDielectric : Fresnel
    {
        /// <summary>
        ///     The index of refraction of the first medium
        /// </summary>
        protected readonly float IndexOfRefraction1;

        /// <summary>
        ///     The index of refraction of the second medium
        /// </summary>
        protected readonly float IndexOfRefraction2;

        /// <summary>
        ///     Create a fresnel conductor from the indexes of refraction of the two sides
        ///     of a surface
        /// </summary>
        /// <param name="indexOfRefraction1">the index of refraction on the first side of the surface</param>
        /// <param name="indexOfRefraction2">the index of refraction on the second side of the surface</param>
        public FresnelDielectric(float indexOfRefraction1, float indexOfRefraction2)
        {
            IndexOfRefraction1 = indexOfRefraction1;
            IndexOfRefraction2 = indexOfRefraction2;
        }

        public override SampledSpectrum Evaluate(float cosi)
        {
            /* The incident ray can either be entering the medium (entering a sphere of water,
             * this going from air to water for example), or be leaving the medium (going from
             * the water inside the sphere to the air outside the sphere. If the cosine
             * of the angle between the surface's normal and incoming ray if positive,
             * then the angle between two is between 0 and 90 degrees, meaning the incoming ray
             * if entering the medium. If it is negative, then the incoming ray is leaving the medium */
            var entering = cosi > 0;
            float indexOfRefractionIncident, indexOfRefractionOther;
            if (entering)
            {
                indexOfRefractionIncident = IndexOfRefraction1;
                indexOfRefractionOther = IndexOfRefraction2;
            }
            else
            {
                indexOfRefractionOther = IndexOfRefraction1;
                indexOfRefractionIncident = IndexOfRefraction2;
            }
            /* Using Snell's law sin(t) * n(t) = sin(i) * n(i), we have sin(t) = (sin(i) * n(i)) / n(t).
             * Here, sin(i) is computed using the fact that sin²(t) + cos²(a) = 1, so sin(a) = sqrt(1 - cos²(a)) */
            var sint = indexOfRefractionIncident/indexOfRefractionOther*Math.Sqrt(1 - cosi*cosi);
            var cost = (float) Math.Sqrt(Math.Max(0, 1 - sint*sint));
            var reflected = DielectricFresnel(Math.Abs(cosi), cost, new SampledSpectrum(indexOfRefractionIncident),
                new SampledSpectrum(indexOfRefractionOther));
            return reflected;
        }
    }
}