using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;

namespace PP5AutoUITests
{
    public interface ITestItem
    {
        void AddTI();

        void SelectTI();

        IWebElement GetTI();
    }
}