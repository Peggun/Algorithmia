using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Genetica.Benchmarks;

[MarkdownExporter]
public class Program
{
    
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<NoisePerformanceTests>();
    }
}