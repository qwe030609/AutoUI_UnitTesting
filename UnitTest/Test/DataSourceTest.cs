using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PP5AutoUITests.Test
{
    [TestClass]
    public class DataSourceTest
    {
        #region Test Report Tests

        public TestContext TestContext { get; set; }
        //private static TestContext _testContext;

        //[ClassInitialize]
        //public static void ClassInitialize(TestContext context)
        //{
        //    _testContext = context;
        //    // Set the DataDirectory property to the output directory
        //    _testContext.Properties["DataDirectory"] = Path.Combine(Directory.GetCurrentDirectory(), "TestCases");
        //}

        //[AssemblyInitialize]
        //public static void BeforeClass(TestContext tc)
        //{

        //}

        //[TestInitialize]
        //public void TestMethodSetup()
        //{

        //}

        //[AssemblyCleanup]
        //public static void AfterClass()
        //{

        //}

        /// <summary>
        /// Test reading datasource as csv file, csv path: C:\\Temp\\TestCaseData.csv
        ///
        /// With Test Data:
        /// "input1","input2","Expected"
        /// 1,2,3
        /// 4,3,7
        /// 
        /// </summary>
        //[DeploymentItem("TestCaseData.csv")]    // 資料檔案之相對路徑為專案之根目錄
        [TestMethod("Test reading datasource as csv file, csv path: C:\\\\Temp\\\\TestCaseData.csv\r\n" +
                                   "\rWith Test Data:\r\n" +
                                   "\r\"input1\",\"input2\",\"Expected\"\r\n" +
                                   "\r1,2,3\r\n" +
                                   "\r4,3,7\r\n")]
        //[DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
        //            "\\TestCases\\TestCaseData.csv",
        //            "TestCaseData#csv",
        //            DataAccessMethod.Sequential), DeploymentItem("TestCaseData.csv", "TestCases")]

        [DataSource("CsvDataSource", "TestCaseData.csv", "TestCaseData#csv", DataAccessMethod.Sequential)]

        [TestCategory("UnitTest_TestReport")]
        public void TPEditor_CreateNewTP_ByMenuItemFileNew()
        {
            int input1 = (int)TestContext.DataRow.ItemArray[0];
            int input2 = (int)TestContext.DataRow.ItemArray[1];
            int expected = (int)TestContext.DataRow.ItemArray[2];

            int actual = input1 + input2;
            expected.ShouldEqualTo(actual);
        }

        #endregion
    }
}
