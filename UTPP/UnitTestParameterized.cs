using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CLP;
using BaseOnRoslyn;
using Percana.Eclipse.CEL.Data.Life;
using Percana.Eclipse.Data.Life;
using Percana.Eclipse.ClassFactory;

namespace UTPP
{
    [TestClass]
    public class UnitTestParameterized
    {
        [TestMethod]
        public void TestMethod1()
        {
            Percana.Eclipse.Data.Life.cDsLifeGroup cdsLifeGroup = ServerDataClassFactory.CreateLifeGroup();

            cdsLifeGroup.cDsIntermedPolRelColl.GetIntermediaryPolRel("");

            Percana.Eclipse.Data.Life.cDsIntermediaryPolRel cdsIntermediaryPolRef = cdsLifeGroup.cDsIntermedPolRelColl.Find(ipr => ipr.PolicyId == 0);

            long iIntermediaryid = cdsIntermediaryPolRef.IntermediaryId;

            (new MethodBoyAnalyzer()).Parse<string>(@"E:\TFS_gkgk.cui@outlook.com\MS_Fakes_CustomUnitTest\HTRD\CLP\CP1.cs", "PamarasG", 4);
            Assert.IsTrue(true);
        }
    }
}
