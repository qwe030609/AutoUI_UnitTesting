using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP5AutoUITests
{
    /// <summary>
    /// 定義Variable子視窗種類
    /// </summary>
    public enum VariableTabType : int
    {
        /// <summary>
        /// Condition variable
        /// </summary>
        [System.ComponentModel.Description("CndGrid")]
        Condition = 0,
        /// <summary>
        /// Result variable
        /// </summary>
        [System.ComponentModel.Description("RstGrid")]
        Result = 1,
        /// <summary>
        /// Temp variable
        /// </summary>
        [System.ComponentModel.Description("TmpGrid")]
        Temp = 2,
        /// <summary>
        /// Global variable
        /// </summary>
        [System.ComponentModel.Description("GlbGrid")]
        Global = 3,
        /// <summary>
        /// Defect Code variable
        /// </summary>
        [System.ComponentModel.Description("DefectCodeGrid")]
        DefectCode = 4,
    }

    /// <summary>
    /// 定義Test Item子視窗種類
    /// </summary>
    public enum TestItemTabType : int
    {
        /// <summary>
        /// TI Context
        /// </summary>
        [System.ComponentModel.Description("PGGrid")]
        TIContext = 5,
        /// <summary>
        /// TI Description
        /// </summary>
        TIDescription = 6,
    }
}
