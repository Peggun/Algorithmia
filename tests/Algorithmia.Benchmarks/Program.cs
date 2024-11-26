using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Algorithmia.Benchmarks;

[MarkdownExporter]
public class Program
{
    
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<NoisePerformanceTests>();
    }
}