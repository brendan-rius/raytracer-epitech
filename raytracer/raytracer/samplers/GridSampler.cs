using System;
using System.Collections.Generic;
using raytracer.core;

namespace raytracer.samplers
{
    /// <summary>
    ///     A grid sampler generate one ray for each "pixel" on the screen. It is the simplest
    ///     sampler, but it can lead to aliasing.
    ///     A grid sampler have to know the size of the screen to be used
    ///     <seealso cref="Sample" />
    ///     <seealso cref="Screen" />
    /// </summary>
    public class GridSampler : ThreadedSampler
    {
        public GridSampler(Screen screen, uint? startLine = null, uint? endLine = null)
            : base(screen, startLine, endLine)
        {
        }

        public override IEnumerable<Sample> Samples()
        {
            for (var y = StartLine + 0.5f; y < EndLine; ++y)
            {
                for (var x = 0.5f; x < Screen.Width; ++x)
                {
                    yield return new Sample(x, y);
                }
            }
        }

        public override List<ThreadedSampler> GetSamplers(uint nsamplers = 4)
        {
            if (nsamplers > EndLine - StartLine)
                throw new Exception("Too much samplers");
            var samplers = new List<ThreadedSampler>();
            var nlines = (EndLine - StartLine)/nsamplers; // the number of lines each sampler has to handle
            /* We generate all the samplers except the last one */
            for (uint i = 0; i < nsamplers - 1; ++i)
                samplers.Add(new GridSampler(Screen, StartLine + i*nlines, StartLine + i*nlines + nlines));
            /* THe last sampler handles the left lines */
            samplers.Add(new GridSampler(Screen, StartLine + (nsamplers - 1)*nlines, EndLine));
            return samplers;
        }
    }

    /// <summary>
    ///     A jitter grid sampler create a random sample for each "pixel", (not at the center
    ///     as a simple <seealso cref="GridSampler" /> would do. Thus, we can generate
    ///     many sample for the same pixel, improving the render.
    /// </summary>
    public class JitterGridSampler : GridSampler
    {
        /// <summary>
        ///     Creates a new sampler
        /// </summary>
        /// <param name="screen">the screen for the grid sampler</param>
        /// <param name="nsamples">the number of sample to generate for each pixel</param>
        /// <param name="rng">a random number generator. If null, this class will create one</param>
        public JitterGridSampler(Screen screen, uint? startLine = null, uint? endLine = null, uint nsamples = 4)
            : base(screen, startLine, endLine)
        {
            NumberOfSamples = nsamples;
        }

        /// <summary>
        ///     The number of sample generated for each pixel
        /// </summary>
        public uint NumberOfSamples { get; set; }

        public override List<ThreadedSampler> GetSamplers(uint nsamplers = 4)
        {
            if (nsamplers > EndLine - StartLine)
                throw new Exception("Too much samplers");
            var samplers = new List<ThreadedSampler>();
            var nlines = (EndLine - StartLine)/nsamplers; // the number of lines each sampler has to handle
            /* We generate all the samplers except the last one */
            for (uint i = 0; i < nsamplers - 1; ++i)
                samplers.Add(new JitterGridSampler(Screen, StartLine + i*nlines, StartLine + i*nlines + nlines,
                    NumberOfSamples));
            /* THe last sampler handles the left lines */
            samplers.Add(new JitterGridSampler(Screen, StartLine + (nsamplers - 1)*nlines, EndLine, NumberOfSamples));
            return samplers;
        }

        /// <summary>
        ///     Get the samples
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Sample> Samples()
        {
            for (var y = StartLine; y < EndLine; ++y)
            {
                for (var x = 0f; x < Screen.Width; ++x)
                {
                    for (var i = 0; i < NumberOfSamples; ++i)
                    {
                        yield return new Sample(x + StaticRandom.NextFloat(), y + StaticRandom.NextFloat());
                    }
                }
            }
        }
    }
}