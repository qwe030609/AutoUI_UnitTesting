using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Threading;
using OpenQA.Selenium.Appium.Windows;

namespace PP5AutoUITests
{
    public static class ThreadHelper
    {
        public static bool WaitUntil(Func<bool> condition, int millisecondsTimeout = 3000, int nTryCount = 5)
        {
            bool isConditionMet = false;

            while (nTryCount-- > 0)
            {
                try
                {
                    isConditionMet = SpinWait.SpinUntil(condition, millisecondsTimeout);
                }
                catch
                {
                }

                if (isConditionMet)
                {
                    break;
                }
                //else
                //{
                //    System.Threading.Thread.Sleep(10);
                //}
            }

            return isConditionMet;
        }
    }
}
