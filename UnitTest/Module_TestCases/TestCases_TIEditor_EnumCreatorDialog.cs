using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
//using ActionHelper = PP5AutoUITests.AutoUIActionHelper;
//using static PP5AutoUITests.AutoUIExtension;

namespace PP5AutoUITests
{
    [TestClass]
    public class TestCases_TIEditor_EnumCreatorDialog : TestBase
    {
#if DEBUG
        // Init Timer
        StopwatchWrapper timer = new StopwatchWrapper();
#endif

        [TestInitialize]
        public void TIEditor_TestMethodSetup()
        {
            OpenNewTIEditorWindow();
        }

        public void CleanUpVariableTable(VariableTabType tabType)
        {
            VariableTabNavi(tabType);

            var variablesRows = CurrentDriver.GetElements(MobileBy.AccessibilityId("ShowName"));
            if (variablesRows.Count == 1)
            {
                if (string.IsNullOrEmpty(variablesRows.First().Text))
                    return;
            }

            // Delete all current existing condition variables
            for ( int rowidx = 0; rowidx < variablesRows.Count - 1; rowidx++) 
            {
                //// last row is empty, skip delete action
                //if (string.IsNullOrEmpty(variablesRows.ToList()[rowidx].Text))
                //    continue;

                variablesRows.ToList()[0].LeftClick();

                // Delete selected condition variable
                CurrentDriver.GetElement(By.Name("Edit")).LeftClick();
                CurrentDriver.GetElement(By.Name("Delete")).LeftClick();
            }

            //Assert.AreEqual(1, uIActionPP5IDE.GetElementsById("ShowName", "ListItemRow").Count);
        }

        public void AddNewEnumItem(string name, string value)
        {
            //System.Threading.Thread.Sleep(100);
            CurrentDriver.GetElement(MobileBy.AccessibilityId("NewBtn")).LeftClick();

            Console.WriteLine("LeftClick on Value textbox in Enum Item Creator dialog");
            CurrentDriver.GetElement(MobileBy.AccessibilityId("enumItemNameTxtBox")).LeftClick();
            CurrentDriver.GetElement(MobileBy.AccessibilityId("enumItemNameTxtBox")).SendKeys(name);
            CurrentDriver.GetElement(MobileBy.AccessibilityId("enumItemValueTxtBox")).LeftClick();
            CurrentDriver.GetElement(MobileBy.AccessibilityId("enumItemValueTxtBox")).SendKeys(value);
            CurrentDriver.GetElement(MobileBy.AccessibilityId("Ok")).LeftClick();
        }

        public void AddNewEnumItemByXPath(string name, string value)
        {
            // LeftClick on Button "New" at (33,5)
            Console.WriteLine("LeftClick on Button \"New\" at (33,5)");
            string xpath_LeftClickButtonNew_33_5 = "/Pane[@ClassName=\"#32769\"][@Name=\"桌面 1\"]/Window[@ClassName=\"Window\"][@Name=\"Enum Item Editor\"]/Button[@Name=\"New\"][@AutomationId=\"NewBtn\"]";
            var winElem_LeftClickButtonNew_33_5 = CurrentDriver.FindElementByAbsoluteXPath(xpath_LeftClickButtonNew_33_5);
            if (winElem_LeftClickButtonNew_33_5 != null)
            {
                winElem_LeftClickButtonNew_33_5.Click();
            }
            else
            {
                Console.WriteLine($"Failed to find element using xpath: {xpath_LeftClickButtonNew_33_5}");
                return;
            }

            // LeftClick on Edit "" at (35,13)
            Console.WriteLine("LeftClick on Edit \"\" at (35,13)");
            string xpath_LeftClickEdit_35_13 = "/Pane[@ClassName=\"#32769\"][@Name=\"桌面 1\"]/Window[@ClassName=\"Window\"][@Name=\"Enum Item Creater\"]/Edit[@AutomationId=\"enumItemNameTxtBox\"]";
            var winElem_LeftClickEdit_35_13 = CurrentDriver.FindElementByAbsoluteXPath(xpath_LeftClickEdit_35_13);
            if (winElem_LeftClickEdit_35_13 != null)
            {
                winElem_LeftClickEdit_35_13.Click();
            }
            else
            {
                Console.WriteLine($"Failed to find element using xpath: {xpath_LeftClickEdit_35_13}");
                return;
            }

            // KeyboardInput VirtualKeys="Keys.NumberPad0 + Keys.NumberPad0" CapsLock=False NumLock=True ScrollLock=False
            Console.WriteLine("KeyboardInput VirtualKeys=\"Keys.NumberPad0 + Keys.NumberPad0\" CapsLock=False NumLock=True ScrollLock=False");
            System.Threading.Thread.Sleep(100);
            winElem_LeftClickEdit_35_13.SendKeys(name);

            // LeftClick on Edit "" at (34,14)
            Console.WriteLine("LeftClick on Edit \"\" at (34,14)");
            string xpath_LeftClickEdit_34_14 = "/Pane[@ClassName=\"#32769\"][@Name=\"桌面 1\"]/Window[@ClassName=\"Window\"][@Name=\"Enum Item Creater\"]/Edit[@AutomationId=\"enumItemValueTxtBox\"]";
            var winElem_LeftClickEdit_34_14 = CurrentDriver.FindElementByAbsoluteXPath(xpath_LeftClickEdit_34_14);
            if (winElem_LeftClickEdit_34_14 != null)
            {
                winElem_LeftClickEdit_34_14.Click();
            }
            else
            {
                Console.WriteLine($"Failed to find element using xpath: {xpath_LeftClickEdit_34_14}");
                return;
            }

            // KeyboardInput VirtualKeys="Keys.NumberPad0 + Keys.NumberPad0" CapsLock=False NumLock=True ScrollLock=False
            Console.WriteLine("KeyboardInput VirtualKeys=\"Keys.NumberPad0 + Keys.NumberPad0\" CapsLock=False NumLock=True ScrollLock=False");
            System.Threading.Thread.Sleep(100);
            winElem_LeftClickEdit_34_14.SendKeys(value);

            // LeftClick on Button "Ok" at (111,4)
            Console.WriteLine("LeftClick on Button \"Ok\" at (111,4)");
            string xpath_LeftClickButtonOk_111_4 = "/Pane[@ClassName=\"#32769\"][@Name=\"桌面 1\"]/Window[@ClassName=\"Window\"][@Name=\"Enum Item Creater\"]/Button[@ClassName=\"Button\"][@Name=\"Ok\"]";
            var winElem_LeftClickButtonOk_111_4 = CurrentDriver.FindElementByAbsoluteXPath(xpath_LeftClickButtonOk_111_4);
            if (winElem_LeftClickButtonOk_111_4 != null)
            {
                winElem_LeftClickButtonOk_111_4.Click();
            }
            else
            {
                Console.WriteLine($"Failed to find element using xpath: {xpath_LeftClickButtonOk_111_4}");
                return;
            }
        }

        public static IWebElement AddNewEnumItemByNameOrId(string name, string value)
        {
            IWebElement enumItemEditorWindow = PP5IDEWindow.GetElement(By.Name("Enum Item Editor"));

            // LeftClick on Button "New" at (33,5)
            //Console.WriteLine("LeftClick on Button \"New\" at (33,5)");
            enumItemEditorWindow.GetBtnElement("New").LeftClick();

            // LeftClick on Edit "" at (35,13)
            //Console.WriteLine("LeftClick on Edit \"\" at (35,13)");
            enumItemEditorWindow.GetEditElement("enumItemNameTxtBox").SendKeys(name);

            // LeftClick on Edit "" at (34,14)
            //Console.WriteLine("LeftClick on Edit \"\" at (34,14)");
            enumItemEditorWindow.GetElement(By.Name("Enum Item Creater"))
                                .GetLastChildOfControlType(ElementControlType.TextBox)
                                .SendKeys(value);

            // LeftClick on Button "Ok" at (111,4)
            //Console.WriteLine("LeftClick on Button \"Ok\" at (111,4)");
            enumItemEditorWindow.GetElement(By.Name("Enum Item Creater"))
                                .GetElement(By.Name("Ok")).LeftClick();

            return enumItemEditorWindow;
        }

        public void RemoveEnumItem(string name) 
        {
            
        }

        [TestMethod]
        public void MainPanel_TIEditor_RepeatAdd10Items()
        {
#if DEBUG
            // Start Timer
            timer.Reset();
            timer.Start();
#endif
            //CleanUpVariableTable(VariableTabType.Condition);
            Console.WriteLine("CleanUpConditionVariable() finished");
            CreateNewVariable1(VariableTabType.Condition, "a", "a", VariableDataType.Float, VariableEditType.ComboBox);
            Console.WriteLine("CreateConditionVariable() finished");

#if DEBUG
            // Stop Timer
            timer.Stop();
            long CreateNewCondVarTime = timer.GetElapsedTimeInMilliSeconds();
            Console.WriteLine($"Create new condition variable time consumed (ms): {CreateNewCondVarTime}");

            // Start Timer
            timer.Reset();
            timer.Start();
#endif

            // Click in Enum Item cell
            GetCellBy("CndGrid", 0, "Enum Item").LeftClick();
            //GetCellBy("CndGrid", 0, "Enum Item").DoubleClick();

            // Check Enum Item Editor Window is opened
            Assert.IsTrue(CurrentDriver.CheckElementExisted(By.Name("Enum Item Editor")));

            IWebElement enumItemEditorWindow = null;

            // Add 10 Enum Items in Enum Creater Window
            for (int i = 0; i < 10; i++)
            {
                enumItemEditorWindow = AddNewEnumItemByNameOrId(i.ToString(), i.ToString());         // By Automation ID
                //AddNewEnumItemByXPath(i.ToString(), i.ToString());  // By XPath
            }

            //uIActionPP5IDE.PerformActionByName(ActionType.LeftClick, "Ok");
            //uIActionPP5IDE.PerformActionByName(ActionType.None, "DisplayedEnum");
            enumItemEditorWindow.GetElement(By.Name("Ok")).LeftClick();
            var ElementFound = PP5IDEWindow.GetElement(MobileBy.AccessibilityId("DisplayedEnum"));
            for (int i = 0; i < 10; i++)
                Assert.AreEqual(string.Concat(i, "=", i), ElementFound.GetAttribute("Value.Value").Split(',')[i]);

            Console.WriteLine("Loop Adding 10 enum items finished");
#if DEBUG
            timer.Stop();
            long ActionTime = timer.GetElapsedTimeInMilliSeconds();
            Console.WriteLine($"Loop adding 10 inum items time consumed (ms): {ActionTime}");
            Console.WriteLine($"Total time consumed (ms): {CreateNewCondVarTime + ActionTime}");
#endif

            //// Stop Timer
            //timer.Stop();
            //long findElementTime = timer.GetElapsedTimeInMilliSeconds();
            //Console.WriteLine($"Find element time consumed (ms): {findElementTime}");

            //timer.Reset();
            //timer.Start();
            //if (winElem_LeftClickValueInEnumItemCreator != null)
            //{
            //    winElem_LeftClickValueInEnumItemCreator.Click();
            //    Console.WriteLine("Input \"1\" in Value textbox in Enum Item Creator dialog");
            //    winElem_LeftClickValueInEnumItemCreator.Clear();
            //    winElem_LeftClickValueInEnumItemCreator.SendKeys("1");
            //    Assert.AreEqual("1", winElem_LeftClickValueInEnumItemCreator.Text, "Input text is not 1");
            //}
            //else
            //{
            //    Console.WriteLine($"Failed to find element using AutomationId: {valueAutomationID}");
            //    return;
            //}

            //// Use XPath method
            //string xpath_LeftClickEdit_43_8 = "/Pane[@ClassName=\"#32769\"][@Name=\"桌面 1\"]/Window[@ClassName=\"Window\"]/Window[@ClassName=\"Window\"][@Name=\"Enum Item Creater\"]/Edit[@AutomationId=\"enumItemNameTxtBox\"]";

            //timer.Stop();
            //long findElementTime = timer.GetElapsedTimeInMilliSeconds();
            //Console.WriteLine($"Find element time consumed (ms): {findElementTime}");

            //timer.Reset();
            //timer.Start();

            //var winElem_LeftClickEdit_43_8 = AutoUIExecutor.FindElementByAbsoluteXPath(xpath_LeftClickEdit_43_8);
            //if (winElem_LeftClickEdit_43_8 != null)
            //{
            //    winElem_LeftClickEdit_43_8.Click();
            //    winElem_LeftClickEdit_43_8.Clear();
            //    winElem_LeftClickEdit_43_8.SendKeys("1");
            //    Assert.AreEqual("1", winElem_LeftClickEdit_43_8.Text, "Input text is not 1");
            //}
            //else
            //{
            //    Console.WriteLine($"Failed to find element using xpath: {xpath_LeftClickEdit_43_8}");
            //    Assert.Fail();
            //    return;
            //}

            //timer.Stop();
            //long ActionTime = timer.GetElapsedTimeInMilliSeconds();
            //Console.WriteLine($"Action time consumed (ms): {ActionTime}");
            //Console.WriteLine($"Total time consumed (ms): {findElementTime + ActionTime}");
        }
    }
}
