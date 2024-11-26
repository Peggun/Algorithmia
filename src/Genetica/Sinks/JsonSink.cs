using Newtonsoft.Json;

using Algorithmia;
namespace Algorithmia.Sinks;

/// <summary>
/// Sink to write to a JSON file
/// </summary>
public class JsonSink : ISink {
    /// <summary>
    /// Writes to a JSON file with the noise data.
    /// </summary>
    /// <param name="noiseData"></param>
    /// <param name="outputPath"></param>
    public void Write(float[,] noiseData, string outputPath = null) {
        outputPath ??= "output.json";

        string jsonData = JsonConvert.SerializeObject(noiseData, Formatting.Indented);
        File.WriteAllText(outputPath, jsonData);
    }
}