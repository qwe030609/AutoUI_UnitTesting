using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP5AutoUITests
{
    /// <summary>
    /// 定義Test Program Parameter子視窗種類
    /// </summary>
    public enum TestProgramParameterTabType : int
    {
        /// <summary>
        /// Condition variable
        /// </summary>
        [System.ComponentModel.Description("Parameter")]
        Condition = 0,
        /// <summary>
        /// Result variable
        /// </summary>
        [System.ComponentModel.Description("Vector")]
        Vector,
        /// <summary>
        /// Temp variable
        /// </summary>
        [System.ComponentModel.Description("Global")]
        Global,
        /// <summary>
        /// Global variable
        /// </summary>
        [System.ComponentModel.Description("Result")]
        Result,
    }

    /// <summary>
    /// 定義Report Format子視窗種類
    /// </summary>
    public enum ReportFormatTabType : int
    {
        /// <summary>
        /// By TI
        /// </summary>
        [System.ComponentModel.Description("By TI")]
        ByTI = 0,
        /// <summary>
        /// By TP
        /// </summary>
        [System.ComponentModel.Description("By TP")]
        ByTP,
    }

    /// <summary>
    /// 定義Test Program Settings子視窗種類
    /// </summary>
    public enum TestProgramSettingTabType : int
    {
        /// <summary>
        /// Test Item
        /// </summary>
        [System.ComponentModel.Description("Test Item")]
        TestItem = 0,
        /// <summary>
        /// TPInfo
        /// </summary>
        [System.ComponentModel.Description("Test Item")]
        TPInfo,
        /// <summary>
        /// TPInfo
        /// </summary>
        [System.ComponentModel.Description("Report Format")]
        RptFormat,
    }
}
