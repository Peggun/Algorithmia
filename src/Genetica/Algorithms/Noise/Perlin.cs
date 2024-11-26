namespace Genetica
{
    public class PerlinNoise : BaseNoiseGenerator
    {
        public PerlinNoise() => Noise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
        public override float GetNoise(float x, float y) => Noise.GetNoise(x, y);
        public override float GetNoise(float x, float y, float z) => Noise.GetNoise(x, y, z);
    }
}
