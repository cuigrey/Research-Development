using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CLP.B.R1;
using CLP.B.R2;

namespace CLP.B
{
    public class CPB1
    {
        public CLP.B.R1.Bottom PropBottom
        {
            get { return new CLP.B.R1.Bottom(); }
        }

        public RCC1 ReturnBR1()
        {
            return new RCC1();
        }
    }
}
