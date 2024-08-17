using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP5AutoUITests
{
    /// <summary>
    /// 定義 Test Item type
    /// </summary>
    public enum TestItemType : int
    {
        [System.ComponentModel.Description("TI")]
        TI = 0,
        [System.ComponentModel.Description("Sub-TI")]
        SubTI,
        [System.ComponentModel.Description("Thread-TI")]
        ThreadTI,
    }

    /// <summary>
    /// 定義 Test Item run type
    /// </summary>
    public enum TestItemRunType : int
    {
        [System.ComponentModel.Description("Pre Test")]
        Pre = 0,
        [System.ComponentModel.Description("UUT Test")]
        UUT,
        [System.ComponentModel.Description("Post Test")]
        Post,
    }

    /// <summary>
    /// 定義 Test Item Source type
    /// </summary>
    public enum TestItemSourceType : int
    {
        [System.ComponentModel.Description("System")]
        System = 0,
        [System.ComponentModel.Description("User-Defined")]
        UserDefined,
    }
}
