using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP5AutoUITests
{
    /// <summary>
    /// 定義當前視窗種類
    /// </summary>
    public enum WindowType : int
    {
        //None = 0,
        ///// <summary>
        ///// Main Panel Login Window
        ///// </summary>
        //LoginWindow,
        ///// <summary>
        ///// Main Panel Menu Window
        ///// </summary>
        //MainPanelMenu,
        /// <summary>
        /// Test Item Editor Window
        /// </summary>
        [System.ComponentModel.Description("TI Editor")]
        TIEditor = 0,
        /// <summary>
        /// Test Program Editor Window
        /// </summary>
        [System.ComponentModel.Description("TP Editor")]
        TPEditor,
        /// <summary>
        /// Execution Window
        /// </summary>
        [System.ComponentModel.Description("Execution")]
        Execution,
        /// <summary>
        /// GUI Editor Window
        /// </summary>
        [System.ComponentModel.Description("GUI Editor")]
        GUIEditor,
        /// <summary>
        /// Hardware Configuration Window
        /// </summary>
        [System.ComponentModel.Description("Hardware Configuration")]
        HardwareConfig,
        /// <summary>
        /// Management Window
        /// </summary>
        [System.ComponentModel.Description("Management")]
        Management,
        /// <summary>
        /// On-Line Control Window
        /// </summary>
        [System.ComponentModel.Description("On-line Control")]
        OnlineControl,
        /// <summary>
        /// Report Window
        /// </summary>
        [System.ComponentModel.Description("Report")]
        Report,
    }
}
