using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace PP5AutoUITests
{
    public interface IModules
    {
        void OpenDefaultModuleWindow();
        void OpenDefaultModuleWindow(string name);
    }
}
