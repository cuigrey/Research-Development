using Microsoft.Data.Tools.Schema.Sql.UnitTesting;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDBUnitTestProject
{
    //[ExportTestCondition("CustExpectedSchemaCondition", typeof(CustExpectedSchemaCondition))]
    //[DisplayName("ExpectedSchemaCondition")]
    //[TypeDescriptionProvider("ExpectedSchemaCondition")]
    [ToolboxItem(false)]
    public class CustExpectedSchemaCondition : ExpectedSchemaCondition
    {
        public CustExpectedSchemaCondition() : base()
        {
        }

        public override void Assert(DbConnection validationConnection, SqlExecutionResult[] results)
        {
            try
            {
                base.Assert(validationConnection, results);
            }
            catch (AssertFailedException AssFailExcep)
            {
                throw AssFailExcep;
            }
        }
    }
}
