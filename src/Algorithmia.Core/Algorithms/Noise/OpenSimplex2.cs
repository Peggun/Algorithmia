using Algorithmia.Enums;
using SkiaSharp;

namespace Algorithmia.Noise
{
    public class OpenSimplex2Noise : BaseNoiseGenerator
    {
        public OpenSimplex2Noise() => Noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
        public override float GetNoise(float x, float y) => Noise.GetNoise(x, y);
        public override float GetNoise(float x, float y, float z) => Noise.GetNoise(x, y, z);
    }
}
