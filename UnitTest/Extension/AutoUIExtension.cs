using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using Chroma.UnitTest.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;

namespace PP5AutoUITests
{
    //[TestClass]
    public static class AutoUIExtension
    {
        static readonly string failCasesFolder = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName.ToString(), "TestItemScreenshots", "FAIL");
        static readonly string passCasesFolder = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName.ToString(), "TestItemScreenshots", "PASS");

        //[TestMethod]
        public static void TakeScreenshotDefaultImageFormat(bool bSuccess, string testCaseNo)
        {
            try
            {
                Screenshot TakeScreenshot = ((ITakesScreenshot)Executor.GetInstance().GetCurrentDriver()).GetScreenshot();
                if (!Directory.Exists(failCasesFolder))
                    Directory.CreateDirectory(failCasesFolder);

                if (!Directory.Exists(passCasesFolder))
                    Directory.CreateDirectory(passCasesFolder);

                if (bSuccess)
                {
                    TakeScreenshot.SaveAsFile(Path.Combine(passCasesFolder, testCaseNo));
                }
                else
                    TakeScreenshot.SaveAsFile(Path.Combine(failCasesFolder, testCaseNo));
            }
            catch (Exception e)
            {
                Logger.LogMessage(e.StackTrace);
            }
        }
        //[TestMethod]
        public static void TakeScreenshotPNGformat(bool bSuccess, string testCaseNo)
        {
            //Using the SaveAsFile() method. We can specify the desired format using System.Drawing.Imaging.ImageFormat.
            try
            {
                Screenshot TakeScreenshot = ((ITakesScreenshot)Executor.GetInstance().GetCurrentDriver()).GetScreenshot();

                if (!Directory.Exists(failCasesFolder))
                    Directory.CreateDirectory(failCasesFolder);

                if (!Directory.Exists(passCasesFolder))
                    Directory.CreateDirectory(passCasesFolder);

                if (bSuccess)
                {
                    TakeScreenshot.SaveAsFile(Path.Combine(passCasesFolder, testCaseNo + "." + System.Drawing.Imaging.ImageFormat.Png));
                }
                else
                    TakeScreenshot.SaveAsFile(Path.Combine(failCasesFolder, testCaseNo + "." + System.Drawing.Imaging.ImageFormat.Png));
            }
            catch (Exception e)
            {
                Logger.LogMessage(e.StackTrace);
            }
        }

        public static void TakeScreenshotJPEGformat(bool bSuccess, string testCaseNo)
        {
            try
            {
                Screenshot TakeScreenshot = ((ITakesScreenshot)Executor.GetInstance().GetCurrentDriver()).GetScreenshot();
                if (!Directory.Exists(failCasesFolder))
                    Directory.CreateDirectory(failCasesFolder);

                if (!Directory.Exists(passCasesFolder))
                    Directory.CreateDirectory(passCasesFolder);

                if (bSuccess)
                {
                    TakeScreenshot.SaveAsFile(Path.Combine(passCasesFolder, testCaseNo + "." + System.Drawing.Imaging.ImageFormat.Jpeg));
                }
                else
                    TakeScreenshot.SaveAsFile(Path.Combine(failCasesFolder, testCaseNo + "." + System.Drawing.Imaging.ImageFormat.Jpeg));
            }
            catch (Exception e)
            {
                Logger.LogMessage(e.StackTrace);
            }
        }

        public static Screenshot GetElementImageFromScreenshot(this IWebElement element)
        {
            //// Find the element
            //IWebElement ele = Executor.GetInstance().GetCurrentDriver().FindElement(By.Id("hplogo"));

            // Take screenshot of the entire page
            Screenshot screenshot = ((ITakesScreenshot)Executor.GetInstance().GetCurrentDriver()).GetScreenshot();
            using (var screenshotStream = new MemoryStream(screenshot.AsByteArray))
            {
                using (var fullImg = Image.FromStream(screenshotStream))
                {
                    // Get the location of the element on the page
                    Point point = element.Location;

                    // Get width and height of the element
                    int eleWidth = element.Size.Width;
                    int eleHeight = element.Size.Height;

                    // Crop the screenshot to get only the element's screenshot
                    using (var eleScreenshot = new Bitmap(eleWidth, eleHeight))
                    {
                        using (var g = Graphics.FromImage(eleScreenshot))
                        {
                            g.DrawImage(fullImg, new Rectangle(0, 0, eleWidth, eleHeight),
                                        new Rectangle(point.X, point.Y, eleWidth, eleHeight),
                                        GraphicsUnit.Pixel);
                        }

                        // Convert cropped image to byte array
                        using (var croppedStream = new MemoryStream())
                        {
                            eleScreenshot.Save(croppedStream, ImageFormat.Png);
                            byte[] croppedImageBytes = croppedStream.ToArray();

                            // Return a new Screenshot object from the byte array
                            return new Screenshot(Convert.ToBase64String(croppedImageBytes));
                        }

                        //// Save the cropped image to disk
                        //string screenshotLocation = @"C:\images\GoogleLogo_screenshot.png";
                        //eleScreenshot.Save(screenshotLocation, ImageFormat.Png);
                    }
                }
            }
        }

        public static string FileToBase64(string filePath)
        {
            try
            {
                byte[] imageBytes = File.ReadAllBytes(filePath);
                return Convert.ToBase64String(imageBytes);
            }
            catch (Exception ex)
            {
                Logger.LogMessage($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public static void CaptureApplicationScreenshot(string procName, string saveFolder = "")
        {
            string savePath;

            if (procName.IsNullOrEmpty() || Process.GetProcessesByName(procName).Length == 0)
                return;

            if (!Directory.Exists(saveFolder))
                Directory.CreateDirectory(saveFolder);

            savePath = System.IO.Path.Combine(saveFolder, DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + ".png");

            var proc = Process.GetProcessesByName(procName)[0];
            var rect = new User32.Rect();
            User32.GetWindowRect(proc.MainWindowHandle, ref rect);

            int width = rect.right - rect.left;
            int height = rect.bottom - rect.top;

            var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            using (Graphics graphics = Graphics.FromImage(bmp))
            {
                graphics.CopyFromScreen(rect.left, rect.top, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);
            }

            bmp.Save(savePath, ImageFormat.Png);
        }

        private class User32
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct Rect
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }

            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);
        }

        public static Process StartProcess(string driverFileName, bool useShellExecute = true, int WaitForAppLaunch = 0)
        {
            string processName = Path.GetFileNameWithoutExtension(driverFileName);
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes != null && processes.Length > 0)
            {
                //if (closeCurrentProcess)
                //    processes[0].Kill();
                //else
                    return processes[0];
            }

            ProcessStartInfo psi = new ProcessStartInfo(driverFileName);
            psi.UseShellExecute = useShellExecute;
            psi.Verb = "runas"; // run as administrator
            psi.WindowStyle = ProcessWindowStyle.Minimized;     // window minimized
            psi.WorkingDirectory = Path.GetDirectoryName(driverFileName);
            Process process = Process.Start(psi);
            Thread.Sleep(WaitForAppLaunch);
            return process;
        }

        public static IWebDriver SwitchToWindow(this WindowsDriver<WindowsElement> driver, out bool switchedSuccess)
        {
            //session.Manage().Window.FullScreen();
            if (driver == null)
            {
                switchedSuccess = false;
                return null;
            }

            var winhandles = driver.WindowHandles;
            if (winhandles.Count == 0 || !winhandles.Contains(PowerPro5Config.PP5WindowHandleHex))
            {
                switchedSuccess = false;
                return null;
            }

            IWebDriver currWindow = driver.SwitchTo().Window(winhandles[winhandles.IndexOf(PowerPro5Config.PP5WindowHandleHex)]);
            //IWebDriver currWindow = driver.SwitchTo().Window(driver.CurrentWindowHandle);
            switchedSuccess = true;
            return currWindow;

            /* Legacy window switching method
            //// Multiple windows detected, close upper window
            //if (session.WindowHandles.Count > 1)
            //{
            //    //IntPtr handleIntPtr;
            //    for (int i = 0; i < session.WindowHandles.Count; i++)
            //    {
            //        string currHandle = session.WindowHandles[i];
            //        //var aaa = DllHelper.FindWindow(null, PowerPro5Config.MainPanelWindowName).ToString("X");
            //        if (session.WindowHandles[i] != "0x00" + DllHelper.FindWindow(null, sessionMainWindowName).ToString("X"))
            //        {
            //            //handleIntPtr = new IntPtr(long.Parse(currHandle, System.Globalization.NumberStyles.HexNumber));
            //            //handleIntPtr = Marshal.StringToHGlobalAnsi(currHandle);
            //            int decimalValue = Convert.ToInt32(currHandle, 16);
            //            IntPtr handleIntPtr = (IntPtr) (decimalValue);
            //            DllHelper.CloseWindow(handleIntPtr);
            //        } 
            //    }
            //    //DllHelper.ShowWindow(handleIntPtr, DllHelper.SW_SHOWNORMAL);
            //}

            //var hwndMainWindow = "0x00" + DllHelper.FindWindow(null, sessionMainWindowName).ToString("X");
            //session.SwitchTo().Window(session.WindowHandles.ToList().Find(hwnd => hwnd == hwndMainWindow));

            //string currHandle = session.WindowHandles.ToList().Where(hwnd => hwnd == DllHelper.FindWindow(null, PowerPro5Config.MainPanelWindowName).ToString("X")).First();

            //// Convert the hexadecimal string to a long
            //long handleValue = long.Parse(currHandle, System.Globalization.NumberStyles.HexNumber);

            //// Create an IntPtr from the long value
            //IntPtr handleIntPtr = new IntPtr(handleValue);
            //DllHelper.ShowWindow(handleIntPtr, DllHelper.SW_SHOWNORMAL);
            */
        }

        //public static IWebDriver SwitchToMessageBox(this WindowsDriver<WindowsElement> driver, out bool switchedSuccess)
        //{
        //    //session.Manage().Window.FullScreen();
        //    if (driver.WindowHandles.Count == 0)
        //    {
        //        switchedSuccess = false;
        //        return null;
        //    }

        //    IWebDriver currentElement = driver.GetElement(By.);
        //    switchedSuccess = true;
        //    return currentElement;
        //}

        public static bool CheckWindowOpened(string windowTitle)
        {
            //Executor.GetInstance().SwitchTo(sessionType);
            var currWindow = Executor.GetInstance().GetCurrentDriver().SwitchToWindow(out bool switchedSuccess);
            if (!switchedSuccess) 
                return false;
            else
                return currWindow.Title == windowTitle;
            //WindowsElement window = Executor.GetInstance().GetCurrentDriver().GetElement(ByWindowInfo);
        }

        //public static bool CheckMessageBoxOpenedByName(this string messageBoxTitle)
        //{
        //    //Executor.GetInstance().SwitchTo(sessionType);
        //    WindowsElement window = Executor.GetInstance().GetCurrentDriver().GetElement(By.Name(messageBoxTitle));
        //    return window != null;
        //}

        //public static bool CheckMessageBoxOpenedById(this string messageBoxID)
        //{
        //    //Executor.GetInstance().SwitchTo(sessionType);
        //    WindowsElement window = Executor.GetInstance().GetCurrentDriver().GetElement(MobileBy.AccessibilityId(messageBoxID));
        //    return window != null;
        //}

        public static bool CheckMessageBoxOpened(By messageBoxInfo)
        {
            //Executor.GetInstance().SwitchTo(sessionType);
            //return window != null;
            return Executor.GetInstance().GetCurrentDriver().CheckElementExisted(messageBoxInfo);
        }

        public static IEnumerable<T> GetIntersectionWithOrder<T>(IEnumerable<T> listA, IEnumerable<T> listB)
        {
            // Use HashSet for faster lookups
            HashSet<T> setB = new HashSet<T>(listB);

            // Filter elements from listA that are also in listB
            IEnumerable<T> result = listA.Where(item => setB.Contains(item));

            return result;
        }
    }
}
