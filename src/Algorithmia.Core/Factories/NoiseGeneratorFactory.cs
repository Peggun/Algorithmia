using Algorithmia.Interfaces;
using Algorithmia.Noise;
using Algorithmia.Enums;

namespace Algorithmia.Factories
{
    /// <summary>
    /// Factory for creating noise generator instances based on the noise type.
    /// </summary>
    public static class NoiseGeneratorFactory
    {
        public static INoiseGenerator CreateNoiseGenerator(NoiseType type) =>
            type switch
            {
                NoiseType.Perlin => new PerlinNoise(),
                NoiseType.OpenSimplex2 => new OpenSimplex2Noise(),
                NoiseType.OpenSimplex2S => new OpenSimplex2SNoise(),
                NoiseType.Cellular => new CellularNoise(),
                NoiseType.Value => new ValueNoise(),
                NoiseType.ValueCubic => new ValueCubicNoise(),
                _ => throw new ArgumentException($"Unsupported noise type: {type}", nameof(type))
            };
    }
}
