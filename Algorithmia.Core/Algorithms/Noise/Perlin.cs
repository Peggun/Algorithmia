using Algorithmia.Core.Enums;
using SkiaSharp;

namespace Algorithmia.Core.Algorithms.Noise
{
    public class PerlinNoise : BaseNoiseGenerator
    {
        public PerlinNoise() => Noise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
        public override float GetNoise(float x, float y) => Noise.GetNoise(x, y);
        public override float GetNoise(float x, float y, float z) => Noise.GetNoise(x, y, z);
        public override void SaveToFile(string filePath, int width, int height, float scale, int quality)
        {
            using var bitmap = new SKBitmap(width, height);
            using var canvas = new SKCanvas(bitmap);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    float noiseValue = GetNoise(x * scale, y * scale);

                    // Normalize to [0, 255]
                    int intensity = (int)((noiseValue + 1) * 127.5f);
                    intensity = Math.Clamp(intensity, 0, 255);

                    // Set pixel color
                    var color = new SKColor((byte)intensity, (byte)intensity, (byte)intensity);
                    bitmap.SetPixel(x, y, color);
                }
            }

            // Save image based on output file type
            using var image = SKImage.FromBitmap(bitmap);
            using var data = OutputFileType switch
            {
                FileTypes.BMP => image.Encode(SKEncodedImageFormat.Bmp, quality),
                FileTypes.GIF => image.Encode(SKEncodedImageFormat.Gif, quality),
                FileTypes.ICO => image.Encode(SKEncodedImageFormat.Ico, quality),
                FileTypes.JPEG => image.Encode(SKEncodedImageFormat.Jpeg, quality),
                FileTypes.PNG => image.Encode(SKEncodedImageFormat.Png, quality),
                FileTypes.WBMP => image.Encode(SKEncodedImageFormat.Wbmp, quality),
                FileTypes.WEBP => image.Encode(SKEncodedImageFormat.Webp, quality),
                FileTypes.PKM => image.Encode(SKEncodedImageFormat.Pkm, quality),
                FileTypes.KTX => image.Encode(SKEncodedImageFormat.Ktx, quality),
                FileTypes.ASTC => image.Encode(SKEncodedImageFormat.Astc, quality),
                FileTypes.DNG => image.Encode(SKEncodedImageFormat.Dng, quality),
                FileTypes.HEIF => image.Encode(SKEncodedImageFormat.Heif, quality),
                FileTypes.AVIF => image.Encode(SKEncodedImageFormat.Avif, quality),
                _ => throw new NotSupportedException($"File type {OutputFileType} is not supported."),
            };

            using var stream = File.OpenWrite(filePath);
            data.SaveTo(stream);
        }
    }
}
