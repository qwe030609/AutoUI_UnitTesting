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
using static PP5AutoUITests.FileProcessingExtension;
using static PP5AutoUITests.ControlElementExtension;

namespace PP5AutoUITests
{
    [TestClass]
    public class TestCases_TPEditor : TestBase
    {
        string saveFolder = Path.Combine(Directory.GetCurrentDirectory(), "TestCases_TPEditor");
        double updateInterval = 60000;

        [TestInitialize]
        public void TPEditor_TestMethodSetup()
        {
            OpenNewTPEditorWindow();

            _scrnShotTimer = new CaptureAppScreenshotTimer(CaptureApplicationScreenshot, "dotMemory.UI.64", saveFolder, updateInterval); // 60000 milliseconds = 1 minute
            _scrnShotTimer.Start();
        }

        [TestCleanup]
        public void TPEditor_TestMethodCleanup()
        {
            _scrnShotTimer.Stop();
        }

        //[ClassInitialize]
        //public static void ClassInitialize(TestContext context)
        //{
        //    //CaptureApplication("dotMemory.UI.64", savePath);

        //    _scrnShotTimer = new CaptureAppScreenshotTimer(CaptureApplicationScreenshot, "dotMemory.UI.64", saveFolder, updateInterval); // 60000 milliseconds = 1 minute
        //    _scrnShotTimer.Start();
        //}

        //[ClassCleanup]
        //public static void ClassCleanup(TestContext context)
        //{
        //    _scrnShotTimer.Stop();
        //}

        /// <summary>
        /// EmptyTest
        /// </summary>
        [TestMethod("Test")]
        public void EmptyTest()
        {
            Assert.IsTrue(true);
        }

        /// <summary>
        /// TestCase: C1-1
        /// </summary>
        [TestMethod("TestCase: C1-1\n" +
                      "\tCategory: 檔案操作(C1)\n" +
                      "\tName: TPEditor_CreateNewTP_ByMenuItemFileNew\n" +
                      "\tSteps: 用滑鼠點選[File]->[New]建立新檔\n" +
                      "\tDescription: 彈出測試項目建立檔案視窗，設定要建立檔案所在的群組\n" +
                      "\tExpected output: 所有編輯畫面與變數資料被清空\n")]
        [TestCategory("檔案操作(C1)")]
        public void TPEditor_CreateNewTP_ByMenuItemFileNew()
        {
            // Open New TP
            MenuSelect("File", "New");

            // New Test Program window, LeftClick on Button "Ok"
            Console.WriteLine("LeftClick on Button \"Ok\"");
            PP5IDEWindow.GetElement(timeOut: 5000, By.Name("New Test Program"),
                                                     By.Name("Ok"))
                        .LeftClick();

            //Text[@Name =\"Test Program : UnClassified\"][@AutomationId=\"TitleTxtBlk\"]"
            string TPNameInfo = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("TitleTxtBlk")).Text;

            // Check new TP is opened by verify the TP name info
            "UnClassified".ShouldEqualTo(TPNameInfo.Split(':')[1].Trim());
        }

        /// <summary>
        /// TestCase: C1-2
        /// </summary>
        [TestMethod("TestCase: C1-2\n" +
                    "\tCategory: 檔案操作(C1)\n" +
                    "\tName: TPEditor_CreateNewTP_ByMenuItemFileNew\n" +
                    "\tSteps: 用滑鼠點選[File]->[Open…] 開啟檔案\n" +
                    "\tDescription: 彈出測試項目讀取檔案視窗，選擇要開啟檔案所在的群組，以及要開啟的檔案名稱\n" +
                    "\tExpected output: 所有編輯畫面與變數資料載入檔案的設定值\n")]
        [TestCategory("檔案操作(C1)")]
        public void TPEditor_OpenLocalTP_ByMenuItemFileOpen()
        {
            // testing TP filename
            string TPName = "C1-1";

            // Open New TP first
            MenuSelect("File", "New");

            // New Test Program window, LeftClick on Button "Ok"
            //Console.WriteLine("LeftClick on Button \"Ok\"");
            PP5IDEWindow.GetWindowElement("New Test Program")
                        .GetBtnElement("Ok")
                        .LeftClick();

            // Save the TP
            MenuSelect("File", "Save As...");

            // Save Test Program window, LeftClick on Button "Ok"
            //Console.WriteLine("LeftClick on Button \"Ok\"");
            PP5IDEWindow.GetWindowElement("Save Test Program")
                        .GetEditElement("TPNameTxtBox")
                        .SendContent(TPName);

            PP5IDEWindow.GetWindowElement("Save Test Program")
                        .GetBtnElement("Ok")
                        .LeftClick();

            // Open Local TP
            MenuSelect("File", "Open...");
            PP5IDEWindow.GetWindowElement("Load Test Program")
                        .GetCellByName(2, TPName)
                        .LeftClick();

            PP5IDEWindow.GetWindowElement("Load Test Program")
                        .GetBtnElement("Ok")
                        .LeftClick();

            //Text[@Name =\"Test Program : UnClassified\"][@AutomationId=\"TitleTxtBlk\"]"
            string TPNameInfo = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("TitleTxtBlk")).Text;

            // Check new TP is opened by verify the TP name info
            TPName.ShouldEqualTo(TPNameInfo.Split(':')[1].Trim());

            // Clean up the TP
            string tpFolderPath = Path.Combine(PowerPro5Config.ReleaseFolder, "TestProgram");
            tpFolderPath.DeleteFilesWithDifferentExtension(TPName);
        }

        /// <summary>
        /// TestCase: C1-3
        /// </summary>
        [TestMethod("TestCase: C1-3\n" +
                    "\tCategory: 檔案操作(C1)\n" +
                    "\tName: TPEditor_OpenTPVersion2nd_CanShowLastVersionTP\n" +
                    "\tSteps: 用滑鼠點選[File]->[Save] 或工具列的Save按鈕儲存檔案\n" +
                    "\tDescription: 編輯測試程式相關資料後按下儲存\n" +
                    "\tExpected output: 所有編輯畫面與變數資料存入檔案中\n")]
        [TestCategory("檔案操作(C1)")]
        public void TPEditor_OpenTPVersion2nd_CanShowLastVersionTP()
        {
            // testing TP filename
            string TPName = "C1-1";
            string actualLastVersion = "2";

            // Open New TP first
            MenuSelect("File", "New");

            // New Test Program window, LeftClick on Button "Ok"
            //Console.WriteLine("LeftClick on Button \"Ok\"");
            PP5IDEWindow.GetWindowElement("New Test Program")
                        .GetBtnElement("Ok")
                        .LeftClick();

            // Save the TP
            MenuSelect("File", "Save As...");

            // Save Test Program window, LeftClick on Button "Ok"
            //Console.WriteLine("LeftClick on Button \"Ok\"");
            PP5IDEWindow.GetWindowElement("Save Test Program")
                        .GetEditElement("TPNameTxtBox")
                        .SendContent(TPName);

            PP5IDEWindow.GetWindowElement("Save Test Program")
                        .GetBtnElement("Ok")
                        .LeftClick();

            // Release the TP
            MenuSelect("Functions", "Management");

            // Click on TP/TI button
            PP5IDEWindow.GetElement(By.ClassName("ToolBar"))
                        .GetElements(By.ClassName("RadioButton"))[1].LeftClick();

            IWebElement ManagementTabControl = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("mainTab"));
            IWebElement TestProgramTabItem = ManagementTabControl.TabSelect(1, 0);

            // Search the TP with name
            IWebElement testItemSearchBox = TestProgramTabItem.GetElement(MobileBy.AccessibilityId("searchText"));
            testItemSearchBox.Clear();
            testItemSearchBox.SendComboKeys(TPName, Keys.Enter);

            // On the selected row > Check on "Released" checkbox
            Console.WriteLine("On the selected row > Check on \"Released\" checkbox");
            TestProgramTabItem.GetFirstDataGridElement()
                              .GetSelectedRow()
                              .GetCellBy(3)
                              .LeftClick();

            //"/Pane[@ClassName=\"#32769\"][@Name=\"桌面 1\"]/Window[@ClassName=\"Window\"][@Name=\"Chroma ATS IDE - [Management]\"]" +
            //    "/Window[@ClassName=\"Window\"][@Name=\"Notice\"]/Button[@ClassName=\"Button\"][@Name=\"Yes\"]"
            MenuSelect("Windows", "TP Editor");

            // Save the TP again for Last Version testing
            MenuSelect("File", "Save As...");

            PP5IDEWindow.GetWindowElement("Save Test Program")
                        .GetBtnElement("Ok")
                        .LeftClick();

            // Open Local TP
            MenuSelect("File", "Open...");
            PP5IDEWindow.GetWindowElement("Notice")
                        .GetCheckBoxElement("Last Version")
                        .TickCheckBox();

            string expectedLastVersion = PP5IDEWindow.GetWindowElement("Notice")
                                                     .GetRowByName(2, TPName)
                                                     .GetCellValue(3);

            expectedLastVersion.ShouldEqualTo(actualLastVersion);

            // Clean up the TP
            string tpFolderPath = Path.Combine(PowerPro5Config.ReleaseFolder, "TestProgram");
            tpFolderPath.DeleteFilesWithDifferentExtension(TPName);

            /*
            //if (File.Exists(Path.Combine(PowerPro5Config.ReleaseFolder, "TestProgram", TPName))

            //var filesToDelete = Directory.GetFiles(Path.Combine(PowerPro5Config.ReleaseFolder, "TestProgram"), TPName + ".*");
            //if (filesToDelete.Length > 0)
            //{
            //    //File exists delete it
            //    foreach (string file in filesToDelete)
            //    {
            //        File.Delete(file);
            //    }
            //}
            //else
            //{
            //    //File does not exist
            //}
            */
        }



        /// <summary>
        /// TestCase: J1-7
        /// Repeat inserting and deleting same TI for N times
        /// </summary>
        [TestMethod("Repeat inserting and deleting same TI for N times")]
        [TestCategory("長時間動作(J1)")]
        [DataRow(100)]
        public void TPEditor_InsertAndDeleteSameTI_ForNTimes(int runCount)
        {
            //"/Pane[@ClassName=\"#32769\"][@Name=\"桌面 1\"]/Window[@ClassName=\"Window\"][@Name=\"Chroma ATS IDE - [TP Editor]\"]/Custom[@AutomationId=\"Container\"]/Pane[@ClassName=\"ScrollViewer\"]/Custom[@AutomationId=\"tp\"]/Group[@Name=\"dockManager\"][@AutomationId=\"dockManager\"]/Group[@Name=\"dockItem1\"][starts-with(@AutomationId,\"dockItem\")]/Group[@Name=\"dockItem3\"][starts-with(@AutomationId,\"dockItem\")]" + 
            //    "/Custom[@ClassName=\"SysUUTCmdTreeView\"]/Tree[@AutomationId=\"DeivceCmdTree\"]/TreeItem[@ClassName=\"TreeViewItem\"][starts-with(@Name,\"Chroma.TestProgramEditor.ComposedElement.Standard.CmdTreeTestIte\")]/Text[@ClassName=\"TextBlock\"][@Name=\"UnClassified\"]";

            //"/Pane[@ClassName=\"#32769\"][@Name=\"桌面 1\"]/Window[@ClassName=\"Window\"][@Name=\"Chroma ATS IDE - [TP Editor]\"]/Custom[@AutomationId=\"Container\"]/Pane[@ClassName=\"ScrollViewer\"]/Custom[@AutomationId=\"tp\"]/Group[@Name=\"dockManager\"][@AutomationId=\"dockManager\"]/Group[@Name=\"dockItem1\"][starts-with(@AutomationId,\"dockItem\")]/Group[@Name=\"dockItem2\"][starts-with(@AutomationId,\"dockItem\")]" +
            //    "/Custom[@ClassName=\"PGUUTGridAeraView\"]/DataGrid[@AutomationId=\"UUTGrid\"]/Pane[@Name=\"DataPanel\"][@AutomationId=\"dataPresenter\"]/DataItem[starts-with(@Name,\"Chroma.TestProgramEditor.ComposedElement.Standard.PGGridRowAeraV\")]/Custom[starts-with(@Name,\"Current Harmonics Test_1, Item: Chroma.TestProgramEditor.Compose\")][@AutomationId=\"TestItem\"]"

            for (int i = 0; i < runCount; i++)
            {
                AddTIBy("UnClassified", 1, TestItemSourceType.System);

                TPExecuteAction(TPAction.SwitchToUUTTestPage);
                GetPP5Window().GetElement(By.ClassName("PGUUTGridAeraView"))
                              .GetFirstDataGridElement()
                              .GetCellBy(1, 1)
                              .LeftClick();

                // Delete by clicking on the buttons (from toolbar)
                var functionBtns = CurrentDriver.GetElement(By.ClassName("ToolBar"))
                                                .GetElements(By.ClassName("RadioButton"));
                functionBtns[12].LeftClick();
            }
        }

        /// <summary>
        /// TestCase: J1-8
        /// Repeat copying and pasting same TI for N times
        /// </summary>
        [TestMethod("Repeat copying and pasting same TI for N times")]
        [TestCategory("長時間動作(J1)")]
        [DataRow(100)]
        public void TPEditor_CopyAndPasteSameTI_ForNTimes(int runCount)
        {
            AddTIBy("UnClassified", 1, TestItemSourceType.System);

            TPExecuteAction(TPAction.SwitchToUUTTestPage);
            GetPP5Window().GetElement(By.ClassName("PGUUTGridAeraView"))
                          .GetFirstDataGridElement()
                          .GetCellBy(1, 1)
                          .LeftClick();

            for (int i = 0; i < runCount; i++)
            {
                // Copy and Paste the command
                Console.WriteLine("Copy and Paste the command");
                CopyAndPaste();
            }
        }

        /// <summary>
        /// TestCase: J1-9
        /// Repeat inserting and deleting same Vector Variable for N times
        /// </summary>
        [TestMethod("Repeat inserting and deleting same Vector Variable for N times")]
        [TestCategory("長時間動作(J1)")]
        [DataRow(100)]
        public void TPEditor_InsertAndDeleteSameVectorVar_ForNTimes(int runCount)
        {
            TPExecuteAction(TPAction.SwitchToVectorVariablePage);
            IWebElement vectorShownameCell = GetPP5Window().GetElement(By.ClassName("VectorAeraView"))
                                                           .GetFirstDataGridElement()
                                                           .GetCellBy(2, "Show Name");
            
            for (int i = 0; i < runCount; i++)
            {
                vectorShownameCell.DoubleClick();
                vectorShownameCell.SendContent("Line In Vector 2");

                Press(Keys.Enter);

                // Delete by clicking on the buttons (from toolbar)
                var functionBtns = CurrentDriver.GetElement(By.ClassName("ToolBar"))
                                                .GetElements(By.ClassName("RadioButton"));
                functionBtns[12].LeftClick();
            }
        }

        /// <summary>
        /// TestCase: J1-10
        /// Repeat copying and pasting same Vector Variable for N times
        /// </summary>
        [TestMethod("Repeat copying and pasting same Vector Variable for N times")]
        [TestCategory("長時間動作(J1)")]
        [DataRow(100)]
        public void TPEditor_CopyAndPasteSameVectorVar_ForNTimes(int runCount)
        {
            TPExecuteAction(TPAction.SwitchToVectorVariablePage);
            IWebElement vectorShownameCell = GetPP5Window().GetElement(By.ClassName("VectorAeraView"))
                                                           .GetFirstDataGridElement()
                                                           .GetCellBy(2, "Show Name");

            vectorShownameCell.DoubleClick();
            vectorShownameCell.SendContent("Line In Vector 2");

            Press(Keys.Enter);

            for (int i = 0; i < runCount; i++)
            {
                // Copy and Paste the variable by clicking on the buttons (from toolbar)
                var functionBtns = CurrentDriver.GetElement(By.ClassName("ToolBar"))
                                                .GetElements(By.ClassName("RadioButton"));
                functionBtns[10].LeftClick();
                functionBtns[11].LeftClick();
            }
        }

        /// <summary>
        /// TestCase: J2-8/J2-9
        /// Create 10000/20000 Vector Variables
        /// </summary>
        [TestMethod("Create 10000/20000 Vector Variables")]
        [TestCategory("大流量資料(J2)")]
        [DataRow(10)] //J2-8
        [DataRow(20)] //J2-9
        public void TPEditor_Create10000VectorVariables(int vecCount)
        {
            TPExecuteAction(TPAction.SwitchToVectorVariablePage);
            IWebElement vectorDGEle = GetPP5Window().GetElement(By.ClassName("VectorAeraView"))
                                                    .GetFirstDataGridElement();

            for (int i = 1; i <= vecCount; i++)
            {
                vectorDGEle.GetCellBy(i + 1, "Show Name").DoubleClick();
                vectorDGEle.GetCellBy(i + 1, "Show Name").SendContent("Line In Vector " + (i + 1).ToString());
                Press(Keys.Enter);
            }
        }

        /// <summary>
        /// TestCase: J2-10/J2-11
        /// Create 10000/20000 TIs
        /// </summary>
        [TestMethod("Create 10000/20000 TIs")]
        [TestCategory("大流量資料(J2)")]
        [DataRow(10000)] //J2-10
        [DataRow(20000)] //J2-11
        public void TPEditor_Create10000TestItems(int tiCount)
        {
            for (int i = 0; i < tiCount; i++)
            {
                AddTIBy("UnClassified", 1, TestItemSourceType.System);
            }
        }

        /// <summary>
        /// TestCase: J2-12
        /// Copy and paste 10000 TIs
        /// </summary>
        [TestMethod("Copy and paste 10000 TIs")]
        [TestCategory("大流量資料(J2)")]
        [DataRow(10000)] //J2-12
        public void TPEditor_CopyAndPaste10000TestItems(int tiCount)
        {
            AddTIBy("UnClassified", 1, TestItemSourceType.System);

            TPExecuteAction(TPAction.SwitchToUUTTestPage);
            GetPP5Window().GetElement(By.ClassName("PGUUTGridAeraView"))
                          .GetFirstDataGridElement()
                          .GetCellBy(1, 1)
                          .LeftClick();

            for (int i = 0; i < tiCount; i++)
            {
                // Copy and Paste the command
                Console.WriteLine("Copy and Paste the command");
                CopyAndPaste();
            }
        }
    }
}
