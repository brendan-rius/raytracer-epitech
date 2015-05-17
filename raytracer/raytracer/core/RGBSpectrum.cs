namespace raytracer.core
{
    /// <summary>
    ///     The spectrum represents the properties of light at a point.
    ///     In simple term, it is a color (or to be more precise, it creates a color).
    /// </summary>
    public class RGBSpectrum
    {
        public RGBSpectrum(float r, float g, float b)
        {
            Red = r;
            Green = g;
            Blue = b;
        }

        public float Red { get; private set; }
        public float Green { get; private set; }
        public float Blue { get; private set; }

        public static RGBSpectrum operator +(RGBSpectrum left, RGBSpectrum right)
        {
            return left.AddSpectrum(right);
        }

        public static RGBSpectrum operator *(RGBSpectrum left, RGBSpectrum right)
        {
            return left.MultiplySpectrum(right);
        }

        protected RGBSpectrum AddSpectrum(RGBSpectrum right)
        {
            return new RGBSpectrum(Red + right.Red, Green + right.Green, Blue + right.Blue);
        }

        protected RGBSpectrum MultiplySpectrum(RGBSpectrum right)
        {
            return new RGBSpectrum(Red*right.Red, Green*right.Green, Blue*right.Blue);
        }
    }
}