using Algorithmia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithmia.Tests.UnitTests
{
    public class NoiseMapTests
    {
        [Fact]
        public void GenerateNoiseMap_ScaleAffectsNoise()
        {
            var noiseGenerator = NoiseGeneratorFactory.CreateNoiseGenerator(NoiseType.Perlin);

            int width = 100;
            int height = 50;

            var map1 = noiseGenerator.GenerateNoiseMap(width, height, scale: 1.0f);
            var map2 = noiseGenerator.GenerateNoiseMap(width, height, scale: 0.5f);

            // Assert maps are different for different scales
            bool mapsAreDifferent = false;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (map1[i, j] != map2[i, j])
                    {
                        mapsAreDifferent = true;
                        break;
                    }
                }
                if (mapsAreDifferent)
                    break;
            }

            Assert.True(mapsAreDifferent, "The noise maps should be different with different scales.");
        }

        [Fact]
        public void GenerateNoiseMap_SameSeedProducesSameMap()
        {
            int width = 100;
            int height = 50;
            float scale = 1.0f;
            int seed = 12345;

            var noiseGenerator = NoiseGeneratorFactory.CreateNoiseGenerator(NoiseType.Perlin);
            noiseGenerator.SetSeed(seed);
            var map1 = noiseGenerator.GenerateNoiseMap(width, height, scale);

            var noiseGenerator2 = NoiseGeneratorFactory.CreateNoiseGenerator(NoiseType.Perlin);
            noiseGenerator2.SetSeed(seed);
            var map2 = noiseGenerator2.GenerateNoiseMap(width, height, scale);


            // Compare the values in the maps
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Assert.Equal(map1[i, j], map2[i, j]);
                }
            }
        }

    }
}
