using Algorithmia.Interfaces;

using System.Diagnostics;

namespace Algorithmia.Sinks
{
    public class DebugSink : ISink
    {
        public void Write(float[,] noiseData, string? outputPath = null)
        {
            int height = noiseData.GetLength(0);
            int width = noiseData.GetLength(1);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Print normalized noise value as ASCII art (for now)  
                    float value = noiseData[y, x];
                    char charRepresentation = value switch
                    {
                        < -0.5f => '#',
                        < 0 => '+',
                        < 0.5f => '.',
                        _ => ' ',
                    };

                    Debug.Write(charRepresentation);
                }
                Debug.WriteLine("");
            }
        }
    }
}
