namespace raytracer
{
    /// <summary>
    ///     A camera sample represents a "position" of the camera.
    ///     A simple camera sample is made of x and a y coordinates, but
    ///     it may also contain some information about time (the (x, y)'s pixel
    ///     at time "t").
    /// </summary>
    public class Sample
    {
        /// <summary>
        ///     Create a sample of coordinates (x; y)
        /// </summary>
        /// <param name="x">the x coordinate</param>
        /// <param name="y">the y coordinate</param>
        public Sample(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        ///     The x coordinate on the camera
        /// </summary>
        public int x { get; private set; }

        /// <summary>
        ///     The y coordinate on the camera
        /// </summary>
        public int y { get; private set; }
    }
}