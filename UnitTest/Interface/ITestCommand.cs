using System.Collections.ObjectModel;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;

namespace PP5AutoUITests
{
    public interface ITestCommand
    {
        void AddCommand(ReadOnlyCollection<IWebElement> cmdTreeItems, int cmdNumber, int addCount = 1);

        void SelectCommand(ReadOnlyCollection<IWebElement> cmdTreeItems, int cmdNumber);

        IWebElement GetCommand(ReadOnlyCollection<IWebElement> cmdTreeItems, int cmdNumber);
    }
}