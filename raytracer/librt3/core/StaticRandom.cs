using System;

namespace raytracer.core
{
    public class StaticRandom
    {
        private static readonly Random _rng = new Random();

        public static float NextFloat()
        {
            lock (_rng)
            {
                return (float) _rng.NextDouble();
            }
        }
    }
}