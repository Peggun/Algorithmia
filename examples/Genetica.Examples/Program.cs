using System;
using Genetica.Examples.NoiseDemos.Perlin;

namespace Genetica.Examples
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