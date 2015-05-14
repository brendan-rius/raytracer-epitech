namespace raytracer.core
{
    /// <summary>
    ///     The spectrum represents the properties of light at a point.
    ///     In simple term, it is a color (or to be more precise, it creates a color).
    /// </summary>
    public struct Spectrum
    {
        public static readonly Spectrum BlackSpectrum = new Spectrum(0f, 0f, 0f);
        public static readonly Spectrum BlueSpectrum = new Spectrum(0f, 0f, 1f);

        public Spectrum(float r, float g, float b) : this()
        {
            Red = r;
            Green = g;
            Blue = b;
        }

        public float Red { get; set; }
        public float Green { get; set; }
        public float Blue { get; set; }
    }
}