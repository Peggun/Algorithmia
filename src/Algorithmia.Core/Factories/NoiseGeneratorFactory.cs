using Algorithmia.Interfaces;
using Algorithmia.Noise;

namespace Algorithmia.Factories
{
    /// <summary>
    /// Factory for creating noise generator instances based on the noise type.
    /// </summary>
    public static class NoiseGeneratorFactory
    {
        public static INoiseGenerator CreateNoiseGenerator(FastNoiseLite.NoiseType type) =>
            type switch
            {
                FastNoiseLite.NoiseType.Perlin => new PerlinNoise(),
                FastNoiseLite.NoiseType.OpenSimplex2 => new OpenSimplex2Noise(),
                FastNoiseLite.NoiseType.OpenSimplex2S => new OpenSimplex2SNoise(),
                _ => throw new ArgumentException($"Unsupported noise type: {type}", nameof(type))
            };
    }
}
