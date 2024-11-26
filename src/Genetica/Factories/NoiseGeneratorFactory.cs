using Genetica;

namespace Genetica;

/// <summary>
/// Factory for creating noise generator instances based on the noise type.
/// </summary>
public static class NoiseGeneratorFactory
{
    /// <summary>
    /// Create a new type of noise.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static INoiseGenerator CreateNoiseGenerator(NoiseType type) =>
        type switch
        {
            NoiseType.Perlin => new PerlinNoise(),
            NoiseType.OpenSimplex2 => new OpenSimplex2Noise(),
            NoiseType.OpenSimplex2S => new OpenSimplex2SNoise(),
            NoiseType.Cellular => new CellularNoise(),
            NoiseType.Value => new ValueNoise(),
            NoiseType.ValueCubic => new ValueCubicNoise(),
            NoiseType.White => new WhiteNoise(),
            _ => throw new ArgumentException($"Unsupported noise type: {type}", nameof(type))
        };
}
