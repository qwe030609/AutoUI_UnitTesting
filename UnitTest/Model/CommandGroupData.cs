using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP5AutoUITests
{
    internal class CommandGroupData
    {
        // Constructor with optional parameters, chaining to the main constructor
        internal CommandGroupData(int groupId, bool isDevice)
        {
            IsDevice = isDevice;
            GroupID = groupId;
            CommandNames = new List<string>();
        }

        internal bool IsDevice { get; set; }
        internal int GroupID { get; set; }
        internal List<string> CommandNames { get; set; }
    }
}
