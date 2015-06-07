namespace librt3.core
{
    public abstract class TextureMapping2D
    {
        public abstract void Map(DifferentialGeometry dg, ref float s, ref float t, ref float dsdx, ref float dtdx,
            ref float dsdy, ref float dtdy);
    }
}