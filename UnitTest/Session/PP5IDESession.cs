using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Appium.Windows;

namespace PP5AutoUITests.Session
{
    public class PP5IDEDriver : DriverBase, IDriver
    {
        public PP5IDEDriver()
        {
            SessionName = PowerPro5Config.IDEProcessName;
            WindowName = PowerPro5Config.IDE_TIEditorWindowName;
            sessionType = SessionType.PP5IDE;
            //targetAppPath = @"C:\Users\adam.chen\Desktop\Debug\bin\Chroma.PP5IDE.exe";
            //targetAppWorkingDir = @"C:\Users\adam.chen\Desktop\Debug\bin\";
            base.targetAppPath = @"C:\Program Files (x86)\Chroma\PowerPro5\Bin\Chroma.PP5IDE.exe";
            base.targetAppWorkingDir = @"C:\Program Files (x86)\Chroma\PowerPro5\Bin\";
        }

        public WindowsDriver<WindowsElement> CreateNewDriver()
        {
            sessionType = SessionType.Desktop;

            OpenQA.Selenium.Appium.AppiumOptions appCapabilities = new OpenQA.Selenium.Appium.AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", "Root");
            appCapabilities.AddAdditionalCapability("deviceName", DeviceName);
            currentDriver = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);

            PowerPro5Config.PP5WindowSessionType = sessionType.ToString();

            // Set implicit timeout to 1.5 seconds to make element search to retry every 500 ms for at most three times
            //currentDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1.5);
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
