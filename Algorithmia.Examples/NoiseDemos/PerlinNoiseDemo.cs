using Algorithmia.Factories;
using Algorithmia.Enums;
using SkiaSharp;

namespace Algorithmia.Examples.NoiseDemos.Perlin
{
    /// <summary>
    /// Demonstrates Perlin noise generation and rendering to a PNG file.
    /// </summary>
    public class PerlinNoiseDemo
    {
        private const int Width = 512;
        private const int Height = 512;
        private const float Scale = 0.01f;
        private const int Quality = 100;

        public static void Run()
        {
            try
            {
                using SKBitmap bitmap = new SKBitmap(Width, Height);
                using SKCanvas canvas = new SKCanvas(bitmap);

                // Create a Perlin noise generator
                var perlinNoise = NoiseGeneratorFactory.CreateNoiseGenerator(FastNoiseLite.NoiseType.Perlin);
                perlinNoise.SetSeed(1337);
                perlinNoise.SetFrequency(0.1f);
                perlinNoise.SetOutputFileType(FileTypes.JPEG);

                // Generate and render noise
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        float noiseValue = perlinNoise.GetNoise(x * Scale, y * Scale);
                        int intensity = Math.Clamp((int)((noiseValue + 1) * 127.5f), 0, 255);
                        bitmap.SetPixel(x, y, new SKColor((byte)intensity, (byte)intensity, (byte)intensity));
                    }
                }

                // Save the image
                perlinNoise.SaveToFile("PerlinNoise.jpeg", Width, Height, Scale, Quality);

                Console.WriteLine("Noise map saved as PerlinNoise.jpeg");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating noise: {ex.Message}");
            }
        }
    }
}
