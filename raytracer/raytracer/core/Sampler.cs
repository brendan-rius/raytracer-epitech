using System.Collections.Generic;

namespace raytracer
{
    /// <summary>
    ///     The goal of a sampler is to generate samples
    ///     <seealso cref="Sample" />
    /// </summary>
    public abstract class Sampler
    {
        /// <summary>
        ///     This methods generates a sample. It will be called many times,
        ///     and this it has to give
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<Sample> Samples();
    }
}