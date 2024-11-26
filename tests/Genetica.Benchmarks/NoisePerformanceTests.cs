using Genetica;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Genetica.Benchmarks
{
    [MemoryDiagnoser] // Adds memory diagnostics to the benchmark results
    public class NoisePerformanceTests
    {
        private INoiseGenerator _noiseGenerator;

        [GlobalSetup]
        public void Setup()
        {
            // Initialize the noise generator once for all benchmarks
            _noiseGenerator = NoiseGeneratorFactory.CreateNoiseGenerator(NoiseType.Perlin);
        }

        [Params(100, 500, 1000, 5000, 10000)] // Define different world widths to test
        public int Width { get; set; }

        [Params(100, 500, 1000, 5000, 10000)] // Define different world heights to test
        public int Height { get; set; }

        [Params(0.5f, 1.0f, 2.0f)] // Define different scale values to test
        public float Scale { get; set; }

        [Benchmark]
        public void GenerateNoiseBenchmark()
        {
            // Run the method for the current combination of parameters
            _noiseGenerator.GenerateNoiseMap(Width, Height, Scale);
        }
    }
}

