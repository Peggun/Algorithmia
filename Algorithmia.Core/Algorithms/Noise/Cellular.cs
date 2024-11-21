using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithmia.Core.Algorithms.Noise
{
    public class CellularNoise
    {
        FastNoiseLite _noise = new FastNoiseLite();
        public CellularNoise(int seed = 1234)
        {
            _noise.SetSeed(seed);   
        }
    }
}
