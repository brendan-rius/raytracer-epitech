using OpenTK;
using raytracer.core;

namespace librt3.core
{
    public class RayDifferential : Ray
    {
        public bool HasDifferentials;

        public Vector3 RxOrigin;

        public Vector3 RyOrigin;

        public Vector3 RxDirection;
        
        public Vector3 RyDirection;

        public RayDifferential(Vector3 direction, Vector3 origin, float start = DefaultStartValue, float end = DefaultEndValue, uint depth = 0) : base(direction, origin, start, end, depth)
        {
            HasDifferentials = false;
        }

        private void ScaleDifferentials(float s)
        {
            RxOrigin = Origin + (RxOrigin - Origin)*s;
            RyOrigin = Origin + (RyOrigin - Origin)*s;
            RxDirection = Direction + (RxDirection - Direction)*s;
            RyDirection = Direction + (RyDirection - Direction)*s;
        }
    }
}