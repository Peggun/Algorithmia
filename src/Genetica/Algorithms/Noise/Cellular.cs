using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithmia
{
    public class CellularNoise : BaseNoiseGenerator
    {
        public CellularNoise() => Noise.SetNoiseType(FastNoiseLite.NoiseType.Cellular);
        public override float GetNoise(float x, float y) => Noise.GetNoise(x, y);
        public override float GetNoise(float x, float y, float z) => Noise.GetNoise(x, y, z);
    }
}
