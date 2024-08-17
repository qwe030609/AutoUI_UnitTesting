using System.Diagnostics;
using OpenQA.Selenium.Appium.Windows;

namespace PP5AutoUITests
{
    public interface IDriver
    {
        WindowsDriver<WindowsElement> CreateDriver();

        WindowsDriver<WindowsElement> AttachExistingDriver();

        WindowsDriver<WindowsElement> CreateNewDriver();
    }
}