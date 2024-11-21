using System;
using Algoritmia.Noise;

namespace Algoritmia.Examples.NoiseDemos
{
    class NoiseDemo
    {
        public static void Run()
        {
            var noiseGenerator = new NoiseGenerator();

            // Set up noise parameters
            noiseGenerator.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
            noiseGenerator.SetFrequency(0.05f);
            noiseGenerator.SetSeed(42);

            Console.WriteLine("Generating 2D Noise (Simplex)...");
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    float noiseValue = noiseGenerator.GetNormalizedNoise(x, y);
                    Console.Write($"{noiseValue:F2} ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nGenerating 3D Noise...");
            for (int z = 0; z < 5; z++)
            {
                Console.WriteLine($"Layer {z}:");
                for (int y = 0; y < 10; y++)
                {
                    for (int x = 0; x < 10; x++)
                    {
                        float noiseValue = noiseGenerator.GetNormalizedNoise(x, y, z);
                        Console.Write($"{noiseValue:F2} ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }
}
