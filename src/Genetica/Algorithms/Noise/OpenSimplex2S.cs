namespace Genetica
{
    public class OpenSimplex2SNoise : BaseNoiseGenerator
    {
        public OpenSimplex2SNoise() => Noise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
        public override float GetNoise(float x, float y) => Noise.GetNoise(x, y);
        public override float GetNoise(float x, float y, float z) => Noise.GetNoise(x, y, z);
    }
}
