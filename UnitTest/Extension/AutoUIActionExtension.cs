using System.Runtime.CompilerServices;
using System.Threading;
using Chroma.UnitTest.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
namespace PP5AutoUITests
{
    public static class AutoUIActionExtension
    {
#if DEBUG
        readonly static StopwatchWrapper timer = new StopwatchWrapper();
#endif
        //static IWebDriver driver = Executor.GetInstance().GetCurrentDriver();

        #region Members

        //private readonly WindowsDriver<WindowsElement> session;
        //private string sessionMainWindowName;
        //private int nTryCount = 5;
        //private int msTimeout = 8000;
        ////private WindowsElement uiTarget;
        ////private System.Collections.Generic.IReadOnlyCollection<WindowsElement> uiTargets;
        //private WindowsElement elementFound;
        //public WindowsElement ElementFound
        //{
        //    get
        //    {
        //        return elementFound;
        //    }
        //}
        //private System.Collections.Generic.IReadOnlyCollection<WindowsElement> elementsFound;
        //public System.Collections.Generic.IReadOnlyCollection<WindowsElement> ElementsFound
        //{
        //    get
        //    {
        //        return elementsFound;
        //    }
        //}

        #endregion

        #region Constructor
        //public AutoUIAction()
        //{
        //this.session = Executor.GetInstance().GetCurrentSession();
        //}

        //public AutoUIAction(WindowsDriver<WindowsElement> session)
        //{
        //    this.session = session;
        //}

        //public AutoUIAction(WindowsDriver<WindowsElement> session, int nTryCount) : this(session)
        //{
        //    this.session = session;
        //    this.nTryCount = nTryCount;
        //}

        //public AutoUIAction(WindowsDriver<WindowsElement> session, int nTryCount, int msTimeout) : this(session, nTryCount)
        //{
        //    this.session = session;
        //    this.nTryCount = nTryCount;
        //    this.msTimeout = msTimeout;
        //}

        //public AutoUIAction(WindowsDriver<WindowsElement> session, string sessionMainWindowName) : this(session)
        //{
        //    this.session = session;
        //    this.sessionMainWindowName = sessionMainWindowName;
        //}
        #endregion

        //public void SetRetryTimes(int nTryCount) => this.nTryCount = nTryCount;

        //public void SetTimeout(int msTimeout) => this.msTimeout = msTimeout;

        #region Action Methods

        /* //Legacy Method
                public void PerformActionByAutomationID(ActionType action, string AutomationID, string textInput = "", bool isFindMultiple = false)
                {
                    System.Threading.Thread.Sleep(10);
                    //if (sessionType == SessionType.MainPanel)
                    //{
                    //    SwitchToWindow(MainPanel_Session);
                    //    elementFound = this.FindElementByAutomationId(MainPanel_Session, AutomationID, 2);
                    //}
                    //else
                    //{
                    //    SwitchToWindow(PP5IDE_Session);
                    //    elementFound = this.FindElementByAutomationId(PP5IDE_Session, AutomationID, 2);
                    //}

        #if DEBUG
                    // Start Timer
                    timer.Reset();
                    timer.Start();
        #endif
            

                    //// your action to measure time
                    SwitchToWindow();

                    elementFound = null;
                    elementsFound = null;
                    //elementFound = this.FindElementByAutomationId(AutomationID);

                    if (isFindMultiple)
                    {
                        var FindElementTask = Task.Run(() => this.FindElementsByAutomationId(AutomationID));
                        if (FindElementTask.Wait(TimeSpan.FromMilliseconds(msTimeout)))
                            elementsFound = FindElementTask.Result;
                        else
                        {
                            Assert.Fail("Timed out");
                            //throw new Exception("Timed out");
                        }
                    }
                
                    else
                    {
                        var FindElementTask = Task.Run(() => this.FindElementByAutomationId(AutomationID));
                        if (FindElementTask.Wait(TimeSpan.FromMilliseconds(msTimeout)))
                            elementFound = FindElementTask.Result;
                        else
                        {
                            Assert.Fail("Timed out");
                            //throw new Exception("Timed out");
                        }
                    }


        #if DEBUG
                    // Stop Timer
                    timer.Stop();
                    long findElementTime = timer.GetElapsedTimeInMilliSeconds();
                    Console.WriteLine($"Find element time consumed (ms): {findElementTime}");

                    // Start Timer
                    timer.Reset();
                    timer.Start();
        #endif

                    if (action == ActionType.None)
                    {
                        ((driver != null) ? (System.Action)(() => { }) :
                        () => { LogHelper.LogFindElementFailedByAutomationID(AutomationID); return; })();
                    }

                    if (action == ActionType.LeftClick)
                    {
                        ((driver != null) ?
                        (System.Action)(() => { elementFound.Click(); }) :
                        () => { LogHelper.LogFindElementFailedByAutomationID(AutomationID); return; })();
                    }
                    else if (action == ActionType.LeftDoubleClick)
                    {
                        //(elementFound != null) ?
                        //(Action)(() => {
                        //    session.Mouse.MouseMove(elementFound.Coordinates);
                        //    session.Mouse.DoubleClick(null);
                        //}) :
                        //() => { LogHelper.LogFindElementFaidByAutomationID(AutomationID); })();
                        if (elementFound != null)
                        {
                            session.Mouse.MouseMove(elementFound.Coordinates);
                            session.Mouse.DoubleClick(null);
                        }
                        else { LogHelper.LogFindElementFailedByAutomationID(AutomationID); };
                    }
                    else if (action == ActionType.SendKeys)
                    {
                        ((elementFound != null) ?
                        (System.Action)(() => {
                            elementFound.Click();
                            elementFound.Clear();
                            LogHelper.LogSendKeys(textInput);
                            elementFound.SendKeys(textInput);
                            Assertor.AssertSendKeys(textInput, elementFound.Text);
                        }) :
                        () => { LogHelper.LogFindElementFailedByAutomationID(AutomationID); })();
                    }

        #if DEBUG
                    timer.Stop();
                    long ActionTime = timer.GetElapsedTimeInMilliSeconds();
                    Console.WriteLine($"Action time consumed (ms): {ActionTime}");
                    Console.WriteLine($"Total time consumed (ms): {findElementTime + ActionTime}");
        #endif
                }

                public void PerformActionByName(ActionType action, string Name, string textInput = "", bool isFindMultiple = false)
                {
                    System.Threading.Thread.Sleep(10);

        #if DEBUG
                    // Start Timer
                    timer.Reset();
                    timer.Start();
        #endif

                    SwitchToWindow();
                    elementFound = null;
                    elementsFound = null;

                    if (isFindMultiple)
                    {
                        var FindElementTask = Task.Run(() => this.FindElementsByName(Name));
                        if (FindElementTask.Wait(TimeSpan.FromMilliseconds(msTimeout)))
                            elementsFound = FindElementTask.Result;
                        else
                        {
                            Assert.Fail("Timed out");
                            //throw new Exception("Timed out");
                        }
                    }

                    else
                    {
                        var FindElementTask = Task.Run(() => this.FindElementByName(Name));
                        if (FindElementTask.Wait(TimeSpan.FromMilliseconds(msTimeout)))
                            elementFound = FindElementTask.Result;
                        else
                        {
                            Assert.Fail("Timed out");
                            //throw new Exception("Timed out");
                        }
                    }

        #if DEBUG
                    // Stop Timer
                    timer.Stop();
                    long findElementTime = timer.GetElapsedTimeInMilliSeconds();
                    Console.WriteLine($"Find element time consumed (ms): {findElementTime}");

                    // Start Timer
                    timer.Reset();
                    timer.Start();
        #endif

                    if (action == ActionType.None)
                    {
                        ((elementFound != null) ? (System.Action)(() => { }) :
                        () => { LogHelper.LogFindElementFailedByName(Name); return; })();
                    }

                    if (action == ActionType.LeftClick)
                    {
                        ((elementFound != null) ?
                        (System.Action)(() => { elementFound.Click(); }) :
                        () => { LogHelper.LogFindElementFailedByName(Name); return; })();
                    }
                    else if (action == ActionType.LeftDoubleClick)
                    {
                        if (elementFound != null)
                        {
                            session.Mouse.MouseMove(elementFound.Coordinates);
                            session.Mouse.DoubleClick(null);
                        }
                        else { LogHelper.LogFindElementFailedByName(Name); };
                    }
                    else if (action == ActionType.SendKeys)
                    {
                        ((elementFound != null) ?
                        (System.Action)(() => {
                            elementFound.Click();
                            elementFound.Clear();
                            LogHelper.LogSendKeys(textInput);
                            elementFound.SendKeys(textInput);
                            Assertor.AssertSendKeys(textInput, elementFound.Text);
                        }) :
                        () => { LogHelper.LogFindElementFailedByName(Name); })();
                    }

        #if DEBUG
                    timer.Stop();
                    long ActionTime = timer.GetElapsedTimeInMilliSeconds();
                    Console.WriteLine($"Action time consumed (ms): {ActionTime}");
                    Console.WriteLine($"Total time consumed (ms): {findElementTime + ActionTime}");
        #endif
                }
        */

        // Element required action
        public static void RightClick(this IWebElement onElement)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).ContextClick(onElement).Perform();
        }

        public static void LeftClick(this IWebElement onElement)
        {
            string showName = !onElement.Text.IsEmpty() ? !onElement.GetAttribute("AutomationId").IsEmpty()
                                            ? onElement.Text
                                            : onElement.GetAttribute("AutomationId")
                                            : onElement.GetAttribute("ClassName");

            Console.WriteLine($"LeftClick on {onElement.TagName} \"{showName}\"");
            new Actions(Executor.GetInstance().GetCurrentDriver()).Click(onElement).Perform();
        }
        public static void LeftClickWithDelay(this IWebElement onElement, int delayMilliSecond)
        {
            //new Actions(Executor.GetInstance().GetCurrentDriver()).ClickAndHold(onElement)
            //                                                      .Release(onElement)
            //                                                      .Perform();
            Actions actions = new Actions(Executor.GetInstance().GetCurrentDriver());
            actions.ClickAndHold(onElement);
            Thread.Sleep(delayMilliSecond);
            actions.Release(onElement);
            actions.Perform();
        }

        public static void DoubleClick(this IWebElement onElement)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).DoubleClick(onElement).Perform();
        }

        public static void ClickAndHold(this IWebElement onElement)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).ClickAndHold(onElement).Perform();
        }

        public static void MoveToElement(this IWebElement toElement)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).MoveToElement(toElement).Perform();
        }

        public static void MoveToElement(this IWebElement toElement, int offsetX, int offsetY)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).MoveToElement(toElement, offsetX, offsetY).Perform();
        }

        public static void MoveToElement(this IWebElement toElement, int offsetX, int offsetY, MoveToElementOffsetOrigin offsetOrigin)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).MoveToElement(toElement, offsetX, offsetY, offsetOrigin).Perform();
        }

        public static void SendSingleKeys(this IWebElement element, string keysToSend)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).SendKeys(element, keysToSend).Perform();
        }

        public static void SendComboKeys(this IWebElement element, params string[] multikeysToSend)
        {
            Actions actions = new Actions(Executor.GetInstance().GetCurrentDriver());

            foreach (string keysToSend in multikeysToSend)
            {
                actions.SendKeys(element, keysToSend);
                Console.WriteLine($"KeyboardInput \"{keysToSend}\" in {element.TagName} \"{element.GetAttribute("AutomationId")}\"");
            }
            
            actions.Perform();
        }

        public static void SendContent(this IWebElement element, string keysToSend)
        {
            element.Clear();
            if (!keysToSend.IsNullOrEmpty())
            {
                new Actions(Executor.GetInstance().GetCurrentDriver()).SendKeys(element, keysToSend).Perform();
                Console.WriteLine($"KeyboardInput \"{keysToSend}\" in {element.TagName} \"{element.GetAttribute("AutomationId")}\"");
            }
            else
                return;
        }

        public static void SelectAllContent(this IWebElement element)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).Click(element)
                                                                  .KeyDown(Keys.Control).SendKeys("a").KeyUp(Keys.Control)
                                                                  .Perform();
        }

        public static void ClearContent(this IWebElement element)
        {
            //char ctrlA = '\u0001';  // ASCII code 1 for Ctrl-A
            //char ctrlV = '\u0016';  // ASCII code 16 for Ctrl-V

            //.SendKeys(element, Keys.Control + "a")
            //.SendKeys(Convert.ToString(ctrlV))

            SelectAllContent(element);
            //new Actions(Executor.GetInstance().GetCurrentDriver()).SendKeys(Keys.Backspace).SendKeys(Keys.Backspace).Perform();
            new Actions(Executor.GetInstance().GetCurrentDriver()).KeyDown(Keys.Control).SendKeys(Keys.Backspace).KeyUp(Keys.Control).Perform();
        }

        public static void PasteContent(this IWebElement element)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).Click(element)
                                                                  .KeyDown(Keys.Control).SendKeys("v").KeyUp(Keys.Control)
                                                                  .Perform();
        }

        public static void CopyContent(this IWebElement element)
        {
            SelectAllContent(element);
            new Actions(Executor.GetInstance().GetCurrentDriver()).KeyDown(Keys.Control).SendKeys("c").KeyUp(Keys.Control)
                                                                  .Perform();
        }

        public static void CutContent(this IWebElement element)
        {
            SelectAllContent(element);
            new Actions(Executor.GetInstance().GetCurrentDriver()).KeyDown(Keys.Control).SendKeys("x").KeyUp(Keys.Control)
                                                                  .Perform();
        }

        public static void CopyAndPaste(this IWebElement FromElement, IWebElement ToElement)
        {
            CopyContent(FromElement);
            PasteContent(ToElement);
        }

        public static void CutAndPaste(this IWebElement FromElement, IWebElement ToElement)
        {
            CutContent(FromElement);
            PasteContent(ToElement);
        }

        public static void KeyDown(this IWebElement element, string theKey)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).KeyDown(element, theKey).Perform();
        }

        public static void KeyUp(this IWebElement element, string theKey)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).KeyUp(element, theKey).Perform();
        }

        public static void DragAndDrop(this IWebElement source, IWebElement target)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).DragAndDrop(source, target).Perform();
        }

        public static void DragAndDropToOffset(this IWebElement source, int offsetX, int offsetY)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).DragAndDropToOffset(source, offsetX, offsetY).Perform();
        }

        #endregion
    }
}

