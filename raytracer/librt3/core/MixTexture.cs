namespace librt3.core
{
    internal class MixTexture<T> : Texture<T>
    {
        private readonly Texture<float> _amount;
        private readonly Texture<T> _tex1;
        private readonly Texture<T> _tex2;

        public MixTexture(Texture<T> t1, Texture<T> t2, Texture<float> amt)
        {
            _tex1 = t1;
            _tex2 = t2;
            _amount = amt;
        }

        public override T Evaluate(DifferentialGeometry dg)
        {
            T t1 = _tex1.Evaluate(dg), t2 = _tex2.Evaluate(dg);
            var amt = _amount.Evaluate(dg);
            return Operators.Add(Operators.Multiply(t1, (1f - amt)), Operators.Multiply(t2, amt));
        }
    }
}