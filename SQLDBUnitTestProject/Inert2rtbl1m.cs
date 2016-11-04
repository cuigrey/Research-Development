using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Diagnostics;

namespace SQLDBUnitTestProject
{
    [TestClass()]
    public class Inert2rtbl1m : SqlDatabaseTestClass
    {

        public Inert2rtbl1m()
        {
            InitializeComponent();
        }

        [TestInitialize()]
        public void TestInitialize()
        {
            base.InitializeTest();
        }
        [TestCleanup()]
        public void TestCleanup()
        {
            base.CleanupTest();
        }

        #region Designer support code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_usp_Insert2rtbl1mTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition notEmptyResultSetCondition1;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_usp_Insert2rtbl1mTest_PretestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition1;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ExpectedSchemaCondition expectedSchemaCondition1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inert2rtbl1m));
            this.dbo_usp_Insert2rtbl1mTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            dbo_usp_Insert2rtbl1mTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            notEmptyResultSetCondition1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition();
            dbo_usp_Insert2rtbl1mTest_PretestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            expectedSchemaCondition1 = new CustExpectedSchemaCondition();
            // 
            // dbo_usp_Insert2rtbl1mTest_TestAction
            // 
            dbo_usp_Insert2rtbl1mTest_TestAction.Conditions.Add(notEmptyResultSetCondition1);
            dbo_usp_Insert2rtbl1mTest_TestAction.Conditions.Add(expectedSchemaCondition1);
            resources.ApplyResources(dbo_usp_Insert2rtbl1mTest_TestAction, "dbo_usp_Insert2rtbl1mTest_TestAction");
            // 
            // notEmptyResultSetCondition1
            // 
            notEmptyResultSetCondition1.Enabled = true;
            notEmptyResultSetCondition1.Name = "notEmptyResultSetCondition1";
            notEmptyResultSetCondition1.ResultSet = 1;
            // 
            // dbo_usp_Insert2rtbl1mTest_PretestAction
            // 
            dbo_usp_Insert2rtbl1mTest_PretestAction.Conditions.Add(inconclusiveCondition1);
            resources.ApplyResources(dbo_usp_Insert2rtbl1mTest_PretestAction, "dbo_usp_Insert2rtbl1mTest_PretestAction");
            // 
            // inconclusiveCondition1
            // 
            inconclusiveCondition1.Enabled = true;
            inconclusiveCondition1.Name = "inconclusiveCondition1";
            // 
            // dbo_usp_Insert2rtbl1mTestData
            // 
            this.dbo_usp_Insert2rtbl1mTestData.PosttestAction = null;
            this.dbo_usp_Insert2rtbl1mTestData.PretestAction = null;
            this.dbo_usp_Insert2rtbl1mTestData.TestAction = dbo_usp_Insert2rtbl1mTest_TestAction;
            // 
            // expectedSchemaCondition1
            // 
            expectedSchemaCondition1.Enabled = true;
            expectedSchemaCondition1.Name = "expectedSchemaCondition1";
            resources.ApplyResources(expectedSchemaCondition1, "expectedSchemaCondition1");
            expectedSchemaCondition1.Verbose = false;
        }

        #endregion


        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        #endregion


        [TestMethod()]
        public void dbo_usp_Insert2rtbl1mTest()
        {
            string sEscapeQuotation = @"\u0022".Replace(@"\u0022", "\"");

            try
            {
                ProcessStartInfo oProcStartInfo = new ProcessStartInfo(@"F:\TFS_gkgk.cui@outlook.com\CodeBench_Mainly4Experiment\HTRD\SQLDBUnitTestProject\RunSQLQuery.bat");
                oProcStartInfo.WindowStyle = ProcessWindowStyle.Normal;
                //oProcStartInfo.RedirectStandardError = true;
                //oProcStartInfo.UseShellExecute = false;

                Process oProc = new Process();
                oProc.StartInfo = oProcStartInfo;
                oProc.Start();
                oProc.WaitForExit();
            }
            catch (Exception e)
            {

            }


            SqlDatabaseTestActions testActions = this.dbo_usp_Insert2rtbl1mTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");

                // Replace sql parameter @Count11 to 100.
                /*
                 * DECLARE @Count INT, @RC INT;
                 * SELECT @RC = -1, @Count = @Count11;  -> SELECT @RC = -1, @Count = 100;
                 */
                DbParameter dbParam1 = this.ExecutionContext.Provider.CreateParameter();
                dbParam1.ParameterName = "@Count11";
                dbParam1.Value = 100;

                // 执行所有 Enabled 的条件断言。
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction, dbParam1);
                
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }
        private SqlDatabaseTestActions dbo_usp_Insert2rtbl1mTestData;

        private void ValidateEmptyColl(SqlExecutionResult sTestResult, string appSettingName)
        {
            char[] seperator = new char[] {
                ',', ' ', '\t'
            };
            string[] sNullableCols = string.IsNullOrWhiteSpace(appSettingName) ? new string[0]
                : ConfigurationSettings.AppSettings[appSettingName].Split(seperator, StringSplitOptions.RemoveEmptyEntries);

            foreach (DataTable oResultSet in sTestResult.DataSet.Tables)
            {
                DataRow[] oDataToCheck = oResultSet.Select("");

                foreach (DataRow oRow in oDataToCheck)
                {
                    foreach (DataColumn oColumn in oResultSet.Columns)
                    {
                        if (!sNullableCols.Any(c => c.Equals(oColumn.ColumnName, StringComparison.OrdinalIgnoreCase)))
                        {
                            Assert.IsTrue(oRow[oColumn.ColumnName] == null || string.IsNullOrWhiteSpace(oRow[oColumn.ColumnName].ToString()), "");
                        }
                    }
                }
            }
        }

        private void ValidateDataDiff(SqlExecutionResult oTestResult)
        {
            string diffDataFilterExpr = "";
            DataRow[] set1Life = oTestResult.DataSet.Tables[0].Select(diffDataFilterExpr);
            DataRow[] set2Life = oTestResult.DataSet.Tables[1].Select(diffDataFilterExpr);

            Assert.IsTrue(set1Life.Length > 0 && set2Life.Length == 0, "");
        }
    }
}
