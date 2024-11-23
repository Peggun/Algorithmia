using System.Collections.Generic;
using System.Globalization;

using CsvHelper.Configuration;
using CsvHelper;

using Algorithmia.Interfaces;

namespace Algorithmia.Sinks;

public class CsvSink : ISink {
    public void Write(float[,] noiseData, string outputPath = null) {
        outputPath ??= "output.csv";

        using (var writer = new StreamWriter(outputPath))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture)) {
            for (int i = 0; i < noiseData.GetLength(0); i++) {
                var row = new float[noiseData.GetLength(1)];
                for (int j = 0; j < noiseData.GetLength(1); j++) {
                    row[j] = noiseData[i, j];
                }
                csv.WriteRecords(row);
                csv.NextRecord();
            }
        }
    }
}