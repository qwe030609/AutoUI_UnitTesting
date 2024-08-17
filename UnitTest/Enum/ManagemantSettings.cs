using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP5AutoUITests
{
    /// <summary>
    /// 定義 System Setup > Color Setting page type
    /// </summary>
    public enum ColorSettingPageType : int
    {
        /// <summary>
        /// Test Item
        /// </summary>
        [System.ComponentModel.Description("ColorTIGroupTreeView")]
        TestItem = 0,
        /// <summary>
        /// Test Command
        /// </summary>
        [System.ComponentModel.Description("ColorTCGroupTreeView")]
        TestCommand,
    }
}
