using System;

namespace Algoritmia.Noise
{
    public class NoiseGenerator
    {
        private FastNoiseLite _noise;

        public NoiseGenerator()
        {
            _noise = new FastNoiseLite();
            SetDefaults();
        }

        // Set default noise type and parameters
        private void SetDefaults()
        {
            _noise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
            _noise.SetFrequency(0.01f);
            _noise.SetSeed(Environment.TickCount); // Use system time as the default seed
        }

        // Configure the noise type
        public void SetNoiseType(FastNoiseLite.NoiseType type)
        {
            _noise.SetNoiseType(type);
        }

        // Configure frequency
        public void SetFrequency(float frequency)
        {
            _noise.SetFrequency(frequency);
        }

        // Configure seed
        public void SetSeed(int seed)
        {
            _noise.SetSeed(seed);
        }

        // Configure fractal type (for fractal noise generation)
        public void SetFractalType(FastNoiseLite.FractalType fractalType)
        {
            _noise.SetFractalType(fractalType);
        }

        // Configure fractal octaves
        public void SetFractalOctaves(int octaves)
        {
            _noise.SetFractalOctaves(octaves);
        }

        // Configure fractal gain
        public void SetFractalGain(float gain)
        {
            _noise.SetFractalGain(gain);
        }

        // Generate 2D noise
        public float GetNoise(float x, float y)
        {
            return _noise.GetNoise(x, y);
        }

        // Generate 3D noise
        public float GetNoise(float x, float y, float z)
        {
            return _noise.GetNoise(x, y, z);
        }

        // Utility: Generate scaled noise (0 to 1 instead of -1 to 1)
        public float GetNormalizedNoise(float x, float y)
        {
            return (GetNoise(x, y) + 1) * 0.5f;
        }

        public float GetNormalizedNoise(float x, float y, float z)
        {
            return (GetNoise(x, y, z) + 1) * 0.5f;
        }
    }
}
