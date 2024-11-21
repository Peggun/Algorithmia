using Algorithmia.Enums;
using Algorithmia.Interfaces;
using SkiaSharp;

namespace Algorithmia.Sinks;
public class ImageSink : ISink
{
    private readonly FileTypes _imageFormat;
    private readonly int _quality;

    public ImageSink(FileTypes imageFormat = FileTypes.PNG, int quality = 100)
    {
        _imageFormat = imageFormat;
        _quality = quality;
    }

    public void Write(float[,] noiseData, string outputPath = null)
    {
        // Default output path if null
        outputPath ??= $"output.{_imageFormat.ToString().ToLowerInvariant()}";

        // Determine dimensions
        int height = noiseData.GetLength(0);
        int width = noiseData.GetLength(1);

        // Create a bitmap and populate it with noise data
        using var bitmap = new SKBitmap(width, height);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Normalize the noise value to [0, 255]
                float noiseValue = noiseData[y, x];
                int intensity = Math.Clamp((int)((noiseValue + 1) * 127.5f), 0, 255);

                // Assign a grayscale color based on intensity
                bitmap.SetPixel(x, y, new SKColor((byte)intensity, (byte)intensity, (byte)intensity));
            }
        }

        // Save the image using SkiaSharp's encoding functionality
        using var image = SKImage.FromBitmap(bitmap);
        using var data = _imageFormat switch
        {
            FileTypes.BMP => image.Encode(SKEncodedImageFormat.Bmp, _quality),
            FileTypes.GIF => image.Encode(SKEncodedImageFormat.Gif, _quality),
            FileTypes.ICO => image.Encode(SKEncodedImageFormat.Ico, _quality),
            FileTypes.JPEG => image.Encode(SKEncodedImageFormat.Jpeg, _quality),
            FileTypes.PNG => image.Encode(SKEncodedImageFormat.Png, _quality),
            FileTypes.WBMP => image.Encode(SKEncodedImageFormat.Wbmp, _quality),
            FileTypes.WEBP => image.Encode(SKEncodedImageFormat.Webp, _quality),
            FileTypes.PKM => image.Encode(SKEncodedImageFormat.Pkm, _quality),
            FileTypes.KTX => image.Encode(SKEncodedImageFormat.Ktx, _quality),
            FileTypes.ASTC => image.Encode(SKEncodedImageFormat.Astc, _quality),
            FileTypes.DNG => image.Encode(SKEncodedImageFormat.Dng, _quality),
            FileTypes.HEIF => image.Encode(SKEncodedImageFormat.Heif, _quality),
            FileTypes.AVIF => image.Encode(SKEncodedImageFormat.Avif, _quality),
            _ => throw new NotSupportedException($"File type {_imageFormat} is not supported."),
        };

        // Ensure the directory exists
        string directory = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Save the file
        using var stream = File.OpenWrite(outputPath);
        data.SaveTo(stream);

        Console.WriteLine($"Image saved to {outputPath}");
    }
}
