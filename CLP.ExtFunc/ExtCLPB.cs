using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CLP.B;

namespace CLP.ExtFunc
{
    public static class ExtCLPB
    {
        public static object FuncExt(this CPB1 target, int p1, bool p2, string p3, object p4)
        {
            return null;
        }

        public static int CharCount(this String target, string value)
        {
            return value.ToCharArray().Length;
        }
    }
}
