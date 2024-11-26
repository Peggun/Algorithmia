using Algorithmia.Enums;
using Algorithmia.Factories;
using Algorithmia.Interfaces;
using Algoritmia.Noise;

namespace Algorithmia.Tests.UnitTests
{
    public class NoiseGeneratorTests
    {
        [Fact]
        public void GenerateNoiseMap_CorrectSize()
        {
            var noiseGenerator = NoiseGeneratorFactory.CreateNoiseGenerator(NoiseType.Perlin);

            int width = 100;
            int height = 50;
            var map = noiseGenerator.GenerateNoiseMap(width, height, scale: 1.0f);

            // Values are opposite??
            Assert.Equal(height, map.GetLength(0)); // Check rows
            Assert.Equal(width, map.GetLength(1)); // Check columns
        }

        [Fact]
        public void GenerateNoiseMap_ValuesWithinRange()
        {
            var noiseGenerator = NoiseGeneratorFactory.CreateNoiseGenerator(NoiseType.Perlin);

            int width = 100;
            int height = 50;
            var map = noiseGenerator.GenerateNoiseMap(width, height, scale: 1.0f);

            foreach (var value in map)
            {
                Assert.InRange(value, -1f, 1f); // Check if each value is within the range
            }
        }
    }
}
