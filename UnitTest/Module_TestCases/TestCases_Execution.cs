using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using static PP5AutoUITests.AutoUIActionHelper;
using static PP5AutoUITests.AutoUIExtension;
using static PP5AutoUITests.ThreadHelper;

namespace PP5AutoUITests
{
    [TestClass]
    public class TestCases_Execution : TestBase
    {
        [TestInitialize]
        public void Execution_TestMethodSetup()
        {
            //string tpName = "DEMO";
            string tpName = "G4-51";
            OpenDefaultExecutionWindow(tpName);
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
        /// TestCase: J1-1
        /// Run Same TP for N times
        /// </summary>
        [TestMethod("Run Same TP for N times")]
        [DataRow(1)]
        public void Execution_RunSameTP_For100Times(int runCount)
        {
            for (int i = 0; i < runCount; i++)
            {
                // Press F10 to execute the TP
                Console.WriteLine("Press F10 to execute the TP");
                Press(Keys.F10);

                // Wait for PASS to show up and continue the next execution
                Console.WriteLine("Wait for \"PASS\" to show up and continue the next execution");
                System.Threading.SpinWait.SpinUntil(() => PP5IDEWindow.GetElement(By.ClassName("TestResultStatusView"))
                                                                      .GetFirstTabControlElement()
                                                                      .GetTextElement("PASS") != null);

                //WaitUntil(() => GetPP5Window().GetElement(By.ClassName("TestResultStatusView"))
                //                              .GetFirstTabControlElement()
                //                              .GetTextElement("PASS") != null, 4000);
            }
        }
    }
}
