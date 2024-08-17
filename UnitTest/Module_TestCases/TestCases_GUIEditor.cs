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
using static PP5AutoUITests.ControlElementExtension;

namespace PP5AutoUITests
{
    [TestClass]
    public class TestCases_GUIEditor : TestBase
    {
        [TestInitialize]
        public void GUIEditor_TestMethodSetup()
        {
            OpenDefaultGUIEditorWindow();
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
        /// Add "Go" Control Element for 100 times
        /// </summary>
        [TestMethod("Add \"Go\" Control Element for 100 times")]
        public void GUIEditor_AddControlElementGo_For100Times()
        {
            var groupControl = CurrentDriver.GetElement(5000, MobileBy.AccessibilityId("GroupTreeView"),
                                                              By.Name("Chroma.GUI.ExecutionEditor.ComposedElement.ItemGroupViewModel"));
            if (groupControl.isElementCollapsed())
                groupControl.GetTextElement("Control").DoubleClick();

            for (var i = 0; i < 100; i++)
                groupControl.GetTextElement("Go").DoubleClick();
        }

        /// <summary>
        /// Add "Go" Control Element for 209 times and fill to whole screen
        /// </summary>
        [TestMethod("Add \"Go\" Control Element for 209 times and fill to whole screen")]
        public void GUIEditor_AddControlElementGo_For209TimesFillTheScreen()
        {
            var groupControl = CurrentDriver.GetElement(5000, MobileBy.AccessibilityId("GroupTreeView"),
                                                              By.Name("Chroma.GUI.ExecutionEditor.ComposedElement.ItemGroupViewModel"));
            if (groupControl.isElementCollapsed())
                groupControl.GetTextElement("Control").DoubleClick();

            for (var i = 0; i < 11; i++)
            {
                for (var j = 0; j < 19; j++)
                {
                    groupControl.GetTextElement("Go").DoubleClick();

                    // Mouse Move from "Go selection item" to "Go screen element": {x:50 y:115} to {x:550 y:339}
                    MoveByOffsetAndLeftClick(offsetX: 550 - 50, offsetY: 339 - 115);
                    CurrentDriver.GetElement(5000, By.Name("Field Set"),
                                                   MobileBy.AccessibilityId("PositionX"))
                                 .SendContent((100 * j).ToString());

                    CurrentDriver.GetElement(5000, By.Name("Field Set"),
                                                   MobileBy.AccessibilityId("PositionY"))
                                 .SendContent((100 * i).ToString());
                }
            }
        }

        [TestMethod]
        public void GUIEditor_OpenFile_Go11x19()
        {
            Stopwatch timer = new Stopwatch();

            for (var i = 0; i <= 10; i++)
            {
                MenuSelect("File", "Open...");
                CurrentDriver.GetElement(5000, By.Name("Loaded"), MobileBy.AccessibilityId("lbFiles"))
                             .GetSpecificChildOfControlType(ElementControlType.ListBoxItem, "Go(11x19)")
                             .LeftClick();

                CurrentDriver.GetElement(5000, By.Name("Loaded"), By.Name("Ok")).LeftClick();
                timer.Reset();
                timer.Start();
                while (!CheckGUIEditorWindowIsOpenedByName("Go(11x19)"))
                timer.Stop();
                //long loadingTime = timer.ElapsedMilliseconds;
                Console.WriteLine($"loadingTime for opening GUI file:{timer.ElapsedMilliseconds}");
            }
        }

        /// <summary>
        /// Add Control Element consecutively for 24 hours
        /// </summary>
        [TestMethod("Add Control Element consecutively for 24 hours")]
        public void GUIEditor_AddControlElement_For24Hours()
        {
            var groupControls = CurrentDriver.GetElement(5000, MobileBy.AccessibilityId("GroupTreeView"))
                                             .GetElements(By.Name("Chroma.GUI.ExecutionEditor.ComposedElement.ItemGroupViewModel"));

            Stopwatch timer = new Stopwatch();
            timer.Reset();
            timer.Start();

            //Stopwatch timer2 = new Stopwatch();
            int round = 0;
            while (timer.ElapsedMilliseconds < (3 * 24 * 60 * 60 * 1000))
            {
                //timer2.Reset();
                //timer2.Start();

                // Control Group
                if (groupControls[0].isElementCollapsed())
                    groupControls[0].GetTextElement("Control").DoubleClick();

                groupControls[0].GetTextElement("Go").DoubleClick();
                Console.WriteLine($"Control element: \"Go\" added into screen\"");
                groupControls[0].GetTextElement("Stop").DoubleClick();
                Console.WriteLine($"Control element: \"Stop\" added into screen\"");
                groupControls[0].GetTextElement("Single").DoubleClick();
                Console.WriteLine($"Control element: \"Single\" added into screen\"");
                groupControls[0].GetTextElement("Report").DoubleClick();
                Console.WriteLine($"Control element: \"Report\" added into screen\"");
                groupControls[0].GetTextElement("Pause").DoubleClick();
                Console.WriteLine($"Control element: \"Pause\" added into screen\"");


                // Execution Information Group
                if (groupControls[1].isElementCollapsed())
                    groupControls[1].GetTextElement("Execution Information").DoubleClick();

                groupControls[1].GetTextElement("Program").DoubleClick();
                Console.WriteLine($"Control element: \"Program\" added into screen\"");
                groupControls[1].GetTextElement("UUT").DoubleClick();
                Console.WriteLine($"Control element: \"UUT\" added into screen\"");
                groupControls[1].GetTextElement("YieldRate").DoubleClick();
                Console.WriteLine($"Control element: \"YieldRate\" added into screen\"");


                // Execution Status Group
                if (groupControls[2].isElementCollapsed())
                    groupControls[2].GetTextElement("Execution Status").DoubleClick();

                groupControls[2].GetTextElement("TestItem List").DoubleClick();
                Console.WriteLine($"Control element: \"TestItem List\" added into screen\"");
                groupControls[2].GetTextElement("Status").DoubleClick();
                Console.WriteLine($"Control element: \"Status\" added into screen\"");


                // Result Variables Group
                if (groupControls[3].isElementCollapsed())
                    groupControls[3].GetTextElement("Result Variables").DoubleClick();

                groupControls[3].GetSpecificChildOfControlType(ElementControlType.TreeViewItem)
                                .GetTextElement("Result Variables")
                                .DoubleClick();
                Console.WriteLine($"Control element: \"Result Variables\" added into screen\"");


                // Result Variables Group
                if (groupControls[4].isElementCollapsed())
                    groupControls[4].GetTextElement("Other").DoubleClick();

                groupControls[4].GetTextElement("Image").DoubleClick();
                Console.WriteLine($"Control element: \"Image\" added into screen\"");
                groupControls[4].GetTextElement("Text").DoubleClick();
                Console.WriteLine($"Control element: \"Text\" added into screen\"");


                //// DLLImport Group
                //if (groupControls[5].isElementCollapsed())
                //    groupControls[5].GetTextElement("DLLImport").DoubleClick();

                //groupControls[5].GetTextElement("LED").DoubleClick();
                //Console.WriteLine($"Control element: \"LED\" added into screen\"");

                //timer2.Stop();

                //int oneRound = (int)Math.Round((decimal)timer2.ElapsedMilliseconds / 1000);

                //// Get cpu & memory usage 
                // 取得 cpu 效能
                //PerformanceCounter cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");

                //// 取得近 x 秒的平均值 (oneRound)
                //double cpuAvg = 0;
                //for (int i = 0; i < oneRound; i++)
                //{
                //    cpuAvg += cpu.NextValue();
                //    Thread.Sleep(1000);
                //}
                //cpuAvg = Math.Round(cpuAvg / oneRound, 0);

                //Console.WriteLine("CPU 使用率 = " + Math.Round(cpu.NextValue(), 0) + " %");

                // 取得 Memory 使用率
                PerformanceCounter memory = new PerformanceCounter("Memory", "% Committed Bytes in Use");
                int memoryUse = Convert.ToInt32(memory.NextValue());
                Console.WriteLine($"Current memory in Use: {memoryUse}");

                round++;

                if (round % 2 == 0)
                    MenuSelect("File", "New");
            }

            timer.Stop();
            Console.WriteLine($"Total elapsed time:{timer.ElapsedMilliseconds}");
        }
    }
}
