using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using Castle.Core.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using static PP5AutoUITests.AutoUIActionHelper;
using static PP5AutoUITests.AutoUIExtension;
using static PP5AutoUITests.ThreadHelper;

namespace PP5AutoUITests
{
    [TestClass]
    public class TestCases_Report : TestBase
    {
        [TestInitialize]
        public void Report_TestMethodSetup()
        {
            OpenDefaultReportWindow();
        }

        /// <summary>
        /// EmptyTest
        /// </summary>
        [TestMethod("Test")]
        public void EmptyTest()
        {
            Assert.IsTrue(true);
        }

        /// <summary>
        /// TestCase: J1-11
        /// Repeat Loading same TI or TP for N times
        /// </summary>
        [TestMethod("Repeat Loading same TI or TP for N times")]
        [TestCategory("長時間動作(J1)")]
        [DataRow(1)]
        public void Report_LoadTIorTP_ForNTimes(int runCount)
        {
            IWebElement ReportEditorTabControl = CurrentDriver.GetElement(MobileBy.AccessibilityId("TITPTab"));

            for (int i = 0; i < runCount; i++)
            {
                // Click on report editor button
                CurrentDriver.GetElement(By.ClassName("ToolBar"))
                             .GetElements(By.ClassName("RadioButton"))[0].LeftClick();


                // Adam, 20240704, Modified method use tabSelect
                // Click on By TI tab > HTML Report
                
                IWebElement ByTIHTMLReportPage = ReportEditorTabControl.TabSelect(0, 0);

                // Click on By TI tab > HTML Report
                //IWebElement ByTITabItem = CurrentDriver.GetElement(MobileBy.AccessibilityId("TITPTab"))
                //                                       .GetElements(By.ClassName("TabItem"))[0];

                //ByTITabItem.LeftClick();

                //IWebElement HTMLReportTabItem = ByTITabItem.GetElement(By.ClassName("TabControl"))
                //                                           .GetElements(By.ClassName("TabItem"))[0];

                //HTMLReportTabItem.LeftClick();



                //HTMLReportTabItem.GetElement(MobileBy.AccessibilityId("testItemCbx"))
                //                 .SelectComboBoxItemByIndex(1);

                ByTIHTMLReportPage.GetElement(MobileBy.AccessibilityId("testItemCbx"))
                                  .LeftClick();
                Press(Keys.Down);

                // Adam, 20240704, Modified method use tabSelect
                // Click on By TP tab > HTML Report
                IWebElement ByTPTabControl = ReportEditorTabControl.TabSelect(1).GetFirstTabControlElement();

                CurrentDriver.GetElement(By.Name("Select Test Program"))
                    .GetElement(By.Name("Ok"))
                    .LeftClick();

                IWebElement ByTPHTMLReportPage = ByTPTabControl.TabSelect(0);

                //var chidren = ByTPHTMLReportPage.GetChildElements();
                //var tbc = ByTPHTMLReportPage.GetFirstCustomElement()
                //                            .GetFirstTabControlElement();

                System.Threading.SpinWait.SpinUntil(() => !ByTPHTMLReportPage.GetElement(By.ClassName("ReportEditorControl")).GetFirstTabControlElement()
                                                                             .TabSelect(1)
                                                                             .GetElement(MobileBy.AccessibilityId("testItemCbxByTP"))
                                                                             .GetCellValue()
                                                                             .IsNullOrEmpty());

                // Click on By TP tab > HTML Report
                //IWebElement ByTPTabItem = CurrentDriver.GetElement(MobileBy.AccessibilityId("TITPTab"))
                //                                       .GetElements(By.ClassName("TabItem"))[1];

                //ByTPTabItem.LeftClick();

                //ByTPTabItem = CurrentDriver.GetElement(MobileBy.AccessibilityId("TITPTab"))
                //                            .GetElements(By.ClassName("TabItem"))[1];

                //HTMLReportTabItem = ByTPTabItem.GetElement(By.ClassName("TabControl"))
                //                               .GetElements(By.ClassName("TabItem"))[0];

                //HTMLReportTabItem.LeftClick();

                //var tabcontrol = HTMLReportTabItem.GetElement(MobileBy.AccessibilityId("reportEditorCtrlByTP"));

                //var cmbTI = HTMLReportTabItem.GetElement(MobileBy.AccessibilityId("editMode"))
                //                             .GetElements(By.ClassName("TabItem"))[1]
                //                             .GetElement(MobileBy.AccessibilityId("testItemCbxByTP"));

                //System.Threading.SpinWait.SpinUntil(() => !HTMLReportTabItem.GetElement(MobileBy.AccessibilityId("testItemCbxByTP"))
                //                                                            .GetCellValue()
                //                                                            .IsNullOrEmpty());
            }

            // Click on report editor button
            CurrentDriver.GetElement(By.ClassName("ToolBar"))
                         .GetElements(By.ClassName("RadioButton"))[0].LeftClick();

            ReportEditorTabControl.TabSelect(0, 0);

            // Click on By TI tab > HTML Report
            //IWebElement ByTITabItem1 = CurrentDriver.GetElement(MobileBy.AccessibilityId("TITPTab"))
            //                                        .GetElements(By.ClassName("TabItem"))[0];

            //ByTITabItem1.LeftClick();
        }
    }
}
