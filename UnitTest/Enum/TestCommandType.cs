using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP5AutoUITests
{
    /// <summary>
    /// 定義 Test Command Source type
    /// </summary>
    public enum TestCommandSourceType : int
    {
        [System.ComponentModel.Description("Device")]
        Device = 0,
        [System.ComponentModel.Description("System")]
        System,
    }
}
