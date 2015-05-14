using System.Collections.Generic;

namespace raytracer.core
{
    /// <summary>
    ///     The goal of a sampler is to generate samples
    ///     <seealso cref="Sample" />
    /// </summary>
    public abstract class Sampler
    {
        /// <summary>
        ///     This methods method acts as an iterator over the samples
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<Sample> Samples();
    }
}