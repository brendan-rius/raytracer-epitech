using System;
using OpenTK;

namespace raytracer.core
{
    /// <summary>
    ///     A bounding box implementation.
    /// </summary>
    public class BBox
    {
        /// <summary>
        ///     Maximum point of the bounding box.
        /// </summary>
        public Vector3 PMax;

        /// <summary>
        ///     Mininum point of the bounding box.
        /// </summary>
        public Vector3 PMin;

        public BBox()
        {
            PMin = new Vector3(float.NegativeInfinity);
            PMax = new Vector3(float.PositiveInfinity);
        }

        public BBox(Vector3 point)
        {
            PMin = new Vector3(point);
            PMax = new Vector3(point);
        }

        /// <summary>
        ///     Creates a new bounding box.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        public BBox(Vector3 p1, Vector3 p2)
        {
            PMin = new Vector3(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Min(p1.Z, p2.Z));
            PMax = new Vector3(Math.Max(p1.X, p2.X), Math.Max(p1.Y, p2.Y), Math.Max(p1.Z, p2.Z));
        }

        /// <summary>
        ///     Adds a point to the bounding box, recalculating it.
        /// </summary>
        /// <param name="box"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static BBox Union(BBox box, Vector3 point)
        {
            var ret = new BBox
            {
                PMin =
                {
                    X = Math.Min(box.PMin.X, point.X),
                    Y = Math.Min(box.PMin.Y, point.Y),
                    Z = Math.Min(box.PMin.Z, point.Z)
                },
                PMax =
                {
                    X = Math.Max(box.PMax.X, point.X),
                    Y = Math.Max(box.PMax.Y, point.Y),
                    Z = Math.Max(box.PMax.Z, point.Z)
                }
            };
            return ret;
        }

        /// <summary>
        /// Creates a new bounding boxes from two bounding boxes
        /// </summary>
        /// <param name="box"></param>
        /// <param name="box2"></param>
        /// <returns></returns>
        public static BBox Union(BBox box, BBox box2)
        {
            var ret = new BBox
            {
                PMin =
                {
                    X = Math.Min(box.PMin.X, box2.PMin.X),
                    Y = Math.Min(box.PMin.Y, box2.PMin.Y),
                    Z = Math.Min(box.PMin.Z, box2.PMin.Z)
                },
                PMax =
                {
                    X = Math.Max(box.PMax.X, box2.PMax.X),
                    Y = Math.Max(box.PMax.Y, box2.PMax.Y),
                    Z = Math.Max(box.PMax.Z, box2.PMax.Z)
                }
            };
            return ret;
        }

        /// <summary>
        ///     Checks whether the given bounding box overlaps with this one.
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        public bool Overlaps(BBox box)
        {
            var x = (PMax.X >= box.PMin.X) && (PMin.X <= box.PMax.X);
            var y = (PMax.Y >= box.PMin.Y) && (PMin.Y <= box.PMax.Y);
            var z = (PMax.Z >= box.PMin.Z) && (PMin.Z <= box.PMax.Z);
            return (x && y && z);
        }

        /// <summary>
        ///     Checks whether a point is in the bounding box.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool Inside(Vector3 point)
        {
            return (point.X >= PMin.X && point.X <= PMax.X &&
                    point.Y >= PMin.Y && point.Y <= PMax.Y &&
                    point.Z >= PMin.Z && point.Z <= PMin.Z);
        }

        /// <summary>
        ///     Expand a buonding box by the given delta on both sides of the bounding box.
        /// </summary>
        /// <param name="delta"></param>
        public BBox Expand(float delta)
        {
            PMin -= new Vector3(delta);
            PMax += new Vector3(delta);
            return this;
        }

        /// <summary>
        ///     Returns the total area of the bounding box.
        /// </summary>
        /// <returns></returns>
        public float SurfaceArea()
        {
            var delta = PMax - PMin;
            return 2*(delta.X*delta.Y + delta.X*delta.Z + delta.Y*delta.Z);
        }

        /// <summary>
        ///     Returns the total volume of the bounding box
        /// </summary>
        /// <returns></returns>
        public float Volume()
        {
            var delta = PMax - PMin;
            return delta.X*delta.Y*delta.Z;
        }

        /// <summary>
        ///     Returns the longest axes of the BBox.
        ///     Returns:
        ///     * 0 if axis X
        ///     * 1 if axis Y
        ///     * 2 if axis Z
        /// </summary>
        /// <returns></returns>
        public int MaximumExtent()
        {
            var delta = PMax - PMin;
            if (delta.X > delta.Y && delta.X > delta.Z)
                return 0;
            if (delta.Y > delta.Z)
                return 1;
            return 2;
        }

        public bool IntersectP(Ray ray, ref float hitt0, ref float hitt1)
        {
            float t0 = ray.Start, t1 = ray.End;
            float invRayDir, tNear, tFar;

            invRayDir = 1/ray.Direction.X;
            tNear = (PMin.X - ray.Origin.X)*invRayDir;
            tFar = (PMax.X - ray.Origin.X)*invRayDir;
            if (tNear > tFar)
            {
                var tmp = tNear;
                tNear = tFar;
                tFar = tmp;
            }
            t0 = tNear > t0 ? tNear : t0;
            t1 = tFar < t1 ? tFar : t1;
            if (t0 > t1) return false;

            invRayDir = 1 / ray.Direction.Y;
            tNear = (PMin.Y - ray.Origin.Y) * invRayDir;
            tFar = (PMax.Y - ray.Origin.Y) * invRayDir;
            if (tNear > tFar)
            {
                var tmp = tNear;
                tNear = tFar;
                tFar = tmp;
            }
            t0 = tNear > t0 ? tNear : t0;
            t1 = tFar < t1 ? tFar : t1;
            if (t0 > t1) return false;

            invRayDir = 1 / ray.Direction.Z;
            tNear = (PMin.Z - ray.Origin.Z) * invRayDir;
            tFar = (PMax.Z - ray.Origin.Z) * invRayDir;
            if (tNear > tFar)
            {
                var tmp = tNear;
                tNear = tFar;
                tFar = tmp;
            }
            t0 = tNear > t0 ? tNear : t0;
            t1 = tFar < t1 ? tFar : t1;
            if (t0 > t1) return false;

            hitt0 = t0;
            hitt1 = t1;
            return true;
        }
    }
}
