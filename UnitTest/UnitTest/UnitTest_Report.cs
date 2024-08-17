using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace PP5AutoUITests
{
    [TestClass]
    public class UnitTest_Report: TestBase
    {
        #region By TI/TP TabControl Tests

        [TestInitialize]
        public void Report_TestMethodSetup()
        {
            OpenDefaultReportWindow();
        }

        [TestMethod]
        public void TestTabControl_ByTIAndHTMLReport()
        {
            IWebElement ReportEditorTabControl = CurrentDriver.GetElement(MobileBy.AccessibilityId("TITPTab"));
            IWebElement ByTIHTMLReportPage = ReportEditorTabControl.TabSelect(0, 0);
            Assert.IsNotNull(ByTIHTMLReportPage);
            //Assert.IsTrue(ByTIHTMLReportPage.Displayed, "ByTIHTMLReportPage.Displayed is true");
            true.ShouldEqualTo(ByTIHTMLReportPage.Displayed);
            Assert.IsTrue(ByTIHTMLReportPage.GetChildElementsCount() > 1);
        }

        [TestMethod]
        public void TestTabControl_ByTIAndExcel()
        {
            IWebElement ReportEditorTabControl = CurrentDriver.GetElement(MobileBy.AccessibilityId("TITPTab"));
            IWebElement ByTIExcelPage = ReportEditorTabControl.TabSelect(0, 1);
            Assert.IsNotNull(ByTIExcelPage);
            //Assert.IsTrue(ByTIExcelPage.Displayed, "ByTIExcelPage.Displayed is true");
            true.ShouldEqualTo(ByTIExcelPage.Displayed, "ByTIExcelPage.Displayed is true");

            //"Excel".ShouldEqualTo(ByTIExcelPage.GetBtnContent("Excel"));
            Assert.IsTrue(ByTIExcelPage.GetChildElementsCount() > 1);
        }

        #endregion
    }
}
