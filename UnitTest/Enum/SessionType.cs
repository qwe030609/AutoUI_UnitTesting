using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP5AutoUITests
{
    /// <summary>
    /// 定義 Auto UI 執行器種類
    /// </summary>
    public enum SessionType : int
    {
        /// <summary>
        /// Desktop
        /// </summary>
        Desktop = 0,
        /// <summary>
        /// By Main Panel
        /// </summary>
        MainPanel,
        /// <summary>
        /// By IDE
        /// </summary>
        PP5IDE,
    }
}
