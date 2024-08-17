using System.Collections.Generic;
using System.Diagnostics;
using OpenQA.Selenium.Appium.Windows;
using static PP5AutoUITests.ThreadHelper;
using static PP5AutoUITests.AutoUIExtension;
using OpenQA.Selenium;

namespace PP5AutoUITests
{
    public class Executor
    {
        #region Members
        /*
        //WindowsDriver<WindowsElement> PP5IDE_Session;
        //WindowsDriver<WindowsElement> MainPanel_Session;
        //WindowsDriver<WindowsElement> desktopSession;

        //public static WindowsDriver<WindowsElement> CurrentDriver
        //{
        //    get
        //    {
        //        //if (currentDriver.WindowHandles.Count > 0)
        //        //{
        //        //    currentDriver = (WindowsDriver<WindowsElement>)currentDriver.SwitchToWindow(out _);
        //        //}
        //        return currentDriver;
        //    }
        //}

        //private SessionType CurrentDriverType;
        //private Process winAppDriverProcess;

        //{
        //    get
        //    {
        //        if (!isWinAppDriverStarted)
        //            StartWinAppDriver();
        //        return winAppDriverProcess;
        //    }
        //}

        ////private bool isPP5Login;
        //public bool IsPP5Login
        //{
        //    get
        //    {
        //        return WindowTypeHelper.GetCurrentWindowType() != WindowType.LoginWindow && WindowTypeHelper.GetCurrentWindowType() != WindowType.None;
        //    }
        //}
        */

        private static Executor executor;
        private static WindowsDriver<WindowsElement> currentDriver;
        private static Dictionary<SessionType, WindowsDriver<WindowsElement>> UsedDrivers;
        public Process WinAppDriverProcess;

        private readonly string winAppDriverPath = @"C:\Program Files (x86)\Windows Application Driver\WinAppDriver.exe";
        //private readonly string PP5AppPath = @"C:\Users\adam.chen\Desktop\Debug\bin\Chroma.MainPanel.exe";
        private readonly string PP5AppPath = @"C:\Program Files (x86)\Chroma\PowerPro5\Bin\Chroma.MainPanel.exe";
        //public string WinAppDriverPath { get; set; }

        //private bool isWinAppDriverStarted = false;

        public WindowsDriver<WindowsElement> GetCurrentDriver()
        {
            return currentDriver;
        }

        #endregion

        #region Constructor

        public static Executor GetInstance()
        {
            if (executor == null)
                executor = new Executor();
            return executor;
        }

        #endregion

        #region Destructor
        ~Executor()
        {
            //// close winAppDriver
            //WinAppDriverProcess.Dispose();
            //WinAppDriverProcess.Close();
            //WinAppDriverProcess.Kill();

            // close current driver
            //currentDriver.Dispose();
            //currentDriver.Close();
            //currentDriver.CloseApp();
            //currentDriver.Quit();
        }
        #endregion

        private Executor()
        {
            Init();
        }

        #region Initialization
        private void Init()
        {
            //UsedDrivers = new Dictionary<SessionType, WindowsDriver<WindowsElement>>();
            StartWinAppDriver();
            StartPP5();

            SwitchTo(SessionType.MainPanel);

            //WaitUntil(() => CheckWindowOpened(PowerPro5Config.LoginWindowName));
        }

        #region WinAppDriver 

        // call WinAppDriver
        private void StartWinAppDriver()
        {
            WinAppDriverProcess = StartProcess(winAppDriverPath);
            //hideWinAppDriverWindow();
            //isWinAppDriverStarted = true;
        }

        private void StartPP5()
        {
            StartProcess(PP5AppPath, WaitForAppLaunch: 25000);
        }

        //private void hideWinAppDriverWindow()
        //{
        //    if (winAppDriverProcess.MainWindowHandle != IntPtr.Zero)
        //    {
        //        if (winAppDriverProcess.StartInfo.WindowStyle == ProcessWindowStyle.Normal) 
        //        {
        //            DllHelper.ShowWindow(winAppDriverProcess.MainWindowHandle, 0);
        //        }
        //    }
        //}

        //public Process GetWinAppDriverProcess()
        //{
        //    return winAppDriverProcess;
        //}

        #endregion

        //public WindowsDriver<WindowsElement> MyDesktopSessionElement
        //{
        //    get { return desktopSession; }
        //}

        //private void CreateNewSession()
        //{
        //    OpenQA.Selenium.Appium.AppiumOptions appCapabilities = new OpenQA.Selenium.Appium.AppiumOptions();
        //    appCapabilities.AddAdditionalCapability("app", "Root");
        //    appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
        //    desktopSession = new WindowsDriver<WindowsElement>(new Uri(PowerPro5Config.WindowsApplicationDriverUrl), appCapabilities);
        //}

        public void StartIDESession()
        {
            //CurrentDriver = DriverFactory.GetInstance().Create(SessionType.PP5IDE);
            if (UsedDrivers.ContainsKey(SessionType.PP5IDE))
            {
                UsedDrivers[SessionType.PP5IDE] = DriverFactory.GetInstance().Create(SessionType.PP5IDE);
            }
            else
            {
                UsedDrivers.Add(SessionType.PP5IDE, DriverFactory.GetInstance().Create(SessionType.PP5IDE));
            }
            //UsedDrivers.Add(SessionType.PP5IDE, DriverFactory.GetInstance().Create(SessionType.PP5IDE));
        }

        public void StartPP5MainPanelSession()
        {
            //CurrentDriver = DriverFactory.GetInstance().Create(SessionType.MainPanel);
            if (UsedDrivers.ContainsKey(SessionType.MainPanel))
            {
                UsedDrivers[SessionType.MainPanel] = DriverFactory.GetInstance().Create(SessionType.MainPanel);
            }
            else
            {
                UsedDrivers.Add(SessionType.MainPanel, DriverFactory.GetInstance().Create(SessionType.MainPanel));
            }
            //UsedDrivers.Add(SessionType.MainPanel, DriverFactory.GetInstance().Create(SessionType.MainPanel));
        }

        public IWebDriver SwitchTo(SessionType sessionType)
        {
            //UsedDrivers.TryGetValue(sessionType, out CurrentDriver);
            //currentDriver.ResetInputState();
            //currentDriver.ResetApp();

            if (currentDriver != null)
            {
                if (PowerPro5Config.PP5WindowSessionType == sessionType.ToString())
                    return currentDriver;
            }

            if (sessionType != SessionType.Desktop)
                currentDriver = DriverFactory.GetInstance().Attach(sessionType);
            else
                currentDriver = DriverFactory.GetInstance().Get(SessionType.PP5IDE).CreateNewDriver();

            return currentDriver;
        }
        #endregion
    }
}
