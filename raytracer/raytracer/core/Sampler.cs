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

    /// <summary>
    ///     A sampler that can be sub-divided into multiple samplers
    /// </summary>
    public abstract class ThreadedSampler : Sampler
    {
        /// <summary>
        ///     The ending line to render
        /// </summary>
        protected uint EndLine;

        /// <summary>
        ///     The starting line to render
        /// </summary>
        protected uint StartLine;

        /// <summary>
        ///     Create a threaded sampler
        /// </summary>
        /// <param name="screen">the screen</param>
        /// <param name="startLine">the starting line the sampler should manage</param>
        /// <param name="endLine">the ending line the sampler should manage</param>
        protected ThreadedSampler(Screen screen, uint? startLine = null, uint? endLine = null)
            : base(screen)
        {
            StartLine = startLine ?? 0;
            EndLine = endLine ?? screen.Height;
        }

        /// <summary>
        ///     Divide the sampler into multiple lines sub-samplers.
        ///     Each sampler will have to sample a contiguous group of lines.
        ///     There is currently no other subdivision strategy.
        /// </summary>
        /// <param name="nsamplers">the number of samplers</param>
        /// <returns>a list a samplers</returns>
        public abstract List<ThreadedSampler> GetSamplers(uint nsamplers = 4);
    }
}