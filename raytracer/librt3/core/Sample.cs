namespace raytracer.core
{
    /// <summary>
    ///     A camera sample represents a "position" of the camera.
    ///     A simple camera sample is made of width and a height coordinates, but
    ///     it may also contain some information about time (the (width, height)'s pixel
    ///     at time "t").
    /// </summary>
    public class Sample
    {
        /// <summary>
        ///     Create a sample of coordinates (width; height)
        /// </summary>
        /// <param name="x">the width coordinate</param>
        /// <param name="y">the height coordinate</param>
        public Sample(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        ///     The X-coordinate on the camera
        /// </summary>
        public float X { get; private set; }

        /// <summary>
        ///     The Y-coordinate on the camera
        /// </summary>
        public float Y { get; private set; }
    }
}