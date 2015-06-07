using System;
using OpenTK;
using raytracer.core;
using raytracer.core.mathematics;

namespace librt3.core
{
    public class DifferentialGeometry
    {
        public readonly Shape Shape;
        public Vector3 Dndu, Dndv;
        public Vector3 Dpdu, Dpdv;
        public Vector3 Dpdx, Dpdy;
        public float dudx, dvdx, dudy, dvdy;
        public Vector3 Normal;
        public Vector3 Point;
        public float U, V;

        public DifferentialGeometry()
        {
            U = 0;
            V = 0;
            Shape = null;
            dudx = dvdx = dudy = dvdy = 0;
        }

        public DifferentialGeometry(ref Vector3 point,
            ref Vector3 dpdu, ref Vector3 dpdv,
            ref Vector3 dndu, ref Vector3 dndv,
            float uu, float vv, Shape shape)
        {
            Point = point;
            Dpdu = dpdu;
            Dpdv = dpdv;
            Dndu = dndu;
            Dndv = dndv;
            Vector3.Cross(ref dpdu, ref dpdv, out Normal);
            Normal.Normalize();
            U = uu;
            V = vv;
            dudx = dvdx = dudy = dvdy = 0;
            Shape = shape;
            if (shape != null && (shape.ReverseOrientation ^ shape.TransformSwapsHandedness))
                Normal *= -1f;
        }

        public void ComputeDifferentials(RayDifferential ray)
        {
            if (ray.HasDifferentials)
            {
                var d = -Vector3.Dot(Normal, Point);
                var rxv = new Vector3(ray.RxOrigin.X, ray.RxOrigin.Y, ray.RxOrigin.Z);
                var tx = -(Vector3.Dot(Normal, rxv) + d)/Vector3.Dot(Normal, ray.RxDirection);
                var px = ray.RxOrigin + tx*ray.RxDirection;
                var ryv = new Vector3(ray.RyOrigin.X, ray.RyOrigin.Y, ray.RyOrigin.Z);
                var ty = -(Vector3.Dot(Normal, ryv) + d)/Vector3.Dot(Normal, ray.RyDirection);
                var py = ray.RyOrigin + ty*ray.RyDirection;
                Dpdx = px - Point;
                Dpdy = py - Point;
                var a = new float[2, 2];
                float[] bx = new float[2], @by = new float[2];
                var axes = new int[2];
                if (Math.Abs(Normal.X) > Math.Abs(Normal.Y) && Math.Abs(Normal.X) > Math.Abs(Normal.Z))
                {
                    axes[0] = 1;
                    axes[1] = 2;
                }
                else if (Math.Abs(Normal.Y) > Math.Abs(Normal.Z))
                {
                    axes[0] = 0;
                    axes[1] = 2;
                }
                else
                {
                    axes[0] = 0;
                    axes[1] = 1;
                }
                a[0, 0] = Dpdu[axes[0]];
                a[0, 1] = Dpdv[axes[0]];
                a[1, 0] = Dpdu[axes[1]];
                a[1, 1] = Dpdv[axes[1]];
                bx[0] = px[axes[0]] - Point[axes[0]];
                bx[1] = px[axes[1]] - Point[axes[1]];
                @by[0] = py[axes[0]] - Point[axes[0]];
                @by[1] = py[axes[1]] - Point[axes[1]];
                if (!Solver.SolveLinearSystem2X2(a, bx, ref dudx, ref dvdx))
                {
                    dudx = 0;
                    dvdx = 0;
                }
                if (Solver.SolveLinearSystem2X2(a, @by, ref dudy, ref dvdy)) return;
                dudy = 0;
                dvdy = 0;
            }
            else
            {
                dudx = dvdx = 0;
                dudy = dvdy = 0;
                Dpdx = Dpdy = Vector3.Zero;
            }
        }
    }
}