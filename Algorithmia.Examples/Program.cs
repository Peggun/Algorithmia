using System;
using Algorithmia.Examples.NoiseDemos.Perlin;

namespace Algorithmia.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Noise Demo...");
            PerlinNoiseDemo.Run();
            Console.WriteLine("Demo Completed.");
        }
    }
}