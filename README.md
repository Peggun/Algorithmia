# Algorithmia
A fast, flexible, and cross-platform world generation library for C# and other languages. Built for speed and versatility, `Algorithmia` enables developers to create procedurally generated 2D and 3D worlds efficiently, whether for games, simulations, or creative tools.

---

## Table of Contents
1. [Features](#features)
2. [Installation](#installation)
3. [Usage](#usage)
4. [Examples](#examples)
5. [Roadmap](#roadmap)
6. [Contributing](#contributing)
7. [License](#license)

## Features
- 🌍 **Multi-Dimensional World Generation:** Generate 2D maps and 3D terrain.
- ⚡ **FastNoiseLite Integration:** High-performance noise generation. See [FastNoiseLite.cs](https://github.com/Auburn/FastNoiseLite/blob/master/CSharp/FastNoiseLite.cs)
- 🛠️ **Customizable Algorithms:** Modify parameters for unique results.
- 🔄 **Cross-Language Support:** Precompiled bindings for Python, C#, and more.
- 📦 **Lightweight and Optimized:** Minimal dependencies for fast execution.

## Installation
**C# Installation**

## Usage

## Example
**C# Example**
```cs
/* 
        Algorithm for generating a gray scale PNG image of a Perlin Noise Generation Map
        Uses SkiaSharp for the drawing of the Grayscale Images
*/

// These are for now. Will slim them down later.
using Algorithmia.Factories;
using Algorithmia.Enums;
using Algorithmia.Interfaces;
using Algorithmia.Sinks;

private const int Width = 512;
private const int Height = 512;
private const float Scale = 0.01f;

try
{
    // You can create multiple noises using this way. Just modify FastNoiseLite.NoiseType.Perlin to specific noise
    var perlinNoise = NoiseGeneratorFactory.CreateNoiseGenerator(FastNoiseLite.NoiseType.Perlin);
    perlinNoise.SetSeed(1337);
    perlinNoise.SetFrequency(0.1f);

    //perlinNoise.SetOutputFileType(FileTypes.JPEG);  // No need for this anymore. Still including it because why not.

    // Generate Noise Map
    float[,] noise = perlinNoise.GenerateNoiseMap(Width, Height, Scale);

    var sinks = new ISink[]
    {
        new ConsoleSink(),                // Output to console
        new ImageSink(FileTypes.PNG),     // Output to an image (you now define the Image Type in here)
        new TextFileSink(),               // Output to an text file
        new DebugSink(),                  // Output to debug console
    };

    // Use each sink
    foreach (var sink in sinks)
    {
        sink.Write(noise);
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error generating noise: {ex.Message}");
}
```

## Roadmap (with more to add)
- [x] Initial library release with Perlin noise support.
- [x] Added Sink Support (Console, Debug Console, Plain Text, Image File)
- [ ] Expand noise algorithms and add more (e.g., Simplex, Voronoi).
- [ ] Multi-language bindings (e.g., Python, JavaScript).
- [ ] Add 3D terrain generation.
- [ ] Distribute via NuGet and other ways like through DLL's.

## Contributing
If you would like to contribute, see the [CONTRIBUTING]() file for details.

## License
This project is licensed under the GNU License. See the [LICENSE]() file for details.

---

Made with ❤️ by [Peggun](https://github.com/Peggun) and others.