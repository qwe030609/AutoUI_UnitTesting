using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP5AutoUITests
{
    /// <summary>
    /// Combined enum class for all tabs
    /// </summary>
    public enum DataTableAutoIDType
    {
        CndGrid = VariableTabType.Condition,
        RstGrid = VariableTabType.Result,
        TmpGrid = VariableTabType.Temp,
        GlbGrid = VariableTabType.Global,
        DefectCodeGrid = VariableTabType.DefectCode,
        PGGrid = TestItemTabType.TIContext,
        ParameterGrid = 6,
        LoginGrid = 7,
    }
}
