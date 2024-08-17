using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using DisableDevice;

namespace PP5AutoUITests
{
    internal class DllHelper
    {
        internal const int SW_SHOWNORMAL = 1;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        //private const int WM_CLOSE = 0x0010;
        //private const int WM_QUIT = 0x12;

        //internal static void CloseWindow(IntPtr hwnd)
        //{
        //    SendMessage(hwnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        //}

        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_CLOSE = 0xF060;

        internal static void CloseWindow(IntPtr hwnd)
        {
            SendMessage(hwnd, WM_SYSCOMMAND, (IntPtr)SC_CLOSE, IntPtr.Zero);
        }

        //internal static void QuitWindow(IntPtr hwnd)
        //{
        //    SendMessage(hwnd, WM_QUIT, IntPtr.Zero, IntPtr.Zero);
        //}

        /// <summary>
        /// 根据窗口标题查找窗体
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "FindWindow")]
        internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 根据句柄查找进程ID
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("User32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        internal static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);

        internal static void DisableMemoryMonitorWindow()
        {
            int registerValue = 0;

            const string RegisterAddress = "Software\\Chroma\\PowerPro5";
            const string RegisterKey = "MemoryCheckEnabled";

            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegisterAddress, writable: true))
                {
                    if (key != null)
                    {
                        Object o = key.GetValue(RegisterKey);
                        if (o != null && (int)o == registerValue) { }
                        else
                            key.SetValue(RegisterKey, registerValue);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        internal static void EnableMouse(bool enable)
        {
            // every type of device has a hard-coded GUID, this is the one for mice
            Guid mouseGuid = new Guid("{4d36e96f-e325-11ce-bfc1-08002be10318}");

            // get this from the properties dialog box of this device in Device Manager
            string instancePath = @"HID\VID_093A&PID_2510\6&3399AC26&0&0000";

            DeviceHelper.SetDeviceEnabled(mouseGuid, instancePath, enable);

            if (enable)
                Console.WriteLine("Mouse is enabled.");
            else
                Console.WriteLine("Mouse is disabled.");
        }
    }
}
