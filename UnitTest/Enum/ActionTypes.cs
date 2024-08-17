using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP5AutoUITests
{
    /// <summary>
    /// 定義 Auto UI 動作種類
    /// </summary>
    public enum ActionType : int
    {
        /// <summary>
        /// No action, only get element
        /// </summary>
        None,
        /// <summary>
        /// 滑鼠左鍵單擊 (left mouse button single click)
        /// </summary>
        LeftClick = 1,
        /// <summary>
        /// 滑鼠左鍵雙擊 (left mouse button double click)
        /// </summary>
        LeftDoubleClick = 2,
        /// <summary>
        /// 鍵盤輸入
        /// </summary>
        SendKeys = 3,
    }

    /// <summary>
    /// 定義Test Program 動作種類
    /// </summary>
    public enum TPAction : int
    {
        /// <summary>
        /// Switch to page: Pre Test
        /// </summary>
        SwitchToPreTestPage = 0,
        /// <summary>
        /// Switch to page: UUT Test
        /// </summary>
        SwitchToUUTTestPage,
        /// <summary>
        /// Switch to page: Post Test
        /// </summary>
        SwitchToPostTestPage,
        /// <summary>
        /// Switch to page: System TI
        /// </summary>
        SwitchToSystemTIPage,
        /// <summary>
        /// Switch to page: User-Defined TI
        /// </summary>
        SwitchToUserDefinedTIPage,
        /// <summary>
        /// Switch to page: Test item > Parameter (condition)
        /// </summary>
        SwitchToTestConditionVariablePage,
        /// <summary>
        /// Switch to page: Test item > Vector
        /// </summary>
        SwitchToVectorVariablePage,
        /// <summary>
        /// Switch to page: Test item > Global
        /// </summary>
        SwitchToGlobalVariablePage,
        /// <summary>
        /// Switch to page: Test item > Result
        /// </summary>
        SwitchToResultVariablePage,
        /// <summary>
        /// Switch to page: TPInfo
        /// </summary>
        SwitchToTPInfoPage,
        /// <summary>
        /// Switch to page: Report Format > ByTI
        /// </summary>
        SwitchToReportFormatByTIPage,
        /// <summary>
        /// Switch to page: Report Format > ByTP
        /// </summary>
        SwitchToReportFormatByTPPage,
    }
}
