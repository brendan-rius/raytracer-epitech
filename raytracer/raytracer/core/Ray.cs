﻿using OpenTK;

namespace raytracer.core
{
    /// <summary>
    ///     A ray is generated by the camera from a sample.
    ///     It is then launched in the scene, from where it gets a spectrum
    ///     <seealso cref="Camera" />
    ///     <seealso cref="Sample" />
    ///     <seealso cref="Scene" />
    /// </summary>
    public struct Ray
    {
        public const float DefaultEndValue = float.PositiveInfinity;
        public const float DefaultStartValue = 0f;

        /// <summary>
        ///     The direction of the vector.
        ///     This direction is normalized.
        /// </summary>
        public Vector3 Direction;

        /// <summary>
        ///     The endpoint of the ray in terms of time
        /// </summary>
        public float End;

        /// <summary>
        ///     The origin point of a ray
        /// </summary>
        public Vector3 Origin;

        /// <summary>
        ///     The start point of the ray in terms of time
        /// </summary>
        public float Start;

        /// <summary>
        ///     Create a ray from a direction and an origin.
        ///     A ray can have a start and an end time which define the two end-points of the
        ///     ray. By default these are 0 (for start time on the ray), and +Infinity (for
        ///     end start of the ray)
        /// </summary>
        /// <param name="direction">the direction of the ray</param>
        /// <param name="origin">the origin of the ray</param>
        /// <param name="start">the start time of the ray (0 by default)</param>
        /// <param name="end">the end time of the ray (+Infinity by default)</param>
        public Ray(Vector3 direction, Vector3 origin, float start = DefaultStartValue,
            float end = float.PositiveInfinity)
        {
            Direction = direction;
            Origin = origin;
            Start = start;
            End = end;
        }

        /// <summary>
        ///     Computes the point at a certain time on the ray
        /// </summary>
        /// <param name="t">the time</param>
        /// <returns>a new point on the ray</returns>
        public Vector3 PointAtTime(float t)
        {
            Vector3 point;
            PointAtTime(t, out point);
            return point;
        }

        /// <summary>
        ///     Computes the point at a certain time on the ray
        /// </summary>
        /// <param name="t">the time</param>
        /// <param name="point">the point</param>
        public void PointAtTime(float t, out Vector3 point)
        {
            point.X = Origin.X + Direction.X*t;
            point.Y = Origin.Y + Direction.Y*t;
            point.Z = Origin.Z + Direction.Z*t;
        }
    }
}