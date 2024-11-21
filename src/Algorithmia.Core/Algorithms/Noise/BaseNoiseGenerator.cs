using Algorithmia.Enums;
using Algorithmia.Interfaces;
using FNLfloat = System.Single;

namespace Algorithmia.Noise
{
    /// <summary>
    /// Base class for noise generation. Implements common FastNoiseLite functionality.
    /// </summary>
    public abstract class BaseNoiseGenerator : INoiseGenerator
    {
        protected readonly FastNoiseLite Noise;
        protected FileTypes OutputFileType { get; private set; } = FileTypes.PNG;

        protected BaseNoiseGenerator() => Noise = new FastNoiseLite();

        public virtual void SetFrequency(float frequency) => Noise.SetFrequency(frequency);
        public virtual void SetSeed(int seed) => Noise.SetSeed(seed);
        public virtual void SetRotationType3D(FastNoiseLite.RotationType3D rotationType3D) => Noise.SetRotationType3D(rotationType3D);
        public virtual void SetFractalType(FastNoiseLite.FractalType fractalType) => Noise.SetFractalType(fractalType);
        public virtual void SetFractalOctaves(int octaves) => Noise.SetFractalOctaves(octaves);
        public virtual void SetFractalLacunarity(float lacunarity) => Noise.SetFractalLacunarity(lacunarity);
        public virtual void SetFractalGain(float gain) => Noise.SetFractalGain(gain);
        public virtual void SetFractalWeightedStrength(float weightedStrength) => Noise.SetFractalWeightedStrength(weightedStrength);
        public virtual void SetFractalPingPongStrength(float pingPongStrength) => Noise.SetFractalPingPongStrength(pingPongStrength);
        public virtual void SetCellularDistanceFunction(FastNoiseLite.CellularDistanceFunction cellularDistanceFunction) => Noise.SetCellularDistanceFunction(cellularDistanceFunction);
        public virtual void SetCellularReturnType(FastNoiseLite.CellularReturnType cellularReturnType) => Noise.SetCellularReturnType(cellularReturnType);
        public virtual void SetCellularJitter(float cellularJitter) => Noise.SetCellularJitter(cellularJitter);
        public virtual void SetDomainWarpType(FastNoiseLite.DomainWarpType domainWarpType) => Noise.SetDomainWarpType(domainWarpType);
        public virtual void SetDomainWarpAmp(float domainWarpAmp) => Noise.SetDomainWarpAmp(domainWarpAmp);

        public abstract float GetNoise(float x, float y);
        public abstract float GetNoise(float x, float y, float z);

        public virtual void DomainWarp(ref FNLfloat x, ref FNLfloat y) => Noise.DomainWarp(ref x, ref y);
        public virtual void DomainWarp(ref FNLfloat x, ref FNLfloat y, ref FNLfloat z) => Noise.DomainWarp(ref x, ref y, ref z);

        public virtual void SetOutputFileType(FileTypes type) => OutputFileType = type;
        public virtual void SaveToFile(string filePath, int width, int height, float scale, int quality)
        {
            // To be overridden in specific noise generator implementations
            throw new NotImplementedException("SaveToFile is not implemented for this noise generator as this is the BaseNoiseGenerator.\nThis is to be overidden by the Noise Types.");
        }

        public virtual float[,] GenerateNoiseMap(int width, int height, float scale)
        {
            var noiseMap = new float[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    noiseMap[y, x] = GetNoise(x * scale, y * scale);
                }
            }
            return noiseMap;
        }
    }
}
