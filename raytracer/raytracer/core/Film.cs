namespace raytracer.core
{
    /// <summary>
    ///     A screen is a basic structure containing a width and a height.
    ///     It defines the size of a screen.
    /// </summary>
    public struct Screen
    {
        /// <summary>
        ///     THe height of the screen
        /// </summary>
        public readonly uint Height;

        /// <summary>
        ///     The width of the screen
        /// </summary>
        public readonly uint Width;

        /// <summary>
        ///     Create a screen for width and height
        /// </summary>
        /// <param name="width">the width</param>
        /// <param name="height">the height</param>
        public Screen(uint width, uint height)
        {
            Width = width;
            Height = height;
        }
    }

    /// <summary>
    ///     A film records
    /// </summary>
    public abstract class Film
    {
        /// <summary>
        ///     Create a new film from a screen
        /// </summary>
        /// <seealso cref="Screen" />
        /// <param name="screen"></param>
        protected Film(Screen screen)
        {
            Screen = screen;
        }

        /// <summary>
        ///     A film has a screen size (which mays be, for a Film that creates an image, the image size).
        /// </summary>
        public Screen Screen { get; private set; }

        /// <summary>
        ///     Adds the result of the ray casting (a spectrum) for the ray generated
        ///     by a sample
        ///     <seealso cref="Spectrum" />
        ///     <seealso cref="Sample" />
        /// </summary>
        /// <param name="sample">the sample from which the ray was generated</param>
        /// <param name="spectrum">the result of the casting of the ray in the scene</param>
        public abstract void AddSample(Sample sample, Spectrum spectrum);
    }
}