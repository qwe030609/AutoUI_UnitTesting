using Microsoft.VisualStudio.TestTools.UnitTesting;
using PP5AutoUITests;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Opera;
using System.Collections.ObjectModel;
using System.Threading;

namespace PP5AutoUITests
{
    [TestClass]
    public class ElementFinderTests
    {
        [TestMethod]
        public void FindElementByAbsoluteXPath_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            //var elementFinder = new ElementFinder();
            WindowsDriver<WindowsElement> driver = Executor.GetInstance().GetCurrentDriver();
            string xPath = null;
            int nTryCount = 0;

            // Act
            var result = driver.FindElementByAbsoluteXPath(
                xPath,
                nTryCount);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindElementByAutomationId_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            //var elementFinder = new ElementFinder();
            WindowsDriver<WindowsElement> driver = Executor.GetInstance().GetCurrentDriver();
            string AutomationId = null;
            int nTryCount = 0;

            // Act
            var result = driver.FindElementByAutomationId(AutomationId, nTryCount);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindElementsByAutomationId_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            //var elementFinder = new ElementFinder();
            WindowsDriver<WindowsElement> driver = Executor.GetInstance().GetCurrentDriver();
            string AutomationId = null;
            int nTryCount = 0;

            // Act
            var result = driver.FindElementsByAutomationId(AutomationId, nTryCount);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindElementByName_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            WindowsDriver<WindowsElement> driver = Executor.GetInstance().GetCurrentDriver();
            string Name = null;
            int nTryCount = 0;
            var result = driver.FindElementByName(Name, nTryCount);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindElementsByName_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            WindowsDriver<WindowsElement> driver = Executor.GetInstance().GetCurrentDriver();
            string Name = null;
            int nTryCount = 0;
            var result = driver.FindElementsByName(Name, nTryCount);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetElementById_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            //var elementFinder = new ElementFinder();
            WindowsDriver<WindowsElement> driver = Executor.GetInstance().GetCurrentDriver();
            string automationId = null;
            string propertyName = null;
            int timeOut = 0;

            // Act
            var result = driver.GetElementById(automationId, propertyName, timeOut);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetElementByName_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            //var elementFinder = new ElementFinder();
            WindowsDriver<WindowsElement> driver = Executor.GetInstance().GetCurrentDriver();
            string name = null;
            string propertyName = null;
            int timeOut = 0;

            // Act
            var result = driver.GetElementByName(
                name,
                propertyName,
                timeOut);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetElementsById_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            //var elementFinder = new ElementFinder();
            WindowsDriver<WindowsElement> driver = Executor.GetInstance().GetCurrentDriver();
            string automationId = null;
            string propertyName = null;
            int timeOut = 0;

            // Act
            var result = driver.GetElementsById(automationId, propertyName, timeOut);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetElementsByName_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            //var elementFinder = new ElementFinder();
            WindowsDriver<WindowsElement> driver = Executor.GetInstance().GetCurrentDriver();
            string name = null;
            string propertyName = null;
            int timeOut = 0;

            // Act
            var result = driver.GetElementsByName(name, propertyName, timeOut);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetElement_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            //var elementFinder = new ElementFinder();
            WindowsDriver<WindowsElement> driver = Executor.GetInstance().GetCurrentDriver();
            By findType = null;
            int timeOut = 0;

            // Act
            var result = driver.GetElement(findType, timeOut);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetElement_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            //var elementFinder = new ElementFinder();
            IWebElement elementSrc = null;
            By findType = null;
            int timeOut = 0;

            // Act
            var result = elementSrc.GetElement(findType, timeOut);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetElement_StateUnderTest_ExpectedBehavior2()
        {
            // Arrange
            //var elementFinder = new ElementFinder();
            WindowsDriver<WindowsElement> driver = Executor.GetInstance().GetCurrentDriver();
            int timeOut = 0;
            By[] findTypes = null;

            // Act
            var result = driver.GetElement(timeOut, findTypes);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetElement_StateUnderTest_ExpectedBehavior3()
        {
            // Arrange
            //var elementFinder = new ElementFinder();
            IWebElement elementSrc = null;
            int timeOut = 0;
            By[] findTypes = null;

            // Act
            var result = elementSrc.GetElement(timeOut, findTypes);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetElements_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            //var elementFinder = new ElementFinder();
            WindowsDriver<WindowsElement> driver = Executor.GetInstance().GetCurrentDriver();
            By findType = null;
            int timeOut = 0;

            // Act
            var result = driver.GetElements(findType, timeOut);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetElements_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            //var elementFinder = new ElementFinder();
            IWebElement elementSrc = null;
            By findType = null;
            int timeOut = 0;

            // Act
            var result = elementSrc.GetElements(findType, timeOut);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void CheckElementExisted_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            //var elementFinder = new ElementFinder();
            WindowsDriver<WindowsElement> driver = Executor.GetInstance().GetCurrentDriver();
            By ByElementInfo = null;
            int timeOut = 0;
            int pollingInterval = 0;

            // Act
            var result = driver.CheckElementExisted(ByElementInfo, timeOut, pollingInterval);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void CheckElementExistedNoTimeout_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            //var elementFinder = new ElementFinder();
            WindowsDriver<WindowsElement> driver = Executor.GetInstance().GetCurrentDriver();
            By findType = null;

            // Act
            var result = driver.CheckElementExistedNoTimeout(findType);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void CheckWindowTitle_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            //var elementFinder = new ElementFinder();
            WindowsDriver<WindowsElement> driver = Executor.GetInstance().GetCurrentDriver();
            string WindowTitle = null;
            int timeOut = 0;

            // Act
            var result = driver.CheckWindowTitle(WindowTitle, timeOut);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void CheckElementSelected_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            WindowsDriver<WindowsElement> driver = Executor.GetInstance().GetCurrentDriver();
            IWebElement element = null;
            int timeOut = 0;
            var result = driver.CheckElementSelected(element, timeOut);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void isElementChecked_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement element = null;

            // Act
            var result = element.isElementChecked();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void isElementCollapsed_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement element = null;

            // Act
            var result = element.isElementCollapsed();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void isElementExpanded_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement element = null;

            // Act
            var result = element.isElementExpanded();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void isElementVisible_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement element = null;
            var result = element.isElementVisible();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetFirstTextElement_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;

            // Act
            IWebElement result = elementSrc.GetFirstTextElement();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetFirstTextContent_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            string result = elementSrc.GetFirstTextContent();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetFirstEditElement_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            IWebElement result = elementSrc.GetFirstEditElement();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetFirstEditContent_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            string result = elementSrc.GetFirstEditContent();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetFirstPaneElement_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;

            // Act
            IWebElement result = elementSrc.GetFirstPaneElement();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetFirstPaneContent_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            string result = elementSrc.GetFirstPaneContent();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetFirstRdoBtnElement_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            IWebElement result = elementSrc.GetFirstRdoBtnElement();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetFirstRdoBtnContent_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            string result = elementSrc.GetFirstRdoBtnContent();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetFirstDataGridElement_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;

            // Act
            IWebElement result = elementSrc.GetFirstDataGridElement();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetFirstTreeViewElement_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            IWebElement result = elementSrc.GetFirstTreeViewElement();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetFirstComboBoxElement_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            IWebElement result = elementSrc.GetFirstComboBoxElement();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetFirstTabControlElement_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            IWebElement result = elementSrc.GetFirstTabControlElement();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetFirstCustomElement_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            IWebElement result = elementSrc.GetFirstCustomElement();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetFirstListBoxItemElement_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            IWebElement result = elementSrc.GetFirstListBoxItemElement();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetFirstCheckBoxElement_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            IWebElement result = elementSrc.GetFirstCheckBoxElement();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetTextElement_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            string elementName = "";

            // Act
            IWebElement result = elementSrc.GetTextElement(elementName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetTextContent_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            string elementName = null;

            // Act
            string result = elementSrc.GetTextContent(elementName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetEditElement_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            string elementName = "";
            IWebElement result = elementSrc.GetEditElement(elementName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetEditContent_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            string elementName = "";
            string result = elementSrc.GetEditContent(elementName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetPaneElement_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            string elementName = "";

            // Act
            IWebElement result = elementSrc.GetPaneElement(elementName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetPaneContent_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            string elementName = "";

            // Act
            string result = elementSrc.GetPaneContent(elementName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetRdoBtnElement_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            string elementName = "";

            // Act
            IWebElement result = elementSrc.GetRdoBtnElement(elementName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetRdoBtnContent_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            string elementName = "";

            // Act
            string result = elementSrc.GetRdoBtnContent(elementName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetBtnElement_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            string elementName = "";

            // Act
            IWebElement result = elementSrc.GetBtnElement(elementName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetBtnContent_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            string elementName = "";
            string result = elementSrc.GetBtnContent(elementName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetDataGridElement_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            string elementName = "";
            IWebElement result = elementSrc.GetDataGridElement(elementName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetDataGridContent_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            string elementName = "";
            string result = elementSrc.GetDataGridContent(elementName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetComboBoxElement_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            string elementName = "";
            IWebElement result = elementSrc.GetComboBoxElement(elementName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetComboBoxContent_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            string elementName = "";
            string result = elementSrc.GetComboBoxContent(elementName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetCustomElement_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            string elementName = "";
            IWebElement result = elementSrc.GetCustomElement(elementName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetCustomContent_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            string elementName = "";
            string result = elementSrc.GetCustomContent(elementName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetWindowElement_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            string elementName = "";
            IWebElement result = elementSrc.GetWindowElement(elementName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetWindowContent_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            string elementName = "";
            string result = elementSrc.GetWindowContent(elementName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetCheckBoxElement_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            string elementName = "";
            IWebElement result = elementSrc.GetCheckBoxElement(elementName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetCheckBoxContent_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            string elementName = "";
            string result = elementSrc.GetCheckBoxContent(elementName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetTreeViewElement_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            string elementName = "";
            IWebElement result = elementSrc.GetTreeViewElement(elementName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetTreeViewContent_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            string elementName = "";
            string result = elementSrc.GetTreeViewContent(elementName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetTextElement_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            IWebElement elementSrc = null;
            int index = 0;

            // Act
            IWebElement result = elementSrc.GetTextElement(index);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetTextContent_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            IWebElement elementSrc = null;
            int index = 0;

            // Act
            string result = elementSrc.GetTextContent(index);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetEditElement_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            IWebElement elementSrc = null;
            int index = 0;

            // Act
            IWebElement result = elementSrc.GetEditElement(index);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetEditContent_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            IWebElement elementSrc = null;
            int index = 0;

            // Act
            string result = elementSrc.GetEditContent(index);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetRdoBtnElement_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            IWebElement elementSrc = null;
            int index = 0;
            IWebElement result = elementSrc.GetRdoBtnElement(index);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetRdoBtnContent_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            IWebElement elementSrc = null;
            int index = 0;
            string result = elementSrc.GetRdoBtnContent(index);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetBtnElement_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            IWebElement elementSrc = null;
            int index = 0;
            IWebElement result = elementSrc.GetBtnElement(index);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetBtnContent_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            IWebElement elementSrc = null;
            int index = 0;
            string result = elementSrc.GetBtnContent(index);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetCustomElement_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            IWebElement elementSrc = null;
            int index = 0;
            IWebElement result = elementSrc.GetCustomElement(index);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetCustomContent_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            IWebElement elementSrc = null;
            int index = 0;
            string result = elementSrc.GetCustomContent(index);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetTabControlElement_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            int index = 0;
            IWebElement result = elementSrc.GetTabControlElement(index);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetTabControlContent_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            int index = 0;
            string result = elementSrc.GetTabControlContent(index);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetToolbarElement_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            Func<IWebElement, bool> condition = null;

            // Act
            IWebElement result = elementSrc.GetToolbarElement(condition);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetChildElements_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;

            // Act
            ReadOnlyCollection<IWebElement> result = elementSrc.GetChildElements();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetChildElements2_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;

            // Act
            ReadOnlyCollection<IWebElement> result = elementSrc.GetChildElements2();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetChildElementsCount_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            int result = elementSrc.GetChildElementsCount();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetChildElementsOfControlType_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            ElementControlType elementType = default(global::PP5AutoUITests.ElementControlType);

            // Act
            ReadOnlyCollection<IWebElement> result = elementSrc.GetChildElementsOfControlType(elementType);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetChildElementsContentOfControlType_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            ElementControlType elementType = default(global::PP5AutoUITests.ElementControlType);

            // Act
            List<string> result = elementSrc.GetChildElementsContentOfControlType(elementType);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetParentElement_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            WindowsDriver<WindowsElement> driver = Executor.GetInstance().GetCurrentDriver();
            IWebElement elementSrc = null;

            // Act
            IWebElement result = driver.GetParentElement(elementSrc);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetSpecificChildContentOfControlType_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            ElementControlType elementType = default(global::PP5AutoUITests.ElementControlType);
            int childIndex = 0;

            // Act
            string result = elementSrc.GetSpecificChildContentOfControlType(elementType, childIndex);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetSpecificChildContentOfControlType_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            IWebElement elementSrc = null;
            ElementControlType elementType = default(global::PP5AutoUITests.ElementControlType);
            string childName = null;

            // Act
            string result = elementSrc.GetSpecificChildContentOfControlType(elementType, childName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetFirstChildContentOfControlType_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            ElementControlType elementType = default(global::PP5AutoUITests.ElementControlType);
            string result = elementSrc.GetFirstChildContentOfControlType(elementType);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetLastChildContentOfControlType_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            ElementControlType elementType = default(global::PP5AutoUITests.ElementControlType);
            string result = elementSrc.GetLastChildContentOfControlType(elementType);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetSpecificChildOfControlType_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            ElementControlType elementType = default(global::PP5AutoUITests.ElementControlType);
            int childIndex = 0;

            // Act
            IWebElement result = elementSrc.GetSpecificChildOfControlType(elementType, childIndex);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetSpecificChildOfControlType_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            IWebElement elementSrc = null;
            ElementControlType elementType = default(global::PP5AutoUITests.ElementControlType);
            string childName = null;

            // Act
            IWebElement result = elementSrc.GetSpecificChildOfControlType(elementType, childName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetSpecificChildOfControlType_StateUnderTest_ExpectedBehavior2()
        {
            // Arrange
            IWebElement elementSrc = null;
            ElementControlType elementType = default(global::PP5AutoUITests.ElementControlType);
            Func<IWebElement, bool> condition = null;

            // Act
            IWebElement result = elementSrc.GetSpecificChildOfControlType(elementType, condition);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetSpecificChildrenOfControlType_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            ElementControlType elementType = default(global::PP5AutoUITests.ElementControlType);

            // Act
            ReadOnlyCollection<IWebElement> results = elementSrc.GetSpecificChildrenOfControlType(elementType);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetFirstChildOfControlType_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            ElementControlType elementType = default(global::PP5AutoUITests.ElementControlType);

            // Act
            IWebElement result = elementSrc.GetFirstChildOfControlType(elementType);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetLastChildOfControlType_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IWebElement elementSrc = null;
            ElementControlType elementType = default(global::PP5AutoUITests.ElementControlType);
            IWebElement result = elementSrc.GetLastChildOfControlType(elementType);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetDataTableRowElements_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            WindowsDriver<WindowsElement> driver = Executor.GetInstance().GetCurrentDriver();
            string DataGridAutomationID = null;

            // Act
            ReadOnlyCollection<IWebElement> results = driver.GetDataTableRowElements(DataGridAutomationID);

            // Assert
            Assert.Fail();
        }

        //[TestMethod]
        //public void GetDataTableHeaders_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var elementFinder = new ElementFinder();
        //    IWebDriver driver = null;
        //    string DataGridAutomationID = null;

        //    // Act
        //    var result = elementFinder.GetDataTableHeaders(
        //        driver,
        //        DataGridAutomationID);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void GetDataTableRowElements_StateUnderTest_ExpectedBehavior1()
        //{
        //    // Arrange
        //    var elementFinder = new ElementFinder();
        //    IWebElement element = null;
        //    string DataGridAutomationID = null;

        //    // Act
        //    var result = elementFinder.GetDataTableRowElements(
        //        element,
        //        DataGridAutomationID);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void GetDataTableHeaders_StateUnderTest_ExpectedBehavior1()
        //{
        //    // Arrange
        //    var elementFinder = new ElementFinder();
        //    IWebElement element = null;
        //    string DataGridAutomationID = null;

        //    // Act
        //    var result = elementFinder.GetDataTableHeaders(
        //        element,
        //        DataGridAutomationID);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void GetDataTableRowElements_StateUnderTest_ExpectedBehavior2()
        //{
        //    // Arrange
        //    var elementFinder = new ElementFinder();
        //    IWebElement DataGridElement = null;

        //    // Act
        //    var result = elementFinder.GetDataTableRowElements(
        //        DataGridElement);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void GetDataTableHeaders_StateUnderTest_ExpectedBehavior2()
        //{
        //    // Arrange
        //    var elementFinder = new ElementFinder();
        //    IWebElement DataGridElement = null;

        //    // Act
        //    var result = elementFinder.GetDataTableHeaders(
        //        DataGridElement);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void GetDataTableElement_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var elementFinder = new ElementFinder();
        //    IWebElement element = null;
        //    string DataGridAutomationID = null;

        //    // Act
        //    var result = elementFinder.GetDataTableElement(
        //        element,
        //        DataGridAutomationID);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void GetDataTableElement_StateUnderTest_ExpectedBehavior1()
        //{
        //    // Arrange
        //    var elementFinder = new ElementFinder();
        //    IWebDriver driver = null;
        //    string DataGridAutomationID = null;

        //    // Act
        //    var result = elementFinder.GetDataTableElement(
        //        driver,
        //        DataGridAutomationID);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void GetSelectedRowCellElements_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var elementFinder = new ElementFinder();
        //    IWebDriver driver = null;
        //    string DataGridAutomationID = null;

        //    // Act
        //    var result = elementFinder.GetSelectedRowCellElements(
        //        driver,
        //        DataGridAutomationID);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void GetSelectedRowCellElements_StateUnderTest_ExpectedBehavior1()
        //{
        //    // Arrange
        //    var elementFinder = new ElementFinder();
        //    IWebElement element = null;
        //    string DataGridAutomationID = null;

        //    // Act
        //    var result = elementFinder.GetSelectedRowCellElements(
        //        element,
        //        DataGridAutomationID);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void GetSelectedRowCellElements_StateUnderTest_ExpectedBehavior2()
        //{
        //    // Arrange
        //    var elementFinder = new ElementFinder();
        //    IEnumerable rowElements = null;

        //    // Act
        //    var result = elementFinder.GetSelectedRowCellElements(
        //        rowElements);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void GetCellElementsOfRow_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var elementFinder = new ElementFinder();
        //    IWebElement row = null;

        //    // Act
        //    var result = elementFinder.GetCellElementsOfRow(
        //        row);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void GetCellElementByColumnIndex_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var elementFinder = new ElementFinder();
        //    IWebDriver driver = null;
        //    string DataGridAutomationID = null;
        //    int ColumnIndex = 0;

        //    // Act
        //    var result = elementFinder.GetCellElementByColumnIndex(
        //        driver,
        //        DataGridAutomationID,
        //        ColumnIndex);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void GetCellElementByColumnIndex_StateUnderTest_ExpectedBehavior1()
        //{
        //    // Arrange
        //    var elementFinder = new ElementFinder();
        //    IWebElement element = null;
        //    string DataGridAutomationID = null;
        //    int ColumnIndex = 0;

        //    // Act
        //    var result = elementFinder.GetCellElementByColumnIndex(
        //        element,
        //        DataGridAutomationID,
        //        ColumnIndex);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void GetCellElementByColumnIndex_StateUnderTest_ExpectedBehavior2()
        //{
        //    // Arrange
        //    var elementFinder = new ElementFinder();
        //    IEnumerable cellElements = null;
        //    int ColumnIndex = 0;

        //    // Act
        //    var result = elementFinder.GetCellElementByColumnIndex(
        //        cellElements,
        //        ColumnIndex);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void GetColumnIndexOfCellElement_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var elementFinder = new ElementFinder();
        //    IWebElement cellElement = null;

        //    // Act
        //    var result = elementFinder.GetColumnIndexOfCellElement(
        //        cellElement);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void GetCellBy_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var elementFinder = new ElementFinder();
        //    IWebElement dataGridElement = null;
        //    int rowNo = 0;
        //    string colName = null;

        //    // Act
        //    var result = elementFinder.GetCellBy(
        //        dataGridElement,
        //        rowNo,
        //        colName);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void GetCellBy_StateUnderTest_ExpectedBehavior1()
        //{
        //    // Arrange
        //    var elementFinder = new ElementFinder();
        //    IWebElement dataGridElement = null;
        //    int rowNo = 0;
        //    int colNo = 0;

        //    // Act
        //    var result = elementFinder.GetCellBy(
        //        dataGridElement,
        //        rowNo,
        //        colNo);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void GetCellBy_StateUnderTest_ExpectedBehavior2()
        //{
        //    // Arrange
        //    var elementFinder = new ElementFinder();
        //    IWebElement rowElement = null;
        //    int colNo = 0;

        //    // Act
        //    var result = elementFinder.GetCellBy(
        //        rowElement,
        //        colNo);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void GetCellByName_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var elementFinder = new ElementFinder();
        //    IWebElement dataGridElement = null;
        //    int colNo = 0;
        //    string cellName = null;

        //    // Act
        //    var result = elementFinder.GetCellByName(
        //        dataGridElement,
        //        colNo,
        //        cellName);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void GetCellValue_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var elementFinder = new ElementFinder();
        //    IWebElement dataGridElement = null;
        //    int rowNo = 0;
        //    string colName = null;

        //    // Act
        //    var result = elementFinder.GetCellValue(
        //        dataGridElement,
        //        rowNo,
        //        colName);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void GetCellValue_StateUnderTest_ExpectedBehavior1()
        //{
        //    // Arrange
        //    var elementFinder = new ElementFinder();
        //    IWebElement rowElement = null;
        //    int colNo = 0;

        //    // Act
        //    var result = elementFinder.GetCellValue(
        //        rowElement,
        //        colNo);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void GetCellValue_StateUnderTest_ExpectedBehavior2()
        //{
        //    // Arrange
        //    var elementFinder = new ElementFinder();
        //    IWebElement cellElement = null;

        //    // Act
        //    var result = elementFinder.GetCellValue(
        //        cellElement);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void GetColumnByIndex_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var elementFinder = new ElementFinder();
        //    IWebElement dataGridElement = null;
        //    int colNo = 0;
        //    string cellName = null;

        //    // Act
        //    var result = elementFinder.GetColumnByIndex(
        //        dataGridElement,
        //        colNo,
        //        cellName);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void GetRowByName_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var elementFinder = new ElementFinder();
        //    IWebElement dataGridElement = null;
        //    int colNo = 0;
        //    string cellName = null;

        //    // Act
        //    var result = elementFinder.GetRowByName(
        //        dataGridElement,
        //        colNo,
        //        cellName);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void GetRowBy_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var elementFinder = new ElementFinder();
        //    IWebElement dataGridElement = null;
        //    int rowNo = 0;

        //    // Act
        //    var result = elementFinder.GetRowElementsBy(
        //        dataGridElement,
        //        rowNo);

        //    // Assert
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void GetSelectedRow_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var elementFinder = new ElementFinder();
        //    IWebElement dataGridElement = null;

        //    // Act
        //    var result = elementFinder.GetSelectedRow(
        //        dataGridElement);

        //    // Assert
        //    Assert.Fail();
        //}

        [TestMethod]
        public void CheckElementHasNameOrId_StateUnderTest_CheckElementNoNameHasAutoID()
        {
            // Arrange
            IWebElement element = null;

            // Act
            bool ElementHasAutoID = element.CheckElementHasNameOrId("OkBtn");

            // Assert
            Assert.IsTrue(ElementHasAutoID);
        }

        [TestMethod]
        public void CheckElementHasNameOrId_StateUnderTest_CheckElementHasNameNoAutoID()
        {
            // Arrange
            IWebElement element = null;

            // Act
            bool ElementHasName = element.CheckElementHasNameOrId("Ok");

            // Assert
            Assert.IsTrue(ElementHasName);
        }

        [TestMethod]
        public void HasAttribute_StateUnderTest_CheckHasNameAttribute()
        {
            // Arrange
            IWebElement element = null;
            string attributeName = "Name";

            // Act
            bool hasNameAttribute = element.hasAttribute(attributeName);

            // Assert
            Assert.IsTrue(hasNameAttribute);
        }

        [TestMethod]
        public void HasAttribute_StateUnderTest_CheckNotExistencyAttribute()
        {
            // Arrange
            IWebElement element = null;
            string attributeName = "xxxxxgiba";

            // Act
            bool hasNotExistencyAttribute = element.hasAttribute(attributeName);

            // Assert
            Assert.IsFalse(hasNotExistencyAttribute);
        }
    }
}
