namespace raytracer
{
    /// <summary>
    ///     The role of the camera is to see the scene, so that a film can record it.
    ///     Each camera can have its own way of seeing (a camera may have a wider field of view than another one,
    ///     while another camera can create a fish-eye effect for example).
    ///     A camera may also use lens.
    ///     <seealso cref="Film" />
    /// </summary>
    public abstract class Camera
    {
        /// <summary>
        ///     Generate a ray from a camera sample
        ///     <seealso cref="Sample" />
        /// </summary>
        /// <param name="sample"></param>
        /// <returns>the generate ray</returns>
        public abstract Ray GenerateRay(Sample sample);
    }
}