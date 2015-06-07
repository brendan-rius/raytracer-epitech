namespace librt3.core
{
    internal abstract class Texture<T>
    {
        public abstract T Evaluate(DifferentialGeometry dg);
    }
}