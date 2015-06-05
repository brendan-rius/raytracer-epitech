using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace raytracer.core
{
    /// <summary>
    /// A bounding box implementation.
    /// </summary>
    public class BBox
    {
        /// <summary>
        /// Mininum point of the bounding box.
        /// </summary>
        public Vector3 PMin;

        /// <summary>
        /// Maximum point of the bounding box.
        /// </summary>
        public Vector3 PMax;

        /// <summary>
        /// Creates a new bounding box.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        public BBox(Vector3 p1, Vector3 p2)
        {
            PMin = new Vector3(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Min(p1.Z, p2.Z));
            PMax = new Vector3(Math.Max(p1.X, p2.X), Math.Max(p1.Y, p2.Y), Math.Max(p1.Z, p2.Z));
        }

        /// <summary>
        /// Adds a point to the bounding box, recalculating it.
        /// </summary>
        /// <param name="box"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static BBox Union(BBox box, Vector3 point)
        {
            BBox ret = box;
            ret.PMin.X = Math.Min(box.PMin.X, point.X);
            ret.PMin.Y = Math.Min(box.PMin.Y, point.Y);
            ret.PMin.Z = Math.Min(box.PMin.Z, point.Z);
            ret.PMax.X = Math.Max(box.PMax.X, point.X);
            ret.PMax.Y = Math.Max(box.PMax.Y, point.Y);
            ret.PMax.Z = Math.Max(box.PMax.Z, point.Z);
            return ret;
        }

        /// <summary>
        /// Checks whether the given bounding box overlaps with this one.
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        public bool Overlaps(BBox box)
        {
            bool x = (PMax.X >= box.PMin.X) && (PMin.X <= box.PMax.X);
            bool y = (PMax.Y >= box.PMin.Y) && (PMin.Y <= box.PMax.Y);
            bool z = (PMax.Z >= box.PMin.Z) && (PMin.Z <= box.PMax.Z);
            return (x && y && z);
        }

        /// <summary>
        /// Checks whether a point is in the bounding box.
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
        /// Expand a buonding box by the given delta on both sides of the bounding box.
        /// </summary>
        /// <param name="delta"></param>
        public void Expand(float delta)
        {
            PMin -= new Vector3(delta);
            PMax += new Vector3(delta);
        }

        /// <summary>
        /// Returns the total area of the bounding box.
        /// </summary>
        /// <returns></returns>
        public float SurfaceArea()
        {
            Vector3 delta = PMax - PMin;
            return 2 * (delta.X * delta.Y + delta.X * delta.Z + delta.Y * delta.Z);
        }

        /// <summary>
        /// Returns the total volume of the bounding box
        /// </summary>
        /// <returns></returns>
        public float Volume()
        {
            Vector3 delta = PMax - PMin;
            return delta.X * delta.Y * delta.Z;
        }

        /// <summary>
        /// Returns the longest axes of the BBox.
        /// Returns:
        ///   * 0 if axis X
        ///   * 1 if axis Y
        ///   * 2 if axis Z
        /// </summary>
        /// <returns></returns>
        public int MaximumExtent()
        {
            Vector3 delta = PMax - PMin;
            if (delta.X > delta.Y && delta.X > delta.Z)
                return 0;
            else if (delta.Y > delta.Z)
                return 1;
            else
                return 2;
        }
    }
}
