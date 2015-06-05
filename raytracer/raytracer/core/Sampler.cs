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
        ///     The screen used to generate the samples
        /// </summary>
        protected Screen Screen;

        /// <summary>
        ///     Create the sampler from a screen.
        ///     <seealso cref="Screen" />
        /// </summary>
        /// <param name="screen">the screen</param>
        /// <param name="startLine">the starting line of the sampler</param>
        /// <param name="endLine">the ending line for the sampler</param>
        protected Sampler(Screen screen)
        {
            Screen = screen;
        }

        /// <summary>
        ///     This methods method acts as an iterator over the samples
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<Sample> Samples();
    }
}