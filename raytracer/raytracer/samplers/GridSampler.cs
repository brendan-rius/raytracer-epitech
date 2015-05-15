﻿using System;
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
    public class GridSampler : Sampler
    {
        /// <summary>
        ///     The screen used to generate the samples
        /// </summary>
        protected Screen Screen;

        /// <summary>
        ///     Create the sampler from a screen.
        ///     <seealso cref="Screen" />
        /// </summary>
        /// <param name="screen"></param>
        public GridSampler(Screen screen)
        {
            Screen = screen;
        }

        public override IEnumerable<Sample> Samples()
        {
            for (var y = 0.5f; y < Screen.Height; ++y)
            {
                for (var x = 0.5f; x < Screen.Width; ++x)
                {
                    yield return new Sample(x, y);
                }
            }
        }
    }

    /// <summary>
    ///     A jitter grid sampler create a random sample for each "pixel", (not at the center
    ///     as a simple <seealso cref="GridSampler" /> would do. Thus, we can generate
    ///     many sample for the same pixel, improving the render.
    /// </summary>
    public class JitterGridSampler : GridSampler
    {
        private readonly Random _rng;

        /// <summary>
        ///     Creates a new sampler
        /// </summary>
        /// <param name="screen">the screen for the grid sampler</param>
        /// <param name="nsamples">the number of sample to generate for each pixel</param>
        /// <param name="rng">a random number generator. If null, this class will create one</param>
        public JitterGridSampler(Screen screen, uint nsamples = 4, Random rng = null) : base(screen)
        {
            _rng = rng ?? new Random();
            NumberOfSamples = nsamples;
        }

        /// <summary>
        ///     The number of sample generated for each pixel
        /// </summary>
        public uint NumberOfSamples { get; set; }

        public override IEnumerable<Sample> Samples()
        {
            for (var y = 0f; y < Screen.Height; ++y)
            {
                for (var x = 0f; x < Screen.Width; ++x)
                {
                    for (var i = 0; i < NumberOfSamples; ++i)
                    {
                        yield return new Sample(x + (float) _rng.NextDouble(), y + (float) _rng.NextDouble());
                    }
                }
            }
        }
    }
}