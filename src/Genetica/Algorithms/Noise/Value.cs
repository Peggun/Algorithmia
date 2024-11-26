namespace Genetica
{
    public class ValueNoise : BaseNoiseGenerator
    {
        public ValueNoise() => Noise.SetNoiseType(FastNoiseLite.NoiseType.Value);
        public override float GetNoise(float x, float y) => Noise.GetNoise(x, y);
        public override float GetNoise(float x, float y, float z) => Noise.GetNoise(x, y, z);
    }
}
