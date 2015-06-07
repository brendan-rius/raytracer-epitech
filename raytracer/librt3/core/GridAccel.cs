using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;

namespace raytracer.core
{
    /// <summary>
    ///     Stupid Simple Accelerator.
    /// </summary>
    public class GridAccel : Aggregate
    {
        private readonly BBox _bbox;
        private readonly int[] _cmpToAxis = {2, 1, 2, 1, 2, 2, 0, 0};
        private readonly float[] _invWidth;
        private readonly int[] _nVoxels;
        private readonly List<Primitive> _primitives;
        private readonly Voxel[] _voxels;
        private readonly float[] _width;

        /// <summary>
        ///     Creates a GridAccel
        /// </summary>
        /// <param name="p"></param>
        public GridAccel(IEnumerable<Primitive> p)
        {
            _nVoxels = new int[3];
            _width = new float[3];
            _invWidth = new float[3];
            _primitives = new List<Primitive>();

            foreach (var primitive in p)
            {
                if (primitive.CanIntersect() == false)
                    primitive.Refine(_primitives);
                else
                    _primitives.Add(primitive);
            }

            _bbox = _primitives.ElementAt(0).WorldBound();
            for (var i = 1; i < _primitives.Count; ++i)
            {
                _bbox = BBox.Union(_bbox, _primitives[i].WorldBound());
            }

            Dimensions(_primitives.Count);

            var nv = _nVoxels[0]*_nVoxels[1]*_nVoxels[2];
            _voxels = new Voxel[nv];
            foreach (var primitive in _primitives)
            {
                var pb = primitive.WorldBound();
                var vmin = new int[3];
                var vmax = new int[3];
                for (var axis = 0; axis < 3; axis++)
                {
                    vmin[axis] = PosToVoxel(pb.PMin, axis);
                    vmax[axis] = PosToVoxel(pb.PMax, axis);
                }
                for (var z = vmin[2]; z <= vmax[2]; ++z)
                {
                    for (var y = vmin[1]; y <= vmax[1]; ++y)
                    {
                        for (var x = vmin[0]; x <= vmax[0]; ++x)
                        {
                            var o = Offset(x, y, z);
                            if (_voxels[o] == null)
                                _voxels[o] = new Voxel(primitive);
                            else
                                _voxels[o].AddPrimitive(primitive);
                        }
                    }
                }
            }
        }

        private void Dimensions(int pCount)
        {
            var delta = _bbox.PMax - _bbox.PMin;
            var maxAxis = _bbox.MaximumExtent();
            var deltaAxis = delta[maxAxis];
            var invMaxWidth = 1f/deltaAxis;
            var cubeRoot = 3f*(float) Math.Pow(pCount, 1f/3);
            var voxelsPerUnitDist = cubeRoot*invMaxWidth;

            for (var i = 0; i < 3; i++)
            {
                _nVoxels[i] = (int) Math.Round(delta[i]*voxelsPerUnitDist);
                _nVoxels[i] = Clamp(_nVoxels[i], 1, 64);
            }

            for (var i = 0; i < 3; i++)
            {
                _width[i] = delta[i]/_nVoxels[i];
                _invWidth[i] = (_width[i] == 0) ? 0 : (1/_width[i]);
            }
        }

        public override bool Intersect(Ray ray)
        {
            float rayT = 0, t = 0;
            if (_bbox.Inside(ray.PointAtTime(ray.Start)))
                rayT = ray.Start;
            else if (!_bbox.IntersectP(ray, ref rayT, ref t))
                return false;
            var gridIntersect = ray.PointAtTime(rayT);
            float[] nextCrossingT = new float[3], deltaT = new float[3];
            int[] step = new int[3], _out = new int[3], pos = new int[3];

            for (var i = 0; i < 3; i++)
            {
                pos[i] = PosToVoxel(gridIntersect, i);
                if (ray.Direction[i] >= 0)
                {
                    nextCrossingT[i] = rayT + (VoxelToPos(pos[i] + 1, i) - gridIntersect[i])/ray.Direction[i];
                    deltaT[i] = _width[i]/ray.Direction[i];
                    step[i] = 1;
                    _out[i] = _nVoxels[i];
                }
                else
                {
                    nextCrossingT[i] = rayT + (VoxelToPos(pos[i], i) - gridIntersect[i])/ray.Direction[i];
                    deltaT[i] = -_width[i]/ray.Direction[i];
                    step[i] = -1;
                    _out[i] = -1;
                }
            }

            for (;;)
            {
                var voxel = _voxels[Offset(pos[0], pos[1], pos[2])];
                if (voxel != null)
                {
                    if (voxel.Intersect(ray))
                        return true;
                }
                var bits = (((nextCrossingT[0] < nextCrossingT[1]) ? 1 : 0) << 2) +
                           (((nextCrossingT[0] < nextCrossingT[2]) ? 1 : 0) << 1) +
                           (((nextCrossingT[1] < nextCrossingT[2]) ? 1 : 0));
                var stepAxis = _cmpToAxis[bits];
                if (ray.End < nextCrossingT[stepAxis])
                    break;
                pos[stepAxis] += step[stepAxis];
                if (pos[stepAxis] == _out[stepAxis])
                    break;
                nextCrossingT[stepAxis] += deltaT[stepAxis];
            }
            return false;
        }

        public override bool TryToIntersect(Ray ray, ref Intersection intersection)
        {
            float rayT = 0, t = 0;
            if (_bbox.Inside(ray.PointAtTime(ray.Start)))
                rayT = ray.Start;
            else if (!_bbox.IntersectP(ray, ref rayT, ref t))
                return false;
            var gridIntersect = ray.PointAtTime(rayT);
            float[] nextCrossingT = new float[3], deltaT = new float[3];
            int[] step = new int[3], _out = new int[3], pos = new int[3];

            for (var i = 0; i < 3; i++)
            {
                pos[i] = PosToVoxel(gridIntersect, i);
                if (ray.Direction[i] >= 0)
                {
                    nextCrossingT[i] = rayT + (VoxelToPos(pos[i] + 1, i) - gridIntersect[i])/ray.Direction[i];
                    deltaT[i] = _width[i]/ray.Direction[i];
                    step[i] = 1;
                    _out[i] = _nVoxels[i];
                }
                else
                {
                    nextCrossingT[i] = rayT + (VoxelToPos(pos[i], i) - gridIntersect[i])/ray.Direction[i];
                    deltaT[i] = -_width[i]/ray.Direction[i];
                    step[i] = -1;
                    _out[i] = -1;
                }
            }

            var hitSomething = false;
            for (;;)
            {
                var voxel = _voxels[Offset(pos[0], pos[1], pos[2])];
                if (voxel != null)
                {
                    hitSomething |= voxel.TryToIntersect(ray, ref intersection);
                }
                var bits = (((nextCrossingT[0] < nextCrossingT[1]) ? 1 : 0) << 2) +
                           (((nextCrossingT[0] < nextCrossingT[2]) ? 1 : 0) << 1) +
                           (((nextCrossingT[1] < nextCrossingT[2]) ? 1 : 0));
                var stepAxis = _cmpToAxis[bits];
                if (ray.End < nextCrossingT[stepAxis])
                    break;
                pos[stepAxis] += step[stepAxis];
                if (pos[stepAxis] == _out[stepAxis])
                    break;
                nextCrossingT[stepAxis] += deltaT[stepAxis];
            }
            return hitSomething;
        }

        private int Offset(int x, int y, int z)
        {
            return z*_nVoxels[0]*_nVoxels[1] + y*_nVoxels[0] + x;
        }

        private int PosToVoxel(Vector3 point, int axis)
        {
            var v = (int) ((point[axis] - _bbox.PMin[axis])*_invWidth[axis]);
            return Clamp(v, 0, _nVoxels[axis] - 1);
        }

        private float VoxelToPos(int point, int axis)
        {
            return _bbox.PMin[axis] + point*_width[axis];
        }

        private static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : ((value > max) ? max : value);
        }
    }

    internal class Voxel
    {
        private readonly List<Primitive> _primitives;

        public Voxel(Primitive p)
        {
            _primitives = new List<Primitive>();
            _primitives.Add(p);
        }

        public void AddPrimitive(Primitive p)
        {
            _primitives.Add(p);
        }

        public bool Intersect(Ray ray)
        {
            return _primitives.Any(primitive => primitive.Intersect(ray));
        }

        public bool TryToIntersect(Ray ray, ref Intersection intersection)
        {
            var hitSomething = false;
            var tmp = new Intersection();
            foreach (var primitive in _primitives)
            {
                if (!primitive.TryToIntersect(ray, ref tmp)) continue;
                hitSomething = true;
                if (tmp.Distance < intersection.Distance)
                    intersection = tmp;
            }
            return hitSomething;
        }
    }
}