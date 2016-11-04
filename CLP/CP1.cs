using System;
using System.Data;
using System.Data.SqlClient;

using CB = CLP.B;
using CLP.ExtFunc;

namespace CLP
{
    public class CP1
    {
        protected DataSet m_oDS = null;
        protected CB.CPB1 cpb1 = new CB.CPB1();

        public SqlCommand PamarasG<TcDs, T2, T3>
            (string p1, DateTime p2, string p3, TcDs p4, TcDs p5, ref bool p6)
            where TcDs : CP1
            where T2 : struct
            where T3 : struct
        {
            cpb1.PropBottom.ConvertXXToString();

            if (string.IsNullOrEmpty(cpb1.PropBottom.DataBaseName()))
            {
                return null;
            }

            cpb1.ReturnBR1().NewRCC2().NextID(maxValue: 100000);
            cpb1.FuncExt(0, false, string.Empty, null);

            CLP.B.CPB1 lvCPB1 = new CB.CPB1();

            return new SqlCommand();
        }
    }
}