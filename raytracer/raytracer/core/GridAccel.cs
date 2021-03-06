﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using OpenTK;

namespace raytracer.core
{
    /// <summary>
    /// Stupid Simple Accelerator.
    /// </summary>
    public class GridAccel : Aggregate
    {
        private readonly BBox _bbox;

        private readonly List<Primitive> _primitives;

        private readonly int[] _nVoxels;
        private readonly float[] _width;
        private readonly float[] _invWidth;
        private readonly Voxel[] _voxels;

        private readonly int[] _cmpToAxis = { 2, 1, 2, 1, 2, 2, 0, 0 };
        
        /// <summary>
        /// Creates a GridAccel
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
            var deltaAxis = maxAxis == 0 ? delta.X : (maxAxis == 1 ? delta.Y : delta.Z);
            var invMaxWidth = 1f / deltaAxis;
            var cubeRoot = 3f * (float)Math.Pow(pCount, 1f / 3);
            var voxelsPerUnitDist = cubeRoot * invMaxWidth;

            _nVoxels[0] = (int)Math.Round(delta.X * voxelsPerUnitDist);
            _nVoxels[0] = Clamp(_nVoxels[0], 1, 64);

            _nVoxels[1] = (int)Math.Round(delta.Y * voxelsPerUnitDist);
            _nVoxels[1] = Clamp(_nVoxels[1], 1, 64);

            _nVoxels[2] = (int)Math.Round(delta.Z * voxelsPerUnitDist);
            _nVoxels[2] = Clamp(_nVoxels[2], 1, 64);

            _width[0] = delta.X / _nVoxels[0];
            _invWidth[0] = (_width[0] == 0) ? 0 : (1 / _width[0]);

            _width[1] = delta.Y / _nVoxels[1];
            _invWidth[1] = (_width[1] == 0) ? 0 : (1 / _width[1]);

            _width[2] = delta.Z / _nVoxels[2];
            _invWidth[2] = (_width[2] == 0) ? 0 : (1 / _width[2]);
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

            pos[0] = PosToVoxel(gridIntersect, 0);
            pos[1] = PosToVoxel(gridIntersect, 1);
            pos[2] = PosToVoxel(gridIntersect, 2);
            if (ray.Direction.X >= 0)
            {
                nextCrossingT[0] = rayT + (VoxelToPos(pos[0] + 1, 0) - gridIntersect.X) / ray.Direction.X;
                deltaT[0] = _width[0] / ray.Direction.X;
                step[0] = 1;
                _out[0] = _nVoxels[0];
            }
            else
            {
                nextCrossingT[0] = rayT + (VoxelToPos(pos[0], 0) - gridIntersect.X) / ray.Direction.X;
                deltaT[0] = -_width[0] / ray.Direction.X;
                step[0] = -1;
                _out[0] = -1;
            }

            if (ray.Direction.Y >= 0)
            {
                nextCrossingT[1] = rayT + (VoxelToPos(pos[1] + 1, 1) - gridIntersect.Y) / ray.Direction.Y;
                deltaT[1] = _width[1] / ray.Direction.Y;
                step[1] = 1;
                _out[1] = _nVoxels[1];
            }
            else
            {
                nextCrossingT[1] = rayT + (VoxelToPos(pos[1], 1) - gridIntersect.Y) / ray.Direction.Y;
                deltaT[1] = -_width[1] / ray.Direction.Y;
                step[1] = -1;
                _out[1] = -1;
            }

            if (ray.Direction.Z >= 0)
            {
                nextCrossingT[2] = rayT + (VoxelToPos(pos[2] + 1, 2) - gridIntersect.Z) / ray.Direction.Z;
                deltaT[2] = _width[2] / ray.Direction.Z;
                step[2] = 1;
                _out[2] = _nVoxels[2];
            }
            else
            {
                nextCrossingT[2] = rayT + (VoxelToPos(pos[2], 2) - gridIntersect.Z) / ray.Direction.Z;
                deltaT[2] = -_width[2] / ray.Direction.Z;
                step[2] = -1;
                _out[2] = -1;
            }

            for (; ; )
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

            pos[0] = PosToVoxel(gridIntersect, 0);
            pos[1] = PosToVoxel(gridIntersect, 1);
            pos[2] = PosToVoxel(gridIntersect, 2);
            if (ray.Direction.X >= 0)
            {
                nextCrossingT[0] = rayT + (VoxelToPos(pos[0] + 1, 0) - gridIntersect.X)/ray.Direction.X;
                deltaT[0] = _width[0]/ray.Direction.X;
                step[0] = 1;
                _out[0] = _nVoxels[0];
            }
            else
            {
                nextCrossingT[0] = rayT + (VoxelToPos(pos[0], 0) - gridIntersect.X)/ray.Direction.X;
                deltaT[0] = -_width[0]/ray.Direction.X;
                step[0] = -1;
                _out[0] = -1;
            }

            if (ray.Direction.Y >= 0)
            {
                nextCrossingT[1] = rayT + (VoxelToPos(pos[1] + 1, 1) - gridIntersect.Y) / ray.Direction.Y;
                deltaT[1] = _width[1] / ray.Direction.Y;
                step[1] = 1;
                _out[1] = _nVoxels[1];
            }
            else
            {
                nextCrossingT[1] = rayT + (VoxelToPos(pos[1], 1) - gridIntersect.Y) / ray.Direction.Y;
                deltaT[1] = -_width[1] / ray.Direction.Y;
                step[1] = -1;
                _out[1] = -1;
            }

            if (ray.Direction.Z >= 0)
            {
                nextCrossingT[2] = rayT + (VoxelToPos(pos[2] + 1, 2) - gridIntersect.Z) / ray.Direction.Z;
                deltaT[2] = _width[2] / ray.Direction.Z;
                step[2] = 1;
                _out[2] = _nVoxels[2];
            }
            else
            {
                nextCrossingT[2] = rayT + (VoxelToPos(pos[2], 2) - gridIntersect.Z) / ray.Direction.Z;
                deltaT[2] = -_width[2] / ray.Direction.Z;
                step[2] = -1;
                _out[2] = -1;
            }

            var hitSomething = false;
            for (; ; )
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
            float axisValue;

            switch (axis)
            {
                case 0:
                    axisValue = point.X - _bbox.PMin.X;
                    break;
                case 1:
                    axisValue = point.Y - _bbox.PMin.Y;
                    break;
                default:
                    axisValue = point.Z - _bbox.PMin.Z;
                    break;
            }
            var v = (int)(axisValue*_invWidth[axis]);
            return Clamp(v, 0, _nVoxels[axis] - 1);
        }

        private float VoxelToPos(int point, int axis)
        {
            switch (axis)
            {
                case 0:
                    return _bbox.PMin.X + point*_width[axis];
                case 1:
                    return _bbox.PMin.Y + point*_width[axis];
                default:
                    return _bbox.PMin.Z + point*_width[axis];
            }
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