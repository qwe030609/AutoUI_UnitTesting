#define DEBUG
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Automation;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Chroma.UnitTest.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Html5;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
//using SeleniumExtras.PageObjects;
using static OpenQA.Selenium.Support.UI.ExpectedConditions;

namespace PP5AutoUITests
{
    public static class ElementFinder
    {
        #region Main Methods

        #region Legacy Find Element methods
        public static IWebElement FindElementByAbsoluteXPath(this WindowsDriver<WindowsElement> driver, string xPath, int nTryCount = 3)
        {
            IWebElement uiTarget = null;

            while (nTryCount-- > 0)
            {
                try
                {
                    uiTarget = driver.FindElementByXPath(xPath);
                }
                catch
                {
                }

                if (uiTarget != null)
                {
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep(10);
                }
            }

            return uiTarget;
        }

        public static IWebElement FindElementByAutomationId(this WindowsDriver<WindowsElement> driver, string AutomationId, int nTryCount = 3)
        {
            IWebElement uiTarget = null;

            while (nTryCount-- > 0)
            {
                try
                {
                    uiTarget = driver.FindElementByAccessibilityId(AutomationId);
                }
                catch
                {
                }

                if (uiTarget != null)
                {
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep(10);
                }
            }

            return uiTarget;
        }

        public static ReadOnlyCollection<IWebElement> FindElementsByAutomationId(this WindowsDriver<WindowsElement> driver, string AutomationId, int nTryCount = 3)
        {
            ReadOnlyCollection<IWebElement> uiTargets = null;

            while (nTryCount-- > 0)
            {
                try
                {
                    uiTargets = (ReadOnlyCollection<IWebElement>)driver.FindElementsByAccessibilityId(AutomationId);
                }
                catch
                {
                }

                if (uiTargets != null)
                {
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep(10);
                }
            }

            return uiTargets;
        }

        public static IWebElement FindElementByName(this WindowsDriver<WindowsElement> driver, string Name, int nTryCount = 3)
        {
            IWebElement uiTarget = null;

            while (nTryCount-- > 0)
            {
                try
                {
                    uiTarget = driver.FindElementByName(Name);
                }
                catch
                {
                }

                if (uiTarget != null)
                {
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep(10);
                }
            }

            return uiTarget;
        }

        public static ReadOnlyCollection<IWebElement> FindElementsByName(this WindowsDriver<IWebElement> driver, string Name, int nTryCount = 3)
        {
            ReadOnlyCollection<IWebElement> uiTargets = null;

            while (nTryCount-- > 0)
            {
                try
                {
                    uiTargets = driver.FindElementsByName(Name);
                }
                catch
                {
                }

                if (uiTargets != null)
                {
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep(10);
                }
            }

            return uiTargets;
        }

        public static IWebElement GetElementById(this WindowsDriver<WindowsElement> driver, string automationId, string propertyName, int timeOut = 10000)
        {
            IWebElement element = null;
            var wait = new DefaultWait<WindowsDriver<WindowsElement>>(driver)
            {
                Timeout = TimeSpan.FromMilliseconds(timeOut),
                Message = $"Element with automationId \"{automationId}\" not found."
            };

            wait.IgnoreExceptionTypes(typeof(WebDriverException));

            try
            {
                wait.Until(Driver =>
                {
                    element = Driver.FindElementByAccessibilityId(automationId);

                    return element != null;
                });
            }
            catch (WebDriverTimeoutException ex)
            {
                Logger.LogMessage($"{ex}, {automationId}, {propertyName}");
                Assert.Fail(ex.Message);
            }

            return element;
        }

        public static IWebElement GetElementByName(this WindowsDriver<WindowsElement> driver, string name, string propertyName, int timeOut = 10000)
        {
            IWebElement element = null;
            var wait = new DefaultWait<WindowsDriver<WindowsElement>>(driver)
            {
                Timeout = TimeSpan.FromMilliseconds(timeOut),
                Message = $"Element with name \"{name}\" not found."
            };

            wait.IgnoreExceptionTypes(typeof(WebDriverException));

            try
            {
                wait.Until(Driver =>
                {
                    element = Driver.FindElementByName(name);

                    return element != null;
                });
            }
            catch (WebDriverTimeoutException ex)
            {
                Logger.LogMessage($"{ex}, {name}, {propertyName}");
                Assert.Fail(ex.Message);
            }

            return element;
        }

        public static ReadOnlyCollection<IWebElement> GetElementsById(this WindowsDriver<WindowsElement> driver, string automationId, string propertyName, int timeOut = 10000)
        {
            ReadOnlyCollection<IWebElement> elements = null;
            var wait = new DefaultWait<WindowsDriver<WindowsElement>>(driver)
            {
                Timeout = TimeSpan.FromMilliseconds(timeOut),
                Message = $"Element with automationId \"{automationId}\" not found."
            };

            wait.IgnoreExceptionTypes(typeof(WebDriverException));

            try
            {
                wait.Until(Driver =>
                {
                    elements = (ReadOnlyCollection<IWebElement>)Driver.FindElementsByAccessibilityId(automationId);

                    return elements != null;
                });
            }
            catch (WebDriverTimeoutException ex)
            {
                Logger.LogMessage($"{ex}, {automationId}, {propertyName}");
                Assert.Fail(ex.Message);
            }

            return elements;
        }

        public static IReadOnlyCollection<IWebElement> GetElementsByName(this WindowsDriver<WindowsElement> driver, string name, string propertyName, int timeOut = 10000)
        {
            IReadOnlyCollection<IWebElement> elements = null;
            var wait = new DefaultWait<WindowsDriver<WindowsElement>>(driver)
            {
                Timeout = TimeSpan.FromMilliseconds(timeOut),
                Message = $"Element with name \"{name}\" not found."
            };

            wait.IgnoreExceptionTypes(typeof(WebDriverException));

            try
            {
                wait.Until(Driver =>
                {
                    elements = Driver.FindElementsByName(name);

                    return elements != null;
                });
            }
            catch (WebDriverTimeoutException ex)
            {
                Logger.LogMessage($"{ex}, {name}, {propertyName}");
                Assert.Fail(ex.Message);
            }

            return elements;
        }
        #endregion

        #region Find Element methods

        #region Base method to FindElement with retry
        // FindElement from driver, given By locator, retry
        public static IWebElement FindElement(this IWebDriver driver, By by, int nTryCount = 3)
        {
            IWebElement uiTarget = null;

            while (nTryCount-- > 0)
            {
                try
                {
                    uiTarget = driver.FindElement(by);
                }
                catch
                {
                }

                if (uiTarget != null)
                {
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep(10);
                }
            }

            return uiTarget;
        }

        // FindElements from driver, given By locator, retry
        public static ReadOnlyCollection<IWebElement> FindElements(this IWebDriver driver, By by, int nTryCount = 3)
        {
            ReadOnlyCollection<IWebElement> uiTargets = null;

            while (nTryCount-- > 0)
            {
                try
                {
                    uiTargets = driver.FindElements(by);
                }
                catch
                {
                }

                if (uiTargets != null)
                {
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep(10);
                }
            }

            return uiTargets;
        }

        // FindElement from element, given By locator, retry
        public static IWebElement FindElement(this IWebElement element, By by, int nTryCount = 3)
        {
            IWebElement uiTarget = null;

            while (nTryCount-- > 0)
            {
                try
                {
                    uiTarget = element.FindElement(by);
                }
                catch
                {
                }

                if (uiTarget != null)
                {
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep(10);
                }
            }

            return uiTarget;
        }

        // FindElements from element, given By locator, retry
        public static ReadOnlyCollection<IWebElement> FindElements(this IWebElement element, By by, int nTryCount = 3)
        {
            ReadOnlyCollection<IWebElement> uiTargets = null;

            while (nTryCount-- > 0)
            {
                try
                {
                    uiTargets = element.FindElements(by);
                }
                catch
                {
                }

                if (uiTargets != null)
                {
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep(10);
                }
            }

            return uiTargets;
        }
        #endregion

        #region GetElement No retry
        // GetElement from driver, given By locator, timeOut
        public static IWebElement GetElement(this IWebDriver driver, By findType, int timeOut = SharedSetting.NORMAL_TIMEOUT)
        {
            IWebElement element = null;
            //bool isElementClickable = false;
            DefaultWait<IWebDriver> waitDriver;

            // Set error message if element not found
            GetNotFoundMessageAndFindingText(findType, out string Message);

            //var wait = new DefaultWait<WindowsDriver<WindowsElement>>(driver)
            //{
            //    Timeout = TimeSpan.FromMilliseconds(timeOut),
            //    Message = Message,
            //};

            //wait.IgnoreExceptionTypes(typeof(WebDriverException));

            try
            {
                //wait.Until(Driver =>
                //{
                //    element = Driver.FindElement(findType);
                //    //if (!element.Displayed || !element.Enabled)
                //    //    return null;

                //    //return element;
                //    return element != null;
                //});

                waitDriver = new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeOut));
                waitDriver.IgnoreExceptionTypes(typeof(WebDriverException));
                element = waitDriver.Until(ElementToBeClickable(findType));
            }
            catch (WebDriverTimeoutException ex)
            {
                Logger.LogMessage($"{ex.Message}, {Message}");
                return null;
                //Assert.Fail(ex.Message);
            }

            return element;
        }

        // GetElement from element, given By locator, timeOut
        public static IWebElement GetElement(this IWebElement elementSrc, By findType, int timeOut = SharedSetting.NORMAL_TIMEOUT)
        {
            IWebElement element = null;

            // Set error message if element not found
            GetNotFoundMessageAndFindingText(findType, out string Message);

            var wait = new DefaultWait<IWebElement>(elementSrc)
            {
                Timeout = TimeSpan.FromMilliseconds(timeOut),

                Message = Message
            };

            wait.IgnoreExceptionTypes(typeof(WebDriverException));

            try
            {
                wait.Until(ElementSrc =>
                {
                    element = ElementSrc.FindElement(findType);
                    return element;
                });
            }
            catch (WebDriverTimeoutException ex)
            {
                Logger.LogMessage($"{ex.Message}, {Message}");
                return null;
            }
            return element;
        }

        // GetElement from driver, given By[] locators, timeOut, retry
        public static IWebElement GetElement(this IWebDriver driver, int timeOut = SharedSetting.NORMAL_TIMEOUT, params By[] findTypes)
        {
            IWebElement element = null;
            int CurrFindElementDepth = 1;
            foreach (By type in findTypes)
            {
                // 20240830, Adam, modify the method to call get element method (single By)
                if (CurrFindElementDepth == 1)
                    element = driver.GetElement(type, timeOut);
                else
                {
                    element = element.GetElement(type, timeOut);
                }
                CurrFindElementDepth++;
            }
            return element;
        }

        // GetElement from element, given By[] locators, timeOut, retry
        public static IWebElement GetElement(this IWebElement elementSrc, int timeOut = SharedSetting.NORMAL_TIMEOUT, params By[] findTypes)
        {
            IWebElement element = elementSrc;
            foreach (By type in findTypes)
            {
                // 20240830, Adam, modify the method to call get element method (single By)
                element = element.GetElement(type, timeOut);
            }
            return element;
        }

        // GetElements from driver, given By locator, timeOut, retry
        public static ReadOnlyCollection<IWebElement> GetElements(this IWebDriver driver, By findType, int timeOut = SharedSetting.NORMAL_TIMEOUT)
        {
            ReadOnlyCollection<IWebElement> elements = null;

            // Set error message if element not found
            GetNotFoundMessageAndFindingText(findType, out string Message);

            var wait = new DefaultWait<IWebDriver>(driver)
            {
                Timeout = TimeSpan.FromMilliseconds(timeOut),

                Message = Message
            };

            try
            {
                wait.Until(Driver =>
                {
                    elements = Driver.FindElements(findType);
                    return elements != null;
                });
            }

            catch (WebDriverTimeoutException ex)
            {
                Logger.LogMessage($"{ex.Message}, {Message}");
                return null;
            }
            return elements;
        }

        // GetElements from element, given By locator, timeOut, retry
        public static ReadOnlyCollection<IWebElement> GetElements(this IWebElement elementSrc, By findType, int timeOut = SharedSetting.NORMAL_TIMEOUT)
        {
            ReadOnlyCollection<IWebElement> elements = null;

            // Set error message if element not found
            GetNotFoundMessageAndFindingText(findType, out string Message);

            var wait = new DefaultWait<IWebElement>(elementSrc)
            {
                Timeout = TimeSpan.FromMilliseconds(timeOut),

                Message = Message
            };

            wait.IgnoreExceptionTypes(typeof(WebDriverException));

            try
            {
                wait.Until(ElementSrc =>
                {
                    elements = ElementSrc.FindElements(findType);
                    return elements != null;
                });
            }
            catch (WebDriverTimeoutException ex)
            {
                Logger.LogMessage($"{ex.Message}, {Message}");
                return null;
            }
            return elements;
        }

        public static ReadOnlyCollection<IWebElement> GetElements(this IWebElement elementSrc, ByAutomationIdOrName findType, int timeOut = SharedSetting.NORMAL_TIMEOUT)
        {
            ReadOnlyCollection<IWebElement> elements = null;

            // Set error message if element not found
            GetNotFoundMessageAndFindingText(findType, out string Message);

            var wait = new DefaultWait<IWebElement>(elementSrc)
            {
                Timeout = TimeSpan.FromMilliseconds(timeOut),

                Message = Message
            };

            wait.IgnoreExceptionTypes(typeof(WebDriverException));

            try
            {
                wait.Until(ElementSrc =>
                {
                    elements = ElementSrc.FindElements(findType);
                    return elements != null;
                });
            }
            catch (WebDriverTimeoutException ex)
            {
                Logger.LogMessage($"{ex.Message}, {Message}");
                return null;
            }
            return elements;
        }
        #endregion

        #region GetElement with retry
        // GetElement from element, given By locator, timeOut, retry
        public static IWebElement GetElementWithRetry(this IWebElement elementSrc, By findType, int timeOut = SharedSetting.NORMAL_TIMEOUT, int nTryCount = 3)
        {
            IWebElement element = null;

            // Set error message if element not found
            GetNotFoundMessageAndFindingText(findType, out string Message);

            var wait = new DefaultWait<IWebElement>(elementSrc)
            {
                Timeout = TimeSpan.FromMilliseconds(timeOut),

                Message = Message
            };

            wait.IgnoreExceptionTypes(typeof(WebDriverException));
            
            try
            {
                wait.Until(ElementSrc =>
                {
                    element = ElementSrc.FindElement(findType, nTryCount);      // 20240830, Adam, add retry when getting element
                    //if (!element.Displayed || !element.Enabled)
                    //    return null;

                    return element;

                    //return element != null;
                });
            }
            catch (WebDriverTimeoutException ex)
            {
                Logger.LogMessage($"{ex.Message}, {Message}");
                return null;
                //Assert.Fail(ex.Message);
            }

            return element;
        }

        // GetElement from driver, given By[] locators, timeOut, retry
        public static IWebElement GetElementWithRetry(this IWebDriver driver, int timeOut = SharedSetting.NORMAL_TIMEOUT, int nTryCount = 3, params By[] findTypes)
        {
            IWebElement element = null;
            //IWebElement elementSrcTemp = null;
            //DefaultWait<IWebElement> waitElement;
            //DefaultWait<IWebDriver> waitDriver;

            int CurrFindElementDepth = 1;
            foreach (By type in findTypes)
            {
                // 20240830, Adam, modify the method to call get element method (single By)
                if (CurrFindElementDepth == 1)
                    element = driver.GetElement(type, timeOut);
                else
                {
                    element = element.GetElementWithRetry(type, timeOut, nTryCount);
                }
                CurrFindElementDepth++;

                //// Set error message if element not found
                //GetNotFoundMessageAndFindingText(type, out string Message);

                //try
                //{
                //    // Find the element by element info
                //    if (CurrFindElementDepth == 1)
                //    {
                //        waitDriver = new DefaultWait<IWebDriver>(driver)
                //        {
                //            Timeout = TimeSpan.FromMilliseconds(timeOut),

                //            Message = Message
                //        };
                //        waitDriver.IgnoreExceptionTypes(typeof(WebDriverException));

                //        //ExpectedConditions.AlertIsPresent
                //        //waitDriver.Until(Driver =>
                //        //{
                //        //    element = (IWebElement)Driver.FindElement(type);
                //        //    //if (!element.Displayed || !element.Enabled)
                //        //    //    return null;

                //        //    return element;
                //        //    //return element != null;
                //        //});
                //        element = waitDriver.Until(ElementToBeClickable(type));

                //        //var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                //        //element = (WindowsElement)wait.Until(drv => drv.FindElement(type));

                //        //return wait.Until(ctx => {
                //        //    var elem = ctx.FindElement(by);
                //        //    if (!elem.Displayed)
                //        //        return null;

                //        //    return elem;
                //        //});
                //    }
                //    else
                //    {
                //        waitElement = new DefaultWait<IWebElement>(elementSrcTemp)
                //        {
                //            Timeout = TimeSpan.FromMilliseconds(timeOut),

                //            Message = Message
                //        };
                //        waitElement.IgnoreExceptionTypes(typeof(WebDriverException));

                //        waitElement.Until(elementSrc =>
                //        {
                //            element = elementSrc.FindElement(type, nTryCount);
                //            //if (!element.Displayed || !element.Enabled)
                //            //    return null;

                //            return element;
                //            //return element != null;
                //        });
                //    }

                //    // Update current element source for next element finding
                //    elementSrcTemp = element;

                //    CurrFindElementDepth++;
                //}
                //catch (WebDriverTimeoutException ex)
                //{
                //    Logger.LogMessage($"{ex.Message}, {Message}");
                //    return null;
                //    //Assert.Fail(ex.Message);
                //}
            }

            return element;
        }

        // GetElement from element, given By[] locators, timeOut, retry
        public static IWebElement GetElementWithRetry(this IWebElement elementSrc, int timeOut = SharedSetting.NORMAL_TIMEOUT, int nTryCount = 3, params By[] findTypes)
        {
            IWebElement element = elementSrc;
            //IWebElement elementSrcTemp = elementSrc;

            foreach (By type in findTypes) 
            {
                //// Set error message if element not found
                //GetNotFoundMessageAndFindingText(type, out string Message);

                //// Find the element by element info
                //var wait = new DefaultWait<IWebElement>(elementSrc)
                //{
                //    Timeout = TimeSpan.FromMilliseconds(timeOut),

                //    Message = Message
                //};

                //wait.IgnoreExceptionTypes(typeof(WebDriverException));

                //try
                //{
                //    wait.Until(ElementSrc =>
                //    {
                //        element = ElementSrc.FindElement(type, nTryCount);
                //        //if (!element.Displayed || !element.Enabled)
                //        //    return null;

                //        return element;

                //        //return element != null;
                //    });

                //    // Update current element source for next element finding
                //    elementSrcTemp = element;
                //}
                //catch (WebDriverTimeoutException ex)
                //{
                //    Logger.LogMessage($"{ex.Message}, {Message}");
                //    return null;
                //    //Assert.Fail(ex.Message);
                //}

                // 20240830, Adam, modify the method to call get element method (single By)
                element = element.GetElementWithRetry(type, timeOut, nTryCount);
            }

            return element;
        }

        // GetElements from driver, given By locator, timeOut, retry
        public static ReadOnlyCollection<IWebElement> GetElementsWithRetry(this IWebDriver driver, By findType, int timeOut = SharedSetting.NORMAL_TIMEOUT, int nTryCount = 3)
        {
            ReadOnlyCollection<IWebElement> elements = null;

            // Set error message if element not found
            GetNotFoundMessageAndFindingText(findType, out string Message);

            var wait = new DefaultWait<IWebDriver>(driver)
            {
                Timeout = TimeSpan.FromMilliseconds(timeOut),

                Message = Message
            };

            //wait.IgnoreExceptionTypes(typeof(WebDriverException));

            try
            {
                wait.Until(Driver =>
                {
                    elements = Driver.FindElements(findType, nTryCount);        // 20240830, Adam, add retry when getting element
                    //if (!elements.All(e => e.Displayed))
                    //    return null;

                    //return elements;
                    return elements != null;
                });

                //var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                //elements = wait.Until(drv => drv.FindElements(findType));
            }

            catch (WebDriverTimeoutException ex)
            {
                Logger.LogMessage($"{ex.Message}, {Message}");
                return null;
                //Assert.Fail(ex.Message);
            }

            return elements;
        }

        // GetElements from element, given By locator, timeOut, retry
        public static ReadOnlyCollection<IWebElement> GetElementsWithRetry(this IWebElement elementSrc, By findType, int timeOut = SharedSetting.NORMAL_TIMEOUT, int nTryCount = 3)
        {
            ReadOnlyCollection<IWebElement> elements = null;

            // Set error message if element not found
            GetNotFoundMessageAndFindingText(findType, out string Message);

            var wait = new DefaultWait<IWebElement>(elementSrc)
            {
                Timeout = TimeSpan.FromMilliseconds(timeOut),

                Message = Message
            };

            wait.IgnoreExceptionTypes(typeof(WebDriverException));

            try
            {
                wait.Until(ElementSrc =>
                {
                    elements = ElementSrc.FindElements(findType, nTryCount);      // 20240830, Adam, add retry when getting element
                    //if (!elements.All(e => e.Displayed))
                    //    return null;

                    //return elements;
                    return elements != null;
                });
            }
            catch (WebDriverTimeoutException ex)
            {
                Logger.LogMessage($"{ex.Message}, {Message}");
                return null;
                //Assert.Fail(ex.Message);
            }

            return elements;
        }
        #endregion

        #endregion

        #region Element related methods
        public static bool CheckElementExisted(this IWebDriver driver, By ByElementInfo, int timeOut = SharedSetting.SUPERSHORT_TIMEOUT, int sleepingInterval = 500)
        {
            IWebElement element = null;
            DefaultWait<IWebDriver> waitDriver;

            // Set error message if element not found
            GetNotFoundMessageAndFindingText(ByElementInfo, out string Message);

            try
            {
                // Find the element by element info
                waitDriver = new WebDriverWait(new SystemClock(), driver, TimeSpan.FromMilliseconds(timeOut), TimeSpan.FromMilliseconds(sleepingInterval));
                waitDriver.IgnoreExceptionTypes(typeof(WebDriverException));
                element = (IWebElement)waitDriver.Until(ElementIsVisible(ByElementInfo));
            }
            catch (WebDriverTimeoutException ex)
            {
                Logger.LogMessage($"{ex.Message}, {Message}");
                return false;
                //Assert.Fail(ex.Message);
            }

            return element != null;
        }

        public static bool CheckElementExistedNoTimeout(this IWebDriver driver, By findType)
        {
            try
            {
                return driver.FindElement(findType) != null;
            }
            catch (Exception ex)
            {
                Logger.LogMessage(ex.Message);
                return false;
            }
        }

        public static bool CheckWindowTitle(this IWebDriver driver, string WindowTitle, int timeOut = SharedSetting.SHORT_TIMEOUT)
        {
            bool isWindowOpened = false;
            DefaultWait<IWebDriver> waitDriver;

            // Set error message if element not found
            //GetNotFoundMessageAndFindingText(ByElementInfo, out string Message);
            string Message = $"Element with Name: \"{WindowTitle}\" not found.";

            try
            {
                // Find the element by element info
                waitDriver = new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeOut));
                waitDriver.IgnoreExceptionTypes(typeof(WebDriverException));
                isWindowOpened = waitDriver.Until(TitleIs(WindowTitle));
            }
            catch (WebDriverTimeoutException ex)
            {
                Logger.LogMessage($"{ex.Message}, {Message}");
                return false;
                //Assert.Fail(ex.Message);
            }

            return isWindowOpened;
        }

        public static bool CheckElementSelected(this IWebDriver driver, IWebElement element, int timeOut = SharedSetting.EXTREMESHORT_TIMEOUT)
        {
            bool isElementSelected = false;
            DefaultWait<IWebDriver> waitDriver;

            // Set error message if element not found
            //GetNotFoundMessageAndFindingText(ByElementInfo, out string Message);
            string Message = $"Element with Name: \"{element.Text}\" not found.";

            try
            {
                // Find the element by element info
                waitDriver = new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeOut));
                waitDriver.IgnoreExceptionTypes(typeof(WebDriverException));
                isElementSelected = waitDriver.Until(ElementToBeSelected(element));
            }
            catch (WebDriverTimeoutException ex)
            {
                Logger.LogMessage($"{ex.Message}, {Message}");
                return false;
                //Assert.Fail(ex.Message);
            }

            return isElementSelected;
        }

        public static bool isElementChecked(this IWebElement element)
        {
            //bool isElementChecked = element.GetAttribute("Value.Value") == "Checked" ? 
            //                         element.GetAttribute("Value.Value") == "Unchecked" ? 
            //                         true : false : false;

            //if (element.GetAttribute("Value.Value") == "Checked") { return true; }
            //else if (element.GetAttribute("Value.Value") == "Unchecked") { return false; }
            //else return false;
            //return element.GetAttribute("Value.Value") == "Checked";
            
            if (element.TagName == "ControlType.Checkbox")
                return element.GetAttribute("Toggle.ToggleState") == "1";   // Toggle.ToggleState: On (1)
            else if (element.TagName == "ControlType.Custom")
                return element.GetAttribute("Value.Value") == "Checked";
            else
                throw new Exception("This element is not supporting element checked property");
        }

        public static bool isElementCollapsed(this IWebElement element)
        {
            return element.GetAttribute("ExpandCollapse.ExpandCollapseState") == ExpandCollapseState.Collapsed.ToString();
        }

        public static bool isElementExpanded(this IWebElement element)
        {
            return element.GetAttribute("ExpandCollapse.ExpandCollapseState") == ExpandCollapseState.Expanded.ToString();
        }

        public static bool isElementAtLeafNode(this IWebElement element)
        {
            return element.GetAttribute("ExpandCollapse.ExpandCollapseState") == ExpandCollapseState.LeafNode.ToString();
        }

        public static bool isElementVisible(this IWebElement element)
        {
            return element.hasAttribute("BoundingRectangle");
        }
        #endregion

        //public static IAlert CheckAlertWindowOpened(this IWebDriver driver, int timeOut = 100)
        //{
        //    IAlert AlertWindow = null;
        //    DefaultWait<IWebDriver> waitDriver;

        //    // Set error message if element not found
        //    //GetNotFoundMessageAndFindingText(ByElementInfo, out string Message);
        //    //string Message = $"Element with Name: \"{element.Text}\" not found.";

        //    try
        //    {
        //        // Find the element by element info
        //        waitDriver = new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeOut));
        //        waitDriver.IgnoreExceptionTypes(typeof(WebDriverException));
        //        AlertWindow = waitDriver.Until(AlertIsPresent());
        //    }
        //    catch (WebDriverTimeoutException ex)
        //    {
        //        Console.WriteLine($"{ex.Message}");
        //        //Assert.Fail(ex.Message);
        //    }

        //    return AlertWindow;
        //}

        #region Find Element with given element type methods

        #region Public Methods
        //public static string GetEditContent(this IWebElement elementSrc, string elementName) => GetChildEditElementContent(elementSrc, elementName);
        //public static string GetEditContent(this IWebElement elementSrc, int index) => GetChildEditElementContent(elementSrc, index);

        //public static string GetTextContent(this IWebElement elementSrc, string elementName) => GetChildTextElementContent(elementSrc, elementName);
        //public static string GetTextContent(this IWebElement elementSrc, int index) => GetChildTextElementContent(elementSrc, index);

        //public static string GetPaneContent(this IWebElement elementSrc, string elementName) => GetChildPaneElementContent(elementSrc, elementName);

        //public static string GetRdoBtnContent(this IWebElement elementSrc, string elementName) => GetChildRdoBtnElementContent(elementSrc, elementName);
        //public static string GetRdoBtnContent(this IWebElement elementSrc, int index) => GetChildRdoBtnElementContent(elementSrc, index);

        //public static string GetBtnContent(this IWebElement elementSrc, string elementName) => GetChildBtnElementContent(elementSrc, elementName);
        //public static string GetBtnContent(this IWebElement elementSrc, int index) => GetChildBtnElementContent(elementSrc, index);

        //public static IWebElement GetEditElement(this IWebElement elementSrc, string elementName) => GetChildEditElement(elementSrc, elementName);
        //public static IWebElement GetEditElement(this IWebElement elementSrc, int index) => GetChildEditElement(elementSrc, index);

        //public static IWebElement GetTextElement(this IWebElement elementSrc, string elementName) => GetChildTextElement(elementSrc, elementName);
        //public static IWebElement GetTextElement(this IWebElement elementSrc, int index) => GetChildTextElement(elementSrc, index);

        //public static IWebElement GetPaneElement(this IWebElement elementSrc, string elementName) => GetChildPaneElement(elementSrc, elementName);

        //public static IWebElement GetRdoBtnElement(this IWebElement elementSrc, string elementName) => GetChildRdoBtnElement(elementSrc, elementName);
        //public static IWebElement GetRdoBtnElement(this IWebElement elementSrc, int index) => GetChildRdoBtnElement(elementSrc, index);

        //public static IWebElement GetBtnElement(this IWebElement elementSrc, string elementName) => GetChildBtnElement(elementSrc, elementName);
        //public static IWebElement GetBtnElement(this IWebElement elementSrc, int index) => GetChildBtnElement(elementSrc, index);

        //public static IWebElement GetDataGridElement(this IWebElement elementSrc, string elementName) => GetChildDataGridElement(elementSrc, elementName);



        //public static string GetFirstEditContent(this IWebElement elementSrc) => GetFirstChildEditElementContent(elementSrc);

        //public static string GetFirstTextContent(this IWebElement elementSrc) => GetFirstChildTextElementContent(elementSrc);

        //public static string GetFirstPaneContent(this IWebElement elementSrc) => GetFirstChildPaneElementContent(elementSrc);

        //public static string GetFirstRdoBtnContent(this IWebElement elementSrc) => GetFirstChildRdoBtnElementContent(elementSrc);

        //public static IWebElement GetFirstEditElement(this IWebElement elementSrc) => GetFirstChildEditElement(elementSrc);

        //public static IWebElement GetFirstTextElement(this IWebElement elementSrc) => GetFirstChildTextElement(elementSrc);

        //public static IWebElement GetFirstPaneElement(this IWebElement elementSrc) => GetFirstChildPaneElement(elementSrc);

        //public static IWebElement GetFirstRdoBtnElement(this IWebElement elementSrc) => GetFirstChildRdoBtnElement(elementSrc);

        //public static IWebElement GetFirstDataGridElement(this IWebElement elementSrc) => GetFirstChildDataGridElement(elementSrc);

        //public static IWebElement GetFirstTreeViewElement(this IWebElement elementSrc) => GetFirstChildTreeViewElement(elementSrc);

        //public static IWebElement GetFirstComboBoxElement(this IWebElement elementSrc) => GetFirstChildComboBoxElement(elementSrc);
        /// <summary>
        /// Get first matched specific kind of control element or element's content
        /// </summary>
        /// <param name="elementSrc"></param>
        /// <returns></returns>
        public static IWebElement GetFirstTextElement(this IWebElement elementSrc, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetFirstChildOfControlType(ElementControlType.TextBlock, searchType);
        }

        public static string GetFirstTextContent(this IWebElement elementSrc, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetFirstChildContentOfControlType(ElementControlType.TextBlock, searchType);
        }

        public static IWebElement GetFirstEditElement(this IWebElement elementSrc, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetFirstChildOfControlType(ElementControlType.TextBox, searchType);
        }

        public static string GetFirstEditContent(this IWebElement elementSrc, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetFirstChildContentOfControlType(ElementControlType.TextBox, searchType);
        }

        public static IWebElement GetFirstPaneElement(this IWebElement elementSrc, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetFirstChildOfControlType(ElementControlType.ScrollViewer, searchType);
        }

        public static string GetFirstPaneContent(this IWebElement elementSrc, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetFirstChildContentOfControlType(ElementControlType.ScrollViewer, searchType);
        }

        public static IWebElement GetFirstRdoBtnElement(this IWebElement elementSrc, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetFirstChildOfControlType(ElementControlType.RadioButton, searchType);
        }

        public static string GetFirstRdoBtnContent(this IWebElement elementSrc, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetFirstChildContentOfControlType(ElementControlType.RadioButton, searchType);
        }

        public static IWebElement GetFirstDataGridElement(this IWebElement elementSrc, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetFirstChildOfControlType(ElementControlType.DataGrid, searchType);
        }

        public static IWebElement GetFirstTreeViewElement(this IWebElement elementSrc, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetFirstChildOfControlType(ElementControlType.TreeView, searchType);
        }

        public static IWebElement GetFirstComboBoxElement(this IWebElement elementSrc, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetFirstChildOfControlType(ElementControlType.ComboBox, searchType);
        }

        public static IWebElement GetFirstTabControlElement(this IWebElement elementSrc, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetFirstChildOfControlType(ElementControlType.TabControl, searchType);
        }

        public static IWebElement GetFirstCustomElement(this IWebElement elementSrc, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetFirstChildOfControlType(ElementControlType.Custom, searchType);
        }

        public static IWebElement GetFirstListBoxItemElement(this IWebElement elementSrc, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetFirstChildOfControlType(ElementControlType.ListBoxItem, searchType);
        }

        public static IWebElement GetFirstCheckBoxElement(this IWebElement elementSrc, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetFirstChildOfControlType(ElementControlType.CheckBox, searchType);
        }

        /// <summary>
        /// Get specific kind of control element or element's content by name
        /// </summary>
        /// <param name="elementSrc"></param>
        /// <param name="elementName"></param>
        /// <returns></returns>

        public static IWebElement GetTextElement(this IWebElement elementSrc, string elementName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildOfControlType(ElementControlType.TextBlock, elementName, searchType);
        }

        public static string GetTextContent(this IWebElement elementSrc, string elementName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildContentOfControlType(ElementControlType.TextBlock, elementName, searchType);
        }

        public static IWebElement GetEditElement(this IWebElement elementSrc, string elementName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildOfControlType(ElementControlType.TextBox, elementName, searchType);
        }

        public static string GetEditContent(this IWebElement elementSrc, string elementName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildContentOfControlType(ElementControlType.TextBox, elementName, searchType);
        }

        public static IWebElement GetPaneElement(this IWebElement elementSrc, string elementName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildOfControlType(ElementControlType.ScrollViewer, elementName, searchType);
        }

        public static string GetPaneContent(this IWebElement elementSrc, string elementName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildContentOfControlType(ElementControlType.ScrollViewer, elementName, searchType);
        }

        public static IWebElement GetRdoBtnElement(this IWebElement elementSrc, string elementName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildOfControlType(ElementControlType.RadioButton, elementName, searchType);
        }

        public static string GetRdoBtnContent(this IWebElement elementSrc, string elementName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildContentOfControlType(ElementControlType.RadioButton, elementName, searchType);
        }

        public static IWebElement GetBtnElement(this IWebElement elementSrc, string elementName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildOfControlType(ElementControlType.Button, elementName, searchType);
        }

        public static string GetBtnContent(this IWebElement elementSrc, string elementName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildContentOfControlType(ElementControlType.Button, elementName, searchType);
        }

        public static IWebElement GetDataGridElement(this IWebElement elementSrc, string elementName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildOfControlType(ElementControlType.DataGrid, elementName, searchType);
        }

        public static string GetDataGridContent(this IWebElement elementSrc, string elementName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildContentOfControlType(ElementControlType.DataGrid, elementName, searchType);
        }

        public static IWebElement GetComboBoxElement(this IWebElement elementSrc, string elementName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildOfControlType(ElementControlType.ComboBox, elementName, searchType);
        }

        public static string GetComboBoxContent(this IWebElement elementSrc, string elementName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildContentOfControlType(ElementControlType.ComboBox, elementName, searchType);
        }

        public static IWebElement GetCustomElement(this IWebElement elementSrc, string elementName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildOfControlType(ElementControlType.Custom, elementName, searchType);
        }

        public static string GetCustomContent(this IWebElement elementSrc, string elementName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildContentOfControlType(ElementControlType.Custom, elementName, searchType);
        }

        public static IWebElement GetWindowElement(this IWebElement elementSrc, string elementName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildOfControlType(ElementControlType.Window, elementName, searchType);
        }

        public static string GetWindowContent(this IWebElement elementSrc, string elementName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildContentOfControlType(ElementControlType.Window, elementName, searchType);
        }

        public static IWebElement GetCheckBoxElement(this IWebElement elementSrc, string elementName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildOfControlType(ElementControlType.CheckBox, elementName, searchType);
        }

        public static string GetCheckBoxContent(this IWebElement elementSrc, string elementName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildContentOfControlType(ElementControlType.CheckBox, elementName, searchType);
        }

        public static IWebElement GetTreeViewElement(this IWebElement elementSrc, string elementName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildOfControlType(ElementControlType.TreeView, elementName, searchType);
        }

        public static string GetTreeViewContent(this IWebElement elementSrc, string elementName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildContentOfControlType(ElementControlType.TreeView, elementName, searchType);
        }

        public static IWebElement GetTreeViewItemElement(this IWebElement elementSrc, string elementName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildOfControlType(ElementControlType.TreeViewItem, elementName, searchType);
        }

        public static string GetTreeViewItemContent(this IWebElement elementSrc, string elementName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildContentOfControlType(ElementControlType.TreeViewItem, elementName, searchType);
        }

        public static IWebElement GetTabItemElement(this IWebElement elementSrc, string elementName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildOfControlType(ElementControlType.TabItem, elementName, searchType);
        }

        public static string GetTabItemContent(this IWebElement elementSrc, string elementName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildContentOfControlType(ElementControlType.TabItem, elementName, searchType);
        }

        public static IWebElement GetTabControlElement(this IWebElement elementSrc, string elementName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildOfControlType(ElementControlType.TabControl, elementName, searchType);
        }

        public static string GetListBoxItemContent(this IWebElement elementSrc, string elementName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildContentOfControlType(ElementControlType.ListBoxItem, elementName, searchType);
        }

        public static IWebElement GetListBoxItemElement(this IWebElement elementSrc, string elementName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildOfControlType(ElementControlType.ListBoxItem, elementName, searchType);
        }


        /// <summary>
        /// Get specific kind of control element or element's content by index
        /// </summary>
        /// <param name="elementSrc"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static IWebElement GetTextElement(this IWebElement elementSrc, int index, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildOfControlType(ElementControlType.TextBlock, index, searchType);
        }

        public static string GetTextContent(this IWebElement elementSrc, int index, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildContentOfControlType(ElementControlType.TextBlock, index, searchType);
        }

        public static IWebElement GetEditElement(this IWebElement elementSrc, int index, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildOfControlType(ElementControlType.TextBox, index, searchType);
        }

        public static string GetEditContent(this IWebElement elementSrc, int index, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildContentOfControlType(ElementControlType.TextBox, index, searchType);
        }

        public static IWebElement GetRdoBtnElement(this IWebElement elementSrc, int index, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildOfControlType(ElementControlType.RadioButton, index, searchType);
        }

        public static string GetRdoBtnContent(this IWebElement elementSrc, int index, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildContentOfControlType(ElementControlType.RadioButton, index, searchType);
        }

        public static IWebElement GetBtnElement(this IWebElement elementSrc, int index, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildOfControlType(ElementControlType.Button, index, searchType);
        }

        public static string GetBtnContent(this IWebElement elementSrc, int index, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildContentOfControlType(ElementControlType.Button, index, searchType);
        }

        public static IWebElement GetCustomElement(this IWebElement elementSrc, int index, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildOfControlType(ElementControlType.Custom, index, searchType);
        }

        public static string GetCustomContent(this IWebElement elementSrc, int index, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildContentOfControlType(ElementControlType.Custom, index, searchType);
        }

        public static IWebElement GetTabControlElement(this IWebElement elementSrc, int index, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildOfControlType(ElementControlType.TabControl, index, searchType);
        }

        public static string GetTabControlContent(this IWebElement elementSrc, int index, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildContentOfControlType(ElementControlType.TabControl, index, searchType);
        }

        #endregion

        /// <summary>
        /// Get specific kind of control element by condition
        /// </summary>
        /// <param name="elementSrc"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static IWebElement GetToolbarElement(this IWebElement elementSrc, Func<IWebElement, bool> condition)
        {
            return elementSrc.GetSpecificChildOfControlType(ElementControlType.ToolBar, condition);
        }

        public static IWebElement GetCustomElement(this IWebElement elementSrc, Func<IWebElement, bool> condition)
        {
            return elementSrc.GetSpecificChildOfControlType(ElementControlType.Custom, condition);
        }

        public static IWebElement GetCustomElement(this IWebElement elementSrc, string childName, Func<IWebElement, bool> condition)
        {
            return elementSrc.GetSpecificChildOfControlType(ElementControlType.Custom, childName, condition);
        }

        /// <summary>
        /// Get specific kind of control element collection
        /// </summary>
        /// <param name="elementSrc"></param>
        /// <returns></returns>
        public static ReadOnlyCollection<IWebElement> GetDataItems(this IWebElement elementSrc)
        {
            return elementSrc.GetSpecificChildrenOfControlType(ElementControlType.DataItem);
        }

        public static ReadOnlyCollection<IWebElement> GetTabItems(this IWebElement elementSrc)
        {
            return elementSrc.GetSpecificChildrenOfControlType(ElementControlType.TabItem);
        }

        public static ReadOnlyCollection<IWebElement> GetMenuItems(this IWebElement elementSrc)
        {
            return elementSrc.GetSpecificChildrenOfControlType(ElementControlType.MenuItem);
        }

        public static ReadOnlyCollection<IWebElement> GetTreeViewItems(this IWebElement elementSrc)
        {
            return elementSrc.GetSpecificChildrenOfControlType(ElementControlType.TreeViewItem);
        }

        #region Base Methods
        /// <summary>
        /// Base methods for finding different kinds of control element/elements
        /// </summary>
        /// <param name="elementSrc"></param>
        /// <returns></returns>
        public static ReadOnlyCollection<IWebElement> GetChildElements(this IWebElement elementSrc)
        {
            return elementSrc.GetElements(By.XPath("*/*"));
            //return elementSrc.GetElements(By.XPath(".//child::*"));
        }

        public static ReadOnlyCollection<IWebElement> GetChildElements2(this IWebElement elementSrc)
        {
            return elementSrc.GetElements(By.CssSelector("*"));
        }

        public static int GetChildElementsCount(this IWebElement elementSrc)
        {
            return elementSrc.GetChildElements().Count;
        }

        // Legacy method
        public static ReadOnlyCollection<IWebElement> GetChildElementsOfControlType(this IWebElement elementSrc, ElementControlType elementType)
        {
            return elementSrc.GetChildElements()
                             .Where(e => e.TagName == elementType.GetDescription())
                             .ToList().AsReadOnly();
        }

        // Legacy method
        public static List<string> GetChildElementsContentOfControlType(this IWebElement elementSrc, ElementControlType elementType)
        {
            //var ChildElements = elementSrc.GetChildElements();
            //var ChildElementsOfControlType = ChildElements.Where(e => e.TagName == elementType.GetDescription());
            return elementSrc.GetChildElements()
                             .Where(e => e.TagName == elementType.GetDescription())
                             .Select(e => e.Text)
                             .ToList();
        }

        public static IWebElement GetParentElement(this IWebDriver driver, IWebElement elementSrc)
        {
            // Get the immediate parent:
            string xPathParent = $"//*[@RuntimeId='{((WindowsElement)elementSrc).Id}']/parent::node()";
            return driver.GetElement(By.XPath(xPathParent));
        }

        public static string GetSpecificChildContentOfControlType(this IWebElement elementSrc, ElementControlType elementType, int childIndex = 0, ElementSearchType searchType = ElementSearchType.BFS)
        {
            IWebElement elementFound = elementSrc.GetSpecificChildOfControlType(elementType, childIndex, searchType);
            string elementContent = elementFound != null ? elementFound.Text : null;
            return elementContent;

            //if (elementSrcChildrenContents.Count == 0)
            //{
            //    return null;
            //}

            //if (childIndex >= elementSrcChildrenContents.Count || childIndex < -1)
            //{
            //    throw new IndexOutOfRangeException(childIndex.ToString());
            //}

            //return childIndex == -1 ? elementSrcChildrenContents.Last() : elementSrcChildrenContents[childIndex];
        }

        public static string GetSpecificChildContentOfControlType(this IWebElement elementSrc, ElementControlType elementType, string childName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            //List<string> elementSrcChildrenContents = elementSrc.GetChildElementsContentOfControlType(elementType);
            //return elementSrc.GetChildElements()
            //                 .Where(e => e.TagName == elementType.GetDescription() && e.CheckElementHasNameOrId(childName))
            //                 .Select(e => e.Text)
            //                 .FirstOrDefault();

            IWebElement elementFound = elementSrc.GetSpecificChildOfControlType(elementType, childName, searchType);
            string elementContent = elementFound != null ? elementFound.Text : null;
            return elementContent;
        }

        public static string GetFirstChildContentOfControlType(this IWebElement elementSrc, ElementControlType elementType, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildContentOfControlType(elementType, 0, searchType);
        }

        public static string GetLastChildContentOfControlType(this IWebElement elementSrc, ElementControlType elementType, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildContentOfControlType(elementType, -1, searchType);
        }

        public static IWebElement GetSpecificChildOfControlType(this IWebElement elementSrc, ElementControlType elementType, int childIndex = 0, ElementSearchType searchType = ElementSearchType.BFS)
        {
            if (searchType == ElementSearchType.BFS)
                return elementSrc.GetSpecificChildOfControlTypeByBFS(elementType, childIndex);
            else
                return elementSrc.GetSpecificChildOfControlTypeByDFS(elementType, childIndex);
        }

        public static IWebElement GetSpecificChildOfControlType(this IWebElement elementSrc, ElementControlType elementType, string childName, ElementSearchType searchType = ElementSearchType.BFS)
        {
            if (searchType == ElementSearchType.BFS)
                return elementSrc.GetSpecificChildOfControlTypeByBFS(elementType, childName);
            else
                return elementSrc.GetSpecificChildOfControlTypeByDFS(elementType, childName);
        }

        public static IWebElement GetSpecificChildOfControlType(this IWebElement elementSrc, ElementControlType elementType, Func<IWebElement, bool> condition, ElementSearchType searchType = ElementSearchType.BFS)
        {
            if (searchType == ElementSearchType.BFS)
                return elementSrc.GetSpecificChildOfControlTypeByBFS(elementType, condition);
            else
                return elementSrc.GetSpecificChildOfControlTypeByDFS(elementType, condition);
        }

        public static IWebElement GetSpecificChildOfControlType(this IWebElement elementSrc, ElementControlType elementType, string childName, Func<IWebElement, bool> condition, ElementSearchType searchType = ElementSearchType.BFS)
        {
            if (searchType == ElementSearchType.BFS)
                return elementSrc.GetSpecificChildOfControlTypeByBFS(elementType, childName, condition);
            else
                return elementSrc.GetSpecificChildOfControlTypeByDFS(elementType, childName, condition);
        }

        // Get Specific Child by ControlType and child index (DFS)
        public static IWebElement GetSpecificChildOfControlTypeByDFS(this IWebElement elementSrc, ElementControlType elementType, int childIndex = 0)
        {
            //ReadOnlyCollection<IWebElement> children = elementSrc.GetChildElements();
            IWebElement elementFound;
            ReadOnlyCollection<IWebElement> elementsMatched = elementSrc.GetElements(GetLocatorByElementType(elementType) as By);

            if (childIndex == 0)
                elementFound = elementsMatched.FirstOrDefault();
            else if (childIndex == -1)
                elementFound = elementsMatched.LastOrDefault();
            else
                elementFound = elementsMatched[childIndex];

            if (elementFound != null) { return elementFound; }

            foreach (var child in elementSrc.GetChildElements())
            {
                // Recursive call to search in child elements
                IWebElement childElement = GetSpecificChildOfControlTypeByDFS(child, elementType);

                // If the element is found in the child hierarchy, return it
                if (childElement != null)
                {
                    return childElement;
                }
            }

            return elementFound;

            //ReadOnlyCollection<IWebElement> elementSrcChildren = elementSrc.GetChildElementsOfControlType(elementType);

            //if (elementSrcChildren.Count == 0)
            //{
            //    return null;
            //}

            //if (childIndex >= elementSrcChildren.Count || childIndex < -1)
            //{
            //    throw new IndexOutOfRangeException(childIndex.ToString());
            //}

            //return childIndex == -1 ? elementSrcChildren.Last() : elementSrcChildren[childIndex];
        }

        // Get Specific Child by ControlType and element name/Id (DFS)
        public static IWebElement GetSpecificChildOfControlTypeByDFS(this IWebElement elementSrc, ElementControlType elementType, string childName)
        {
            //var elementFound = children.Where(e => e.TagName == elementType.GetDescription() && e.GetAttribute("Name") == childName)
            //                            .SingleOrDefault();

            //var elementFound = children.FirstOrDefault(e => e.TagName == elementType.GetDescription() && e.GetAttribute("Name") == childName);

            //var children = elementSrc.GetChildElements();
            //var elementFound = children.FirstOrDefault(e => e.TagName == elementType.GetDescription() && e.CheckElementHasNameOrId(childName));

            //var children = elementSrc.GetElements(By.ClassName(elementType.ToString()));
            //Console.WriteLine($"elementType: \"{elementType.ToString()}\"");

            //ReadOnlyCollection<IWebElement> elementsMatched = elementSrc.GetElements(By.TagName(elementType.GetDescription()));

            // Get element with Element locator (By/ByIdOrName)
            ReadOnlyCollection<IWebElement> elementsMatched = null;
            if (GetLocatorByElementType(elementType, childName) is ByAutomationIdOrName byIdOrNameLocator)
            {
                elementsMatched = elementSrc.GetElements(byIdOrNameLocator);
            }
            else if (GetLocatorByElementType(elementType, childName) is By byLocator)
            {
                elementsMatched = elementSrc.GetElements(byLocator);
            }
            
            IWebElement elementFound = elementsMatched.FirstOrDefault(e => e.CheckElementHasNameOrId(childName));
            if (elementFound != null) { return elementFound; }
#if DEBUG
                    Console.WriteLine($"elementSrc: {elementSrc.Text}");
                    Console.WriteLine($"ChildElementsCount: {elementSrc.GetChildElementsCount()}");
#endif
            foreach (var child in elementSrc.GetChildElements())
            {
                // Recursive call to search in child elements
                IWebElement childElement = GetSpecificChildOfControlTypeByDFS(child, elementType, childName);

                // If the element is found in the child hierarchy, return it
                if (childElement != null)
                {
                    return childElement;
                }
            }

            return elementFound;

            //ReadOnlyCollection<IWebElement> elementSrcChildren = elementSrc.GetChildElementsOfControlType(elementType);
            //return elementSrc.GetChildElements()
            //                 .Where(e => e.TagName == elementType.GetDescription() && e.GetAttribute("Name") == childName)
            //                 .SingleOrDefault();
        }

        // Get Specific Child by ControlType and child index (BFS)
        public static IWebElement GetSpecificChildOfControlTypeByBFS(this IWebElement elementSrc, ElementControlType elementType, int childIndex = 0)
        {
            // Create a queue for BFS and enqueue the starting element
            Queue<IWebElement> queue = new Queue<IWebElement>();
            queue.Enqueue(elementSrc);

            while (queue.Count > 0)
            {
                // Dequeue the front element
                IWebElement currentElement = queue.Dequeue();

                // Check if the current element matches the criteria
                IWebElement elementFound;
                ReadOnlyCollection<IWebElement> elementsMatched = currentElement.GetElements(GetLocatorByElementType(elementType) as By);

                if (childIndex == 0)
                    elementFound = elementsMatched.FirstOrDefault();
                else if (childIndex == -1)
                    elementFound = elementsMatched.LastOrDefault();
                else
                    elementFound = elementsMatched[childIndex];

                if (elementFound != null) { return elementFound; }

                //#if DEBUG
                //Console.WriteLine($"currentElement: {currentElement.Text}({currentElement.TagName})");
                //Console.WriteLine($"ChildElementsCount: {currentElement.GetChildElementsCount()}");
                //#endif

                // Enqueue all child elements
                foreach (var child in currentElement.GetChildElements())
                {
                    queue.Enqueue(child);
                }
            }

            // Return null if no matching element was found
            return null;
        }

        // Assuming necessary namespaces are already included
        // Get Specific Child by ControlType and element name/Id (BFS)
        public static IWebElement GetSpecificChildOfControlTypeByBFS(this IWebElement elementSrc, ElementControlType elementType, string childName)
        {
            // Create a queue for BFS and enqueue the starting element
            Queue<IWebElement> queue = new Queue<IWebElement>();
            queue.Enqueue(elementSrc);

            while (queue.Count > 0)
            {
                // Dequeue the front element
                IWebElement currentElement = queue.Dequeue();

                // Check if the current element matches the criteria
                //Console.WriteLine($"TagName to query: {elementType.GetDescription()}");

                // Get element with Element locator (By/ByIdOrName)
                ReadOnlyCollection<IWebElement> elementsMatched = null;
                if (GetLocatorByElementType(elementType, childName) is ByAutomationIdOrName byIdOrNameLocator)
                {
                    elementsMatched = currentElement.GetElements(byIdOrNameLocator);
                }
                else if (GetLocatorByElementType(elementType, childName) is By byLocator)
                {
                    elementsMatched = currentElement.GetElements(byLocator);
                }

                //ReadOnlyCollection<IWebElement> elementsMatched = currentElement.GetElements(By.TagName(elementType.GetDescription()));
                IWebElement elementFound = elementsMatched.FirstOrDefault(e => e.CheckElementHasNameOrId(childName));
                if (elementFound != null)
                {
                    return elementFound;
                }
//#if DEBUG
                //Console.WriteLine($"currentElement: {currentElement.Text}({currentElement.TagName})");
                //Console.WriteLine($"ChildElementsCount: {currentElement.GetChildElementsCount()}");
//#endif
                // Enqueue all child elements
                foreach (var child in currentElement.GetChildElements())
                {
                    queue.Enqueue(child);
                }
            }

            // Return null if no matching element was found
            return null;
        }

        // Get Child by ControlType and condition (DFS)
        public static IWebElement GetSpecificChildOfControlTypeByDFS(this IWebElement elementSrc, ElementControlType elementType, Func<IWebElement, bool> condition)
        {
            //ReadOnlyCollection<IWebElement> children = elementSrc.GetChildElements();
            //elementFound = children.FirstOrDefault(e => e.TagName == elementType.GetDescription() && condition(e));

            // Element locator
            By locator = GetLocatorByElementType(elementType) as By;
            IWebElement elementFound = elementSrc.GetElements(locator)
                                                 .FirstOrDefault(e => condition(e));

            if (elementFound != null) { return elementFound; }

            foreach (var child in elementSrc.GetChildElements())
            {
                // Recursive call to search in child elements
                IWebElement childElement = child.GetSpecificChildOfControlType(elementType, condition);

                // If the element is found in the child hierarchy, return it
                if (childElement != null)
                {
                    return childElement;
                }
            }

            return elementFound;
        }

        // Get Child by ControlType and condition (BFS)
        public static IWebElement GetSpecificChildOfControlTypeByBFS(this IWebElement elementSrc, ElementControlType elementType, Func<IWebElement, bool> condition)
        {
            //ReadOnlyCollection<IWebElement> children = elementSrc.GetChildElements();
            //elementFound = children.FirstOrDefault(e => e.TagName == elementType.GetDescription() && condition(e));

            // Create a queue for BFS and enqueue the starting element
            Queue<IWebElement> queue = new Queue<IWebElement>();
            queue.Enqueue(elementSrc);

            while (queue.Count > 0)
            {
                // Dequeue the front element
                IWebElement currentElement = queue.Dequeue();
                IWebElement elementFound = currentElement.GetElements(GetLocatorByElementType(elementType) as By)
                                                         .FirstOrDefault(e => condition(e));

                if (elementFound != null) { return elementFound; }

                // Enqueue all child elements
                foreach (var child in currentElement.GetChildElements())
                {
                    queue.Enqueue(child);
                }
            }

            // Return an empty collection if no matching element was found
            return null;
        }

        // Get Child by ControlType, childName and condition (DFS)
        public static IWebElement GetSpecificChildOfControlTypeByDFS(this IWebElement elementSrc, ElementControlType elementType, string childName, Func<IWebElement, bool> condition)
        {
            //ReadOnlyCollection<IWebElement> children = elementSrc.GetChildElements();
            //elementFound = children.FirstOrDefault(e => e.TagName == elementType.GetDescription() && condition(e));

            // Get element with Element locator (By/ByAutomationIdOrName)
            IWebElement elementFound = null;
            if (GetLocatorByElementType(elementType, childName) is ByAutomationIdOrName byIdOrNameLocator)
            {
                elementFound = elementSrc.GetElements(byIdOrNameLocator)
                                         .FirstOrDefault(e => condition(e));
            }
            else if(GetLocatorByElementType(elementType, childName) is By byLocator)
            {
                elementFound = elementSrc.GetElements(byLocator)
                                         .FirstOrDefault(e => condition(e));
            }

            if (elementFound != null) { return elementFound; }

            foreach (var child in elementSrc.GetChildElements())
            {
                // Recursive call to search in child elements
                IWebElement childElement = child.GetSpecificChildOfControlType(elementType, condition);

                // If the element is found in the child hierarchy, return it
                if (childElement != null)
                {
                    return childElement;
                }
            }

            return elementFound;
        }

        // Get Child by ControlType, childName and condition (BFS)
        public static IWebElement GetSpecificChildOfControlTypeByBFS(this IWebElement elementSrc, ElementControlType elementType, string childName, Func<IWebElement, bool> condition)
        {
            //ReadOnlyCollection<IWebElement> children = elementSrc.GetChildElements();
            //elementFound = children.FirstOrDefault(e => e.TagName == elementType.GetDescription() && condition(e));

            // Create a queue for BFS and enqueue the starting element
            Queue<IWebElement> queue = new Queue<IWebElement>();
            queue.Enqueue(elementSrc);

            while (queue.Count > 0)
            {
                // Dequeue the front element
                IWebElement currentElement = queue.Dequeue();

                // Get element with Element locator (By/ByAutomationIdOrName)
                IWebElement elementFound = null;
                if (GetLocatorByElementType(elementType, childName) is ByAutomationIdOrName byIdOrNameLocator)
                {
                    elementFound = currentElement.GetElements(byIdOrNameLocator)
                                                 .FirstOrDefault(e => condition(e));
                }
                else if (GetLocatorByElementType(elementType, childName) is By byLocator)
                {
                    elementFound = currentElement.GetElements(byLocator)
                                                 .FirstOrDefault(e => condition(e));
                }

                if (elementFound != null) { return elementFound; }

                // Enqueue all child elements
                foreach (var child in currentElement.GetChildElements())
                {
                    queue.Enqueue(child);
                }
            }

            // Return an empty collection if no matching element was found
            return null;
        }

        // Get Child by ControlType and condition (BFS)
        public static ReadOnlyCollection<IWebElement> GetSpecificChildrenOfControlType(this IWebElement elementSrc, ElementControlType elementType, Func<IWebElement, bool> condition)
        {
            // Create a queue for BFS and enqueue the starting element
            Queue<IWebElement> queue = new Queue<IWebElement>();
            queue.Enqueue(elementSrc);

            while (queue.Count > 0)
            {
                // Dequeue the front element
                IWebElement currentElement = queue.Dequeue();

                // elements Found
                List<IWebElement> elementsFound = currentElement.GetElements(GetLocatorByElementType(elementType) as By)
                                                                .Where(condition)
                                                                .ToList();
                if (elementsFound.Count > 0)
                {
                    return elementsFound.AsReadOnly();
                }

                // Enqueue all child elements
                foreach (var child in currentElement.GetChildElements())
                {
                    queue.Enqueue(child);
                }
            }

            // Return an empty collection if no matching element was found
            return new List<IWebElement>().AsReadOnly();
        }

        public static IEnumerable<string> GetSpecificChildrenContentOfControlType(this IWebElement elementSrc, ElementControlType elementType, Func<IWebElement, bool> condition)
        {
            ReadOnlyCollection<IWebElement> elementsFound = elementSrc.GetSpecificChildrenOfControlType(elementType, condition);
            IEnumerable<string> elementContents = elementsFound != null ? elementsFound.Select(x => x.Text) : null;
            return elementContents;
        }

        // Get Children by ControlType (DFS)
        public static ReadOnlyCollection<IWebElement> GetSpecificChildrenOfControlType(this IWebElement elementSrc, ElementControlType elementType)
        {
            //var elementFound = children.Where(e => e.TagName == elementType.GetDescription() && e.GetAttribute("Name") == childName)
            //                            .SingleOrDefault();

            //var elementFound = children.FirstOrDefault(e => e.TagName == elementType.GetDescription() && e.GetAttribute("Name") == childName);

            //var children = elementSrc.GetChildElements();
            //var elementsFound = children.ToList().FindAll(e => e.TagName == elementType.GetDescription());

            try
            {
#if DEBUG
                Logger.LogMessage($"elementSrc.TagName:{elementSrc.TagName}");
#endif
                ReadOnlyCollection<IWebElement> elementsFound = elementSrc.GetElements(GetLocatorByElementType(elementType) as By);

                if (elementsFound != null) { return elementsFound; }

                foreach (var child in elementSrc.GetChildElements())
                {
                    // Recursive call to search in child elements
                    ReadOnlyCollection<IWebElement> grandChildren = GetSpecificChildrenOfControlType(child, elementType);

                    // If the elements is found in the child hierarchy, return it
                    if (grandChildren != null)
                    {
                        return grandChildren;
                    }
                }

                return elementsFound;
            }
            catch (ArgumentNullException ex)
            {
                string Message = $"element type is: \"{elementType.ToString()}\"";
                Logger.LogMessage($"{ex.Message}, {Message}");
                return null;
            }
        }

        public static IWebElement GetFirstChildOfControlType(this IWebElement elementSrc, ElementControlType elementType, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildOfControlType(elementType, 0, searchType);
        }

        public static IWebElement GetLastChildOfControlType(this IWebElement elementSrc, ElementControlType elementType, ElementSearchType searchType = ElementSearchType.BFS)
        {
            return elementSrc.GetSpecificChildOfControlType(elementType, -1, searchType);
        }
#endregion

#endregion

        #endregion

        #region DataTable Methods

        public static ReadOnlyCollection<IWebElement> GetDataTableRowElements(this IWebDriver driver, string DataGridAutomationID)
        {
            return driver.GetDataTableElement(DataGridAutomationID)
                         /*.GetElements(By.XPath(".//DataItem"));*/
                         .GetDataTableRowElements();
        }

        public static IEnumerable<string> GetDataTableHeaders(this IWebDriver driver, string DataGridAutomationID)
        {
            //return driver.GetDataTableElement(DataGridAutomationID)
            //             .GetElements(By.XPath(".//HeaderItem"))
            //             .Select(tg => tg.GetElement(By.XPath(".//Text")).Text);
            return driver.GetDataTableElement(DataGridAutomationID)
                         .GetDataTableHeaders();
        }

        public static ReadOnlyCollection<IWebElement> GetDataTableRowElements(this IWebElement element, string DataGridAutomationID)
        {
            return element.GetDataTableElement(DataGridAutomationID)
                          /*.GetElements(By.XPath(".//DataItem"));*/
                          //.GetDataItems().AsReadOnly();
                          .GetDataTableRowElements();
        }

        public static IEnumerable<string> GetDataTableHeaders(this IWebElement element, string DataGridAutomationID)
        {
            //return element.GetDataTableElement(DataGridAutomationID)
            //              .GetElements(By.XPath(".//HeaderItem"))
            //              .Select(tg => tg.GetElement(By.XPath(".//Text")).Text);
            return element.GetDataTableElement(DataGridAutomationID)
                          .GetDataTableHeaders();
        }

        public static IWebElement GetDataTableElement(this IWebElement element, string DataGridAutomationID)
        {
            return element.GetElement(MobileBy.AccessibilityId(DataGridAutomationID));
        }

        public static IWebElement GetDataTableElement(this IWebDriver driver, string DataGridAutomationID)
        {
            return driver.GetElement(MobileBy.AccessibilityId(DataGridAutomationID));
        }

        public static IEnumerable<IWebElement> GetSelectedRowCellElements(this IWebDriver driver, string DataGridAutomationID)
        {
            return driver.GetDataTableRowElements(DataGridAutomationID).GetSelectedRowCellElements();
        }

        public static IEnumerable<IWebElement> GetSelectedRowCellElements(this IWebElement element, string DataGridAutomationID)
        {
            return element.GetDataTableRowElements(DataGridAutomationID).GetSelectedRowCellElements();
        }

        public static ReadOnlyCollection<IWebElement> GetDataTableRowElements(this IWebElement DataGridElement)
        {
            //return DataGridElement.GetElements(By.XPath(".//DataItem"));
            return DataGridElement.GetDataItems();
        }

        public static IEnumerable<string> GetDataTableHeaders(this IWebElement DataGridElement)
        {
            return DataGridElement.GetElements(By.XPath(".//HeaderItem//Text"))
                                  .Select(e => e.Text);
        }


        public static IEnumerable<IWebElement> GetSelectedRowCellElements(this IEnumerable<IWebElement> rowElements)
        {
            return rowElements.FirstOrDefault(r => r.Selected)
                              .GetCellElementsOfRow();
        }

        public static IEnumerable<IWebElement> GetCellElementsOfRow(this IWebElement row)
        {
            return row.GetChildElements()
                      .Where(r => r.GetAttribute("IsGridItemPatternAvailable") == bool.TrueString);
        }

        public static IWebElement GetCellElementByColumnIndex(this IWebDriver driver, string DataGridAutomationID, int ColumnIndex)
        {
            return driver.GetSelectedRowCellElements(DataGridAutomationID)
                         .GetCellElementByColumnIndex(ColumnIndex);
        }

        public static IWebElement GetCellElementByColumnIndex(this IWebElement element, string DataGridAutomationID, int ColumnIndex)
        {
            return element.GetSelectedRowCellElements(DataGridAutomationID)
                          .GetCellElementByColumnIndex(ColumnIndex);
        }

        public static IWebElement GetCellElementByColumnIndex(this IEnumerable<IWebElement> cellElements, int ColumnIndex)
        {
            return cellElements.FirstOrDefault(c => c.GetAttribute("GridItem.Column") == ColumnIndex.ToString());
        }

        public static int GetColumnIndexOfCellElement(this IWebElement cellElement)
        {
            return int.Parse(cellElement.GetAttribute("GridItem.Column"));
        }

        // Original datagrid method 
        public static IWebElement GetCellBy(this IWebElement dataGridElement, int rowNo, string colName)
        {
            List<string> headers = dataGridElement.GetDataTableHeaders().ToList();
            // Retrieve all row elements once
            ReadOnlyCollection<IWebElement> rows = dataGridElement.GetDataTableRowElements();

            return rows[rowNo - 1].GetCellElementsOfRow().GetCellElementByColumnIndex(headers.IndexOf(colName));
        }

        // Adam 2024/05/20, modified datagrid method 
        public static IWebElement GetCellBy(this IWebElement dataGridElement, int rowNo, int colNo)
        {
            //List<string> headers = dataGridElement.GetDataTableHeaders().ToList();
            // Retrieve all row elements once
            ReadOnlyCollection<IWebElement> rows = dataGridElement.GetDataTableRowElements();

            return rows[rowNo - 1].GetCellElementsOfRow().GetCellElementByColumnIndex(colNo - 1);
        }

        // row get cell
        public static IWebElement GetCellBy(this IWebElement rowElement, int colNo)
        {
            //List<string> headers = dataGridElement.GetDataTableHeaders().ToList();
            // Retrieve all row elements once

            return rowElement.GetCellElementsOfRow().GetCellElementByColumnIndex(colNo - 1);
        }

        public static IWebElement GetCellByName(this IWebElement dataGridElement, int colNo, string cellName)
        {
            // Retrieve all row elements once
            ReadOnlyCollection<IWebElement> rows = dataGridElement.GetDataTableRowElements();

            //// Use LINQ to find the first matching cell
            //IWebElement cellFound = rows.Where(row => row.GetCellElementsOfRow().GetCellElementByColumnIndex(colNo - 1))
            //                            .FirstOrDefault(e => e.GetCellValue() == cellName);
            
            IWebElement cellFound = (from row in rows
                                     from cell in row.GetCellElementsOfRow()
                                     where cell.GetColumnIndexOfCellElement() == (colNo - 1)
                                     select cell)
                                     .FirstOrDefault(c => c.GetCellValue() == cellName);

            return cellFound;
        }


        public static string GetCellValue(this IWebElement dataGridElement, int rowNo, string colName)
        {
            // Get cell value By rowNo & colName
            IWebElement cell = dataGridElement.GetCellBy(rowNo, colName);
            return cell.GetCellValue();
        }

        public static string GetCellValue(this IWebElement dataGridElement, int rowNo, int colNo)
        {
            // Get cell value By rowNo & colNo
            IWebElement cell = dataGridElement.GetCellBy(rowNo, colNo);
            return cell.GetCellValue();
        }

        public static string GetCellValue(this IWebElement rowElement, int colNo)
        {
            // Get cell value By colNo
            IWebElement cell = rowElement.GetCellBy(colNo);
            return cell.GetCellValue();
        }

        public static string GetCellValueByName(this IWebElement dataGridElement, int colNo, string cellName)
        {
            // Get cell value By cellname
            IWebElement cell = dataGridElement.GetCellByName(colNo, cellName);
            return cell.GetCellValue();
        }

        public static string GetCellValue(this IWebElement dataGridElement, string DataGridAutomationID, int ColumnIndex)
        {
            // Get cell value By SelectedRow And ColumnIndex
            IWebElement cell = dataGridElement.GetSelectedRowCellElements(DataGridAutomationID)
                                              .GetCellElementByColumnIndex(ColumnIndex);
            return cell.GetCellValue();
        }

        public static string GetCellValue(this IWebElement cellElement)
        {
            if (cellElement == null)
                return null;
            else
                return !cellElement.GetAttribute("Value.Value").IsEmpty() ? cellElement.GetAttribute("Value.Value") : cellElement.GetFirstTextContent();
        }

        public static IEnumerable<IWebElement> GetColumnBy(this IWebElement dataGridElement, int colNo)
        {
            // Retrieve all row elements once
            ReadOnlyCollection<IWebElement> rows = dataGridElement.GetDataTableRowElements();

            IEnumerable<IWebElement> ColumnCells = (from row in rows
                                                    from cell in row.GetCellElementsOfRow()
                                                    where cell.GetColumnIndexOfCellElement() == (colNo - 1)
                                                    select cell);

            return ColumnCells;
        }

        public static IWebElement GetRowByName(this IWebElement dataGridElement, int colNo, string cellName)
        {
            // Retrieve all row elements once and return the row with cellName matched
            ReadOnlyCollection<IWebElement> rows = dataGridElement.GetDataTableRowElements();  
            return rows.FirstOrDefault(row => row.GetCellValue(colNo) == cellName);
        }

        public static IWebElement GetRowBy(this IWebElement dataGridElement, int rowNo)
        {
            // Retrieve all row elements once
            return dataGridElement.GetDataTableRowElements()[rowNo - 1];
        }

        public static IWebElement GetSelectedRow(this IWebElement dataGridElement)
        {
            return dataGridElement.GetDataTableRowElements().FirstOrDefault(r => r.Selected);
        }

        public static IWebElement GetSelectedRow(this IWebElement element, string DataGridAutomationID)
        {
            return element.GetDataTableElement(DataGridAutomationID).GetSelectedRow();
        }

        public static int GetRowCount(this IWebElement dataGridElement)
        {
            return dataGridElement.GetDataTableRowElements().Count;
        }

        #endregion

        #region MISC

        private static void GetNotFoundMessageAndFindingText(By findType, out string Message)
        {
            Message = string.Empty;

            // Set default values
            string toFind;
            if (findType.ToString().StartsWith("By.Id"))
            {
                toFind = findType.ToString().Replace("By.Id: ", "");
                Message = $"Element with automationId: \"{toFind}\" not found.";
            }
            if (findType.ToString().StartsWith("ByAccessibilityId"))
            {
                toFind = findType.ToString().Replace("ByAccessibilityId", "").TrimStart('(').TrimEnd(')');
                Message = $"Element with automationId: \"{toFind}\" not found.";
            }
            else if (findType.ToString().StartsWith("By.Name"))
            {
                toFind = findType.ToString().Replace("By.Name: ", "");
                Message = $"Element with Name: \"{toFind}\" not found.";
            }
            else if (findType.ToString().StartsWith("By.XPath"))
            {
                toFind = findType.ToString().Replace("By.XPath: ", "");
                Message = $"Element with XPath: \"{toFind}\" not found.";
            }
            else if (findType.ToString().StartsWith("By.ClassName[Contains]"))
            {
                toFind = findType.ToString().Replace("By.ClassName[Contains]: ", "");
                Message = $"Element with ClassName: \"{toFind}\" not found.";
            }
            else if (findType.ToString().StartsWith("By.TagName"))
            {
                toFind = findType.ToString().Replace("By.TagName: ", "");
                Message = $"Element with TagName: \"{toFind}\" not found.";
            }
            else if (findType.ToString().StartsWith("ByAutomationIdOrName"))
            {
                toFind = findType.ToString().Replace("ByAutomationIdOrName", "");
                Message = $"Element with Id Or Name: \"{toFind}\" not found.";
            }
        }

        public static object GetLocatorByElementType(ElementControlType controlType, string locatorValue = "")
        {
            switch (controlType) 
            {
                case ElementControlType.Button:
                case ElementControlType.CheckBox:
                case ElementControlType.ComboBox:
                case ElementControlType.TextBox:
                case ElementControlType.Image:
                case ElementControlType.ListBoxItem:
                case ElementControlType.ListBox:
                case ElementControlType.Menu:
                case ElementControlType.MenuItem:
                case ElementControlType.ProgressBar:
                case ElementControlType.RadioButton:
                case ElementControlType.ScrollBar:
                case ElementControlType.TabControl:
                case ElementControlType.TabItem:
                case ElementControlType.TextBlock:
                case ElementControlType.ToolBar:
                case ElementControlType.ToolTip:
                case ElementControlType.TreeView:
                case ElementControlType.TreeViewItem:
                case ElementControlType.Window:
                case ElementControlType.ScrollViewer:
                case ElementControlType.DatePicker:
                case ElementControlType.Calendar:
                case ElementControlType.CalendarDayButton:
                case ElementControlType.CalendarButton:
                case ElementControlType.Separator:
                    return By.ClassName(controlType.ToString());

                case ElementControlType.Header:
                case ElementControlType.HeaderItem:
                case ElementControlType.DataGrid:
                case ElementControlType.DataItem:
                case ElementControlType.Thumb:
                case ElementControlType.Group:
                case ElementControlType.Custom:
                    if (!locatorValue.IsNullOrEmpty())
                        return new ByAutomationIdOrName(locatorValue);
                    else
#if DEBUG
                        Logger.LogMessage($"controlType.GetDescription():{controlType.GetDescription()}");
#endif
                        return By.TagName(controlType.GetDescription());
                default:
                    return By.ClassName(controlType.ToString());
            }
        }

        public static bool CheckElementHasNameOrId(this IWebElement element, string NameOrAutomationID)
        {
            if (NameOrAutomationID.IsNullOrEmpty())
                return false;

            if (element == null) return false;

            List<string> identifiers = new List<string> 
            {
                element.GetAttribute("Name"),
                element.GetAttribute("AutomationId")
            };

            if (identifiers.Contains(NameOrAutomationID))
                return true;
            else
                return false;
        }

        public static bool hasAttribute(this IWebElement element, string attributeName)
        {
            return element.GetAttribute(attributeName) != null ? true : false;
        }
#endregion
    }
}
