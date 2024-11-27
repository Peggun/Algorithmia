using SkiaSharp;
using FNLfloat = System.Single;

namespace Genetica
{
    /// <summary>
    /// Interface for all noise generators. Encapsulates common operations for noise generation.
    /// </summary>
    public interface INoiseGenerator
    {
        /// <summary>
        /// Sets seed used for all noise types
        /// </summary>
        /// <remarks>
        /// Default: 1337
        /// </remarks>
        void SetSeed(int seed);

        /// <summary>
        /// Sets frequency for all noise types
        /// </summary>
        /// <remarks>
        /// Default: 0.01
        /// </remarks>
        void SetFrequency(float frequency);

        /// <summary>
        /// Sets domain rotation type for 3D Noise and 3D DomainWarp.
        /// Can aid in reducing directional artifacts when sampling a 2D plane in 3D
        /// </summary>
        /// <remarks>
        /// Default: None
        /// </remarks>
        void SetRotationType3D(RotationType3D rotationType3D);

        /// <summary>
        /// Sets method for combining octaves in all fractal noise types
        /// </summary>
        /// <remarks>
        /// Default: None
        /// Note: FractalType.DomainWarp... only affects DomainWarp(...)
        /// </remarks>
        void SetFractalType(FractalType fractalType);

        /// <summary>
        /// Sets octave count for all fractal noise types 
        /// </summary>
        /// <remarks>
        /// Default: 3
        /// </remarks>
        void SetFractalOctaves(int octaves);

        /// <summary>
        /// Sets octave lacunarity for all fractal noise types
        /// </summary>
        /// <remarks>
        /// Default: 2.0
        /// </remarks>
        void SetFractalLacunarity(float lacunarity);

        /// <summary>
        /// Sets octave gain for all fractal noise types
        /// </summary>
        /// <remarks>
        /// Default: 0.5
        /// </remarks>
        void SetFractalGain(float gain);

        /// <summary>
        /// Sets octave weighting for all none DomainWarp fratal types
        /// </summary>
        /// <remarks>
        /// Default: 0.0
        /// Note: Keep between 0...1 to maintain -1...1 output bounding
        /// </remarks>
        void SetFractalWeightedStrength(float weightedStrength);

        /// <summary>
        /// Sets strength of the fractal ping pong effect
        /// </summary>
        /// <remarks>
        /// Default: 2.0
        /// </remarks>
        void SetFractalPingPongStrength(float pingPongStrength);

        /// <summary>
        /// Sets distance function used in cellular noise calculations
        /// </summary>
        /// <remarks>
        /// Default: Distance
        /// </remarks>
        void SetCellularDistanceFunction(CellularDistanceFunction cellularDistanceFunction);

        /// <summary>
        /// Sets return type from cellular noise calculations
        /// </summary>
        /// <remarks>
        /// Default: EuclideanSq
        /// </remarks>
        void SetCellularReturnType(CellularReturnType cellularReturnType);

        /// <summary>
        /// Sets the maximum distance a cellular point can move from it's grid position
        /// </summary>
        /// <remarks>
        /// Default: 1.0
        /// Note: Setting this higher than 1 will cause artifacts
        /// </remarks> 
        void SetCellularJitter(float cellularJitter);

        /// <summary>
        /// Sets the warp algorithm when using DomainWarp(...)
        /// </summary>
        /// <remarks>
        /// Default: OpenSimplex2
        /// </remarks>
        void SetDomainWarpType(DomainWarpType domainWarpType);

        /// <summary>
        /// Sets the maximum warp distance from original position when using DomainWarp(...)
        /// </summary>
        /// <remarks>
        /// Default: 1.0
        /// </remarks>
        void SetDomainWarpAmp(float domainWarpAmp);


        /// <summary>
        /// 2D noise at given position using current settings
        /// </summary>
        /// <returns>
        /// Noise output bounded between -1...1
        /// </returns>
        float GetNoise(FNLfloat x, FNLfloat y);

        /// <summary>
        /// 3D noise at given position using current settings
        /// </summary>
        /// <returns>
        /// Noise output bounded between -1...1
        /// </returns>
        float GetNoise(FNLfloat x, FNLfloat y, FNLfloat z);


        /// <summary>
        /// 2D warps the input position using current domain warp settings
        /// </summary>
        /// <example>
        /// Example usage with GetNoise
        /// <code>DomainWarp(repu   f x, ref y)
        /// noise = GetNoise(x, y)</code>
        /// </example>
        void DomainWarp(ref FNLfloat x, ref FNLfloat y);

        /// <summary>
        /// 3D warps the input position using current domain warp settings
        /// </summary>
        /// <example>
        /// Example usage with GetNoise
        /// <code>DomainWarp(ref x, ref y, ref z)
        /// noise = GetNoise(x, y, z)</code>
        /// </example>
        void DomainWarp(ref FNLfloat x, ref FNLfloat y, ref FNLfloat z);


        /// <summary>
        /// Sets the default file output type for image files. However, this is deprecated, use
        /// <code>ImageSink(FileTypes.PNG)</code>
        /// instead
        /// </summary>
        void SetOutputFileType(FileTypes type);

        /// <summary>
        /// Saves the created bitmap image to the a file. 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="scale"></param>
        /// <param name="quality"></param>
        void SaveToFile(string filePath, int width, int height, float scale, int quality);
        //void SaveToFile(string filePath, SKBitmap bitmap);

        /// <summary>
        /// Generate the Noise map with the specified noise type.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="scale"></param>
        /// <returns>A float[int, int] with the noise values</returns>
        float[,] GenerateNoiseMap(int width, int height, float scale);
    }
}
