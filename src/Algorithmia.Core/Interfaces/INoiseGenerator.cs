using FNLfloat = System.Single;
using Algorithmia.Enums;

namespace Algorithmia.Interfaces
{
    /// <summary>
    /// Interface for all noise generators. Encapsulates common operations for noise generation.
    /// </summary>
    public interface INoiseGenerator
    {
        void SetSeed(int seed);
        void SetFrequency(float frequency);
        void SetRotationType3D(RotationType3D rotationType3D);
        void SetFractalType(FractalType fractalType);
        void SetFractalOctaves(int octaves);
        void SetFractalLacunarity(float lacunarity);
        void SetFractalGain(float gain);
        void SetFractalWeightedStrength(float weightedStrength);
        void SetFractalPingPongStrength(float pingPongStrength);
        void SetCellularDistanceFunction(CellularDistanceFunction cellularDistanceFunction);
        void SetCellularReturnType(CellularReturnType cellularReturnType);
        void SetCellularJitter(float cellularJitter);
        void SetDomainWarpType(DomainWarpType domainWarpType);
        void SetDomainWarpAmp(float domainWarpAmp);

        float GetNoise(FNLfloat x, FNLfloat y);
        float GetNoise(FNLfloat x, FNLfloat y, FNLfloat z);

        void DomainWarp(ref FNLfloat x, ref FNLfloat y);
        void DomainWarp(ref FNLfloat x, ref FNLfloat y, ref FNLfloat z);

        void SetOutputFileType(FileTypes type);
        void SaveToFile(string filePath, int width, int height, float scale, int quality);
        float[,] GenerateNoiseMap(int width, int height, float scale);
    }
}
