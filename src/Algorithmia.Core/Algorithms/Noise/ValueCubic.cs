using Algorithmia.Noise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithmia.Core.Algorithms.Noise
{
    public class ValueCubic : BaseNoiseGenerator
    {
        public ValueCubic() => Noise.SetNoiseType(FastNoiseLite.NoiseType.ValueCubic);
        public override float GetNoise(float x, float y) => Noise.GetNoise(x, y);
        public override float GetNoise(float x, float y, float z) => Noise.GetNoise(x, y, z);
    }
}
