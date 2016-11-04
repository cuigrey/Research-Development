using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLP.B.R2
{
    public class RCC2
    {
        public int NextID(int maxValue = 100)
        {
            return (new Random()).Next(maxValue);
        }
    }
}
