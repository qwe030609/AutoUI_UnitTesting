//#define WRITE_LOG
//using System.Timers;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Castle.Core.Internal;
using Chroma.UnitTest.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static PP5AutoUITests.AutoUIActionHelper;
using static PP5AutoUITests.AutoUIExtension;
using static PP5AutoUITests.ControlElementExtension;
using static PP5AutoUITests.ThreadHelper;
using static PP5AutoUITests.DllHelper;
using static PP5AutoUITests.FileProcessingExtension;
using Keys = OpenQA.Selenium.Keys;
using static PP5AutoUITests.TestCases_TIEditor_EnumCreatorDialog;
using System.Collections.Generic;
using System.Timers;
using Microsoft.Win32;
using OpenQA.Selenium.Interactions;
using System;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Opera;
using Chroma.UnitTest.Common.AutoUI;
using System.Data;
using Castle.Components.DictionaryAdapter.Xml;
using System.Linq;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Windows.Automation;
using static System.Net.Mime.MediaTypeNames;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using System.Collections.Concurrent;

namespace PP5AutoUITests
{
    [TestClass]
    public class TestBase : UTCommon.TestBase, ITestCommand
    {
        //private const string WinAppDriverPath = @"C:\Program Files (x86)\Windows Application Driver\WinAppDriver.exe";
        //public string TestResultOutputDirectory = Directory.GetCurrentDirectory();
        public static readonly string TestResultOutputDirectory = CurrWorkingDirectory;

        public static class TestResultCollection
        {
            public static Dictionary<ITestMethod, TestResult[]> Results { get; set; } = new Dictionary<ITestMethod, TestResult[]>();
        }

        public class MyTestMethodAttribute : TestMethodAttribute
        {

            public MyTestMethodAttribute(string displayName) : base(displayName) { }

            public override TestResult[] Execute(ITestMethod testMethod)
            {
                TestResult[] results = base.Execute(testMethod);

                TestResultCollection.Results.Add(testMethod, results);

                return results;
            }
        }

        /// <summary>The RepeatAttribute is used on a test method to specify that it should be executed multiple times. If any repetition fails, the remaining ones are not run and a failure is reported.</summary>
        /// <param name="repeatCount">int representing the repitition count.</param>"
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
        public class RepeatAttribute : Attribute
        {
            private const int MIN_REPEAT_COUNT = 1;
            private const int MAX_REPEAT_COUNT = 50;

            public RepeatAttribute(
                int repeatCount)
            {
                if (repeatCount < MIN_REPEAT_COUNT || MAX_REPEAT_COUNT < repeatCount)
                {
                    repeatCount = MIN_REPEAT_COUNT;
                }

                Value = repeatCount;
            }

            public int Value { get; }
        }


        public static Executor AutoUIExecutor;
        //public static WindowsDriver<WindowsElement> MainPanel_Session, PP5IDE_Session, Desktop_Session;
        static WindowsDriver<WindowsElement> currentDriver;
        public static WindowsDriver<WindowsElement> CurrentDriver
        {
            get
            {
                currentDriver = AutoUIExecutor.GetCurrentDriver();
                return currentDriver;
            }
        }

        //public AutoUIAction uIActionMainPanel, uIActionPP5IDE;
        //public static AutoUIAction uIAction;
        public static Process winAppDriverProcess;

        protected static bool IsPP5IDEWindowStale => ExpectedConditions.StalenessOf(_PP5IDEWindow)(CurrentDriver);

        //private bool isIDEWindowPresent = false;
        public static bool IsIDEWindowPresent
        {
            get
            {
                return _PP5IDEWindow != null && !IsPP5IDEWindowStale;
            }
        }

        static IWebElement _PP5IDEWindow;
        public static IWebElement PP5IDEWindow
        {
            get
            {
                if (!IsIDEWindowPresent)
                    _PP5IDEWindow = GetPP5Window();
                return _PP5IDEWindow;
            }
        }


        //public MemoryCache<(int?, string), IWebElement> dataTableConditionCache;
        //public MemoryCache<(int?, string), IWebElement> dataTableResultCache;
        //public MemoryCache<(int?, string), IWebElement> dataTableTempCache;
        //public MemoryCache<(int?, string), IWebElement> dataTableGlobalCache;
        //public MemoryCache<(int?, string), IWebElement> dataTableDefectCodeCache;
        //public MemoryCache<(int?, string), IWebElement> dataTableTestItemsCache;
        //public MemoryCache<(int?, string), IWebElement> dataTableTestCmdParamCache;
        //public MemoryCache<(int?, string), IWebElement> dataTableAllTestItemsCache;

        public MemoryCache<string, IWebElement> dataTablesCache;

        //public static WindowsElement menuCache;
        public MemoryCache<string, IWebElement> CommandsMapCache;
        public string[] moduleNames = { "TI Editor", "TP Editor", "Report", "On-Line Control",
                                        "Management", "Hardware Configuration", "GUI Editor", "Execution" };

        internal static CaptureAppScreenshotTimer _scrnShotTimer;
        //public static Dictionary<string, bool> cmdGroupSourceTypeDict;
        internal static OrderedDictionary<string, CommandGroupData> cmdGroupDataDict;
        internal static TaskManager taskManager = new TaskManager();

        [AssemblyInitialize]
        public static void BeforeClass(TestContext tc)
        {
            TestEnvSetup();     // Test environment setup before testing started

            AutoUIExecutor = Executor.GetInstance();
            PP5LogIn();
            _PP5IDEWindow = null;

            //winAppDriverProcess = AutoUIExecutor.WinAppDriverProcessElement;  // Get winAppDriver session
            //PP5IDE_Session = AutoUIExecutor.DesktopSessionElement;            // get PP5 IDE session
            //MainPanel_Session = AutoUIExecutor.PP5SessionElement;                    // get PP5 main panel session    
            //Desktop_Session = AutoUIExecutor.MyDesktopSessionElement;                    // get desktop session   
        }

        [AssemblyCleanup]
        public static void AfterClass()
        {
            //EnableMouse(true);   // Enable mouse usage after all testing are finished

            // close winAppDriver
            //AutoUIExecutor.WinAppDriverProcess.Dispose();
            //AutoUIExecutor.WinAppDriverProcess.Close();
            AutoUIExecutor.WinAppDriverProcess.Kill();

            //Console.WriteLine("After all tests");
            GC.Collect();
            CurrentDriver.ReleaseComObject();
            AutoUIExecutor.ReleaseComObject();
        }

        //[ClassInitialize]
        //public static void Setup(TestContext context)
        //{

        //}

        //[ClassCleanup]
        //public static void Cleanup(TestContext context)
        //{

        //}

        [TestInitialize]
        public void TestMethodSetup()
        {
            // Element cache initialize
            CommandsMapCache = new MemoryCache<string, IWebElement>();
            dataTablesCache = new MemoryCache<string, IWebElement>();

            //dataTableConditionCache = new MemoryCache<(int?, string), IWebElement>();
            //dataTableResultCache = new MemoryCache<(int?, string), IWebElement>();
            //dataTableTempCache = new MemoryCache<(int?, string), IWebElement>();
            //dataTableGlobalCache = new MemoryCache<(int?, string), IWebElement>();
            //dataTableDefectCodeCache = new MemoryCache<(int?, string), IWebElement>();
            //dataTableTestItemsCache = new MemoryCache<(int?, string), IWebElement>();
            //dataTableTestCmdParamCache = new MemoryCache<(int?, string), IWebElement>();
            //dataTableAllTestItemsCache = new MemoryCache<(int?, string), IWebElement>();
        }

        [TestCleanup]
        public void AfterTest()
        {
            //Console.WriteLine("After each test");
        }

        [TestMethod]
        public void TryTestBase()
        {
            //Console.WriteLine("TestBase try method executed!");
        }

        public static void TestEnvSetup()
        {
#if !DEBUG
            //EnableMouse(false); // Disable mouse usage before testing
#endif
            DisableMemoryMonitorWindow(); // Disable Memory Monitor that shows memory warning window before starting the driver
            JsonUpdateProperty(filePath: $"{PowerPro5Config.ReleaseDataFolder}//SystemSetup.ssx",   // Turn off TI&TP autosave
                               nodePath: "Datas",
                               propertyName: "AutoBackupInterval",
                               newValue: 0);

            //LoadCommandGroupInfos();
            //await LoadCommandGroupTask();
            int taskId = taskManager.StartNewTask("LoadCommandGroup", LoadCommandGroup);
            SharedSetting.forceRefreshPP5Window = false;
        }

        internal TaskInfo GetTaskInfo(int taskId)
        {
            return taskManager.GetTaskInfo(taskId);
        }

        internal int GetFirstTaskId()
        {
            return taskManager.GetAllTaskIds().OrderBy(n => n).First();
        }

        internal int GetLastTaskId()
        {
            return taskManager.GetAllTaskIds().OrderBy(n => n).Last();
        }

        internal bool CheckAllTasksCompleted()
        {
            return taskManager.GetAllTaskIds().TrueForAll(id => GetTaskInfo(id).Status is TaskStatus.Completed);
        }

        public static void LoadCommandGroup()
        {
            LoadCommandGroupInfos();

            foreach (string groupName in GetGroupNames())
                LoadCommandGroupCommandNames(groupName);

            //Logger.LogMessage($"LoadCommandGroup() executed time (ms): {taskManager.GetTaskInfo(taskId).TotalTimeMilliseconds}");
            Logger.LogMessage("LoadCommandGroup() done.");
        }

        //public static Task LoadCommandGroupTask()
        //{
        //    return Task.Run(() =>
        //    {
        //        try
        //        {
        //            LoadCommandGroup();
        //            Logger.LogMessage("Task has completed.");
        //        }
        //        catch (Exception ex)
        //        {
        //            Logger.LogMessage($"Error in LoadCommandGroup: {ex.Message}");
        //            throw; // Re-throw to propagate the error
        //        }
        //    });
        //}

        public static List<string> GetGroupNames()
        {
            if (cmdGroupDataDict.Count == 0)
                LoadCommandGroupInfos();
            
            return cmdGroupDataDict.Keys.ToList();
        }

        public static int GetGroupID(string groupName)
        {
            if (cmdGroupDataDict.Count == 0)
                LoadCommandGroupInfos();

            return cmdGroupDataDict[groupName].GroupID;
        }

        public static void LoadCommandGroupInfos()
        {
            // Read SystemCommand.csx and find all GroupName and IsDevice values
            JsonGetProperty(filePath: $"{PowerPro5Config.ReleaseDataFolder}//SystemCommand.csx", nodePath: "CommandGroupInfos@IsDevice", out List<bool> IsDeviceBooleanValues);
            JsonGetProperty(filePath: $"{PowerPro5Config.ReleaseDataFolder}//SystemCommand.csx", nodePath: "CommandGroupInfos@GroupID", out List<int> GroupIds);
            JsonGetProperty(filePath: $"{PowerPro5Config.ReleaseDataFolder}//SystemCommand.csx", nodePath: "CommandGroupInfos@GroupName", out List<string> GroupNameKeys);

            var commandGroupDataList = new List<CommandGroupData>();
            for (int i = 0; i < GroupNameKeys.Count; i++)
            {
                commandGroupDataList.Add(new CommandGroupData(GroupIds[i], IsDeviceBooleanValues[i]));
            }

            // Create dictionary of groupname - isdevice (true, false) pairs
            cmdGroupDataDict = CreateOrderedDictionary(GroupNameKeys, commandGroupDataList);
        }

        public static void LoadCommandGroupCommandNames(string groupName)
        {
            string commandFilePath = GetCommandFileFullPath();
            //JsonGetProperty(filePath: commandFilePath, nodePath: "CommandGroupInfos@GroupName", out List<string> GroupNameKeys);

            int groupIndex = GetGroupNames().IndexOf(groupName);

            // Read SystemCommand.csx and find all GroupName and IsDevice values
            JsonGetProperty(filePath: commandFilePath, nodePath: $"CommandGroupInfos[{groupIndex}]/Commands@CommandName", out List<string> commandNames);
            //JsonGetProperty(filePath: $"{PowerPro5Config.ReleaseDataFolder}//SystemCommand.csx", nodePath: "CommandGroupInfos@GroupName", out List<string> GroupNameKeys);

            //List<CommandGroupData> commandGroupDataList = new List<CommandGroupData>();
            //for (int i = 0; i < GroupNameKeys.Count; i++)
            //{
            //    commandGroupDataList.Add(new CommandGroupData(commandNames[i]));
            //}

            // Create dictionary of groupname - isdevice (true, false) pairs
            //cmdGroupDataDict = CreateDictionary(GroupNameKeys, commandGroupDataList);
            cmdGroupDataDict[groupName].CommandNames = commandNames.OrderBy(n=>n).ToList();
        }


        public List<string> QueryCommandNames(string groupName)
        {
            if (cmdGroupDataDict.AsDictionary().TryGetValue(groupName, out CommandGroupData groupData))
            {
                if (groupData.CommandNames.Count == 0)
                {
                    taskManager.StartNewTask("LoadCommandGroupCommandNames", () => LoadCommandGroupCommandNames(groupName));
                    //LoadCommandGroupCommandNames(groupName);
                    return cmdGroupDataDict[groupName].CommandNames;
                }
                else 
                    return groupData.CommandNames;
            }
            else
                throw new GroupNameNotExistedException(groupName);
        }

        public string QueryGroupName(string commandName)
        {
            if (!HasCommand(commandName))
                throw new CommandNameNotExistedException(commandName);

            return cmdGroupDataDict.AsDictionary().FirstOrDefault(kvp => kvp.Value.CommandNames.Contains(commandName)).Key;
        }

        public bool HasGroup(string groupName)
        {
            return cmdGroupDataDict.ContainsKey(groupName);
        }

        public bool HasCommand(string groupName, string commandName)
        {
            if (!HasGroup(groupName))
                return false;

            return cmdGroupDataDict[groupName].CommandNames.Contains(commandName);
        }

        public bool HasCommand(string commandName)
        {
            return cmdGroupDataDict.AsDictionary().Count(kvp => kvp.Value.CommandNames.Contains(commandName)) != 0;
        }

        public static bool AddCommandInCGIList(string groupName, string cmdName)
        {
            //if (cmdGroupDataDict.Count == 0)
            //    LoadCommandGroupInfos();

            //var groupList = cmdGroupDataDict.Keys.ToList();

            int CGIListIdx = GetGroupNames().IndexOf(groupName);

            string nodepathToUpdate = $"CommandGroupInfos[{CGIListIdx}]/Commands[0>-1]@CommandName={cmdName}";

            // Create a new node in the cgi list and saved in the SystemCommand.csx file
            bool isJsonCreateNewNodeSuccess = JsonCreateNewNodeInList(filePath: GetCommandFileFullPath(),
                                                                      nodePath: nodepathToUpdate);

            taskManager.StartNewTask("LoadCommandGroupCommandNames", () => LoadCommandGroupCommandNames(groupName));
            //LoadCommandGroupCommandNames(groupName);
            return isJsonCreateNewNodeSuccess;
        }

        public static bool AddEmptyCommandInCGIList(string groupName, string cmdName)
        {
            //if (cmdGroupDataDict.Count == 0)
            //    LoadCommandGroupInfos();

            //var groupList = cmdGroupDataDict.Keys.ToList();

            int CGIListIdx = GetGroupNames().IndexOf(groupName);
            int groupId = GetGroupID(groupName);
            GetCommandSourceType(groupName, out bool IsDevice);

            string nodePart = $"CommandGroupInfos[{CGIListIdx}]/Commands[>-1]";
            string propertyPart = $"CommandName={cmdName},IsDevice={IsDevice},GroupID={groupId},Visible={true}";
            string nodepathToUpdate = $"{nodePart}@{propertyPart}";

            // Create a new node in the cgi list and saved in the SystemCommand.csx file
            bool isJsonCreateNewNodeSuccess = JsonCreateNewNodeInList(filePath: GetCommandFileFullPath(),
                                                                      nodePath: nodepathToUpdate);

            taskManager.StartNewTask("LoadCommandGroupCommandNames", () => LoadCommandGroupCommandNames(groupName));
            //LoadCommandGroupCommandNames(groupName);
            return isJsonCreateNewNodeSuccess;
        }

        public static void PP5LogIn()
        {
            try
            {
                CurrentDriver.SwitchToWindow(out bool switchToMainPanel);
                if (!switchToMainPanel)
                    return;

                WaitUntil(() => CheckWindowOpened(PowerPro5Config.LoginWindowName));

                if (!CheckLoginPageIsOpened())
                    return;

                // KeyboardInput VirtualKeys=""root"Keys.Tab + Keys.TabKeys.Tab + Keys.TabKeys.Tab + Keys.Tab" CapsLock=False NumLock=True ScrollLock=False
                Logger.LogMessage("KeyboardInput VirtualKeys=\"\"root\" CapsLock=False NumLock=True ScrollLock=False");
                System.Threading.Thread.Sleep(100);

                //PP5Session.FindElementByAccessibilityId("Username")?.SendKeys("root");
                //PP5Session.FindElementByAccessibilityId("OK_button")?.Click();

                CurrentDriver.GetElement(MobileBy.AccessibilityId("Username")).SendContent("root");
                CurrentDriver.GetElement(MobileBy.AccessibilityId("OK_button")).LeftClick();

                //// Tag current window type as MainPanelMenu in text file
                //WindowTypeHelper.UpdateCurrentWindowType(WindowType.MainPanelMenu);
            }
            catch (Exception ex)
            {
                Logger.LogMessage(ex.ToString());
                return;
            }
        }

        // At MainPanel, Open New TI
        public void OpenNewTIEditorWindow()
        {
            // Delete TI Backup file if existed
            //if (File.Exists(Path.Combine(PowerPro5Config.ReleaseDataFolder, "TestItemBackup.tix")))
            //    File.Delete(Path.Combine(PowerPro5Config.ReleaseDataFolder, "TestItemBackup.tix"));
            PowerPro5Config.ReleaseDataFolder.DeleteFilesWithDifferentExtension("TestItemBackup");

            if (!CheckMainPanelIsOpened())
            {
                // Switch to PP5 IDE session
                AutoUIExecutor.SwitchTo(SessionType.PP5IDE);

                //if (CheckPP5WindowIsOpened(WindowType.TIEditor))
                //{
                // Check how many windows need to close
                //IEnumerable<string> openedWindows = GetIntersectionWithOrder(GetSubMenuListItemNames("Windows"), moduleNames);

                //// If TI Editor window existed, close all windows
                //if (openedWindows.Contains(WindowType.TIEditor.GetDescription()))
                //{
                //    int windowNumbersToClose = openedWindows.Count();

                //    for (int i = 0; i < windowNumbersToClose; i++)
                //    {
                //        CurrentDriver.GetElement(MobileBy.AccessibilityId("CloseButton")).LeftClick();

                //        // If save window message box popup, click No
                //        if (CurrentDriver.CheckElementExisted(MobileBy.AccessibilityId("MessageBoxExDialog")))
                //            CurrentDriver.GetElement(timeOut: 5000, MobileBy.AccessibilityId("MessageBoxExDialog"),
                //                                                    By.Name("No")).LeftClick();
                //    }
                //}
                // Close all existing windows
                while (IsPP5Window(PP5IDEWindow.Text))
                {
                    // Click close button
                    PP5IDEWindow.GetElement(MobileBy.AccessibilityId("CloseButton")).LeftClick();

                    // If save window message box popup, click No
                    if (CurrentDriver.CheckElementExisted(MobileBy.AccessibilityId("MessageBoxExDialog")))
                        PP5IDEWindow.GetElement(timeOut: SharedSetting.NORMAL_TIMEOUT, MobileBy.AccessibilityId("MessageBoxExDialog"),
                                                                By.Name("No")).LeftClick();
                }


                // Open New TI Editor
                MenuSelect("Functions", "TI Editor");
                //WaitUntil(() => .Displayed);
                // Adam, 2024/07/08, only get PP5 IDE window when module is launched
                WaitUntil(() => PP5IDEWindow.Displayed);
                //isIDEWindowPresent = true;
            }
            else
            {
                // Click "Test Item" button in MainPanel
                CurrentDriver.GetElement(By.Name("Test Item")).LeftClick();

                System.Threading.SpinWait.SpinUntil(() => Process.GetProcessesByName(PowerPro5Config.IDEProcessName).Count() >= 2, 10000);

                //                foreach (var pp5IDE in Process.GetProcessesByName(PowerPro5Config.IDEProcessName))
                //                {
                //                    // Wait for pp5IDE's MainWindowHandle is created
                //                    if (pp5IDE.MainWindowHandle != new IntPtr())
                //                        break;

                ////                    while (pp5IDE.MainWindowHandle == new IntPtr())
                ////                    {
                ////                        System.Threading.Thread.Sleep(100);
                ////#if WRITE_LOG
                ////                    Console.WriteLine($"pp5IDE.MainWindowHandle:{pp5IDE.MainWindowHandle}");
                ////                    Console.WriteLine("IDE not ready yet, sleep for 100ms.");
                ////#endif
                ////                    }
                //                }

                while (Process.GetProcessesByName(PowerPro5Config.IDEProcessName).All(p => p.MainWindowHandle == new IntPtr()))
                {
                    System.Threading.Thread.Sleep(100);
                }

                //AutoUIExecutor.StartIDESession();
                AutoUIExecutor.SwitchTo(SessionType.PP5IDE);
            }

            // Open New TI
            PerformOpenNewTI(false);

            // Wait until TIEditor window is opened
            WaitUntil(() => CheckPP5WindowIsOpened(WindowType.TIEditor));
        }

        // At MainPanel, Open New TPEditor Window
        public void OpenNewTPEditorWindow()
        {
            // Delete TP Backup file if existed
            PowerPro5Config.ReleaseDataFolder.DeleteFilesWithDifferentExtension("TestProgramBackup");
            //if (File.Exists(Path.Combine(PowerPro5Config.ReleaseDataFolder, "TestProgramBackup.tpx")))
            //    File.Delete(Path.Combine(PowerPro5Config.ReleaseDataFolder, "TestProgramBackup.tpx"));

            if (!CheckMainPanelIsOpened())
            {
                // Switch to PP5 IDE session
                AutoUIExecutor.SwitchTo(SessionType.PP5IDE);

                // Check how many windows need to close
                IEnumerable<string> openedWindows = GetIntersectionWithOrder(GetSubMenuListItemNames("Windows"), moduleNames);


                // If TP Editor window existed, close all windows
                //if (openedWindows.Contains(WindowType.TPEditor.GetDescription()))
                //{
                //    int windowNumbersToClose = openedWindows.Count();

                //    for (int i = 0; i < windowNumbersToClose; i++)
                //    {
                //        CurrentDriver.GetElement(MobileBy.AccessibilityId("CloseButton")).LeftClick();

                //        // If save window message box popup, click No
                //        if (CurrentDriver.CheckElementExisted(MobileBy.AccessibilityId("MessageBoxExDialog")))
                //            CurrentDriver.GetElement(timeOut: 5000, MobileBy.AccessibilityId("MessageBoxExDialog"),
                //                                                    By.Name("No")).LeftClick();
                //    }
                //}
                while (IsPP5Window(PP5IDEWindow.Text))
                {
                    // Click close button
                    PP5IDEWindow.GetElement(MobileBy.AccessibilityId("CloseButton")).LeftClick();

                    // If save window message box popup, click No
                    if (PP5IDEWindow.GetElement(MobileBy.AccessibilityId("MessageBoxExDialog")) != null)
                        PP5IDEWindow.GetElement(timeOut: 5000, MobileBy.AccessibilityId("MessageBoxExDialog"),
                                                                By.Name("No")).LeftClick();
                }

                // Open New TP Editor
                MenuSelect("Functions", "TP Editor");
                WaitUntil(() => PP5IDEWindow.Displayed);
                //isIDEWindowPresent = true;
            }
            else
            {
                // Click "Test Program" button in MainPanel
                CurrentDriver.GetElement(By.Name("Test Program")).LeftClick();

                System.Threading.SpinWait.SpinUntil(() => Process.GetProcessesByName(PowerPro5Config.IDEProcessName).Count() >= 2, 10000);
                while (Process.GetProcessesByName(PowerPro5Config.IDEProcessName).All(p => p.MainWindowHandle == new IntPtr()))
                {
                    System.Threading.Thread.Sleep(100);
                }

                //                foreach (var pp5IDE in Process.GetProcessesByName(PowerPro5Config.IDEProcessName))
                //                {
                //                    // Wait for pp5IDE's MainWindowHandle is created
                //                    while (pp5IDE.MainWindowHandle == new IntPtr())
                //                    {
                //                        System.Threading.Thread.Sleep(100);
                //#if WRITE_LOG
                //                    Console.WriteLine($"pp5IDE.MainWindowHandle:{pp5IDE.MainWindowHandle}");
                //                    Console.WriteLine("IDE not ready yet, sleep for 100ms.");
                //#endif
                //                    }
                //                }

                AutoUIExecutor.SwitchTo(SessionType.PP5IDE);
            }

            PerformOpenNewTP(false);

            // Wait until TPEditor window is opened
            WaitUntil(() => CheckPP5WindowIsOpened(WindowType.TPEditor));
        }

        // At MainPanel, Open New GUI Editor
        public void OpenDefaultGUIEditorWindow()
        {
            if (!CheckMainPanelIsOpened())
            {
                // Switch to PP5 IDE session
                AutoUIExecutor.SwitchTo(SessionType.PP5IDE);

                // Check how many windows need to close
                IEnumerable<string> openedWindows = GetIntersectionWithOrder(GetSubMenuListItemNames("Windows"), moduleNames);

                // If GUI Editor window existed, close all windows
                if (openedWindows.Contains(WindowType.GUIEditor.GetDescription()))
                {
                    int windowNumbersToClose = openedWindows.Count();

                    for (int i = 0; i < windowNumbersToClose; i++)
                    {
                        CurrentDriver.GetElement(MobileBy.AccessibilityId("CloseButton")).LeftClick();

                        // If save window message box popup, click No
                        if (CurrentDriver.CheckElementExisted(MobileBy.AccessibilityId("MessageBoxExDialog")))
                            CurrentDriver.GetElement(timeOut: 5000, MobileBy.AccessibilityId("MessageBoxExDialog"),
                                                                    By.Name("No")).LeftClick();
                    }
                }

                MenuSelect("Functions", "GUI Editor");

                //if (CheckPP5WindowIsOpened(WindowType.GUIEditor))
                //{
                //    // If GUI window existed, open a new GUI template
                //    MenuSelect("File", "New");
                //}
            }
            else
            {
                // Click "GUI Editor" button in MainPanel
                CurrentDriver.GetElement(By.Name("GUI Editor")).LeftClick();

                System.Threading.SpinWait.SpinUntil(() => Process.GetProcessesByName(PowerPro5Config.IDEProcessName).Count() >= 2, 10000);
                while (Process.GetProcessesByName(PowerPro5Config.IDEProcessName).All(p => p.MainWindowHandle == new IntPtr()))
                {
                    System.Threading.Thread.Sleep(100);
                }

                AutoUIExecutor.SwitchTo(SessionType.PP5IDE);

                //                foreach (var pp5IDE in Process.GetProcessesByName(PowerPro5Config.IDEProcessName))
                //                {
                //                    // Wait for pp5IDE's MainWindowHandle is created
                //                    while (pp5IDE.MainWindowHandle == new IntPtr())
                //                    {
                //                        System.Threading.Thread.Sleep(100);
                //#if WRITE_LOG
                //                    Console.WriteLine($"pp5IDE.MainWindowHandle:{pp5IDE.MainWindowHandle}");
                //                    Console.WriteLine("IDE not ready yet, sleep for 100ms.");
                //#endif
                //                    }
                //                }

                //while(!CheckPP5WindowIsOpened(WindowType.GUIEditor)){}

                //System.Threading.SpinWait.SpinUntil(() => CheckPP5WindowIsOpened(WindowType.GUIEditor));
                //WaitUntil(() => CheckPP5WindowIsOpened(WindowType.GUIEditor));

                //System.Threading.SpinWait.SpinUntil(() => Process.GetProcessesByName(PowerPro5Config.IDEProcessName).Count() >= 2, 10000);
                //foreach (var pp5IDE in Process.GetProcessesByName(PowerPro5Config.IDEProcessName))
                //    pp5IDE.WaitForInputIdle();

            }
            // Wait until GUIEditor window is opened
            WaitUntil(() => CheckPP5WindowIsOpened(WindowType.GUIEditor));

            MenuSelect("File", "New");

            // Wait until GUIEditor window is opened
            WaitUntil(() => CheckPP5WindowIsOpened(WindowType.GUIEditor));
        }

        // At MainPanel, Open New Report window
        public void OpenDefaultReportWindow()
        {
            if (!CheckMainPanelIsOpened())
            {
                // Switch to PP5 IDE session
                AutoUIExecutor.SwitchTo(SessionType.PP5IDE);

                // Check how many windows need to close
                IEnumerable<string> openedWindows = GetIntersectionWithOrder(GetSubMenuListItemNames("Windows"), moduleNames);

                // If Report window existed, close all windows
                if (openedWindows.Contains(WindowType.Report.GetDescription()))
                {
                    int windowNumbersToClose = openedWindows.Count();

                    for (int i = 0; i < windowNumbersToClose; i++)
                    {
                        CurrentDriver.GetElement(MobileBy.AccessibilityId("CloseButton")).LeftClick();

                        // If save window message box popup, click No
                        if (CurrentDriver.CheckElementExisted(MobileBy.AccessibilityId("MessageBoxExDialog")))
                            CurrentDriver.GetElement(timeOut: 5000, MobileBy.AccessibilityId("MessageBoxExDialog"),
                                By.Name("No")).LeftClick();
                    }
                }

                MenuSelect("Functions", "Report");

                //if (CheckPP5WindowIsOpened(WindowType.Report))
                //{
                //    // If Report window existed, open a new Report template
                //    MenuSelect("File", "New");

                //    // If save Report window message box popup, click No
                //    if (CurrentDriver.CheckElementExisted(By.Name("Report")))
                //        CurrentDriver.GetElement(timeOut: 5000, By.Name("Report"),
                //                                                By.Name("No")).LeftClick();
                //}
                //else
                //    MenuSelect("Functions", "Report");

                //System.Threading.SpinWait.SpinUntil(() => CheckPP5WindowIsOpened(WindowType.Report), 15000);
            }
            else
            {
                // Click "Report" button in MainPanel
                CurrentDriver.GetElement(By.Name("Report")).LeftClick();

                //while(!CheckPP5WindowIsOpened(WindowType.GUIEditor)){}
                //System.Threading.SpinWait.SpinUntil(() => CheckPP5WindowIsOpened(WindowType.Report), 15000);

                //System.Threading.SpinWait.SpinUntil(() => Process.GetProcessesByName(PowerPro5Config.IDEProcessName).Count() >= 2, 10000);
                System.Threading.SpinWait.SpinUntil(() => Process.GetProcessesByName(PowerPro5Config.IDEProcessName).Count() >= 2, 10000);
                while (Process.GetProcessesByName(PowerPro5Config.IDEProcessName).All(p => p.MainWindowHandle == new IntPtr()))
                {
                    System.Threading.Thread.Sleep(100);
                }

                AutoUIExecutor.SwitchTo(SessionType.PP5IDE);

                //                foreach (var pp5IDE in Process.GetProcessesByName(PowerPro5Config.IDEProcessName))
                //                {
                //                    // Wait for pp5IDE's MainWindowHandle is created
                //                    while (pp5IDE.MainWindowHandle == new IntPtr())
                //                    {
                //                        System.Threading.Thread.Sleep(100);
                //#if WRITE_LOG
                //                    Console.WriteLine($"pp5IDE.MainWindowHandle:{pp5IDE.MainWindowHandle}");
                //                    Console.WriteLine("IDE not ready yet, sleep for 100ms.");
                //#endif
                //                    }
                //                }


                //System.Threading.SpinWait.SpinUntil(() => CheckPP5WindowIsOpened(WindowType.Report));
                //WaitUntil(() => CheckPP5WindowIsOpened(WindowType.Report));
            }

            // Wait until report window is opened
            WaitUntil(() => CheckPP5WindowIsOpened(WindowType.Report));
        }

        // At MainPanel, Open New Management window
        public void OpenDefaultManagementWindow()
        {
            if (!CheckMainPanelIsOpened())
            {
                // Switch to PP5 IDE session
                AutoUIExecutor.SwitchTo(SessionType.PP5IDE);

                //// Check how many windows need to close
                //IEnumerable<string> openedWindows = GetIntersectionWithOrder(GetSubMenuListItemNames("Windows"), moduleNames);

                //// If Management window existed, close all windows
                //if (openedWindows.Contains(WindowType.Management.GetDescription()))
                //{
                //    int windowNumbersToClose = openedWindows.Count();

                //    for (int i = 0; i < windowNumbersToClose; i++)
                //    {
                //        CurrentDriver.GetElement(MobileBy.AccessibilityId("CloseButton")).LeftClick();

                //        // If save window message box popup, click No
                //        if (CurrentDriver.CheckElementExisted(MobileBy.AccessibilityId("MessageBoxExDialog")))
                //            CurrentDriver.GetElement(timeOut: 5000, MobileBy.AccessibilityId("MessageBoxExDialog"),
                //                                                    By.Name("No")).LeftClick();
                //    }
                //}

                // Close all existing windows
                while (IsPP5Window(PP5IDEWindow.Text))
                {
                    // Click close button
                    PP5IDEWindow.GetElement(MobileBy.AccessibilityId("CloseButton")).LeftClick();

                    // If save window message box popup, click No
                    if (CurrentDriver.CheckElementExisted(MobileBy.AccessibilityId("MessageBoxExDialog")))
                        PP5IDEWindow.GetElement(timeOut: 5000, MobileBy.AccessibilityId("MessageBoxExDialog"),
                                                                By.Name("No")).LeftClick();
                }

                MenuSelect("Functions", "Management");

                //if (CheckPP5WindowIsOpened(WindowType.Management))
                //{
                //    // If Management window existed, do nothing
                //}
                //else
                //    MenuSelect("Functions", "Management");

                //System.Threading.SpinWait.SpinUntil(() => CheckPP5WindowIsOpened(WindowType.Management), 3000);
                //WaitUntil(() => GetPP5Window().Displayed);
            }
            else
            {
                // Click "Management" button in MainPanel
                CurrentDriver.GetElement(By.Name("Management")).LeftClick();

                //AutoUIExecutor.SwitchTo(SessionType.PP5IDE);

                //while (!CheckPP5WindowIsOpened(WindowType.Management))
                //while (!System.Threading.SpinWait.SpinUntil(() => CheckPP5WindowIsOpened(WindowType.Management), 15000))
                //{
                //    var sessDetaileds = currentDriver.SessionDetails;
                //    AutoUIExecutor.SwitchTo(SessionType.PP5IDE);
                //}
                System.Threading.SpinWait.SpinUntil(() => Process.GetProcessesByName(PowerPro5Config.IDEProcessName).Count() >= 2, 10000);
                while (Process.GetProcessesByName(PowerPro5Config.IDEProcessName).All(p => p.MainWindowHandle == new IntPtr()))
                {
                    System.Threading.Thread.Sleep(100);
                }
                AutoUIExecutor.SwitchTo(SessionType.PP5IDE);

                //                foreach (var pp5IDE in Process.GetProcessesByName(PowerPro5Config.IDEProcessName))
                //                {
                //                    // Wait for pp5IDE's MainWindowHandle is created
                //                    while (pp5IDE.MainWindowHandle == new IntPtr())
                //                    {
                //                        System.Threading.Thread.Sleep(100);
                //#if WRITE_LOG
                //                    Console.WriteLine($"pp5IDE.MainWindowHandle:{pp5IDE.MainWindowHandle}");
                //                    Console.WriteLine("IDE not ready yet, sleep for 100ms.");
                //#endif
                //                    }
                //                }

                //System.Threading.SpinWait.SpinUntil(() => CheckPP5WindowIsOpened(WindowType.Management));
                //WaitUntil(() => CheckPP5WindowIsOpened(WindowType.Management), 5000);
            }

            // Wait until Management window is opened
            WaitUntil(() => CheckPP5WindowIsOpened(WindowType.Management));
        }

        // At MainPanel, Open New Execution Window
        public void OpenDefaultExecutionWindow(string tpName)
        {
            if (!CheckMainPanelIsOpened())
            {
                // Switch to PP5 IDE session
                AutoUIExecutor.SwitchTo(SessionType.PP5IDE);

                // Check how many windows need to close
                IEnumerable<string> openedWindows = GetIntersectionWithOrder(GetSubMenuListItemNames("Windows"), moduleNames);

                // If Execution window existed, close all windows
                if (openedWindows.Contains(WindowType.Execution.GetDescription()))
                {
                    int windowNumbersToClose = openedWindows.Count();

                    for (int i = 0; i < windowNumbersToClose; i++)
                    {
                        CurrentDriver.GetElement(MobileBy.AccessibilityId("CloseButton")).LeftClick();

                        // If save window message box popup, click No
                        if (CurrentDriver.CheckElementExisted(MobileBy.AccessibilityId("MessageBoxExDialog")))
                            CurrentDriver.GetElement(timeOut: 5000, MobileBy.AccessibilityId("MessageBoxExDialog"),
                                                                    By.Name("No")).LeftClick();
                    }
                }

                MenuSelect("Functions", "Execution");
                //WaitUntil(() => GetPP5Window().Displayed);
                // Adam, 2024/07/08, only get PP5 IDE window when module is launched
                WaitUntil(() => PP5IDEWindow.Displayed);
                //isIDEWindowPresent = true;

                //if (CheckPP5WindowIsOpened(WindowType.Execution))
                //{
                //    // If Execution window existed, open an existing TP
                //    MenuSelect("File", "OpenTestProgram...");
                //}
                //else
                //{
                //    MenuSelect("Functions", "Execution");
                //}
                //IWebElement TPtoOpen = CurrentDriver.GetElement(By.Name("Open"))
                //                                    .GetCellBy(1, 2);

                ////string TPName = TPtoOpen.Text;
                //TPtoOpen.LeftClick();
                //CurrentDriver.GetElement(5000, By.Name("Open"), By.Name("Ok"))
                //             .LeftClick();

                //while(GetPP5Window().GetEditContent(1) != TPtoOpen.Text){}
                //System.Threading.SpinWait.SpinUntil(() => GetPP5Window().GetElement(By.ClassName("FullScreenExecutionStateAeraView"))
                //                                                        .GetFirstPaneElement()
                //                                                        .GetEditElement(1).GetCellValue() == tpName, 3000);
            }
            else
            {
                // Click "Execution" button in MainPanel
                CurrentDriver.GetElement(By.Name("Execution")).LeftClick();

                System.Threading.SpinWait.SpinUntil(() => Process.GetProcessesByName(PowerPro5Config.IDEProcessName).Count() >= 2, 10000);
                while (Process.GetProcessesByName(PowerPro5Config.IDEProcessName).All(p => p.MainWindowHandle == new IntPtr()))
                {
                    System.Threading.Thread.Sleep(100);
                }
                AutoUIExecutor.SwitchTo(SessionType.PP5IDE);

                //                foreach (var pp5IDE in Process.GetProcessesByName(PowerPro5Config.IDEProcessName))
                //                {
                //                    // Wait for pp5IDE's MainWindowHandle is created
                //                    while (pp5IDE.MainWindowHandle == new IntPtr())
                //                    {
                //                        System.Threading.Thread.Sleep(100);
                //#if WRITE_LOG
                //                    Console.WriteLine($"pp5IDE.MainWindowHandle:{pp5IDE.MainWindowHandle}");
                //                    Console.WriteLine("IDE not ready yet, sleep for 100ms.");
                //#endif
                //                    }
                //                }

                //System.Threading.Thread.Sleep(5000);

                //System.Threading.SpinWait.SpinUntil(() => GetPP5Window().GetElement(By.ClassName("FullScreenExecutionStateAeraView"))
                //    .GetFirstPaneElement()
                //    .GetEditElement(1).GetCellValue() == tpName);
                //WaitUntil(() => GetPP5Window().GetElement(By.ClassName("FullScreenExecutionStateAeraView"))
                //                              .GetFirstPaneElement()
                //                              .GetEditElement(1).GetCellValue() == tpName, 8000);
            }

            PerformOpenTPFile(tpName);

            // Wait until execution window is loaded
            // Adam, 2024/07/08, only get PP5 IDE window when module is launched
            WaitUntil(() => PP5IDEWindow.GetElement(By.ClassName("FullScreenExecutionStateAeraView"))
                                        .GetFirstPaneElement()
                                        .GetEditElement(1).GetCellValue() == tpName, 8000);
            //isIDEWindowPresent = true;
        }

        //public void PerformOpenNewGUIFile()
        //{
        //    MenuSelect("File", "New");
        //}

        public void PerformOpenTPFile(string TPName)
        {
            CurrentDriver.GetElement(By.Name("Open"))
                         .GetFirstDataGridElement()
                         .GetCellByName(2, TPName)
                         .LeftClick();

            CurrentDriver.GetElement(5000, By.Name("Open"), By.Name("Ok"))
                         .LeftClick();
        }

        public void PerformOpenNewTP(bool tpNotSaved)
        {
            // If save TP window message box popup, click No
            if (tpNotSaved)
            {
                if (CurrentDriver.CheckElementExisted(By.Name("Exit")))
                    CurrentDriver.GetElement(timeOut: 5000, By.Name("Exit"),
                                                            By.Name("No")).LeftClick();
            }

            //if (fromMainPanel)
            //{
            // LeftClick on RadioButton "New Test Program"
            Logger.LogMessage("LeftClick on RadioButton \"New Test Program\"");
            CurrentDriver.GetElement(timeOut: 5000, By.Name("Enter Point"),
                                                    MobileBy.AccessibilityId("NewRadioBtn")).LeftClick();

            // Enter Point window, LeftClick on Text "Ok"
            Logger.LogMessage("LeftClick on Text \"Ok\"");
            CurrentDriver.GetElement(timeOut: 5000, By.Name("Enter Point"),
                                                    By.Name("Ok")).LeftClick();
            //}

            // New Test Program window, LeftClick on Button "Ok"
            Logger.LogMessage("LeftClick on Button \"Ok\"");
            CurrentDriver.GetElement(timeOut: 5000, By.Name("New Test Program"),
                                                    By.Name("Ok")).LeftClick();

            // If save TP window message box popup, click No
            CurrentDriver.SwitchToWindow(out _);
            if (CurrentDriver.CheckElementExisted(MobileBy.AccessibilityId("MessageBoxExDialog")))
                CurrentDriver.GetElement(timeOut: 5000, MobileBy.AccessibilityId("MessageBoxExDialog"),
                                                        By.Name("OK")).LeftClick();
        }

        public void PerformOpenNewTI(bool tiNotSaved = true)
        {
            // If save TI window message box popup, click No
            if (tiNotSaved)
            {
                if (CurrentDriver.CheckElementExisted(By.Name("Exit")))
                    CurrentDriver.GetElement(timeOut: SharedSetting.NORMAL_TIMEOUT, 
                                                      By.Name("Exit"),
                                                      By.Name("No")).LeftClick();
            }

            // LeftClick on RadioButton "New Test Item"
            //if (fromMainPanel)
            //{
            //Console.WriteLine("LeftClick on RadioButton \"New Test Item\"");
            PP5IDEWindow.GetElementWithRetry(timeOut: SharedSetting.NORMAL_TIMEOUT, nTryCount: 2,
                                                      By.Name("Enter Point"),
                                                      MobileBy.AccessibilityId("NewRadioBtn")).LeftClick();
            //Thread.Sleep(1500);

            // Enter Point window, LeftClick on Text "Ok"
            //Console.WriteLine("LeftClick on Text \"Ok\"");
            PP5IDEWindow.GetElement(timeOut: SharedSetting.NORMAL_TIMEOUT, 
                                             By.Name("Enter Point"),
                                             By.Name("Ok")).LeftClick();
            //}

            // New Test Item window, LeftClick on Button "Ok"
            //Console.WriteLine("LeftClick on Button \"Ok\"");
            PP5IDEWindow.GetElement(timeOut: SharedSetting.NORMAL_TIMEOUT, 
                                             MobileBy.AccessibilityId("LoginDialog"),
                                             MobileBy.AccessibilityId("OkBtn")).LeftClick();

            //// Close time consuming message box (Debug Mode)
            //// If save TI window message box popup, click No
            //CurrentDriver.SwitchToWindow(out _);
            //if (CurrentDriver.CheckElementExisted(MobileBy.AccessibilityId("MessageBoxExDialog")))
            //    PP5IDEWindow.GetElement(timeOut: 5000, MobileBy.AccessibilityId("MessageBoxExDialog"),
            //                                            By.Name("OK")).LeftClick();

            //AutoUIExecutor.SwitchTo(SessionType.PP5IDE);
        }

        public void PerformOpenNewTI(TestItemType tiType, TestItemRunType tiRunType)
        {
            // Get the radio button text of given item type and run type
            string tiTypeText = tiType.GetDescription();
            string tiRunTypeText = tiRunType.GetDescription();

            // LeftClick on RadioButton "New Test Item"
            Logger.LogMessage("LeftClick on RadioButton \"New Test Item\"");
            CurrentDriver.GetElement(timeOut: 5000, By.Name("Enter Point"),
                                                    MobileBy.AccessibilityId("NewRadioBtn")).LeftClick();

            // Enter Point window, LeftClick on Text "Ok"
            Logger.LogMessage("LeftClick on Text \"Ok\"");
            CurrentDriver.GetElement(timeOut: 5000, By.Name("Enter Point"),
                                                    By.Name("Ok")).LeftClick();

            // New Test Item window, select TI Type & Run Type
            CurrentDriver.GetElement(timeOut: 5000, MobileBy.AccessibilityId("LoginDialog"),
                                                    By.Name(tiTypeText)).LeftClick();
            CurrentDriver.GetElement(timeOut: 5000, MobileBy.AccessibilityId("LoginDialog"),
                                                    By.Name(tiRunTypeText)).LeftClick();

            // New Test Item window, LeftClick on Button "Ok"
            Logger.LogMessage("LeftClick on Button \"Ok\"");
            CurrentDriver.GetElement(timeOut: 5000, MobileBy.AccessibilityId("LoginDialog"),
                                                    MobileBy.AccessibilityId("OkBtn")).LeftClick();

            // Close time consuming message box (Debug Mode)
            // If save TI window message box popup, click No
            CurrentDriver.SwitchToWindow(out _);
            if (CurrentDriver.CheckElementExisted(MobileBy.AccessibilityId("MessageBoxExDialog")))
                CurrentDriver.GetElement(timeOut: 5000, MobileBy.AccessibilityId("MessageBoxExDialog"),
                                                        By.Name("OK")).LeftClick();
        }

        public void PerformLoadOldTI(string tiName)
        {
            // LeftClick on RadioButton "Load Test Item"
            //Console.WriteLine("LeftClick on RadioButton \"Load Test Item\"");

            IWebElement TIEnterWindow = PP5IDEWindow.GetElement(By.Name("Enter Point"));

            TIEnterWindow.GetRdoBtnElement("LoadRadioBtn").LeftClick();

            // Enter Point window, LeftClick on Text "Ok"
            //Console.WriteLine("LeftClick on Text \"Ok\"");
            TIEnterWindow.GetBtnElement("Ok").LeftClick();

            //RefreshDataTable(DataTableAutoIDType.LoginGrid);
            //List<string> existingTINames = GetSingleColumnValues(DataTableAutoIDType.LoginGrid, "Test Item Name", excludeLastRow: false);

            //if (!existingTINames.Contains(tiName))
            //    Assert.Fail($"No TI existed with given TI Name: {tiName}");


            // Search the TI
            IWebElement LoadTIWindow = PP5IDEWindow.GetElement(By.Name("Load Test Item"));

            LoadTIWindow.GetEditElement("searchText").SendContent(tiName);
            Press(Keys.Enter);

            while (LoadTIWindow.GetSelectedRow("LoginGrid").GetCellValue(1 /*"Test Item Name"*/) != tiName)
            {
                LoadTIWindow.GetBtnElement("NextBtn").LeftClick();
            }

            // Load Test Item window, LeftClick on Button "Ok"
            //Console.WriteLine("LeftClick on Button \"Ok\"");
            //PP5IDEWindow.GetElement(timeOut: 5000, MobileBy.AccessibilityId("LoginDialog"),
            //                                        MobileBy.AccessibilityId("OkBtn")).LeftClick();

            // If TI is found, in Load Test Item window, LeftClick on Button "Ok"
            LoadTIWindow.GetBtnElement("Ok").LeftClick();

            // Close time consuming message box (Debug Mode)
            // If save TI window message box popup, click No
            //AutoUIExecutor.SwitchTo(SessionType.PP5IDE);
            //if (CurrentDriver.CheckElementExisted(MobileBy.AccessibilityId("MessageBoxExDialog")))
            //CurrentDriver.GetElement(timeOut: 5000, MobileBy.AccessibilityId("MessageBoxExDialog"),
            //                                        By.Name("OK")).LeftClick();
        }

        public void PerformLoadOldTI(string tiName, TestItemType type = TestItemType.TI, TestItemRunType runType = TestItemRunType.UUT)
        {
            // LeftClick on RadioButton "Load Test Item"
            //Console.WriteLine("LeftClick on RadioButton \"Load Test Item\"");
            //PP5IDEWindow.GetElement(timeOut: 5000, By.Name("Enter Point"),
            //                                        MobileBy.AccessibilityId("LoadRadioBtn")).LeftClick();
            PP5IDEWindow/*.GetWindowElement("Enter Point")*/
                        .GetElement(MobileBy.AccessibilityId("LoadRadioBtn"))
                        .LeftClick();

            // Enter Point window, LeftClick on Text "Ok"
            //Console.WriteLine("LeftClick on Text \"Ok\"");
            //PP5IDEWindow.GetElement(timeOut: 5000, By.Name("Enter Point"),
            //                                        By.Name("Ok")).LeftClick();
            PP5IDEWindow.GetElement(By.Name("Enter Point"))
                        .GetBtnElement("Ok")
                        .LeftClick();

            // Choose the type and run type
            //PP5IDEWindow.GetElement(MobileBy.AccessibilityId("LoginDialog"))
            //             .GetElement(By.Name(type.GetDescription())).LeftClick();
            PP5IDEWindow.GetElement(By.Name("Load Test Item"))
                        .GetRdoBtnElement(type.GetDescription())
                        .LeftClick();

            //PP5IDEWindow.GetElement<ElementControlType.Window, ElementControlType.RadioButton>
            //                        ("Load Test Item", By.Name(type.GetDescription()))

            //GetRdoBtnElement(type.GetDescription()).LeftClick();
            //PP5IDEWindow.GetElement(MobileBy.AccessibilityId("Load Test Item"))
            //             .GetElement(By.Name(runType.GetDescription())).LeftClick();
            PP5IDEWindow.GetElement(By.Name("Load Test Item"))
                        .GetRdoBtnElement(runType.GetDescription())
                        .LeftClick();

            //.GetRdoBtnElement(runType.GetDescription()).LeftClick();

            //RefreshDataTable(DataTableAutoIDType.LoginGrid);
            //List<string> existingTINames = GetSingleColumnValues(DataTableAutoIDType.LoginGrid, "Test Item Name", excludeLastRow: false);
            //if (!existingTINames.Contains(tiName))
            //    Assert.Fail($"No TI existed with given TI Name: {tiName}");

            //GetCellBy("LoginGrid", existingTINames.IndexOf(tiName), "Test Item Name").LeftClick();
            PP5IDEWindow.GetFirstDataGridElement()
                        .GetCellByName(1, tiName)
                        .LeftClick();

            // Load Test Item window, LeftClick on Button "Ok"
            //Console.WriteLine("LeftClick on Button \"Ok\"");
            //PP5IDEWindow.GetElement(timeOut: 5000, MobileBy.AccessibilityId("LoginDialog"),
            //                                        MobileBy.AccessibilityId("OkBtn")).LeftClick();
            PP5IDEWindow.GetElement(By.Name("Load Test Item"))
                        .GetBtnElement("Ok")
                        .LeftClick();

            // Close time consuming message box (Debug Mode)
            // If save TI window message box popup, click No
            //AutoUIExecutor.SwitchTo(SessionType.PP5IDE);
            //if (CurrentDriver.CheckElementExisted(MobileBy.AccessibilityId("MessageBoxExDialog")))
            //CurrentDriver.GetElement(timeOut: 5000, MobileBy.AccessibilityId("MessageBoxExDialog"),
            //                                        By.Name("OK")).LeftClick();
        }

        public void PerformLoadOldTI(string tiName, out string desc)
        {
            // LeftClick on RadioButton "Load Test Item"
            IWebElement TIEnterWindow = PP5IDEWindow.GetElement(By.Name("Enter Point"));

            TIEnterWindow.GetRdoBtnElement("LoadRadioBtn").LeftClick();

            // Enter Point window, LeftClick on Text "Ok"
            TIEnterWindow.GetBtnElement("Ok").LeftClick();

            IWebElement LoadTIWindow = PP5IDEWindow.GetElement(By.Name("Load Test Item"));

            // Search the TI
            LoadTIWindow.GetEditElement("searchText").SendContent(tiName);
            Press(Keys.Enter);

            while (LoadTIWindow.GetSelectedRow("LoginGrid").GetCellValue(0 /*"Test Item Name"*/) != tiName)
            {
                LoadTIWindow.GetBtnElement("NextBtn").LeftClick();
            }

            // Get Description result
            desc = LoadTIWindow.GetEditElement("DesTxtBox").Text;

            // If TI is found, in Load Test Item window, LeftClick on Button "Ok"
            LoadTIWindow.GetBtnElement("Ok").LeftClick();

            // Close time consuming message box (Debug Mode)
            // If save TI window message box popup, click No
            //AutoUIExecutor.SwitchTo(SessionType.PP5IDE);
            //CurrentDriver.GetElement(timeOut: 5000, MobileBy.AccessibilityId("MessageBoxExDialog"), By.Name("OK")).LeftClick();
        }

        public void PerformLoadTIBySearchingTIName(string tiName)
        {
            // LeftClick on RadioButton "Load Test Item"
            IWebElement TIEnterWindow = PP5IDEWindow.GetElement(By.Name("Enter Point"));

            TIEnterWindow.GetRdoBtnElement("LoadRadioBtn").LeftClick();

            // Enter Point window, LeftClick on Text "Ok"
            TIEnterWindow.GetBtnElement("Ok").LeftClick();

            // Search TI by TIName
            //IWebElement TISearchBox = CurrentDriver.GetElement(5000, MobileBy.AccessibilityId("LoginDialog"),
            //                                                         MobileBy.AccessibilityId("searchBox"));
            //TISearchBox.ClearContent();
            //TISearchBox.SendComboKeys(tiName, Keys.Enter);

            IWebElement LoadTIWindow = PP5IDEWindow.GetElement(By.Name("Load Test Item"));

            // Search the TI
            LoadTIWindow.GetEditElement("searchText").SendContent(tiName);
            Press(Keys.Enter);

            while (LoadTIWindow.GetSelectedRow("LoginGrid").GetCellValue(0 /*"Test Item Name"*/) != tiName)
            {
                LoadTIWindow.GetBtnElement("NextBtn").LeftClick();
            }

            // If TI is found, in Load Test Item window, LeftClick on Button "Ok"
            LoadTIWindow.GetBtnElement("Ok").LeftClick();

            // Close time consuming message box (Debug Mode)
            // If save TI window message box popup, click No
            //AutoUIExecutor.SwitchTo(SessionType.PP5IDE);
            //CurrentDriver.GetElement(timeOut: 5000, MobileBy.AccessibilityId("MessageBoxExDialog"), By.Name("OK")).LeftClick();
        }

        public void SaveAsNewTI(string tiName, bool isInputDescription = false, string desc = "")
        {
            // LeftClick on Text "File" > "Save As..."
            MenuSelect("File", "Save As...");

            //// Rename the TI Name
            //if (GetSingleColumnValues("LoginGrid", "Test Item Name").Contains(tiName))
            //    Assert.Fail($"Same TI existed with TI Name: {tiName}");

            PP5IDEWindow.GetElement(MobileBy.AccessibilityId("TINameTxtBox"))
                        .SendContent(tiName);

            if (isInputDescription)
                PP5IDEWindow.GetElement(MobileBy.AccessibilityId("DesTxtBox"))
                            .SendContent(desc);

            // LeftClick on Button "Ok"
            Logger.LogMessage("LeftClick on Button \"Ok\"");
            PP5IDEWindow.GetElement(By.Name("Save Test Item"))
                        .GetBtnElement("Ok")
                        .LeftClick();

            // Wait for Save As radiobutton enabled (TI is saved)
            WaitUntil(() => PP5IDEWindow.GetToolbarElement((e) => e.isElementVisible())
                                        .GetRdoBtnElement(4)
                                        .Enabled);
        }

        public void ChangeGroupAndSaveAsNewTI(string tiName, string group = "")
        {
            // LeftClick on Text "File" > "Save As..."
            MenuSelect("File", "Save As...");

            //// Rename the TI Name
            //if (GetSingleColumnValues("LoginGrid", "Test Item Name").Contains(tiName))
            //    Assert.Fail($"Same TI existed with TI Name: {tiName}");

            IWebElement SaveTIWindow = PP5IDEWindow.GetElement(By.Name("Save Test Item"));

            SaveTIWindow.GetEditElement("TINameTxtBox").SendContent(tiName);

            // Change the group
            //WindowsElement comboBox = GetComboBoxElementByID("GroupCmb");
            //if (CheckComboBoxHasItemByName(comboBox, group))
            //    ComboBoxSelectByName(comboBox, group);
            ComboBoxSelectByName("GroupCmb", group);

            // LeftClick on Button "Ok"
            SaveTIWindow.GetBtnElement("Ok").LeftClick();
        }

        public void VariableTabNavi(VariableTabType tabType)
        {
            IWebElement ele;
            switch (tabType)
            {
                case VariableTabType.Condition:
                    ele = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("CndRdoBtn"));
                    break;
                case VariableTabType.Result:
                    ele = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("RstRdoBtn"));
                    break;
                case VariableTabType.Temp:
                    ele = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("TmpRdoBtn"));
                    break;
                case VariableTabType.Global:
                    ele = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("GlbRdoBtn"));
                    break;
                case VariableTabType.DefectCode:
                    ele = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("DftRdoBtn"));
                    break;
                default:
                    ele = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("CndRdoBtn"));
                    break;
            }

            if (!ele.Selected)
                ele.LeftClick();
            Assert.IsTrue(ele.Selected);
        }

        public void TestItemTabNavi(TestItemTabType tabType)
        {
            IWebElement ele;
            switch (tabType)
            {
                case TestItemTabType.TIContext:
                    ele = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("TIRdoBtn"));
                    break;
                case TestItemTabType.TIDescription:
                    ele = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("TIDesRdoBtn"));
                    break;
                default:
                    ele = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("TIRdoBtn"));
                    break;
            }

            if (!ele.Selected)
                ele.LeftClick();
            Assert.IsTrue(ele.Selected);
        }

        public void TestItemListNavi(TestItemSourceType tiSType)
        {
            IWebElement ele;
            switch (tiSType)
            {
                case TestItemSourceType.System:
                    ele = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("SysRdoBtn"));
                    break;
                case TestItemSourceType.UserDefined:
                    ele = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("UserDefRdoBtn"));
                    break;
                default:
                    ele = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("SysRdoBtn"));
                    break;
            }

            if (!ele.Selected)
                ele.LeftClick();
            Assert.IsTrue(ele.Selected);
        }

        public void TestProgramTestTypeNavi(TestItemRunType tiRunType)
        {
            IWebElement ele;
            switch (tiRunType)
            {
                case TestItemRunType.Pre:
                    ele = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("PreTestRdoBtn"));
                    break;
                case TestItemRunType.UUT:
                    ele = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("UUTTestRdoBtn"));
                    break;
                case TestItemRunType.Post:
                    ele = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("PostTestRdoBtn"));
                    break;
                default:
                    ele = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("UUTTestRdoBtn"));
                    break;
            }

            if (!ele.Selected)
                ele.LeftClick();
            Assert.IsTrue(ele.Selected);
        }

        public void TPExecuteAction(TPAction tpAction)
        {
            switch (tpAction)
            {
                case TPAction.SwitchToPreTestPage:
                    TestProgramTestTypeNavi(TestItemRunType.Pre);
                    break;
                case TPAction.SwitchToUUTTestPage:
                    TestProgramTestTypeNavi(TestItemRunType.UUT);
                    break;
                case TPAction.SwitchToPostTestPage:
                    TestProgramTestTypeNavi(TestItemRunType.Post);
                    break;
                case TPAction.SwitchToSystemTIPage:
                    TestItemListNavi(TestItemSourceType.System);
                    break;
                case TPAction.SwitchToUserDefinedTIPage:
                    TestItemListNavi(TestItemSourceType.UserDefined);
                    break;
                case TPAction.SwitchToTestConditionVariablePage:
                    TestProgramAllSettingPageNavi(TestProgramSettingTabType.TestItem);
                    TestProgramParamInfoNavi(TestProgramParameterTabType.Condition);
                    break;
                case TPAction.SwitchToVectorVariablePage:
                    TestProgramAllSettingPageNavi(TestProgramSettingTabType.TestItem);
                    TestProgramParamInfoNavi(TestProgramParameterTabType.Vector);
                    break;
                case TPAction.SwitchToGlobalVariablePage:
                    TestProgramAllSettingPageNavi(TestProgramSettingTabType.TestItem);
                    TestProgramParamInfoNavi(TestProgramParameterTabType.Global);
                    break;
                case TPAction.SwitchToResultVariablePage:
                    TestProgramAllSettingPageNavi(TestProgramSettingTabType.TestItem);
                    TestProgramParamInfoNavi(TestProgramParameterTabType.Result);
                    break;
                case TPAction.SwitchToTPInfoPage:
                    TestProgramAllSettingPageNavi(TestProgramSettingTabType.TPInfo);
                    break;
                case TPAction.SwitchToReportFormatByTIPage:
                    TestProgramAllSettingPageNavi(TestProgramSettingTabType.RptFormat);
                    ReportFormatNavi(ReportFormatTabType.ByTI);
                    break;
                case TPAction.SwitchToReportFormatByTPPage:
                    TestProgramAllSettingPageNavi(TestProgramSettingTabType.RptFormat);
                    ReportFormatNavi(ReportFormatTabType.ByTP);
                    break;
                default:
                    TestProgramTestTypeNavi(TestItemRunType.UUT);
                    break;
            }
        }

        public void TestProgramAllSettingPageNavi(TestProgramSettingTabType tpSettingType)
        {
            IWebElement ele;
            switch (tpSettingType)
            {
                case TestProgramSettingTabType.TestItem:
                    ele = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("TIRdoBtn"));
                    break;
                case TestProgramSettingTabType.TPInfo:
                    ele = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("TPInfoRdoBtn"));
                    break;
                case TestProgramSettingTabType.RptFormat:
                    ele = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("ReportFormatRdoBtn"));
                    break;
                default:
                    ele = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("TIRdoBtn"));
                    break;
            }

            if (!ele.Selected)
                ele.LeftClick();
            Assert.IsTrue(ele.Selected);
        }

        public void TestProgramParamInfoNavi(TestProgramParameterTabType tiParamType)
        {
            IWebElement ele;
            switch (tiParamType)
            {
                case TestProgramParameterTabType.Condition:
                    ele = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("ParameterRdoBtn"));
                    break;
                case TestProgramParameterTabType.Vector:
                    ele = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("VectorRdoBtn"));
                    break;
                case TestProgramParameterTabType.Global:
                    ele = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("GlobalRdoBtn"));
                    break;
                case TestProgramParameterTabType.Result:
                    ele = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("ResultRdoBtn"));
                    break;
                default:
                    ele = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("ParameterRdoBtn"));
                    break;
            }

            if (!ele.Selected)
                ele.LeftClick();
            Assert.IsTrue(ele.Selected);
        }

        public void ReportFormatNavi(ReportFormatTabType rptFrmtType)
        {
            IWebElement ele;
            switch (rptFrmtType)
            {
                case ReportFormatTabType.ByTI:
                    ele = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("ByTIName"));
                    break;
                case ReportFormatTabType.ByTP:
                    ele = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("ByTPName"));
                    break;
                default:
                    ele = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("ByTIName"));
                    break;
            }

            if (!ele.Selected)
                ele.LeftClick();
            Assert.IsTrue(ele.Selected);
        }

        /// <summary>
        /// For variable: Condition, Global
        /// </summary>
        /// <param name="tabType"></param>
        /// <param name="ShowName"></param>
        /// <param name="CallName"></param>
        /// <param name="DataType">Ex: Float, Integer, etc...</param>
        /// <param name="EditType">Edit, ComboBox, External_Signal</param>
        public void CreateNewVariable1(VariableTabType tabType, string ShowName = "varSN", string CallName = "varCN",
                                       VariableDataType DataType = VariableDataType.Float, VariableEditType EditType = VariableEditType.EditBox,
                                       OrderedDictionary enumItems = null, int enumItemSelectionIndex = 0)
        {
            enumItems ??= new OrderedDictionary();

            VariableTabNavi(tabType);

            //RefreshDataTable((DataTableAutoIDType)tabType);

            int rowCount = GetRowCount((DataTableAutoIDType)tabType);

            /* Legacy Method (No Cache)
            //// Gets the datapanel in condition variable window
            //SaveGridTable(MobileBy.AccessibilityId("CndGrid"));

            //// First Add a new empty row
            //((WindowsElement)dataTableCache[0]["ShowName"]).DoubleClick();

            //// Input "a" in ShowName cell
            //((WindowsElement)dataTableCache[0]["ShowName"]).LeftClick();
            //((WindowsElement)dataTableCache[0]["ShowName"]).SendKeys("a");

            //// Input "a" in CallName cell
            //((WindowsElement)dataTableCache[0]["CallName"]).LeftClick();
            //((WindowsElement)dataTableCache[0]["CallName"]).SendKeys("a");

            //// Input "Float" in DisplayedType cell
            //((WindowsElement)dataTableCache[0]["DisplayedType"]).LeftClick();
            //((WindowsElement)dataTableCache[0]["DisplayedType"]).SendKeys("Float");
            //((WindowsElement)dataTableCache[0]["DisplayedType"]).LeftClick();

            //// Select "ComboBox" in DisplayedEditType combobox
            //((WindowsElement)dataTableCache[0]["DisplayedEditType"]).LeftClick();
            //((WindowsElement)dataTableCache[0]["DisplayedEditType"]).SendKeys("ComboBox");
            //((WindowsElement)dataTableCache[0]["DisplayedEditType"]).SendKeys(OpenQA.Selenium.Keys.Return);

            //// Click in DisplayedEnum cell
            //((WindowsElement)dataTableCache[0]["DisplayedEnum"]).LeftClick();
            //((WindowsElement)dataTableCache[0]["DisplayedEnum"]).DoubleClick();

            //// Check Enum Item Editor Window is opened
            //Assert.IsTrue(CurrentDriver.CheckElementExisted(By.Name("Enum Item Editor")));
            */

            //// Testing get data table element from cache
            // Press page down until last row show up
            string dataGridID = tabType.GetDescription();
            IWebElement datagridTmp = GetDataTableElement((DataTableAutoIDType)tabType);

            if (rowCount > 1 && datagridTmp.GetAttribute("Scroll.VerticallyScrollable") == bool.TrueString
                             && datagridTmp.GetAttribute("Scroll.VerticalScrollPercent") != "100")
            {
                GetCellBy(dataGridID, 0, "No").LeftClick();
                while (double.Parse(datagridTmp.GetAttribute("Scroll.VerticalScrollPercent")) <= 99.99)
                {
                    Press(Keys.PageDown);
                }
            }

            //RefreshDataTable((DataTableAutoIDType)tabType);
            IWebElement varDataGrid = GetDataTableElement((DataTableAutoIDType)tabType);
            int rowIndex = varDataGrid.GetRowCount() - 1;

            // First Add a new empty row
            varDataGrid.GetCellBy(rowIndex, "Show Name").DoubleClick();

            // Input "a" in Show Name cell
            //GetCellBy("CndGrid", 0, "Show Name").LeftClick();
            varDataGrid.GetCellBy(rowIndex, "Show Name").SendSingleKeys(ShowName);

            // Input "a" in Call Name cell
            //GetCellBy("CndGrid", 0, "Call Name").LeftClick();
            varDataGrid.GetCellBy(rowIndex, "Call Name").SendSingleKeys(CallName);

            // Input "Float" in Data Type cell
            //GetCellBy("CndGrid", 0, "Data Type").LeftClick();
            varDataGrid.GetCellBy(rowIndex, "Data Type").SendSingleKeys(DataType.GetDescription());
            Press(Keys.Enter);

            // Select "ComboBox" in Edit Type combobox
            //GetCellBy("CndGrid", 0, "Edit Type").LeftClick();
            varDataGrid.GetCellBy(rowIndex, "Edit Type").SendSingleKeys(EditType.ToString());
            Press(Keys.Enter);

            // If combobox, need to edit enum item, and select on Default
            if (EditType == VariableEditType.ComboBox)
            {
                IWebElement enumItemEditorWindow = null;

                // Click in Enum Item cell
                varDataGrid.GetCellBy(rowIndex, "Enum Item").LeftClick();

                foreach (DictionaryEntry enumItem in enumItems)
                {
                    enumItemEditorWindow = AddNewEnumItemByNameOrId(enumItem.Key.ToString(), enumItem.Value.ToString());
                }

                // Press OK to finish editing enum items
                enumItemEditorWindow.GetElement(By.Name("Ok")).LeftClick();

                // Select enum item in Default cell
                string SelectedEnumValue = enumItems.Count != 0 ? enumItems[enumItemSelectionIndex].ToString() : "";
                if (!SelectedEnumValue.IsEmpty())
                    varDataGrid.GetCellBy(rowIndex, "Default").SendSingleKeys(SelectedEnumValue);
                Press(Keys.Enter);
            }
        }

        /// <summary>
        /// For variable: Result, Temp
        /// </summary>
        /// <param name="tabType"></param>
        /// <param name="ShowName"></param>
        /// <param name="CallName"></param>
        /// <param name="DataType">Ex: Float, Integer, etc...</param>
        public void CreateNewVariable2(VariableTabType tabType, string ShowName = "a", string CallName = "a", VariableDataType DataType = VariableDataType.Float)
        {
            VariableTabNavi(tabType);

            //RefreshDataTable((DataTableAutoIDType)tabType);

            int rowCount = GetRowCount((DataTableAutoIDType)tabType);

            //// Testing get data table element from cache
            // Press page down until last row show up
            string dataGridID = tabType.GetDescription();
            IWebElement datagridTmp = GetDataTableElement((DataTableAutoIDType)tabType);
            if (rowCount > 1 && datagridTmp.GetAttribute("Scroll.VerticallyScrollable") == bool.TrueString
                             && datagridTmp.GetAttribute("Scroll.VerticalScrollPercent") != "100")
            {
                GetCellBy(dataGridID, 0, "No").LeftClick();
                while (double.Parse(datagridTmp.GetAttribute("Scroll.VerticalScrollPercent")) <= 99.99)
                    Press(Keys.PageDown);
            }

            // First Add a new empty row
            GetCellBy(dataGridID, rowCount, "Show Name").DoubleClick();

            // Input "a" in Show Name cell
            //GetCellBy("CndGrid", 0, "Show Name").LeftClick();
            GetCellBy(dataGridID, rowCount, "Show Name").SendSingleKeys(ShowName);

            // Input "a" in Call Name cell
            //GetCellBy("CndGrid", 0, "Call Name").LeftClick();
            GetCellBy(dataGridID, rowCount, "Call Name").SendSingleKeys(CallName);

            // Input "Float" in Data Type cell
            //GetCellBy("CndGrid", 0, "Data Type").LeftClick();
            GetCellBy(dataGridID, rowCount, "Data Type").SendSingleKeys(DataType.GetDescription());
            Press(Keys.Enter);
        }

        public IWebElement GetCommandIsSelected(string cmdName, string GroupNameToSearch = "", bool findExactSameCommand = true)
        {
            ////var CommandsMapMemoryCache = new MemoryCache<string, IWebElement>(CommandsMapCache);

            //// If row index within current CommandsMap cache row count, query element from cache
            //if (!CommandsMapCache.IsEmpty() && CommandsMapCache.Keys.Contains(cmdName))
            //    return CommandsMapCache.Get(cmdName);

            //else  // If row index out of current CommandsMap cache, resave the table
            //{
            //    SaveCommandMap(cmdName, GroupNameToSearch, findExactSameCommand);
            //    return CommandsMapCache.Get(cmdName);
            //}

            var cmdTreeItems = GetExpandedCommandGroup(GetCommandTreeByGroupName(QueryGroupName(cmdName)));
            return GetCommandIsSelected(cmdTreeItems);
        }

        public void SaveCommandMap(string cmdNameToSearch, string GroupNameToSearch = "", bool findExactSameCommand = true)
        {
            //bool RestartCommandSearch;
            int CommandSearchCount = 1;
            IWebElement cmdFound;

            //do
            //{
            //RestartCommandSearch = false;

            //Dictionary<string, AppiumWebElement> CommandsMap = new Dictionary<string, AppiumWebElement>();

            //WindowsElement testCmdSearchBox = CurrentDriver.GetElement(5000, By.ClassName("CmdTreeView"), MobileBy.AccessibilityId("searchBox"));
            //testCmdSearchBox.ClearContent();
            //testCmdSearchBox.SendComboKeys(CommandName, OpenQA.Selenium.Keys.Enter);

            IWebElement CommandTreeView = CurrentDriver.GetElement(By.ClassName("CmdTreeView"));
            IWebElement testCmdSearchBox = CommandTreeView.GetElement(MobileBy.AccessibilityId("searchBox"));
            IReadOnlyCollection<IWebElement> subCmdTreeViews = CommandTreeView.GetElements(By.ClassName("TreeView"));

            Assert.AreEqual(2, subCmdTreeViews.Count);
            testCmdSearchBox.ClearContent();
            testCmdSearchBox.SendComboKeys(cmdNameToSearch, OpenQA.Selenium.Keys.Enter);

            foreach (var subCmdTreeView in subCmdTreeViews)
            {
                //string CmdTreeViewID = subCmdTreeView.Id;

                var CmdGroupsTreeViewItems = subCmdTreeView.GetChildElements();  // Collapsed (0)

                foreach (var cmdGroup in CmdGroupsTreeViewItems)
                {
                    if (cmdGroup.isElementCollapsed())
                        continue;

                    string GroupName = cmdGroup.GetFirstTextContent();

                    if (CommandSearchCount > 3 && !GroupNameToSearch.IsNullOrEmpty() && GroupName == GroupNameToSearch)
                    {
                        // Maximum search limit (3 times) reached, open group tree to add command directly
                        cmdGroup.LeftClick();
                        cmdFound = cmdGroup.GetElements(By.XPath($".//TreeItem[@ClassName='TreeViewItem']")).ToList()
                                           .Find(cmd => cmd.GetFirstTextContent() == cmdNameToSearch);

                        if (cmdFound != null)
                            CommandsMapCache.Add(cmdNameToSearch, cmdFound);
                        return;
                    }

                    var cmdGroupTemp = cmdGroup.GetElements(By.XPath($".//TreeItem[@ClassName='TreeViewItem']")).ToList();
                    cmdGroupTemp.RemoveAt(0);

                    cmdFound = cmdGroupTemp.Find(c => c.Selected == true);

                    string cmdName = cmdFound.GetFirstTextContent();

                    //ReadOnlyCollection<AppiumWebElement> cmdsTreeViewItems = new ReadOnlyCollection<AppiumWebElement>(cmdGroupTemp);

                    if (cmdName != cmdNameToSearch)
                    {
                        // Command searched not matched with given command name, press F3 to continue to the next matched command
                        // Then restart the search action
                        if (!findExactSameCommand)
                        {
                            CommandsMapCache.Add(cmdNameToSearch, cmdFound);
                            return;
                        }
                        SendSingleKeys(Keys.F3);
                        continue;
                        //RestartCommandSearch = true;
                        //break;
                    }
                    else
                    {
                        CommandsMapCache.Add(cmdName, cmdFound);
                        return;
                    }

                    /* Legacy
                    //foreach (var cmd in cmdsTreeViewItems)
                    //{
                        //if (cmd.GetAttribute("SelectionItem.IsSelected") == "false")
                        //    continue;

                        //string cmdName = ((WindowsElement)cmd).GetSubElementText();

                        //if (cmdName != cmdNameToSearch)
                        //{
                        //    // Command searched not matched with given command name, press F3 to continue to the next matched command
                        //    // Then restart the search action
                        //    SendKeys(Keys.F3);
                        //    RestartCommandSearch = true;
                        //    break;
                        //}
                        //else
                        //{
                        //    CommandsMapCache.Add(cmdName, cmd);
                        //    return;
                        //}
                    //}
                    //if (RestartCommandSearch)
                    //    continue;
                    */
                }
            }
            CommandSearchCount++;
            //}
            //while (RestartCommandSearch);

            //return CommandsMapCache;
        }

        //public void RefreshDataTable(DataTableAutoIDType DataGridType)
        //{
        //    SaveGridTable(GetDataTableElement(DataGridType), DataGridType);
        //}

        public IWebElement GetCellBy(string DataGridAutomationID, int rowNo, string colName)
        {
            return GetDataTableElement(DataGridAutomationID).GetCellBy(rowNo, colName);

            //// If row index within current datatable, query element from cache
            //if (!dataTableTemp.IsEmpty() && dataTableTemp.Keys.Contains((rowIdx, colName)))
            //    return dataTableTemp.Get((rowIdx, colName));

            //else  // If row index out of current datatable cache, resave the table
            //{
            //    SaveGridCell(DataGridAutomationID, rowIdx, colName);
            //    return dataTableTemp.Get((rowIdx, colName));
            //}
        }

        public IEnumerable<IWebElement> GetColumnBy(DataTableAutoIDType DataGridType, int colNo, bool excludeLastRow = true)
        {
            return GetDataTableElement(DataGridType).GetColumnBy(colNo);

            //return excludeLastRow ? dataTableLookUp[colName].ToList().GetRange(0, colElements.Count - 1) // Exclude the last column (empty)
            //                      : dataTableLookUp[colName].ToList();  

            //// If Column name in current datatable, query single column from cache
            //ILookup<string, IWebElement> dataTableLookUp;
            //if (!dataTableTemp.IsEmpty() && dataTableTemp.Keys.Select(k => k.Item2).Contains(colName))
            //{
            //    // Skip action
            //}
            //else  // If Column name out of current datatable cache, resave the table and return single column from cache
            //{
            //    SaveGridTable(GetDataTableElement(DataGridType), DataGridType);
            //}

            //dataTableLookUp = dataTableTemp.CacheData.ToLookup(x => x.Key.Item2, x => x.Value);
            //List<IWebElement> colElements = dataTableLookUp[colName].ToList();
            //return excludeLastRow ? dataTableLookUp[colName].ToList().GetRange(0, colElements.Count - 1) // Exclude the last column (empty)
            //                      : dataTableLookUp[colName].ToList();    
        }

        public IEnumerable<IWebElement> GetRowCellElementsBy(DataTableAutoIDType DataGridType, int rowNo)
        {
            return GetDataTableElement(DataGridType).GetDataTableRowElements()[rowNo - 1].GetCellElementsOfRow();

            //// If row index within current datatable, query single row from cache
            //ILookup<int?, IWebElement> dataTableLookUp;
            //if (!dataTableTemp.IsEmpty() && dataTableTemp.Keys.Select(k => k.Item1).Contains(rowIdx))
            //{
            //    // Skip action
            //}
            //else  // If row index out of current datatable cache, resave the table and return single row from cache
            //{
            //    SaveGridTable(GetDataTableElement(DataGridType), DataGridType);
            //}

            //dataTableLookUp = dataTableTemp.CacheData.ToLookup(x => x.Key.Item1, x => x.Value);
            //return dataTableLookUp[rowIdx].ToList();
        }

        public string GetCellValue(string DataGridAutomationID, int rowIdx, string colName)
        {
            IWebElement cell = GetCellBy(DataGridAutomationID, rowIdx, colName);
            return cell.GetCellValue();
        }

        public int GetRowCount(DataTableAutoIDType DataGridType)
        {
            string DataGridAutomationID = DataGridType.ToString();
            return GetRowCount(DataGridAutomationID);
        }

        public int GetRowCount(string DataGridAutomationID)
        {
            return GetDataTableElement(DataGridAutomationID).GetDataTableRowElements().Count;
        }

        public List<string> GetSingleColumnValues(DataTableAutoIDType DataGridType, int colNo, bool excludeLastRow = true)
        {
            IEnumerable<IWebElement> column = GetColumnBy(DataGridType, colNo, excludeLastRow);
            List<string> columnValues = new List<string>();

            if (column == null)
                return null;
            else
            {
                columnValues = column.Select(c => c.GetAttribute("Value.Value")).ToList();
                columnValues.RemoveAll(s => s.IsNullOrEmpty());
                return columnValues;
            }
        }

        public List<string> GetSingleRowValues(DataTableAutoIDType DataGridType, int rowNo)
        {
            IEnumerable<IWebElement> row = GetRowCellElementsBy(DataGridType, rowNo);
            List<string> rowValues = new List<string>();

            if (row == null)
                return null;
            else
            {
                rowValues = row.Select(c => c.GetAttribute("Value.Value")).ToList();
                rowValues.RemoveAll(s => s.IsNullOrEmpty());
                return rowValues;
            }
        }

        #region Test Command/Test Item operations

        // 2024/07/12, Adam, add checking command source type
        public void AddCommandBy(string groupName, int cmdNumber = 1, int addCount = 1)
        {
            /* Legacy method of getting cmdtree by group name
            //IWebElement commandTree;
            //if (!GetCommandSourceType(groupName, out bool IsDevice))
            //    return;
            
            //if (IsDevice)
            //{
            //    commandTree = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("DeivceCmdTree"));
            //}
            //else
            //{
            //    commandTree = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("SystemCmdTree"));
            //}
            */

            IWebElement commandTree = GetCommandTreeByGroupName(groupName);                 // Find the command tree
            if (commandTree == null)
                return;

            CommandTreeViewScrollToTop(commandTree);                                        // Scroll to the top if not
            ExpandCommandGroup(commandTree, groupName, out IWebElement groupTreeItem);      // Expand the group item tree
            AddCommandBy(groupTreeItem, cmdNumber, addCount, out IWebElement cmdToAdd);     // Add the command by given parameters
            TreeViewCollapseAll(cmdToAdd);                                                  // Press left arrow key twice to Close the group tree view

            /* Legacy method
            //// Get the element that matching the given groupname directly by XPath (Faster)
            //groupTreeItem = commandTree.GetElement(By.XPath($".//TreeItem[@ClassName='TreeViewItem']/Text[@Name='{groupName}']/parent::node()"), 3000);

            //if (groupTreeItem == null && !cmdListIsFocused)
            //{
            //    commandTree.LeftClick();
            //    cmdListIsFocused = true; // Set the flag to true after the left click on the command list
            //}

            //// If element if out of screen, press page down to find the element
            //if (groupTreeItem == null)
            //{
            //    Press(Keys.PageDown);
            //    Thread.Sleep(1);

            //    // If scroll to end of the command list, group item still not found, throw exception
            //    foreach (var cmdList in commandTree.GetElements(By.ClassName("TreeView")))
            //    {
            //        if (cmdList.GetAttribute("Scroll.VerticallyScrollable") == bool.FalseString)
            //            continue;

            //        if (cmdList.GetAttribute("Scroll.VerticalScrollPercent") == "100")
            //            throw new GroupNameNotExistedException(groupName);
            //    }
            //}
            */
        }

        // 2024/07/12, Adam, add checking command source type
        public void AddCommandBy(string groupName, string cmdName, int addCount = 1)
        {
            /* Legacy method of getting cmdtree by group name
            //IWebElement commandTree;
            //if (!GetCommandSourceType(groupName, out bool IsDevice))
            //    return;
            
            //if (IsDevice)
            //{
            //    commandTree = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("DeivceCmdTree"));
            //}
            //else
            //{
            //    commandTree = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("SystemCmdTree"));
            //}
            */

            IWebElement commandTree = GetCommandTreeByGroupName(groupName);
            if (commandTree == null)
                return;

            CommandTreeViewScrollToTop(commandTree);                                        // Scroll to the top if not
            ExpandCommandGroup(commandTree, groupName, out IWebElement groupTreeItem);      // Expand the group item tree
            AddCommandBy(groupTreeItem, cmdName, addCount, out IWebElement cmdToAdd);       // Add the command by given parameters
            TreeViewCollapseAll(cmdToAdd);                                                  // Press left arrow key twice to Close the group tree view
        }

        //// Current used method for adding command
        //public void AddCommandBy(string groupName, int cmdNumber = 1, TestCommandSourceType tcType = TestCommandSourceType.System, int addCount = 1)
        //{
        //    //IWebElement commandTree = CurrentDriver.GetElement(By.ClassName("CmdTreeView"));
        //    IWebElement commandTree;
        //    if (tcType == TestCommandSourceType.Device)
        //    {
        //        commandTree = CurrentDriver.GetElement(MobileBy.AccessibilityId("DeivceCmdTree"));
        //    }
        //    else
        //    {
        //        commandTree = CurrentDriver.GetElement(MobileBy.AccessibilityId("SystemCmdTree"));
        //    }

        //    //Console.WriteLine($"LeftClick on Text \"{groupName}\"");

        //    bool cmdListIsFocused = false;
        //    IWebElement groupTreeItem = null;
        //    while (groupTreeItem == null)
        //    {
        //        // Get the element that matching the given groupname directly by XPath (Faster)
        //        groupTreeItem = commandTree.GetElement(By.XPath($".//TreeItem[@ClassName='TreeViewItem']/Text[@Name='{groupName}']/parent::node()"), 3000);

        //        if (groupTreeItem == null && !cmdListIsFocused)
        //        {
        //            commandTree.LeftClick();
        //            cmdListIsFocused = true; // Set the flag to true after the left click on the command list
        //        }

        //        // If element if out of screen, press page down to find the element
        //        if (groupTreeItem == null)
        //        {
        //            Press(Keys.PageDown);
        //            Thread.Sleep(1);

        //            // If scroll to end of the command list, group item still not found, throw exception
        //            foreach (var cmdList in commandTree.GetElements(By.ClassName("TreeView")))
        //            {
        //                if (cmdList.GetAttribute("Scroll.VerticallyScrollable") == bool.FalseString)
        //                    continue;

        //                if (cmdList.GetAttribute("Scroll.VerticalScrollPercent") == "100")
        //                    throw new GroupNameNotExistedException(groupName);
        //            }
        //        }
        //    }

        //    // 2024/07/09, Adam, Expand the command group
        //    groupTreeItem.ExpandTreeView();

        //    //// Get all elements, then find element that matching the given groupname (Longer time required)
        //    //var groupTreeItem = commandTree.GetElements(By.XPath($".//TreeItem[@ClassName='TreeViewItem']")).ToList()
        //    //                   .Find(e => e.GetSubElementText() == groupName);

        //    //// Use attribute: "ExpandCollapse.ExpandCollapseState" to check the expand/collapse state, where: Expanded (1), Collapsed (0)
        //    //if (groupTreeItem.isElementCollapsed())
        //    //    groupTreeItem.DoubleClick();

        //    var cmdTreeItems = groupTreeItem.GetElements(By.XPath($".//TreeItem[@ClassName='TreeViewItem']"));

        //    if (cmdTreeItems.Count == 0)
        //        return;
        //    if (cmdNumber > cmdTreeItems.Count || cmdNumber <= -1)
        //        throw new CommandNumberOutOfRangeException(cmdNumber.ToString());

        //    IWebElement cmdToAdd = cmdNumber == -1 ? cmdTreeItems.Last() : cmdTreeItems[cmdNumber];

        //    //var cmdTreeItem = groupTreeItem.GetElement(By.XPath($"(.//TreeItem[@ClassName='TreeViewItem'])[{cmdNumber + 1}]"));
        //    Console.WriteLine($"LeftClick on Text \"{cmdToAdd.GetFirstTextContent()}\"");

        //    //// If element if out of screen, move to the element first
        //    while (bool.Parse(cmdToAdd.GetAttribute("IsOffscreen")))
        //    {
        //        Press(Keys.PageDown);
        //        Thread.Sleep(50);
        //    }

        //    // Add the command
        //    for (int i = 0; i < addCount; i++)
        //        cmdToAdd.DoubleClick();

        //    // Press left arrow key twice to Close the group tree view
        //    Press(Keys.Left);
        //    Press(Keys.Left);
        //}

        // Legacy method adding repeated commands with command name and repeat count
        //public void AddCommandBy(string groupName, string cmdName = "Arithmetic", TestCommandSourceType tcType = TestCommandSourceType.System, int addCount = 1)
        //{
        //    //IWebElement commandTree = CurrentDriver.GetElement(By.ClassName("CmdTreeView"));
        //    IWebElement commandTree;
        //    if (tcType == TestCommandSourceType.Device)
        //    {
        //        commandTree = CurrentDriver.GetElement(MobileBy.AccessibilityId("DeivceCmdTree"));
        //    }
        //    else
        //    {
        //        commandTree = CurrentDriver.GetElement(MobileBy.AccessibilityId("SystemCmdTree"));
        //    }

        //    //Console.WriteLine($"LeftClick on Text \"{groupName}\"");

        //    bool cmdListIsFocused = false;
        //    IWebElement groupTreeItem = null;
        //    while (groupTreeItem == null)
        //    {
        //        // Get the element that matching the given groupname directly by XPath (Faster)
        //        groupTreeItem = commandTree.GetElement(By.XPath($".//TreeItem[@ClassName='TreeViewItem']/Text[@Name='{groupName}']/parent::node()"), 3000);

        //        if (groupTreeItem == null && !cmdListIsFocused)
        //        {
        //            commandTree.LeftClick();
        //            cmdListIsFocused = true; // Set the flag to true after the left click on the command list
        //        }

        //        // If element if out of screen, press page down to find the element
        //        if (groupTreeItem == null)
        //        {
        //            Press(Keys.PageDown);
        //            Thread.Sleep(1);

        //            // If scroll to end of the command list, group item still not found, throw exception
        //            foreach (var cmdList in commandTree.GetElements(By.ClassName("TreeView")))
        //            {
        //                if (cmdList.GetAttribute("Scroll.VerticallyScrollable") == bool.FalseString)
        //                    continue;

        //                if (cmdList.GetAttribute("Scroll.VerticalScrollPercent") == "100")
        //                    throw new GroupNameNotExistedException(groupName);
        //            }
        //        }
        //    }


        //    // 2024/07/09, Adam, Expand the command group
        //    groupTreeItem.ExpandTreeView();

        //    //// Use attribute: "ExpandCollapse.ExpandCollapseState" to check the expand/collapse state, where: Expanded (1), Collapsed (0)
        //    //if (groupTreeItem.isElementCollapsed())
        //    //    groupTreeItem.DoubleClick();

        //    Console.WriteLine($"LeftClick on Text \"{cmdName}\"");

        //    // Get all elements, then find element that matching the given command name (Longer time required, can use)
        //    var cmdTreeItem = groupTreeItem.GetElements(By.XPath($".//TreeItem[@ClassName='TreeViewItem']"))
        //                                   .FirstOrDefault(e => e.GetFirstTextContent() == cmdName) 
        //                                   ?? throw new CommandNameNotExistedException(cmdName);

        //    //*[contains(@label,"text you want to find")]
        //    //var cmdTreeItem = groupTreeItem.GetElement(By.XPath($".//TreeItem[@ClassName='TreeViewItem']/Text[(@Name='{cmdName}')]/parent::node()"))
        //    //                               ?? throw new CommandNameNotExistedException(cmdName);

        //    //// Get the element that matching the given command name directly by XPath (Faster, but not executable)
        //    //var cmdTreeItem = groupTreeItem.GetElement(By.XPath($".//TreeItem[@ClassName='TreeViewItem']/Text[@Name='{cmdName}']/parent::node()"));

        //    // If element if out of screen, move to the element first
        //    while (bool.Parse(cmdTreeItem.GetAttribute("IsOffscreen")))
        //    {
        //        Press(Keys.PageDown);
        //        Thread.Sleep(50);
        //    }

        //    // Add the command
        //    for (int i = 0; i < addCount; i++)
        //        cmdTreeItem.DoubleClick();

        //    // Press left arrow key twice to Close the group tree view
        //    Press(Keys.Left);
        //    Press(Keys.Left);
        //}

        // Legacy method adding commands with command names

        public void AddCommandsBy(string groupName, params string[] cmdNames)
        {
            /* Legacy method of getting cmdtree by group name
            //IWebElement commandTree;
            //if (!GetCommandSourceType(groupName, out bool IsDevice))
            //    return;
            
            //if (IsDevice)
            //{
            //    commandTree = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("DeivceCmdTree"));
            //}
            //else
            //{
            //    commandTree = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("SystemCmdTree"));
            //}
            */

            IWebElement commandTree = GetCommandTreeByGroupName(groupName);                 // Find the command tree
            if (commandTree == null)
                return;

            CommandTreeViewScrollToTop(commandTree);                                        // Scroll to the top if not
            ExpandCommandGroup(commandTree, groupName, out IWebElement groupTreeItem);      // Expand the group item tree
            AddCommandsBy(groupTreeItem, cmdNames, out IWebElement cmdToAdd);               // Add the command by given parameters
            TreeViewCollapseAll(cmdToAdd);                                                  // Press left arrow key twice to Close the group tree view

            /* Legacy methods 
            //// Use attribute: "ExpandCollapse.ExpandCollapseState" to check the expand/collapse state, where: Expanded (1), Collapsed (0)
            //if (groupTreeItem.isElementCollapsed())
            //    groupTreeItem.DoubleClick();

            // Get all elements, then find element that matching the given command name (Longer time required, can use)
            //var cmdTreeItems = groupTreeItem.GetElements(By.XPath($".//TreeItem[@ClassName='TreeViewItem']"));
            //HashSet<IWebElement> cmdTreeItemsHash = new HashSet<IWebElement>(cmdTreeItems);
            //var h = new HashSet<int>(Enumerable.Range(1, 17519).Select(i => i * 17519));

            //var cmdTreeItems = groupTreeItem.GetElements(By.XPath(".//TreeItem[@ClassName='TreeViewItem']"));

            // Get commands in the list
            //Stopwatch sw = Stopwatch.StartNew();
            //sw.Start();
            //var cmdNamesFound = cmdTreeItems.Select(e => e.GetSubElementText());
            //sw.Stop();
            //var GetSubElementTextTime = sw.ElapsedMilliseconds;
            //ArrayList cmdNamesFound = new ArrayList();
            //foreach (string text in cmdTreeItems.Select(e => e.GetSubElementText()))
            //    cmdNamesFound.Add(text);

            //List<string> cmdNamesFound = new List<string>(cmdTreeItems.Select(e => e.GetSubElementText()));

            //for (int i = 0; i < cmdTreeItems.Count(); i++)
            //{
            //    ((WindowsElement)cmdTreeItems[i]).SetCacheValues(new Dictionary<string, object> { ["text"] = cmdNamesFound.ElementAt(i) });
            //}

            //// Convert cmdNamesFound to a HashSet for faster lookups using Distinct and HashSet constructor
            //HashSet<string> cmdNamesFoundSet = new HashSet<string>(cmdTreeItems.Select(e => e.GetSubElementText()), new StringEqualityComparer());

            //var cmdTreeItems = groupTreeItem.GetTreeViewItems();
            //foreach (string cmdName in cmdNames.OrderBy(n => n))
            //{
            //    Console.WriteLine($"LeftClick on Text \"{cmdName}\"");

            //    //sw.Restart();
            //    //if (!cmdNamesFound.Contains(cmdName))
            //    //    throw new CommandNameNotExistedException(cmdName);
            //    //sw.Stop();
            //    //var sListContainsTime = sw.ElapsedMilliseconds;

            //    // Reuse the previously found elements
            //    //IWebElement cmdTreeItem = cmdTreeItems.FirstOrDefault(e => e.GetSubElementText() == cmdName);
            //    //IWebElement cmdTreeItem = cmdTreeItems.FirstOrDefault(e => ((WindowsElement)e).Text == cmdName);
            //    //IWebElement cmdTreeItem = cmdTreeItems[cmdNamesFound.IndexOf(cmdName)];

            //    IWebElement cmdTreeItem = cmdTreeItems.FirstOrDefault(e => e.GetFirstTextContent() == cmdName)
            //                                          ?? throw new CommandNameNotExistedException(cmdName);

            //    // If the element is out of screen, move to the element first
            //    while (bool.Parse(cmdTreeItem.GetAttribute("IsOffscreen")))
            //    {
            //        Press(Keys.PageDown);
            //        Thread.Sleep(1);

            //        //// Find the element again if it shows up
            //        //cmdTreeItem = groupTreeItem.GetElement(By.XPath($".//TreeItem[@ClassName='TreeViewItem']/Text[@Name='{cmdName}']/parent::node()"), 3000);
            //    }

            //    // Add the command
            //    cmdTreeItem.DoubleClick();
            //}

            //// Close the group tree view
            //while (!groupTreeItem.Selected)
            //{
            //    PressUp();
            //    Thread.Sleep(1);
            //}
            */
        }

        // Legacy method adding commands with command indeces in the given group
        public void AddCommandsBy(string groupName, params int[] cmdNumbers)
        {
            /* Legacy method of getting cmdtree by group name
            //IWebElement commandTree;
            //if (!GetCommandSourceType(groupName, out bool IsDevice))
            //    return;
            
            //if (IsDevice)
            //{
            //    commandTree = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("DeivceCmdTree"));
            //}
            //else
            //{
            //    commandTree = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("SystemCmdTree"));
            //}
            */

            if (cmdNumbers.Any(n => n <= 0))                                                // Check cmdNumbers has no negative values
                throw new CommandNumberOutOfRangeException(cmdNumbers.Min());

            IWebElement commandTree = GetCommandTreeByGroupName(groupName);                 // Find the command tree
            if (commandTree == null)
                return;

            //string ScrollBarPosPerc = commandTree.GetAttribute("Scroll.VerticalScrollPercent");
            CommandTreeViewScrollToTop(commandTree);                                        // Scroll to the top if not
            ExpandCommandGroup(commandTree, groupName, out IWebElement groupTreeItem);      // Expand the group item tree
            AddCommandsBy(groupTreeItem, cmdNumbers, out IWebElement cmdToAdd);             // Add the command by given parameters
            TreeViewCollapseAll(cmdToAdd);                                                  // Press left arrow key twice to Close the group tree view

            /* Legacy methods  
            //// Get all elements, then find element that matching the given groupname (Longer time required)
            //var groupTreeItem = commandTree.GetElements(By.XPath($".//TreeItem[@ClassName='TreeViewItem']")).ToList()
            //                   .Find(e => e.GetSubElementText() == groupName);

            //// Use attribute: "ExpandCollapse.ExpandCollapseState" to check the expand/collapse state, where: Expanded (1), Collapsed (0)
            //if (groupTreeItem.isElementCollapsed())
            //    groupTreeItem.DoubleClick();

            //IWebElement cmdToAdd = null;

            ////var cmdTreeItems = groupTreeItem.GetElements(By.XPath($".//TreeItem[@ClassName='TreeViewItem']"));
            //var cmdTreeItems = groupTreeItem.GetTreeViewItems();
            //if (cmdNumbers.Any(n => n > cmdTreeItems.Count))
            //    throw new CommandNumberOutOfRangeException(cmdNumbers.Max().ToString());

            ////cmdNumbers.ToList().ForEach(num =>
            ////{
            ////    if (cmdTreeItems.Count == 0)
            ////        return;
            ////    if (num >= cmdTreeItems.Count || num <= -1)
            ////        throw new CommandNumberOutOfRangeException(num.ToString());
            ////});

            ////if (cmdNumbers.ToList().Any(num => num >= cmdTreeItems.Count || num <= 0))
            ////{
            ////    throw new CommandNumberOutOfRangeException(cmdNumbers.ToList().First().ToString());
            ////}

            //foreach (int cmdNumber in cmdNumbers.OrderBy(n => n))
            //{
            //    cmdToAdd = cmdNumber == -1 ? cmdTreeItems.Last() : cmdTreeItems[cmdNumber - 1];

            //    //var cmdTreeItem = groupTreeItem.GetElement(By.XPath($"(.//TreeItem[@ClassName='TreeViewItem'])[{cmdNumber + 1}]"));
            //    Console.WriteLine($"LeftClick on Text \"{cmdToAdd.GetFirstTextContent()}\"");

            //    //// If element if out of screen, move to the element first
            //    while (bool.Parse(cmdToAdd.GetAttribute("IsOffscreen")))
            //    {
            //        Press(Keys.PageDown);
            //        Thread.Sleep(1);
            //    }

            //    // Add the command
            //    cmdToAdd.DoubleClick();
            //}

            // Close the group tree view
            //while (!groupTreeItem.Selected)
            //{
            //    PressUp();
            //    Thread.Sleep(1);
            //}
            */
        }


        // Base method to add command by command indeces
        public void AddCommandsBy(IWebElement groupTreeItem, int[] cmdNumbers, out IWebElement cmdToAdd)
        {
            cmdToAdd = null;
            var cmdTreeItems = groupTreeItem.GetTreeViewItems();
            if (cmdNumbers.Any(n => n > cmdTreeItems.Count))
                throw new CommandNumberOutOfRangeException(cmdNumbers.Max());

            AddCommands(cmdTreeItems, cmdNumbers);
        }

        // Base method to add command by command Names
        public void AddCommandsBy(IWebElement groupTreeItem, string[] cmdNames, out IWebElement cmdToAdd)
        {
            cmdToAdd = null;
            var cmdTreeItems = groupTreeItem.GetTreeViewItems();
            //var cmdNamesActual = groupTreeItem.GetSpecificChildrenContentOfControlType(ElementControlType.TextBlock, e => e.GetFirstTextContent() != null);
            //var cmdNamesActual = cmdTreeItems.ToArray().Select(e => e.GetFirstTextContent()).ToArray();
            List<string> cmdNamesActual = QueryCommandNames(groupTreeItem.GetFirstTextContent());

            if (cmdNames.Except(cmdNamesActual).Count() > 0)
                throw new CommandNameNotExistedException(cmdNames.Except(cmdNamesActual).First());

            var cmdNumbers = cmdNames.Select(x => cmdNamesActual.ToList().IndexOf(x) + 1).ToArray();
            AddCommands(cmdTreeItems, cmdNumbers);

            /* Legacy using cmdNames
            //foreach (string cmdName in cmdNames.OrderBy(n => n))
            //{
            //    Console.WriteLine($"LeftClick on Text \"{cmdName}\"");

            //    //sw.Restart();
            //    //if (!cmdNamesFound.Contains(cmdName))
            //    //    throw new CommandNameNotExistedException(cmdName);
            //    //sw.Stop();
            //    //var sListContainsTime = sw.ElapsedMilliseconds;

            //    cmdToAdd = cmdTreeItems.FirstOrDefault(e => e.GetFirstTextContent() == cmdName)
            //                           ?? throw new CommandNameNotExistedException(cmdName);

            //    // If the element is out of screen, move to the element first
            //    while (bool.Parse(cmdToAdd.GetAttribute("IsOffscreen")))
            //    {
            //        Press(Keys.PageDown);
            //        Thread.Sleep(1);
            //    }

            //    // Add the command
            //    cmdToAdd.DoubleClick();
            //}
            */
        }

        // Base method to get command by command Name
        public IWebElement GetCommandBy(string groupName, string cmdName, bool collapseTreeView = false)
        {
            IWebElement commandTree = GetCommandTreeByGroupName(groupName);
            if (commandTree == null)
                return null;

            CommandTreeViewScrollToTop(commandTree);                                                                    // Scroll to the top if not
            ExpandCommandGroup(commandTree, groupName, out IWebElement groupTreeItem);                                  // Expand the group item tree
            IWebElement cmdToAdd = GetCommand(groupTreeItem, groupName, cmdName);                                       // Add the command by given parameters

            if (collapseTreeView)
                TreeViewCollapseAll(cmdToAdd);                                                                          // Press left arrow key twice to Close the group tree view

            return cmdToAdd;
        }

        public void AddCommandBy(IWebElement groupTreeItem, string cmdName, int addCount, out IWebElement cmdToAdd)
        {
            // Get all elements, then find element that matching the given command name (Longer time required, can use)
            int cmdNumber;
            var cmdTreeItems = groupTreeItem.GetTreeViewItems();
            List<string> cmdNamesActual = QueryCommandNames(groupTreeItem.GetFirstTextContent());
            if (!cmdNamesActual.Contains(cmdName))
                throw new CommandNameNotExistedException(cmdName);

            cmdNumber = cmdNamesActual.IndexOf(cmdName) + 1;
            AddCommand(cmdTreeItems, cmdNumber, addCount);
            cmdToAdd = cmdTreeItems[cmdNumber];
        }

        public void AddCommandBy(IWebElement groupTreeItem, int cmdNumber, int addCount, out IWebElement cmdToAdd)
        {
            cmdToAdd = null;
            var cmdTreeItems = groupTreeItem.GetTreeViewItems();

            if (cmdTreeItems.Count == 0)
                return;
            if (cmdNumber > cmdTreeItems.Count || cmdNumber <= -1)
                throw new CommandNumberOutOfRangeException(cmdNumber);

            AddCommand(cmdTreeItems, cmdNumber, addCount);
            cmdToAdd = cmdTreeItems[cmdNumber];
            /*
            //cmdToAdd = cmdNumber == -1 ? cmdTreeItems.Last() : cmdTreeItems[cmdNumber];

            ////var cmdTreeItem = groupTreeItem.GetElement(By.XPath($"(.//TreeItem[@ClassName='TreeViewItem'])[{cmdNumber + 1}]"));
            ////Console.WriteLine($"LeftClick on Text \"{cmdToAdd.GetFirstTextContent()}\"");

            ////// If element if out of screen, move to the element first
            //while (bool.Parse(cmdToAdd.GetAttribute("IsOffscreen")))
            //{
            //    Press(Keys.PageDown);
            //    Thread.Sleep(50);
            //}

            //// Add the command
            //if (isAddCommand)
            //{
            //    for (int i = 0; i < addCount; i++)
            //        cmdToAdd.DoubleClick();
            //}
            */
        }

        public void SelectCommandBy(IWebElement groupTreeItem, int cmdNumber, out IWebElement cmdToAdd)
        {
            cmdToAdd = null;
            var cmdTreeItems = groupTreeItem.GetTreeViewItems();

            if (cmdTreeItems.Count == 0)
                return;
            if (cmdNumber > cmdTreeItems.Count || cmdNumber <= -1)
                throw new CommandNumberOutOfRangeException(cmdNumber);

            SelectCommand(cmdTreeItems, cmdNumber);
        }

        public IWebElement GetCommand(IWebElement groupTreeItem, string groupName, string cmdName)
        {
            if (!HasCommand(groupName, cmdName))
                throw new CommandNameNotExistedException(cmdName);

            var cmdTreeItems = groupTreeItem.GetTreeViewItems();
            int cmdNumber = QueryCommandNames(groupName).IndexOf(cmdName) + 1;
            return GetCommand(cmdTreeItems, cmdNumber);
        }

        public IWebElement GetCommand(IWebElement groupTreeItem, int cmdNumber)
        {
            var cmdTreeItems = groupTreeItem.GetTreeViewItems();

            if (cmdTreeItems.Count == 0)
                return null;
            if (cmdNumber > cmdTreeItems.Count || cmdNumber <= -1)
                throw new CommandNumberOutOfRangeException(cmdNumber);

            return GetCommand(cmdTreeItems, cmdNumber);
        }

        // Common base method to add command by command indeces
        public void AddCommands(ReadOnlyCollection<IWebElement> cmdTreeItems, int[] cmdNumbers)
        {
            foreach (int cmdNumber in cmdNumbers.OrderBy(n => n))
            {
                AddCommand(cmdTreeItems, cmdNumber, 1);
            }
        }

        // Base method to add a single command (used by all other AddCommand/AddCommands methods)
        public void AddCommand(ReadOnlyCollection<IWebElement> cmdTreeItems, int cmdNumber, int addCount = 1)
        {
            IWebElement cmdToAdd = cmdNumber == -1 ? cmdTreeItems.Last() : cmdTreeItems[cmdNumber];

            //// If element if out of screen, move to the element first
            while (bool.Parse(cmdToAdd.GetAttribute("IsOffscreen")))
            {
                Press(Keys.PageDown);
                Thread.Sleep(1);
            }

            // Add the command
            for (int i = 0; i < addCount; i++)
                cmdToAdd.DoubleClick();
        }

        // Base method to select a single command (used by all other AddCommand/AddCommands methods)
        public void SelectCommand(ReadOnlyCollection<IWebElement> cmdTreeItems, int cmdNumber)
        {
            IWebElement cmdToAdd = cmdNumber == -1 ? cmdTreeItems.Last() : cmdTreeItems[cmdNumber];

            //// If element if out of screen, move to the element first
            while (bool.Parse(cmdToAdd.GetAttribute("IsOffscreen")))
            {
                Press(Keys.PageDown);
                Thread.Sleep(1);
            }

            // Select the command
            cmdToAdd.LeftClick();
        }

        // Base method to get a single command (used by all other AddCommand/AddCommands methods)
        public IWebElement GetCommand(ReadOnlyCollection<IWebElement> cmdTreeItems, int cmdNumber)
        {
            IWebElement cmdToAdd = cmdNumber == -1 ? cmdTreeItems.Last() : cmdTreeItems[cmdNumber];

            //// If element if out of screen, move to the element first
            while (bool.Parse(cmdToAdd.GetAttribute("IsOffscreen")))
            {
                Press(Keys.PageDown);
                Thread.Sleep(1);
            }

            return cmdToAdd;
        }

        public IWebElement GetCommandIsSelected(ReadOnlyCollection<IWebElement> cmdTreeItems)
        {
            IWebElement cmdToAdd = cmdTreeItems.FirstOrDefault(cmd => cmd.Selected);

            //// If element if out of screen, move to the element first
            while (bool.Parse(cmdToAdd.GetAttribute("IsOffscreen")))
            {
                Press(Keys.PageDown);
                Thread.Sleep(1);
            }

            return cmdToAdd;
        }


        public IWebElement GetCommandTreeByGroupName(string groupName)
        {
            IWebElement commandTree;
            if (!GetCommandSourceType(groupName, out bool IsDevice))
                throw new GroupNameNotExistedException(groupName);

            if (IsDevice)
            {
                commandTree = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("DeivceCmdTree"));
            }
            else
            {
                commandTree = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("SystemCmdTree"));
            }
            return commandTree;
        }

        public void CommandTreeViewScrollToTop(IWebElement commandTree)
        {
            // Scroll to the top if not
            //string ScrollBarPosPerc = commandTree.GetAttribute("Scroll.VerticalScrollPercent");
            if (commandTree.GetAttribute("Scroll.VerticalScrollPercent") != "-1")
            {
                while (double.Parse(commandTree.GetAttribute("Scroll.VerticalScrollPercent")) >= 0.00001)
                {
                    //MoveToElementAndLeftClick(commandTree);
                    commandTree.LeftClick();
                    Press(Keys.Home);
                }
            }
        }

        public void SearchForCommandGroup(IWebElement commandTree, string groupName, out IWebElement groupTreeItem)
        {
            commandTree.LeftClick();
            do
            {
                // Get the group element by groupname
                groupTreeItem = commandTree.GetTreeViewItems()
                                           .FirstOrDefault(e => e.GetTextElement(groupName)?.Text == groupName);

                // If element if out of screen, press page down to find the element
                if (groupTreeItem == null || !groupTreeItem.Displayed)
                {
                    Press(Keys.PageDown);
                    Thread.Sleep(1);
                }

            } while (bool.Parse(groupTreeItem?.GetAttribute("IsOffscreen")));
        }

        public void ExpandCommandGroup(IWebElement commandTree, string groupName, out IWebElement groupTreeItem)
        {
            SearchForCommandGroup(commandTree, groupName, out groupTreeItem);

            // 2024/07/09, Adam, Expand the command group
            groupTreeItem.ExpandTreeView();
        }

        public ReadOnlyCollection<IWebElement> GetExpandedCommandGroup(IWebElement commandTree)
        {
            // Get the group element by groupname
            IWebElement groupTreeItem = commandTree.GetTreeViewItems()
                                                   .FirstOrDefault(e => e.isElementExpanded());
            return groupTreeItem.GetTreeViewItems();
        }

        public void AddTIBy(string groupName, int tiIndex = 1, TestItemSourceType tiType = TestItemSourceType.System, int addCount = 1)
        {
            IWebElement commandTree;
            if (tiType == TestItemSourceType.System)
            {
                TPExecuteAction(TPAction.SwitchToSystemTIPage);
                commandTree = CurrentDriver.GetElement(By.ClassName("SysUUTCmdTreeView")).GetFirstTreeViewElement();
            }
            else
            {
                TPExecuteAction(TPAction.SwitchToUserDefinedTIPage);
                commandTree = CurrentDriver.GetElement(By.ClassName("UserUUTCmdTreeView")).GetFirstTreeViewElement();
            }

            Logger.LogMessage($"LeftClick on Text \"{groupName}\"");

            bool cmdListIsFocused = false;
            IWebElement groupTreeItem = null;
            while (groupTreeItem == null)
            {
                // Get the element that matching the given groupname directly by XPath (Faster)
                groupTreeItem = commandTree.GetElement(By.XPath($".//TreeItem[@ClassName='TreeViewItem']/Text[@Name='{groupName}']/parent::node()"), 3000);

                if (groupTreeItem == null && !cmdListIsFocused)
                {
                    commandTree.LeftClick();
                    cmdListIsFocused = true; // Set the flag to true after the left click on the command list
                }

                // If element if out of screen, press page down to find the element
                if (groupTreeItem == null)
                {
                    Press(Keys.PageDown);
                    Thread.Sleep(1);

                    // If scroll to end of the command list, group item still not found, throw exception
                    foreach (var cmdList in commandTree.GetElements(By.ClassName("TreeView")))
                    {
                        if (cmdList.GetAttribute("Scroll.VerticallyScrollable") == bool.FalseString)
                            continue;

                        if (cmdList.GetAttribute("Scroll.VerticalScrollPercent") == "100")
                            throw new GroupNameNotExistedException(groupName);
                    }
                }
            }

            // 2024/07/09, Adam, Expand the command group
            groupTreeItem.ExpandTreeView();

            //// Get all elements, then find element that matching the given groupname (Longer time required)
            //var groupTreeItem = commandTree.GetElements(By.XPath($".//TreeItem[@ClassName='TreeViewItem']")).ToList()
            //                   .Find(e => e.GetSubElementText() == groupName);

            //// Use attribute: "ExpandCollapse.ExpandCollapseState" to check the expand/collapse state, where: Expanded (1), Collapsed (0)
            //if (groupTreeItem.isElementCollapsed())
            //    groupTreeItem.DoubleClick();

            var cmdTreeItems = groupTreeItem.GetElements(By.XPath($".//TreeItem[@ClassName='TreeViewItem']"));

            if (cmdTreeItems.Count == 0)
                return;
            if (tiIndex > cmdTreeItems.Count || tiIndex < -1 || tiIndex == 0)
                throw new ArgumentOutOfRangeException(tiIndex.ToString());

            IWebElement tiToAdd = tiIndex == -1 ? cmdTreeItems.Last() : cmdTreeItems[tiIndex];

            //var cmdTreeItem = groupTreeItem.GetElement(By.XPath($"(.//TreeItem[@ClassName='TreeViewItem'])[{cmdNumber + 1}]"));
            Logger.LogMessage($"LeftClick on Text \"{tiToAdd.GetFirstTextContent()}\"");

            //// If element if out of screen, move to the element first
            while (bool.Parse(tiToAdd.GetAttribute("IsOffscreen")))
            {
                Press(Keys.PageDown);
                Thread.Sleep(50);
            }

            // Add the TI
            for (int i = 0; i < addCount; i++)
                tiToAdd.DoubleClick();

            // Press left arrow key twice to Close the group tree view
            Press(Keys.Left);
            Press(Keys.Left);
        }

        public void AddTIBy(string groupName, int tiIndex = 1, int addCount = 1)
        {
            // Switch to System or user-defined page by checking groupname

            IWebElement commandTree = CurrentDriver.GetElement(By.ClassName("DeivceCmdTree"));

            Logger.LogMessage($"LeftClick on Text \"{groupName}\"");

            bool cmdListIsFocused = false;
            IWebElement groupTreeItem = null;
            while (groupTreeItem == null)
            {
                // Get the element that matching the given groupname directly by XPath (Faster)
                groupTreeItem = commandTree.GetElement(By.XPath($".//TreeItem[@ClassName='TreeViewItem']/Text[@Name='{groupName}']/parent::node()"), 3000);

                if (groupTreeItem == null && !cmdListIsFocused)
                {
                    commandTree.LeftClick();
                    cmdListIsFocused = true; // Set the flag to true after the left click on the command list
                }

                // If element if out of screen, press page down to find the element
                if (groupTreeItem == null)
                {
                    Press(Keys.PageDown);
                    Thread.Sleep(1);

                    // If scroll to end of the command list, group item still not found, throw exception
                    foreach (var cmdList in commandTree.GetElements(By.ClassName("TreeView")))
                    {
                        if (cmdList.GetAttribute("Scroll.VerticallyScrollable") == bool.FalseString)
                            continue;

                        if (cmdList.GetAttribute("Scroll.VerticalScrollPercent") == "100")
                            throw new GroupNameNotExistedException(groupName);
                    }
                }
            }

            // 2024/07/09, Adam, Expand the command group
            groupTreeItem.ExpandTreeView();

            //// Get all elements, then find element that matching the given groupname (Longer time required)
            //var groupTreeItem = commandTree.GetElements(By.XPath($".//TreeItem[@ClassName='TreeViewItem']")).ToList()
            //                   .Find(e => e.GetSubElementText() == groupName);

            //// Use attribute: "ExpandCollapse.ExpandCollapseState" to check the expand/collapse state, where: Expanded (1), Collapsed (0)
            //if (groupTreeItem.isElementCollapsed())
            //    groupTreeItem.DoubleClick();

            var cmdTreeItems = groupTreeItem.GetElements(By.XPath($".//TreeItem[@ClassName='TreeViewItem']"));

            if (cmdTreeItems.Count == 0)
                return;
            if (tiIndex > cmdTreeItems.Count || tiIndex < -1 || tiIndex == 0)
                throw new ArgumentOutOfRangeException(tiIndex.ToString());

            IWebElement tiToAdd = tiIndex == -1 ? cmdTreeItems.Last() : cmdTreeItems[tiIndex];

            //var cmdTreeItem = groupTreeItem.GetElement(By.XPath($"(.//TreeItem[@ClassName='TreeViewItem'])[{cmdNumber + 1}]"));
            Logger.LogMessage($"LeftClick on Text \"{tiToAdd.GetFirstTextContent()}\"");

            //// If element if out of screen, move to the element first
            while (bool.Parse(tiToAdd.GetAttribute("IsOffscreen")))
            {
                Press(Keys.PageDown);
                Thread.Sleep(50);
            }

            // Add the TI
            for (int i = 0; i < addCount; i++)
                tiToAdd.DoubleClick();

            // Press left arrow key twice to Close the group tree view
            Press(Keys.Left);
            Press(Keys.Left);
        }

        public void SelectColorSettingItem(IWebElement ColorSettingPage, ColorSettingPageType csPageType, string groupName, int idx = 1, bool collapseTreeView = false)
        {
            IWebElement colorSettingItem = SelectColorSettingItem(ColorSettingPage, csPageType, groupName, idx);
            if (collapseTreeView)
                TreeViewCollapseAll(colorSettingItem);
        }

        public IWebElement SelectColorSettingItem(IWebElement ColorSettingPage, ColorSettingPageType csPageType, string groupName, int idx = 1)
        {
            IWebElement colorSettingItem = GetColorSettingItem(ColorSettingPage, csPageType, groupName, idx);
            colorSettingItem.GetFirstTextElement().LeftClick();
            return colorSettingItem;
        }


        public IWebElement GetColorSettingItem(IWebElement ColorSettingPage, ColorSettingPageType csPageType, string groupName, int idx = 1)
        {
            IWebElement commandTree = ColorSettingPage.GetElement(MobileBy.AccessibilityId(csPageType.GetDescription()));
            //Console.WriteLine($"LeftClick on Text \"{groupName}\"");

            CommandTreeViewScrollToTop(commandTree);                                        // Scroll to the top if not
            ExpandCommandGroup(commandTree, groupName, out IWebElement groupTreeItem);      // Expand the group item tree
            return GetCommand(groupTreeItem, idx);                                          // Add the command by given parameters

            /* Legacy methods  
            //bool cmdListIsFocused = false;
            //IWebElement groupTreeItem = null;
            //while (groupTreeItem == null)
            //{
            //    // Get the element that matching the given groupname directly by XPath (Faster)
            //    groupTreeItem = commandTree.GetElement(By.XPath($".//TreeItem[@ClassName='TreeViewItem']/Text[@Name='{groupName}']/parent::node()"), 3000);

            //    if (groupTreeItem == null && !cmdListIsFocused)
            //    {
            //        commandTree.LeftClick();
            //        cmdListIsFocused = true; // Set the flag to true after the left click on the command list
            //    }

            //    // If element if out of screen, press page down to find the element
            //    if (groupTreeItem == null)
            //    {
            //        Press(Keys.PageDown);
            //        Thread.Sleep(1);

            //        // If scroll to end of the command list, group item still not found, throw exception
            //        foreach (var cmdList in commandTree.GetElements(By.ClassName("TreeView")))
            //        {
            //            if (cmdList.GetAttribute("Scroll.VerticallyScrollable") == bool.FalseString)
            //                continue;

            //            if (cmdList.GetAttribute("Scroll.VerticalScrollPercent") == "100")
            //                throw new GroupNameNotExistedException(groupName);
            //        }
            //    }
            //}

            //// Use attribute: "ExpandCollapse.ExpandCollapseState" to check the expand/collapse state, where: Expanded (1), Collapsed (0)
            //if (groupTreeItem.isElementCollapsed())
            //    groupTreeItem.GetFirstTextElement().DoubleClick();

            //var cmdTreeItems = groupTreeItem.GetElements(By.XPath($".//TreeItem[@ClassName='TreeViewItem']"));

            //var cmdTreeItems = groupTreeItem.GetTreeViewItems();

            //if (cmdTreeItems.Count == 0)
            //    return null;
            //if (idx > cmdTreeItems.Count || idx < -1 || idx == 0)
            //    throw new ArgumentOutOfRangeException(idx.ToString());

            //IWebElement itemToSetColor = idx == -1 ? cmdTreeItems.Last() : cmdTreeItems[idx];

            ////var cmdTreeItem = groupTreeItem.GetElement(By.XPath($"(.//TreeItem[@ClassName='TreeViewItem'])[{cmdNumber + 1}]"));
            ////Console.WriteLine($"LeftClick on Text \"{itemToSetColor.GetFirstTextContent()}\"");

            ////// If element if out of screen, move to the element first
            //while (bool.Parse(itemToSetColor.GetAttribute("IsOffscreen")))
            //{
            //    Press(Keys.PageDown);
            //    Thread.Sleep(50);
            //}

            ////// Click on the item
            ////itemToSetColor.LeftClick();

            //return itemToSetColor;
            */
        }


        public void TreeViewCollapseAll(IWebElement treeItemEle)
        {
            if (treeItemEle.GetAttribute("IsExpandCollapsePatternAvailable") == bool.TrueString)
            {
                Press(Keys.Left);       // Press left arrow key to move selection one tree level up
                Press(Keys.Left);       // Press left arrow key again to collapse current selection tree view
            }
        }

        #endregion

        //public IWebElement GetComboBoxElementByID(string comboBoxID)
        //{
        //    return CurrentDriver.GetElement(MobileBy.AccessibilityId(comboBoxID));
        //    //return CurrentDriver.GetElement(By.XPath($".//ComboBox[@AutomationId=\"{comboBoxID}\"]"));
        //    //return CurrentDriver.GetElements(By.ClassName(ElementControlType.ComboBox.ToString()))
        //    //                    .FirstOrDefault(e => e.CheckElementHasNameOrId() == comboBoxID);
        //}

        //public IWebElement GetListBoxElementByID(string listBoxID)
        //{
        //    return CurrentDriver.GetElement(MobileBy.AccessibilityId(listBoxID));
        //    //return CurrentDriver.GetElement(By.XPath($".//List[@AutomationId=\"{listBoxID}\"]"));
        //    //return CurrentDriver.GetElements(By.ClassName(ElementControlType.ListBox.ToString()))
        //    //                    .FirstOrDefault(e => e.CheckElementHasNameOrId() == listBoxID);
        //}

        //public IWebElement GetCustomComboBoxElementByID(string comboBoxID)
        //{
        //    //return CurrentDriver.GetElement(MobileBy.AccessibilityId(listBoxID));
        //    return CurrentDriver.GetElement(By.XPath($".//Pane[@AutomationId=\"{comboBoxID}\"]"));
        //    //return CurrentDriver.GetElements(By.ClassName(ElementControlType.ListBox.ToString()))
        //    //                    .FirstOrDefault(e => e.CheckElementHasNameOrId() == listBoxID);
        //}

        public void GetComboBoxItems(string comboBoxID, out ReadOnlyCollection<IWebElement> cmbItems)
        {
            //IWebElement comboBox = CurrentDriver.GetElement(MobileBy.AccessibilityId(comboBoxID));
            //if (comboBox.TagName == ElementControlType.ComboBox.GetDescription())
            //    return comboBox.GetElements(By.ClassName("ComboBoxItem"));
            //else if (comboBox.TagName == ElementControlType.ListBox.GetDescription())
            //    return comboBox.GetElements(By.ClassName("ListBoxItem"));
            //else
            //    return null;
            
            PP5IDEWindow.GetElement(MobileBy.AccessibilityId(comboBoxID)).LeftClick();
            cmbItems = PP5IDEWindow.GetElement(By.ClassName("Popup"))
                                    .GetElements(By.ClassName("ListBoxItem"));
        }

        // 先暫時分成兩個方法(給/不給comboBoxID)
        // 不給comboBoxID作法: combobox後再做動作
        public void ComboBoxSelectByName(IWebElement element, string name)
        {
            ElementControlType eleCtrlType = TypeExtension.GetEnumByDescription<ElementControlType>(element.TagName);
            switch (eleCtrlType)
            {
                // Scenario A: dataGrid/Cell中，點選後會開啟的列表框(ListView)
                case ElementControlType.Custom:
                    element.DoubleClickWithDelay(10);                                       // Open the combobox / listbox
                    if (element.GetFirstComboBoxElement() != null)                          // Case A-1: gridcell觸發Combobox子元素 (management內適用)
                        element.GetFirstComboBoxElement().SelectComboBoxItemByName2(name);
                    else
                        element.SelectComboBoxItemByName2(name);                            // Case A-2: gridcell觸發Listbox顯示於popup視窗，在控件中直接輸入Name選擇item (TI/TP內適用)
                    break;

                // Scenario B: 原本即有combobox控件的情形
                //  1.management各種多選控件
                //  2.TI / TP Editor open/ load 視窗的group多選
                //  3.Report中Category, Test Items多選控件      
                //    > 行為同下拉式組合框
                case ElementControlType.ComboBox:                                           
                    element.SelectComboBoxItemByName2(name);                                
                    break;                                                                                                                                                                       

                // Scenario C: 複合式視窗，但包含列表選項(ListItem)的情形:
                //  1.management的TI/TestCommand color設定，Font&Background按鈕開啟的colorPicker列表框(ListView)
                //  2.GUI Editor中，colorPicker視窗
                case ElementControlType.ListBox:
                    if (CheckComboBoxHasItemByName(element, name, out IWebElement cmbItem))
                        cmbItem.LeftClick();                             
                    break;

                default:
                    throw new NotImplementedException();
            }
            //element.DoubleClickWithDelay(10);       // Open the combobox / listbox

            //if (element.GetAttribute("Value.IsReadOnly") == bool.FalseString)
            //{
            //    cmb = PP5IDEWindow.GetWindowElement("Popup").GetElement(MobileBy.AccessibilityId("PART_Content"));
            //}

            ////if (element.GetAttribute("IsValuePatternAvailable") == bool.FalseString)
            ////{

            ////}
            
            //ComboBoxSelectByName(name);
            //else
            //    return;
        }

        public void ComboBoxSelectByIndex(IWebElement element, int index)
        {
            ElementControlType eleCtrlType = TypeExtension.GetEnumByDescription<ElementControlType>(element.TagName);
            switch (eleCtrlType)
            {
                // Scenario A: dataGrid/Cell中，點選後會開啟的列表框(ListView)
                case ElementControlType.Custom:
                    element.DoubleClickWithDelay(10);                                       // Open the combobox / listbox
                    if (element.GetFirstComboBoxElement() != null)                          // Case A-1: gridcell觸發Combobox子元素 (management內適用)
                        element.GetFirstComboBoxElement().SelectComboBoxItemByIndex2(index);
                    else
                        element.SelectComboBoxItemByIndex2(index);                            // Case A-2: gridcell觸發Listbox顯示於popup視窗，在控件中直接輸入Name選擇item (TI/TP內適用)
                    break;

                // Scenario B: 原本即有combobox控件的情形
                //  1.management各種多選控件
                //  2.TI / TP Editor open/ load 視窗的group多選
                //  3.Report中Category, Test Items多選控件      
                //    > 行為同下拉式組合框
                case ElementControlType.ComboBox:
                    element.SelectComboBoxItemByIndex2(index);
                    break;

                // Scenario C: 複合式視窗，但包含列表選項(ListItem)的情形:
                //  1.management的TI/TestCommand color設定，Font&Background按鈕開啟的colorPicker列表框(ListView)
                //  2.GUI Editor中，colorPicker視窗
                case ElementControlType.ListBox:
                    element.GetComboBoxItems(out ReadOnlyCollection<IWebElement> cmbItems);
                    cmbItems[index].LeftClick();
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        // 先暫時分成兩個方法(給/不給comboBoxID)
        // 給comboBoxID作法: 用comboBoxID定位combobox後再做動作
        public void ComboBoxSelectByName(string comboBoxID, string name)
        {
            //IWebElement comboBox = PP5IDEWindow.GetElement(MobileBy.AccessibilityId(comboBoxID));
            ////IWebElement comboBox = GetComboBoxElementByID(comboBoxID);

            //comboBox.SelectComboBoxItemByName2(name);
            PP5IDEWindow.GetElement(MobileBy.AccessibilityId(comboBoxID))
                        .SelectComboBoxItemByName2(name);
        }

        public void ComboBoxSelectByIndex(string comboBoxID, int index, bool supportKeyInputSearch = true)
        {
            IWebElement comboBox = PP5IDEWindow.GetElement(MobileBy.AccessibilityId(comboBoxID));
            //IWebElement comboBox = GetComboBoxElementByID(comboBoxID);
            if (comboBox.isElementCollapsed())
                comboBox.LeftClick();
            comboBox.SelectComboBoxItemByIndex(index, supportKeyInputSearch);
        }

        //public void ListBoxSelectByName(string listBoxID, string name)
        //{
        //    IWebElement listBox = CurrentDriver.GetElement(MobileBy.AccessibilityId(listBoxID));
        //    //IWebElement listBox = GetListBoxElementByID(listBoxID);
        //    if (listBox.isElementCollapsed())
        //        listBox.LeftClick();
        //    ListBoxSelectByName(listBox, name);
        //}

        //public void ListBoxSelectByIndex(string listBoxID, int index)
        //{
        //    IWebElement listBox = CurrentDriver.GetElement(MobileBy.AccessibilityId(listBoxID));
        //    //IWebElement listBox = GetListBoxElementByID(listBoxID);
        //    if (listBox.isElementCollapsed())
        //        listBox.LeftClick();
        //    ListBoxSelectByIndex(listBox, index);
        //}

        //public void ListBoxSelectByName(IWebElement listBox, string name)
        //{
        //    if (CheckComboBoxHasItemByName(listBox, name))
        //    {
        //        SendComboKeys(name, Keys.Enter);
        //    }
        //}

        //public void ListBoxSelectByIndex(IWebElement listBox, int index)
        //{
        //    if (GetComboBoxItems(listBox).Count() >= index + 1)
        //    {
        //        var listBoxItems = GetComboBoxItems(listBox);
        //        string name = listBoxItems[index].Text;
        //        listBox.SendComboKeys(name, Keys.Enter);
        //    }
        //}



        //public int GetComboBoxSelectionIndex(string comboBoxID)
        //{
        //    IWebElement comboBox = CurrentDriver.GetElement(MobileBy.AccessibilityId(comboBoxID));
        //    //IWebElement comboBox = GetComboBoxElementByID(comboBoxID);

        //    string selectionName = comboBox.GetAttribute("Selection.Selection");
        //    comboBox.DoubleClickWithDelay(10);
        //    comboBox.GetComboBoxItems(out ReadOnlyCollection<IWebElement> cmbItems);
        //    return cmbItems.Select(e => e.Text).ToList().IndexOf(selectionName);
        //}

        //public bool CheckListBoxHasItemByName(IWebElement listBox, string name)
        //{
        //    return GetComboBoxItems(listBox).Select(e => e.Text).Contains(name);
        //}

        // cell headers: "ShowName", "CallName", "DisplayedType", "DisplayedEditType", "Minimum",
        //               "Maximum", "Default", "Format", "DisplayedEnum"
        // DataGridByInfo classNames: PGGrid, CndGrid, RstGrid, TmpGrid, GlbGrid, DefectCodeGrid, ParameterGrid, LoginGrid
        //public void SaveGridTable(IWebElement gridTableElement, DataTableAutoIDType DataGridType)
        //{
        //    //WindowsElement HeaderPanel = CurrentDriver.GetElement(MobileBy.AccessibilityId(DataGridAutomationID)).GetElement(By.Name("HeaderPanel"));

        //    //List<string> headers = HeaderPanel.GetChildElements().Where(ape => ape.TagName == "ControlType.HeaderItem")
        //    //                                                     .Select(tg => ((WindowsElement)tg).GetSubElementText())
        //    //                                                     .ToList();

        //    //WindowsElement DataPanel = CurrentDriver.GetElement(MobileBy.AccessibilityId(DataGridAutomationID))
        //    //                                        .GetElement(By.Name("DataPanel"));
        //    //ReadOnlyCollection<AppiumWebElement> rows = DataPanel.GetChildElements();

        //    //List<string> rowsss = CurrentDriver.GetElement(MobileBy.AccessibilityId(DataGridAutomationID))
        //    //                                   .GetElement(By.Name("DataPanel"))
        //    //                                   .GetChildElements().Select(e => e.TagName).ToList();

        //    //List<string> headers = CurrentDriver.GetElement(MobileBy.AccessibilityId(DataGridAutomationID))
        //    //                                    .GetElements(By.XPath(".//HeaderItem"))
        //    //                                    .Select(tg => tg.GetElement(By.XPath(".//Text")).Text).ToList();

        //    List<string> headers = gridTableElement.GetDataTableHeaders(DataGridType.ToString()).ToList();
        //    ReadOnlyCollection<IWebElement> rows = gridTableElement.GetDataTableRowElements(DataGridType.ToString());

        //    //int rowIdx = 0;
        //    foreach (var row in rows)
        //    {
        //        var column = row.GetChildElements();
        //        Dictionary<string, IWebElement> kvps = new Dictionary<string, IWebElement>();
        //        string header;
        //        IWebElement cell;
                
        //        foreach (var headerAndCol in headers.Zip(column, Tuple.Create))
        //        {
        //            header = headerAndCol.Item1;
        //            cell = headerAndCol.Item2;
        //            int rowIdx = int.Parse(cell.GetAttribute("GridItem.Row")); // Get GridItem.Row index
        //            GetDataTableElement(DataGridType.ToString()).Add((rowIdx, header), cell);
        //        }
        //        //rowIdx++;
        //    }
        //}

        //public void SaveGridCell(string DataGridAutomationID, int rowIdx, string colName)
        //{
        //    List<string> headers = CurrentDriver.GetDataTableHeaders(DataGridAutomationID).ToList();
        //    ReadOnlyCollection<IWebElement> rows = CurrentDriver.GetDataTableRowElements(DataGridAutomationID);

        //    IWebElement cell = rows[rowIdx].GetChildElements()[headers.IndexOf(colName)];
        //    GetDataTableElement(DataGridAutomationID).Add((rowIdx, colName), cell);
        //}

        public IWebElement GetDataTableElement(string DataGridAutomationID)
        {
            if (dataTablesCache.Get(DataGridAutomationID) == null)
                dataTablesCache.Add(DataGridAutomationID, PP5IDEWindow.GetElement(MobileBy.AccessibilityId(DataGridAutomationID)));

            return dataTablesCache.Get(DataGridAutomationID);

            /* Legacy method
            //switch (DataGridAutomationID)
            //{
            //    case "PGGrid":
            //        // Test Item Context
            //        return dataTableTestItemsCache;

            //    case "CndGrid":
            //        // Condition
            //        return dataTableConditionCache;

            //    case "RstGrid":
            //        // Result
            //        return dataTableResultCache;

            //    case "TmpGrid":
            //        // Temp
            //        return dataTableTempCache;

            //    case "GlbGrid":
            //        // Global
            //        return dataTableGlobalCache;

            //    case "DefectCodeGrid":
            //        // Defect Code
            //        return dataTableDefectCodeCache;

            //    case "ParameterGrid":
            //        // Test command Parameter
            //        return dataTableTestCmdParamCache;

            //    case "LoginGrid":
            //        // Open Test Item
            //        return dataTableAllTestItemsCache;

            //    default:
            //        return null;
            //}

            /* if else method
            //if (DataGridAutomationID == "PGGrid")
            //// Test Item Context
            //    return new MemoryCache<(int?, string), IWebElement>(dataTableTestItemsCache);

            ////// Variable
            //else if (DataGridAutomationID == "CndGrid")
            //// Condition
            //    return new MemoryCache<(int?, string), IWebElement>(dataTableConditionCache);

            //else if (DataGridAutomationID == "RstGrid")
            //// Result
            //    return new MemoryCache<(int?, string), IWebElement>(dataTableResultCache);

            //else if (DataGridAutomationID == "TmpGrid")
            //// Temp
            //    return new MemoryCache<(int?, string), IWebElement>(dataTableTempCache);

            //else if (DataGridAutomationID == "GlbGrid")
            //// Global
            //    return new MemoryCache<(int?, string), IWebElement>(dataTableGlobalCache);

            //else if (DataGridAutomationID == "DefectCodeGrid")
            //// Defect Code
            //    return new MemoryCache<(int?, string), IWebElement>(dataTableDefectCodeCache);

            //else if (DataGridAutomationID == "ParameterGrid")
            //// Test command Parameter
            //    return new MemoryCache<(int?, string), IWebElement>(dataTableTestCmdParamCache);

            //else if (DataGridAutomationID == "LoginGrid")
            //    // Test command Parameter
            //    return new MemoryCache<(int?, string), IWebElement>(dataTableAllTestItemsCache);

            //else return null;
            */
        }

        public IWebElement GetDataTableElement(DataTableAutoIDType DataGridType)
        {
            string DataGridAutomationID = DataGridType.ToString();

            if (dataTablesCache.Get(DataGridAutomationID) == null)
                dataTablesCache.Add(DataGridAutomationID, PP5IDEWindow.GetElement(MobileBy.AccessibilityId(DataGridAutomationID)));

            return dataTablesCache.Get(DataGridAutomationID);

            /* Legacy method  
            //switch (DataGridType)
            //{
            //    case DataTableAutoIDType.PGGrid:
            //        // Test Item Context
            //        return CurrentDriver.GetElement(MobileBy.AccessibilityId(DataTableAutoIDType.PGGrid.ToString()));

            //    case DataTableAutoIDType.CndGrid:
            //        // Condition
            //        return CurrentDriver.GetElement(MobileBy.AccessibilityId(DataTableAutoIDType.CndGrid.ToString()));

            //    case DataTableAutoIDType.RstGrid:
            //        // Result
            //        return CurrentDriver.GetElement(MobileBy.AccessibilityId(DataTableAutoIDType.RstGrid.ToString()));

            //    case DataTableAutoIDType.TmpGrid:
            //        // Temp
            //        return CurrentDriver.GetElement(MobileBy.AccessibilityId(DataTableAutoIDType.TmpGrid.ToString()));

            //    case DataTableAutoIDType.GlbGrid:
            //        // Global
            //        return CurrentDriver.GetElement(MobileBy.AccessibilityId(DataTableAutoIDType.GlbGrid.ToString()));

            //    case DataTableAutoIDType.DefectCodeGrid:
            //        // Defect Code
            //        return CurrentDriver.GetElement(MobileBy.AccessibilityId(DataTableAutoIDType.DefectCodeGrid.ToString()));

            //    case DataTableAutoIDType.ParameterGrid:
            //        // Test command Parameter
            //        return CurrentDriver.GetElement(MobileBy.AccessibilityId(DataTableAutoIDType.ParameterGrid.ToString()));

            //    case DataTableAutoIDType.LoginGrid:
            //        // Open Test Item
            //        return CurrentDriver.GetElement(MobileBy.AccessibilityId(DataTableAutoIDType.LoginGrid.ToString()));

            //    default:
            //        return null;
            //}
            */
        }

        internal static bool CheckLoginPageIsOpened()
        {
            //AutoUIExecutor.SwitchTo(SessionType.PP5IDE);
            //WindowsElement PP5_TIEditorWindow= CurrentDriver.FindElement(By.Name(PowerPro5Config.IDE_TIEditorWindowName));
            //return PP5_TIEditorWindow != null;
            string WindowTitle = PowerPro5Config.LoginWindowName;
            return CheckWindowOpened(WindowTitle);
        }

        internal static bool CheckMainPanelIsOpened()
        {
            //AutoUIExecutor.SwitchTo(SessionType.MainPanel);
            //WindowsElement PP5_MainPanel = CurrentDriver.FindElement(By.Name(PowerPro5Config.MainPanelWindowName));
            //return PP5_MainPanel != null;
            string WindowTitle = PowerPro5Config.MainPanelWindowName;
            return CheckWindowOpened(WindowTitle);
        }

        internal static bool CheckTIEditorWindowIsOpened()
        {
            //AutoUIExecutor.SwitchTo(SessionType.PP5IDE);
            //WindowsElement PP5_TIEditorWindow= CurrentDriver.FindElement(By.Name(PowerPro5Config.IDE_TIEditorWindowName));
            //return PP5_TIEditorWindow != null;
            string WindowTitle = PowerPro5Config.IDE_TIEditorWindowName;
            return CheckWindowOpened(WindowTitle);
        }

        internal static bool CheckGUIEditorWindowIsOpenedByName(string GUIFileName = "Demo")
        {
            string GUIWindowTitle = PowerPro5Config.IDE_GUIEditorWindowName;
            if (GUIFileName != "Demo")
                GUIWindowTitle = PowerPro5Config.IDE_GUIEditorWindowName.Replace("Demo", GUIFileName);

            return CheckWindowOpened(GUIWindowTitle);
        }

        internal static bool CheckGUIEditorWindowIsOpened()
        {
            IWebElement currWindow = PP5IDEWindow;
            return Regex.IsMatch(currWindow.Text, @"Chroma ATS IDE - \[GUI Editor - .+\]");
        }

        internal static bool CheckPP5WindowIsOpened(WindowType windowType)
        {
            IWebElement currWindow = PP5IDEWindow;
            
            if (currWindow == null) return false;

            if (windowType == WindowType.GUIEditor || windowType == WindowType.OnlineControl || windowType == WindowType.Report)
            {
                return Regex.IsMatch(currWindow.Text, @$"Chroma ATS IDE - \[{windowType.GetDescription()} - .+\]");
            }
            else
            {
                return Regex.IsMatch(currWindow.Text, @$"Chroma ATS IDE - \[{windowType.GetDescription()}\]");
            }
        }

        internal static bool IsPP5Window(string windowName)
        {
            return Regex.IsMatch(windowName, @$"Chroma ATS IDE - \[.+\]");
        }

        internal static IWebElement GetPP5Window()
        {
            var currWindow = CurrentDriver.SwitchToWindow(out bool _);

            if (currWindow == null || !currWindow.Title.Contains("Chroma ATS IDE")) return null;

            return currWindow.GetElement(By.Name(currWindow.Title));

            //if (windowType == WindowType.GUIEditor || windowType == WindowType.OnlineControl || windowType == WindowType.Report)
            //{
            //    return Regex.IsMatch(currWindow.Title, @$"Chroma ATS IDE - \[{windowType.GetDescription()} - .+\]");
            //}
            //else
            //{
            //    return Regex.IsMatch(currWindow.Title, @$"Chroma ATS IDE - \[{windowType.GetDescription()}\]");
            //}
        }

        static bool GetCommandSourceType(string groupName, out bool IsDevice)
        {
            IsDevice = false;
            if (!cmdGroupDataDict.AsDictionary().TryGetValue(groupName, out CommandGroupData cgdata))
            {
                //Console.WriteLine($"GroupName with name '{groupName}' not found.");
                return false;
            }
            IsDevice = cgdata.IsDevice;
            return true;
        }

        internal void PP5IDEWindowRefresh()
        {
            AutoUIExecutor.SwitchTo(SessionType.PP5IDE);
            _PP5IDEWindow = GetPP5Window();
        }

        public static string GetCommandFileFullPath()
        {
           return string.Format(PowerPro5Config.SubPathPattern, PowerPro5Config.ReleaseDataFolder, PowerPro5Config.SystemCommandFileName);
        }


        public void SetColor(IWebElement colorTabItem, ColorSettingType colorSettingType, string colorCode = "default" /*default color: transparent White (#00FFFFFF)*/)
        {
            var FontColorEditBtn = colorTabItem.GetCustomElement(colorSettingType.GetDescription(), (e) => e.Enabled);
            FontColorEditBtn.LeftClick();

            SetColor(colorCode);
        }

        public void SetColor(IWebElement colorTabItem, ColorSettingType colorSettingType, Colors colorType /*default color: transparent White (#00FFFFFF)*/)
        {
            var FontColorEditBtn = colorTabItem.GetCustomElement(colorSettingType.GetDescription(), (e) => e.Enabled);
            FontColorEditBtn.LeftClick();

            string colorCode = colorType.GetDescription();
            SetColor(colorCode);
        }

        private void SetColor(string colorCode)
        {
            //string[] colorNames = Enum.GetNames(typeof(Colors));
            if (!colorCode.Contains('#') && colorCode == "default")
            {
                PP5IDEWindow.GetElement(MobileBy.AccessibilityId("DefaultColor"))
                            .GetFirstListBoxItemElement()
                            .LeftClick();
            }
            else
            {
                List<string> colorCodeList = TypeExtension.GetEnumDescriptions<Colors>();
                Logger.LogMessage($"colorCode:{colorCode}");
                int colorIndex = colorCodeList.IndexOf(colorCode);
                Logger.LogMessage($"colorIndex:{colorIndex}");

                //PP5IDEWindow.GetElement(MobileBy.AccessibilityId("DefaultPicker"))
                //            .SelectComboBoxItemByIndex(colorIndex, supportKeyInputSearch: false);

                ComboBoxSelectByIndex(PP5IDEWindow.GetElement(MobileBy.AccessibilityId("DefaultPicker")), colorIndex);
            }
        }

        //// For TP Editor, later to do
        //private bool getTISourceType(string groupName, out bool IsDevice)
        //{
        //    if (!cmdGroupSourceTypeDict.TryGetValue(groupName, out IsDevice))
        //    {
        //        Console.WriteLine($"GroupName with name '{groupName}' not found.");
        //        return false;
        //    }
        //    return true;
        //}

        //private static void SetTimer(string procName, int updateMilliseconds)
        //{
        //    // Create a timer with a 1 minute interval.
        //    _timer = new System.Timers.Timer(updateMilliseconds); // 60000 milliseconds = 1 minute
        //    _timer.Elapsed += (sender, e) => OnTimedEvent(sender, e, procName);
        //    _timer.AutoReset = true;
        //    _timer.Enabled = true;
        //}

        //private static void OnTimedEvent(Object source, ElapsedEventArgs e, string procName)
        //{
        //    Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}", e.SignalTime);
        //    // Call your method here
        //    CaptureApplicationScreenshot(procName);
        //}

        //internal bool CheckTreeViewIsCollapsed(IWebElement TreeItem)
        //{
        //    return TreeItem.GetAttribute("ExpandCollapse.ExpandCollapseState") == ExpandCollapseState.Collapsed.ToString();
        //}
    }
}
