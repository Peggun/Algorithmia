using Algorithmia;

namespace Algorithmia.Sinks;
/// <summary>
/// Sink to write to a text file
/// </summary>
public class TextFileSink : ISink
{
    /// <summary>
    /// Writes to a text file with all of the noise data
    /// </summary>
    /// <param name="noiseData"></param>
    /// <param name="outputPath"></param>
    public void Write(float[,] noiseData, string outputPath = null)
    {
        // Incase outputPath is null
        outputPath ??= "output.txt";

        int height = noiseData.GetLength(0);
        int width = noiseData.GetLength(1);

        using var writer = new StreamWriter(outputPath);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                writer.Write($"{noiseData[y, x]:F2} ");
            }
            writer.WriteLine();
        }

        Console.WriteLine($"Text file saved to {outputPath}");
    }
}
