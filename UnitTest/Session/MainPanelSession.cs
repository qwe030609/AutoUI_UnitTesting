using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;

namespace PP5AutoUITests.Session
{
    public class MainPanelDriver : DriverBase, IDriver
    {
        public MainPanelDriver()
        {
            SessionName = PowerPro5Config.MainPanelProcessName;
            WindowName = PowerPro5Config.MainPanelWindowName;
            sessionType = SessionType.MainPanel;
            //targetAppPath = @"C:\Users\adam.chen\Desktop\Debug\bin\Chroma.MainPanel.exe";
            //targetAppWorkingDir = @"C:\Users\adam.chen\Desktop\Debug\bin\";
            WaitForAppLaunch = 35;
            base.targetAppPath = @"C:\Program Files (x86)\Chroma\PowerPro5\Bin\Chroma.MainPanel.exe";
            base.targetAppWorkingDir = @"C:\Program Files (x86)\Chroma\PowerPro5\Bin\";
        }

        public WindowsDriver<WindowsElement> CreateNewDriver()
        {
            OpenQA.Selenium.Appium.AppiumOptions appCapabilities = new OpenQA.Selenium.Appium.AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", targetAppPath);
            appCapabilities.AddAdditionalCapability("appWorkingDir", targetAppWorkingDir);
            appCapabilities.AddAdditionalCapability("deviceName", DeviceName);
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", WaitForAppLaunch);
            currentDriver = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);

            Assert.IsNotNull(currentDriver);

            // Set implicit timeout to 1.5 seconds to make element search to retry every 500 ms for at most three times
            //currentDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10.0);
            return currentDriver;
        }

        public override WindowsDriver<WindowsElement> CreateDriver()
        {
            processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(targetAppPath));
            if (processes.Length == 0)
            {
                CreateNewDriver();
            }
            else
            {
                AttachExistingDriver();
            }
            return currentDriver;
        }

        //public new void AttachExistingDriver()
        //{
        //    base.AttachExistingDriver();
        //}
    }
}
