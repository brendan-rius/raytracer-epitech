using System;
using OpenTK;

namespace raytracer.core.mathematics
{
    /// <summary>
    ///     The solver class aims to solve simple equations (such as polynoms).
    /// </summary>
    public static class Solver
    {
        public static bool TrySolvePolynomial2(float a, float b, float c, out float firstRoot, out float secondRoot)
        {
            var delta = b*b - 4*a*c;
            if (delta < 0f)
            {
                firstRoot = 0;
                secondRoot = 0;
                return false;
            }
            var deltaroot = (float) Math.Sqrt(delta);
            var q = b < 0 ? -0.5f*(b - deltaroot) : -0.5f*(b + deltaroot);
            firstRoot = q/a;
            secondRoot = c/q;
            if (!(secondRoot < firstRoot)) return true;
            var tmp = firstRoot;
            firstRoot = secondRoot;
            secondRoot = tmp;
            return true;
        }

        public static float Lerp(float t, float start, float end)
        {
            return (1 - t)*start + t*end;
        }

        public static Vector4 Lerp(Vector4 t, float start, float end)
        {
            return (new Vector4(1, 1, 1, 1) - t)*start + t*end;
        }
    }
}