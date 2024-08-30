using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Appium;
using System.Windows.Input;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;

namespace PP5AutoUITests
{
    public static class AutoUIActionHelper
    {
#if DEBUG
        readonly static StopwatchWrapper timer = new StopwatchWrapper();
#endif
        //static readonly IWebDriver driver = Executor.GetInstance().GetCurrentDriver();

        #region Action Methods

        // Element required action
        public static void RightClick(IWebElement onElement)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).ContextClick(onElement).Perform();
        }

        public static void LeftClick(IWebElement onElement)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).Click(onElement).Perform();
        }

        public static void DoubleClick(IWebElement onElement)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).DoubleClick(onElement).Perform();
        }

        public static void ClickAndHold(IWebElement onElement)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).ClickAndHold(onElement).Perform();
        }

        public static void MoveToElement(IWebElement toElement)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).MoveToElement(toElement).Perform();
        }

        public static void MoveToElement(IWebElement toElement, int offsetX, int offsetY)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).MoveToElement(toElement, offsetX, offsetY).Perform();
        }

        public static void MoveToElement(IWebElement toElement, int offsetX, int offsetY, MoveToElementOffsetOrigin offsetOrigin)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).MoveToElement(toElement, offsetX, offsetY, offsetOrigin).Perform();
        }

        public static void SendKeys(IWebElement element, string keysToSend)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).SendKeys(element, keysToSend).Perform();
        }

        public static void KeyDown(IWebElement element, string theKey)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).KeyDown(element, theKey).Perform();
        }

        public static void KeyUp(IWebElement element, string theKey)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).KeyUp(element, theKey).Perform();
        }

        public static void DragAndDrop(IWebElement source, IWebElement target)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).DragAndDrop(source, target).Perform();
        }

        public static void DragAndDropToOffset(IWebElement source, int offsetX, int offsetY)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).DragAndDropToOffset(source, offsetX, offsetY).Perform();
        }

        // No Element required action
        public static void MoveByOffset(int offsetX, int offsetY)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).MoveByOffset(offsetX, offsetY).Perform();
        }

        public static void MoveByOffsetAndLeftClick(int offsetX, int offsetY)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).MoveByOffset(offsetX, offsetY).Click().Perform();
        }

        public static void MoveByOffsetAndRightClick(int offsetX, int offsetY)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).MoveByOffset(offsetX, offsetY).ContextClick().Perform();
        }

        public static void MoveByOffsetAndDoubleClick(int offsetX, int offsetY)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).MoveByOffset(offsetX, offsetY).DoubleClick().Perform();
        }

        public static void MoveToElementAndLeftClick(IWebElement target)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).MoveToElement(target, offsetX: 0, offsetY: 0, MoveToElementOffsetOrigin.Center)
                                                                  .Click().Perform();
        }

        public static void SendSingleKeys(string keysToSend)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).SendKeys(keysToSend).Perform();
        }

        public static void SendComboKeys(string keysToSend1, string keysToSend2)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).SendKeys(keysToSend1).SendKeys(keysToSend2).Perform();
        }

        public static void KeyDown(string theKey)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).KeyDown(theKey).Perform();
        }

        public static void KeyUp(string theKey)
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).KeyUp(theKey).Perform();
        }

        public static void LeftClick()
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).Click().Perform();
        }

        public static void Release()
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).Release().Perform();
        }

        public static void ClickAndHold()
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).ClickAndHold().Perform();
        }

        //public static void PressUp()
        //{
        //    new Actions(Executor.GetInstance().GetCurrentDriver()).SendKeys(Keys.Up).Perform();
        //}

        //public static void PressDown()
        //{
        //    new Actions(Executor.GetInstance().GetCurrentDriver()).SendKeys(Keys.Down).Perform();
        //}

        //public static void PressLeft()
        //{
        //    new Actions(Executor.GetInstance().GetCurrentDriver()).SendKeys(Keys.Left).Perform();
        //}

        //public static void PressRight()
        //{
        //    new Actions(Executor.GetInstance().GetCurrentDriver()).SendKeys(Keys.Right).Perform();
        //}

        //public static void PressEnter()
        //{
        //    new Actions(Executor.GetInstance().GetCurrentDriver()).SendKeys(Keys.Enter).Perform();
        //}

        //public static void PressPageUp()
        //{
        //    new Actions(Executor.GetInstance().GetCurrentDriver()).SendKeys(Keys.PageUp).Perform();
        //}

        //public static void PressPageDown()
        //{
        //    new Actions(Executor.GetInstance().GetCurrentDriver()).SendKeys(Keys.PageDown).Perform();
        //}

        public static void ScrollPageUp()
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).KeyDown(Keys.Control).SendKeys(Keys.PageUp).KeyUp(Keys.Control).Perform();
        }

        public static void ScrollPageDown()
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).KeyDown(Keys.Control).SendKeys(Keys.PageDown).KeyUp(Keys.Control).Perform();
        }

        //public static void PressHome()
        //{
        //    new Actions(Executor.GetInstance().GetCurrentDriver()).SendKeys(Keys.Home).Perform();
        //}

        //public static void PressEnd()
        //{
        //    new Actions(Executor.GetInstance().GetCurrentDriver()).SendKeys(Keys.End).Perform();
        //}

        //public static void PressBackSpace()
        //{
        //    new Actions(Executor.GetInstance().GetCurrentDriver()).KeyDown(Keys.Control).SendKeys(Keys.Backspace).KeyUp(Keys.Control).Perform();
        //}

        //public static void PressDelete()
        //{
        //    new Actions(Executor.GetInstance().GetCurrentDriver()).KeyDown(Keys.Control).SendKeys(Keys.Delete).KeyUp(Keys.Control).Perform();
        //}

        //public static void PressCancel()
        //{
        //    new Actions(Executor.GetInstance().GetCurrentDriver()).KeyDown(Keys.Control).SendKeys(Keys.Cancel).KeyUp(Keys.Control).Perform();
        //}

        //public static void PressTab()
        //{
        //    new Actions(Executor.GetInstance().GetCurrentDriver()).KeyDown(Keys.Control).SendKeys(Keys.Tab).KeyUp(Keys.Control).Perform();
        //}

        public static void Press(string keyStr)
        {
            if (keyStr == Keys.Backspace || keyStr == Keys.Delete || keyStr == Keys.Cancel || keyStr == Keys.Tab)
                new Actions(Executor.GetInstance().GetCurrentDriver()).KeyDown(Keys.Control).SendKeys(keyStr).KeyUp(Keys.Control).Perform();
            else
                new Actions(Executor.GetInstance().GetCurrentDriver()).SendKeys(keyStr).Perform();

            //string keyDesc = KeysConverter.GetDescription(keyStr);
            Logger.LogMessage($"Keyboard Pressing on: \"{KeysConverter.GetInstance().GetDescription(keyStr)}\"");
        }

        public static void SelectAll()
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).SendKeys(Keys.LeftControl).SendKeys("a").SendKeys(Keys.LeftControl).Perform();
        }

        public static void CopyAndPaste()
        {
            Copy();
            Paste();
        }

        public static void CutAndPaste()
        {
            Cut();
            Paste();
        }

        public static void Copy()
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).SendKeys(Keys.LeftControl).SendKeys("c").SendKeys(Keys.LeftControl).Perform();
        }

        public static void Cut()
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).SendKeys(Keys.LeftControl).SendKeys("x").SendKeys(Keys.LeftControl).Perform();
        }

        public static void Paste()
        {
            new Actions(Executor.GetInstance().GetCurrentDriver()).SendKeys(Keys.LeftControl).SendKeys("v").SendKeys(Keys.LeftControl).Perform();
        }

        //public static void PressUpNoDelay()
        //{
        //    new SuperAction(Executor.GetInstance().GetCurrentDriver()).KeyDownNoPause(Keys.Up).Perform();
        //}

        #endregion
    }

    //public class SuperAction : Actions
    //{
    //    //private CompositeAction action = new CompositeAction();
    //    private IActionExecutor actionExecutor;
    //    private ActionBuilder actionBuilder = new ActionBuilder();
    //    private KeyInputDevice defaultKeyboard = new KeyInputDevice("default keyboard");

    //    //
    //    // 摘要:
    //    //     Initializes a new instance of the OpenQA.Selenium.Interactions.Actions class.
    //    //
    //    // 參數:
    //    //   driver:
    //    //     The OpenQA.Selenium.IWebDriver object on which the actions built will be performed.
    //    public SuperAction(IWebDriver driver) : base(driver)
    //    {
    //        IHasInputDevices driverAs = GetDriverAs<IHasInputDevices>(driver);
    //        if (driverAs == null)
    //        {
    //            throw new ArgumentException("The IWebDriver object must implement or wrap a driver that implements IHasInputDevices.", "driver");
    //        }

    //        IActionExecutor driverAs2 = GetDriverAs<IActionExecutor>(driver);
    //        if (driverAs2 == null)
    //        {
    //            throw new ArgumentException("The IWebDriver object must implement or wrap a driver that implements IActionExecutor.", "driver");
    //        }

    //        actionExecutor = driverAs2;
    //    }

    //    private T GetDriverAs<T>(IWebDriver driver) where T : class
    //    {
    //        T val = driver as T;
    //        if (val == null)
    //        {
    //            for (IWrapsDriver wrapsDriver = driver as IWrapsDriver; wrapsDriver != null; wrapsDriver = wrapsDriver.WrappedDriver as IWrapsDriver)
    //            {
    //                val = wrapsDriver.WrappedDriver as T;
    //                if (val != null)
    //                {
    //                    driver = wrapsDriver.WrappedDriver;
    //                    break;
    //                }
    //            }
    //        }

    //        return val;
    //    }

    //    public SuperAction KeyDownNoPause(string keysToSend)
    //    {
    //        if (string.IsNullOrEmpty(keysToSend))
    //        {
    //            throw new ArgumentException("The key value must not be null or empty", "keysToSend");
    //        }

    //        foreach (char codePoint in keysToSend)
    //        {
    //            actionBuilder.AddAction(defaultKeyboard.CreateKeyDown(codePoint));
    //            //actionBuilder.AddAction(defaultKeyboard.CreateKeyUp(codePoint));
    //        }

    //        //actionBuilder.AddAction(new PauseInteraction(defaultKeyboard, TimeSpan.FromMilliseconds(100.0)));
    //        return this;
    //    }

    //    //
    //    // 摘要:
    //    //     Performs the currently built action.
    //    public new void Perform()
    //    {
    //        //if (actionExecutor.IsActionExecutor)
    //        //{
    //            actionExecutor.PerformActions(actionBuilder.ToActionSequenceList());
    //        //}
    //    }
    //}

    //internal class CompositeAction : IAction
    //{
    //    private List<IAction> actionsList = new List<IAction>();

    //    //
    //    // 摘要:
    //    //     Adds an action to the list of actions to be performed.
    //    //
    //    // 參數:
    //    //   action:
    //    //     An OpenQA.Selenium.Interactions.IAction to be appended to the list of actions
    //    //     to be performed.
    //    //
    //    // 傳回:
    //    //     A self reference.
    //    public CompositeAction AddAction(IAction action)
    //    {
    //        actionsList.Add(action);
    //        return this;
    //    }

    //    //
    //    // 摘要:
    //    //     Performs the actions defined in this list of actions.
    //    public void Perform()
    //    {
    //        foreach (IAction actions in actionsList)
    //        {
    //            actions.Perform();
    //        }
    //    }
    //}
}
