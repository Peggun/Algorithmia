using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetica
{
    public class WhiteNoise : BaseNoiseGenerator
    {
        Random random;
        public WhiteNoise()
        {
            random = new Random(_seed);
        }

        public override float GetNoise(float x, float y)
        {
            return (float)random.NextDouble();
        }

        public override float GetNoise(float x, float y, float z)
        {
            return (float)random.NextDouble();
        }
    }
}
