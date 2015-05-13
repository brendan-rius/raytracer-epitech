namespace raytracer
{
    /// <summary>
    ///     A screen is a basic structure containing a width (x) and a height (y).
    ///     It defines the size of a screen.
    /// </summary>
    public struct Screen
    {
        public readonly int x;
        public readonly int y;

        /// <summary>
        ///     Create a screen for width and height
        /// </summary>
        /// <param name="x">the width</param>
        /// <param name="y">the height</param>
        public Screen(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    /// <summary>
    ///     A film records
    /// </summary>
    public abstract class Film
    {
        /// <summary>
        ///     Create a new film with a defined width and height
        /// </summary>
        /// <param name="x">the width of the film</param>
        /// <param name="y">the height of the film</param>
        protected Film(int x, int y)
        {
            Screen = new Screen(5, 5);
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