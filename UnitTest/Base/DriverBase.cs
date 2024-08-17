using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.Appium.Windows;

namespace PP5AutoUITests
{
    public abstract class DriverBase
    {
        public string SessionName { get; set; }
        public string WindowName { get; set; }

        public SessionType sessionType { get; set; }

        public WindowsDriver<WindowsElement> currentDriver;

        public Process[] processes;

        public string targetAppPath { get; set; }

        public string targetAppWorkingDir { get; set; }
        public int WaitForAppLaunch { get; set; }

        public const string DeviceName = "WindowsPC";
        public const string WindowsApplicationDriverUrl = PowerPro5Config.WindowsApplicationDriverUrl;

        //public abstract void CreateNewDriver();
        //protected abstract void AttachExistingDriver();

        public WindowsDriver<WindowsElement> AttachExistingDriver()
        {
            processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(targetAppPath));
            foreach (Process clsProcess in processes)
            {
                //// Attaching to existing Application Window
                //if (clsProcess.ProcessName.Contains(SessionName) && clsProcess != null)
                //{

                //    PP5WindowHandle = clsProcess.MainWindowHandle;
                //    var PP5WindowHandleHex = PP5WindowHandle.ToString("X");
                //    OpenQA.Selenium.Appium.AppiumOptions appCapabilities = new OpenQA.Selenium.Appium.AppiumOptions();
                //    appCapabilities.AddAdditionalCapability("appTopLevelWindow", PP5WindowHandleHex);
                //    currentSession = new WindowsDriver<WindowsElement>(new Uri(PowerPro5Config.WindowsApplicationDriverUrl), appCapabilities);
                //    return;
                //}

                // Attaching to existing Application Window
                if (clsProcess.ProcessName.Contains(SessionName) && clsProcess != null)
                {
                    if (processes.Length > 1)
                        if (clsProcess.MainWindowTitle == "") continue;

                    IntPtr hWnd = clsProcess.MainWindowHandle;

                    // Check current process is opened already
                    if (clsProcess.Id != Process.GetCurrentProcess().Id)
                    {
                        // main Panel 程序視窗已隱藏，其 handle = 0
                        if (hWnd.ToInt32() == 0)
                        {
                            //使用FindWindow获取窗口句柄
                            hWnd = DllHelper.FindWindow(null, WindowName);

                            int id = -1;

                            // 用GetWindowThreadProcessId函数来验证句柄属否属于该进程
                            DllHelper.GetWindowThreadProcessId(hWnd, out id);
                            if (id != clsProcess.Id)
                                continue;
                        }

                        // hWnd 為之前運行的 main Panel 程序之 handle，attach 到 appium session
                        PowerPro5Config.PP5WindowHandleHex = string.Format("0x{0:X8}", hWnd.ToInt64());
                        PowerPro5Config.PP5WindowSessionType = sessionType.ToString();
                        //PowerPro5Config.PP5WindowHandleHex = hWnd.ToString("X");
                        OpenQA.Selenium.Appium.AppiumOptions appCapabilities = new OpenQA.Selenium.Appium.AppiumOptions();
                        //appCapabilities.AddAdditionalCapability("app", targetAppPath);
                        //appCapabilities.AddAdditionalCapability("appWorkingDir", targetAppWorkingDir);
                        appCapabilities.AddAdditionalCapability("appTopLevelWindow", PowerPro5Config.PP5WindowHandleHex);
                        //appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", 1);

                        // 2024/01/30, Adam updated
                        // Work-around method to handle webdriver timeout when re-attached PP5 app to driver
                        //
                        // timeout Error:
                        // 組件初始設定方法 PP5AutoUITests.TestBase.BeforeClass 擲回例外狀況。
                        // OpenQA.Selenium.WebDriverException: OpenQA.Selenium.WebDriverException:
                        // Operation timed out. (0x80131505)。正在中止測試執行。
                        try
                        {
                            currentDriver = new WindowsDriver<WindowsElement>(new Uri(PowerPro5Config.WindowsApplicationDriverUrl), appCapabilities, TimeSpan.FromSeconds(30));
                        }
                        catch (OpenQA.Selenium.WebDriverException ex) 
                        {
                            //Console.Write(ex.ToString());
                        }
                        finally
                        {
                            currentDriver = new WindowsDriver<WindowsElement>(new Uri(PowerPro5Config.WindowsApplicationDriverUrl), appCapabilities, TimeSpan.FromSeconds(30));
                        }
                        
                        // Set implicit timeout to 1.5 seconds to make element search to retry every 500 ms for at most three times
                        //currentDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1.5); // Adjust the timeout as needed
                        //return currentDriver;
                    }
                }
            }
            return currentDriver;
        }

        public abstract WindowsDriver<WindowsElement> CreateDriver();
        //public WindowsDriver<WindowsElement> CreateDriver()
        //{
        //    processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(targetAppPath));
        //    if (processes.Length == 0)
        //    {
        //        CreateNewDriver();
        //    }
        //    else
        //    {
        //        AttachExistingDriver();
        //    }
        //    return currentDriver;
        //}

        //public WindowsDriver<WindowsElement> GetCurrentDriver()
        //{
        //    return currentDriver;
        //}
    }
}
