namespace librt3.core
{
    internal class ConstantTexture<T> : Texture<T>
    {
        private readonly T _value;

        public ConstantTexture(T v)
        {
            _value = v;
        }

        public override T Evaluate(DifferentialGeometry dg)
        {
            return _value;
        }
    }
}