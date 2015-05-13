using System.Collections.Generic;

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
        private Screen Screen;

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
            for (var y = 0.5f; y < Screen.y; ++y)
            {
                for (var x = 0.5f; x < Screen.x; ++x)
                {
                    yield return new Sample(x, y);
                }
            }
        }
    }
}