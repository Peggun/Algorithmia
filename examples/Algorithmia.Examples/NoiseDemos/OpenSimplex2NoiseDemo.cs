using Algorithmia.Enums;
using Algorithmia.Factories;
using Algorithmia.Interfaces;
using Algorithmia.Sinks;

namespace Algorithmia.Examples.NoiseDemos
{
    /// <summary>
    /// Demonstrates Open Simplex 2 noise generation and rendering to a image file.
    /// </summary>
    public class OpenSimplex2NoiseDemo
    {
        private const int Width = 16;
        private const int Height = 16;
        private const float Scale = 0.01f;

        public static void Run()
        {
            try
            {
                var openSimplex2 = NoiseGeneratorFactory.CreateNoiseGenerator(NoiseType.OpenSimplex2);
                openSimplex2.SetSeed(1337);
                openSimplex2.SetFrequency(0.1f);

                // Generate Noise Map
                float[,] noise = openSimplex2.GenerateNoiseMap(Width, Height, Scale);

                var sinks = new ISink[]
                {
                    new ConsoleSink(),                // Output to console
                    new ImageSink(FileTypes.PNG),     // Output to a PNG image
                    new ImageSink(FileTypes.JPEG),    // Output to a JPEG image
                    new TextFileSink(),               // Output to an text file
                    new DebugSink(),                  // Output to debug console
                };

                // Use each sink
                foreach (var sink in sinks)
                {
                    sink.Write(noise);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating noise: {ex.Message}");
            }
        }
    }
}
