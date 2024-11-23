using Newtonsoft.Json;

using Algorithmia.Interfaces;
namespace Algorithmia.Sinks;

public class JsonSink : ISink {
    public void Write(float[,] noiseData, string outputPath = null) {
        outputPath ??= "output.json";

        string jsonData = JsonConvert.SerializeObject(noiseData, Formatting.Indented);
        File.WriteAllText(outputPath, jsonData);
    }
}