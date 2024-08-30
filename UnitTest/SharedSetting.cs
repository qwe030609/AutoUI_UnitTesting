using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP5AutoUITests
{
    internal static class SharedSetting
    {
        internal static bool isScreenshotEnabled;
        internal static bool forceRefreshPP5Window;
        internal const int EXTREMESHORT_TIMEOUT = 100;
        internal const int SUPERSHORT_TIMEOUT = 1500;
        internal const int SHORT_TIMEOUT = 3000;
        internal const int NORMAL_TIMEOUT = 10000;
        internal const int LONG_TIMEOUT = 20000;
        internal const int SUPERLONG_TIMEOUT = 30000;
        internal const int EXTREMELONG_TIMEOUT = 40000;
    }
}
