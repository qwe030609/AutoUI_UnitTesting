using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace PP5AutoUITests
{
    internal class PowerPro5Config
    {
        internal const string SubPathPattern = "{0}/{1}";
        internal const string SubsubPathPattern = "{0}/{1}/{2}";
        internal const string ReleaseFolder = "C:/Program Files (x86)/Chroma/PowerPro5";
        internal const string ReleaseTIUserPreTestFolder = "C:/Program Files (x86)/Chroma/PowerPro5/TestItem/UserDefined/TI/PreTest";
        internal const string ReleaseDataFolder = "C:/Program Files (x86)/Chroma/PowerPro5/Data";
        internal const string SystemCommandFileName = "SystemCommand.csx";
        internal const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723/";
        //internal const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723/wd/hub";
        //internal const string WindowsApplicationDriverUrl = "http://127.0.0.1:4444/wd/hub";

        internal const string MainPanelProcessName = "Chroma.MainPanel";
        //internal const string IDE_TIEditorProcessName = "IDE_TIEditor";
        internal const string IDEProcessName = "Chroma.PP5IDE";

        internal const string MainPanelWindowName = "Chroma ATE - MainPanel";
        internal const string IDEWindowName = "Chroma ATS IDE";
        internal const string IDE_TIEditorWindowName = "Chroma ATS IDE - [TI Editor]";
        internal const string IDE_TPEditorWindowName = "Chroma ATS IDE - [TP Editor]";
        internal const string IDE_GUIEditorWindowName = "Chroma ATS IDE - [GUI Editor - Demo]";
        internal const string IDE_ManagementWindowName = "Chroma ATS IDE - [Management]";
        internal const string LoginWindowName = "Login";

        internal const string LoginWindowAutomationID = "LoginWindow";
        internal const string MainPanelAutomationID = "winMainPanel";

        internal static string PP5WindowHandleHex;
        internal static string PP5WindowSessionType;
    }
}
