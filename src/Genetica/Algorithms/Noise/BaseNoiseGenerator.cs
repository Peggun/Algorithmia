using static FastNoiseLite;
using FNLfloat = System.Single;

using Genetica;

namespace Genetica
{
    /// <summary>
    /// Base class for noise generation. Implements common FastNoiseLite functionality and other functions required for custom noises
    /// </summary>
    public abstract class BaseNoiseGenerator : INoiseGenerator
    {
        // These are for the non-FastNoiseLite noise functions
        protected int _seed { get; set; } = 12345;


        protected readonly FastNoiseLite Noise;
        protected FileTypes OutputFileType { get; private set; } = FileTypes.PNG;

        protected BaseNoiseGenerator() => Noise = new FastNoiseLite();

        public virtual void SetFrequency(float frequency) => Noise.SetFrequency(frequency);
        public virtual void SetSeed(int seed)
        {
            Noise.SetSeed(seed);
            _seed = seed;
        }

        public virtual void SetRotationType3D(RotationType3D rotationType3D) => Noise.SetRotationType3D(MapToFastNoiseLite(rotationType3D));
        public virtual void SetFractalType(FractalType fractalType) => Noise.SetFractalType(MapToFastNoiseLite(fractalType));
        public virtual void SetFractalOctaves(int octaves) => Noise.SetFractalOctaves(octaves);
        public virtual void SetFractalLacunarity(float lacunarity) => Noise.SetFractalLacunarity(lacunarity);
        public virtual void SetFractalGain(float gain) => Noise.SetFractalGain(gain);
        public virtual void SetFractalWeightedStrength(float weightedStrength) => Noise.SetFractalWeightedStrength(weightedStrength);
        public virtual void SetFractalPingPongStrength(float pingPongStrength) => Noise.SetFractalPingPongStrength(pingPongStrength);
        public virtual void SetCellularDistanceFunction(CellularDistanceFunction cellularDistanceFunction) => Noise.SetCellularDistanceFunction(MapToFastNoiseLite(cellularDistanceFunction));
        public virtual void SetCellularReturnType(CellularReturnType cellularReturnType) => Noise.SetCellularReturnType(MapToFastNoiseLite(cellularReturnType));
        public virtual void SetCellularJitter(float cellularJitter) => Noise.SetCellularJitter(cellularJitter);
        public virtual void SetDomainWarpType(DomainWarpType domainWarpType) => Noise.SetDomainWarpType(MapToFastNoiseLite(domainWarpType));
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

        // RotationType3D Mapping
        public static FastNoiseLite.RotationType3D MapToFastNoiseLite(RotationType3D rotationType)
        {
            return rotationType switch
            {
                RotationType3D.None => FastNoiseLite.RotationType3D.None,
                RotationType3D.ImproveXYPlanes => FastNoiseLite.RotationType3D.ImproveXYPlanes,
                RotationType3D.ImproveXZPlanes => FastNoiseLite.RotationType3D.ImproveXZPlanes,
                _ => throw new ArgumentOutOfRangeException(nameof(rotationType), rotationType, null)
            };
        }

        // FractalType Mapping
        public static FastNoiseLite.FractalType MapToFastNoiseLite(FractalType fractalType)
        {
            return fractalType switch
            {
                FractalType.None => FastNoiseLite.FractalType.None,
                FractalType.FBm => FastNoiseLite.FractalType.FBm,
                FractalType.Ridged => FastNoiseLite.FractalType.Ridged,
                FractalType.PingPong => FastNoiseLite.FractalType.PingPong,
                FractalType.DomainWarpProgressive => FastNoiseLite.FractalType.DomainWarpProgressive,
                FractalType.DomainWarpIndependent => FastNoiseLite.FractalType.DomainWarpIndependent,
                _ => throw new ArgumentOutOfRangeException(nameof(fractalType), fractalType, null)
            };
        }

        // CellularDistanceFunction Mapping
        public static FastNoiseLite.CellularDistanceFunction MapToFastNoiseLite(CellularDistanceFunction distanceFunction)
        {
            return distanceFunction switch
            {
                CellularDistanceFunction.Euclidean => FastNoiseLite.CellularDistanceFunction.Euclidean,
                CellularDistanceFunction.EuclideanSq => FastNoiseLite.CellularDistanceFunction.EuclideanSq,
                CellularDistanceFunction.Manhattan => FastNoiseLite.CellularDistanceFunction.Manhattan,
                CellularDistanceFunction.Hybrid => FastNoiseLite.CellularDistanceFunction.Hybrid,
                _ => throw new ArgumentOutOfRangeException(nameof(distanceFunction), distanceFunction, null)
            };
        }

        // CellularReturnType Mapping
        public static FastNoiseLite.CellularReturnType MapToFastNoiseLite(CellularReturnType returnType)
        {
            return returnType switch
            {
                CellularReturnType.CellValue => FastNoiseLite.CellularReturnType.CellValue,
                CellularReturnType.Distance => FastNoiseLite.CellularReturnType.Distance,
                CellularReturnType.Distance2 => FastNoiseLite.CellularReturnType.Distance2,
                CellularReturnType.Distance2Add => FastNoiseLite.CellularReturnType.Distance2Add,
                CellularReturnType.Distance2Sub => FastNoiseLite.CellularReturnType.Distance2Sub,
                CellularReturnType.Distance2Mul => FastNoiseLite.CellularReturnType.Distance2Mul,
                CellularReturnType.Distance2Div => FastNoiseLite.CellularReturnType.Distance2Div,
                _ => throw new ArgumentOutOfRangeException(nameof(returnType), returnType, null)
            };
        }

        // DomainWarpType Mapping
        public static FastNoiseLite.DomainWarpType MapToFastNoiseLite(DomainWarpType warpType)
        {
            return warpType switch
            {
                DomainWarpType.OpenSimplex2 => FastNoiseLite.DomainWarpType.OpenSimplex2,
                DomainWarpType.OpenSimplex2Reduced => FastNoiseLite.DomainWarpType.OpenSimplex2Reduced,
                DomainWarpType.BasicGrid => FastNoiseLite.DomainWarpType.BasicGrid,
                _ => throw new ArgumentOutOfRangeException(nameof(warpType), warpType, null)
            };
        }
    }
}
