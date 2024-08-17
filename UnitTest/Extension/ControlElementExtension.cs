using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chroma.UnitTest.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using static PP5AutoUITests.AutoUIActionHelper;
using static PP5AutoUITests.ElementFinder;
using static PP5AutoUITests.ThreadHelper;
using Keys = OpenQA.Selenium.Keys;

namespace PP5AutoUITests
{
    public static class ControlElementExtension
    {
        #region MenuList

        //static IWebDriver driver = Executor.GetInstance().GetCurrentDriver();

        //public static bool IsMenuItemEnabled(this IWebElement element, params string[] itemNames)
        //{
        //    return element.GetMenuItem(itemNames).Enabled;
        //}

        //public static IWebElement GetMenuItem(this IWebElement element, params string[] itemNames)
        //{
        //    IWebElement menu = element.GetElement(By.ClassName("Menu"));

        //    foreach (string itemName in itemNames)
        //    {
        //        Console.WriteLine($"LeftClick on Text \"{itemName}\"");
        //        var menuItemText = menu.GetElement(By.XPath($".//Text[@Name='{itemName}']"));
        //        var subMenu = CurrentDriver.GetParentElement(menuItemText);

        //        // Use attribute: 'IsExpandCollapsePatternAvailable' to check if menuitem can be expanded
        //        if (subMenu.GetAttribute("IsExpandCollapsePatternAvailable") == false.ToString())
        //        {
        //            return subMenu;
        //        }

        //        menuItemText.LeftClick();
        //        menu = subMenu;
        //    }
        //    return menu;
        //}

        public static void MenuSelect(params string[] itemNames)
        {
            IWebElement menu = Executor.GetInstance().GetCurrentDriver().GetElement(By.ClassName("Menu"));

            foreach (string itemName in itemNames)
            {
                //Console.WriteLine($"LeftClick on Text \"{itemName}\"");
                //menu.GetElement(By.XPath($".//Text[@Name='{itemName}']")).LeftClick();
                //menu.GetTextElement(itemName).LeftClick();
                //menu.GetTextElement(itemName).LeftClick();
                menu.GetElement(By.Name(itemName)).LeftClick();
            }
        }

        public static IEnumerable<string> GetSubMenuListItemNames(string itemName)
        {
            IWebElement menu = Executor.GetInstance().GetCurrentDriver().GetElement(By.ClassName("Menu"));
            IEnumerable<string> MenuListItemsNames;

            //Console.WriteLine($"LeftClick on Text \"{itemName}\"");
            //IWebElement subMenu = menu.GetElement(By.XPath($".//Text[@Name='{itemName}']/.."));
            IWebElement subMenu = menu.GetElement(By.Name(itemName));
            subMenu.LeftClick();
            MenuListItemsNames = subMenu.GetChildElements().Select(e => e.Text);
            subMenu.LeftClick();
            return MenuListItemsNames;
        }

        public static IEnumerable<string> GetMainMenuListItemNames()
        {
            IWebElement menu = Executor.GetInstance().GetCurrentDriver().GetElement(By.ClassName("Menu"));
            return menu.GetMenuItems().Select(e => e.Text);
        }

        #endregion

        #region ComboBox

        public static void GetComboBoxItems(this IWebElement comboBox, out ReadOnlyCollection<IWebElement> cmbItems)
        {
            //if (comboBox.TagName == ElementControlType.ComboBox.GetDescription())
            //    return CurrentDriver.GetElements(By.ClassName("ComboBoxItem"));
            //else if (comboBox.TagName == ElementControlType.ListBox.GetDescription())
            //    return CurrentDriver.GetElements(By.ClassName("ListBoxItem"));
            //else
            //    return null;
            
            comboBox.LeftClick();
            //cmbItems = CurrentDriver.GetElement(By.ClassName("Popup"))
            //                        .GetElements(By.ClassName("ListBoxItem"));
            cmbItems = comboBox.GetElements(By.ClassName("ListBoxItem"));
        }

        public static int GetComboBoxItemCount(this IWebElement comboBox)
        {
            comboBox.LeftClick();
            //cmbItems = CurrentDriver.GetElement(By.ClassName("Popup"))
            //                        .GetElements(By.ClassName("ListBoxItem"));
            return comboBox.GetElements(By.ClassName("ListBoxItem")).Count;
        }

        public static void SelectComboBoxItemByName(this IWebElement comboBox, string name, bool supportKeyInputSearch = true)
        {
            if (CheckComboBoxHasItemByName(comboBox, name, out IWebElement cmbItem))
            {
                if (supportKeyInputSearch)
                {
                    comboBox.SendSingleKeys(name);
                }
                else
                {
                    cmbItem.LeftClick();
                }
            }
        }

        public static void SelectComboBoxItemByIndex(this IWebElement comboBox, int index, bool supportKeyInputSearch = true)
        {
            comboBox.GetComboBoxItems(out ReadOnlyCollection<IWebElement> cmbItems);
            if (cmbItems.Count() >= index + 1)
            {
                string name = cmbItems[index].Text;

                if (supportKeyInputSearch)
                {
                    SendComboKeys(name, Keys.Enter);
                }
                else
                {
                    cmbItems[index].LeftClick();
                }
            }
        }

        public static bool CheckComboBoxHasItemByName(IWebElement comboBox, string name, out IWebElement cmbItem)
        {
            comboBox.GetComboBoxItems(out ReadOnlyCollection<IWebElement> cmbItems);
            cmbItem = cmbItems.FirstOrDefault(item => item.GetFirstTextContent() == name);
            return cmbItem != null;
        }

        #endregion

        #region DataGrid

        public static void SelectDataGridItemByRowIndex(this IWebElement element, int rowIdx)
        {
            //element.GetElements(By.XPath(".//DataItem"))[rowIdx].LeftClick();

            //ReadOnlyCollection<IWebElement> rows = element.GetChildElementsOfControlType(ElementControlType.DataItem);
            ReadOnlyCollection<IWebElement> rows = element.GetDataItems();

            if (rowIdx >= rows.Count || rowIdx < -1)
                throw new ArgumentException("wrong row index!");

            if (rowIdx == -1)
                rows[rows.Count - 1].LeftClick();
            else
                rows[rowIdx].LeftClick();
        }

        #endregion

        #region TreeView

        public static IWebElement GetColorSettingItem(this IWebElement ColorSettingPage, ColorSettingPageType csPageType, string groupName, int idx = 1)
        {
            IWebElement commandTree;
            //if (csPageType == ColorSettingPageType.TestCommand)
            //{
            //    commandTree = ColorSettingPage.GetElement(MobileBy.AccessibilityId(csPageType.GetDescription()));
            //}
            //else
            //{
            //    commandTree = ColorSettingPage.GetElement(MobileBy.AccessibilityId(csPageType.GetDescription()));
            //}

            commandTree = ColorSettingPage.GetElement(MobileBy.AccessibilityId(csPageType.GetDescription()));
            Console.WriteLine($"LeftClick on Text \"{groupName}\"");

            bool cmdListIsFocused = false;
            IWebElement groupTreeItem = null;
            while (groupTreeItem == null)
            {
                // Get the element that matching the given groupname directly by XPath (Faster)
                groupTreeItem = commandTree.GetElement(By.XPath($".//TreeItem[@ClassName='TreeViewItem']/Text[@Name='{groupName}']/parent::node()"), 3000);

                if (groupTreeItem == null && !cmdListIsFocused)
                {
                    commandTree.LeftClick();
                    cmdListIsFocused = true; // Set the flag to true after the left click on the command list
                }

                // If element if out of screen, press page down to find the element
                if (groupTreeItem == null)
                {
                    Press(Keys.PageDown);
                    Thread.Sleep(1);

                    // If scroll to end of the command list, group item still not found, throw exception
                    foreach (var cmdList in commandTree.GetElements(By.ClassName("TreeView")))
                    {
                        if (cmdList.GetAttribute("Scroll.VerticallyScrollable") == bool.FalseString)
                            continue;

                        if (cmdList.GetAttribute("Scroll.VerticalScrollPercent") == "100")
                            throw new GroupNameNotExistedException(groupName);
                    }
                }
            }

            // Use attribute: "ExpandCollapse.ExpandCollapseState" to check the expand/collapse state, where: Expanded (1), Collapsed (0)
            if (groupTreeItem.isElementCollapsed())
                groupTreeItem.GetFirstTextElement().DoubleClick();

            var cmdTreeItems = groupTreeItem.GetElements(By.XPath($".//TreeItem[@ClassName='TreeViewItem']"));

            if (cmdTreeItems.Count == 0)
                return null;
            if (idx > cmdTreeItems.Count || idx < -1 || idx == 0)
                throw new ArgumentOutOfRangeException(idx.ToString());

            IWebElement itemToSetColor = idx == -1 ? cmdTreeItems.Last() : cmdTreeItems[idx];

            //var cmdTreeItem = groupTreeItem.GetElement(By.XPath($"(.//TreeItem[@ClassName='TreeViewItem'])[{cmdNumber + 1}]"));
            //Console.WriteLine($"LeftClick on Text \"{itemToSetColor.GetFirstTextContent()}\"");

            //// If element if out of screen, move to the element first
            while (bool.Parse(itemToSetColor.GetAttribute("IsOffscreen")))
            {
                Press(Keys.PageDown);
                Thread.Sleep(50);
            }

            //// Click on the item
            //itemToSetColor.LeftClick();

            return itemToSetColor;
        }

        public static bool ExpandTreeView(this IWebElement treeviewElement)
        {
            // Use attribute: "ExpandCollapse.ExpandCollapseState" to check the expand/collapse state, where: Expanded (1), Collapsed (0)
            // if the element is the tree item leaf node: LeafNode (3), it's not expandable
            if (treeviewElement.isElementAtLeafNode())
                return false;
            else if (treeviewElement.isElementCollapsed())
                treeviewElement.DoubleClick();

            return WaitUntil(() => treeviewElement.isElementExpanded());
        }

        public static bool SelectTreeViewItem(this IWebElement treeviewElement, params string[] labels)
        {
            IWebElement tve = treeviewElement;
            IWebElement tvieTmp;
            bool notLeafNode = false;
            //string ExpandCollapsePatternAvailabilityTag = "IsExpandCollapsePatternAvailable";

            foreach (string label in labels)
            {
                tvieTmp = tve.GetTreeViewItemElement(label);
                //if (tve.hasAttribute(ExpandCollapsePatternAvailabilityTag))
                //    if (tve.GetAttribute(ExpandCollapsePatternAvailabilityTag) == false.ToString())
                notLeafNode = tvieTmp.ExpandTreeView();
                
                if (!notLeafNode)
                {
                    // Select on the tree node
                    if (!tvieTmp.Selected)
                        tvieTmp.LeftClick();
                    break;
                }
                else
                    tve = tvieTmp;
            }
            return !notLeafNode;
        }

        #endregion

        #region CheckBox

        public static void TickCheckBox(this IWebElement checkBox)
        {
            if (!checkBox.isElementChecked())
                checkBox.LeftClick();
        }

        public static void UnTickCheckBox(this IWebElement checkBox)
        {
            if (checkBox.isElementChecked())
                checkBox.LeftClick();
        }

        #endregion

        #region TabControl

        public static IWebElement TabSelect(this IWebElement tabControl, params object[] tabNamesOrIdxs)
        {
            if (tabNamesOrIdxs == null || tabNamesOrIdxs.Length == 0)
                return null;

            IWebElement tabControlTemp = tabControl;
            IWebElement tabItem = null;

            for (int i = 0; i < tabNamesOrIdxs.Length; i++)
            {
                tabItem = tabControlTemp.TabSelect(tabNamesOrIdxs[i]);
                if (i == tabNamesOrIdxs.Length - 1) break;
                //tabControlTemp = tabItem.GetFirstTabControlElement();
                tabControlTemp = tabItem.GetElement(By.ClassName("TabControl"));
            }

            return tabItem;
        }

        public static IWebElement TabSelect(this IWebElement tabControl, params string[] tabNames)
        {
            if (tabNames == null || tabNames.Length == 0)
                return null;

            IWebElement tabControlTemp = tabControl;
            IWebElement tabItem = null;

            //tabControlTemp.TabSelect(tabNames.First());

            //if (tabNames.Length > 1)
            //{
            //    string[] tabNamesTmp = tabNames.Skip(1).ToArray();
            //    tabControlTemp = tabItem.GetFirstTabControlElement();
            //    tabItem = tabControlTemp.TabSelect(tabNamesTmp);
            //}

            for (int i = 0; i < tabNames.Length; i++)
            {
                tabItem = tabControlTemp.TabSelect(tabNames[i]);
                if (i == tabNames.Length - 1) break;
                //tabControlTemp = tabItem.GetFirstTabControlElement();
                tabControlTemp = tabItem.GetElement(By.ClassName("TabControl"));
            }

            return tabItem;
        }

        public static IWebElement TabSelect(this IWebElement tabControl, params int[] indeces)
        {
            if (indeces == null || indeces.Length == 0)
                return null;

            IWebElement tabControlTemp = tabControl;
            IWebElement tabItem = null;

            /*
            //foreach (int idx in indeces)
            //{
            //    tabItem = tabControlTemp.GetElements(By.ClassName("TabItem"))[idx];
            //    if (!tabItem.Selected)
            //        tabItem.LeftClick();

            //    tabControlTemp = tabItem.GetElement(By.ClassName("TabControl"));
            //}

            //tabItem = tabControlTemp.GetElements(By.ClassName("TabItem"))[indeces.First()];
            //if (!tabItem.Selected && tabItem.isElementVisible())
            //    tabItem.LeftClick();
            */

            for (int i = 0; i < indeces.Length; i++)
            {
                tabItem = tabControlTemp.TabSelect(indeces[i]);
                if (i == indeces.Length - 1) break;
                tabControlTemp = tabItem.GetElement(By.ClassName("TabControl"));
            }

            return tabItem;
        }

        public static IWebElement TabSelect(this IWebElement tabControl, int index)
        {
            IWebElement tabItem = tabControl.GetElements(By.ClassName("TabItem"))[index];
            if (!tabItem.Selected && tabItem.isElementVisible())
                tabItem.LeftClick();
            return tabItem;
        }

        public static IWebElement TabSelect(this IWebElement tabControl, string tabName)
        {
            //IWebElement tabItem = tabControl.GetTabItemElement(tabName);
            IWebElement tabItem = tabControl.GetElement(By.Name(tabName));
            if (!tabItem.Selected && tabItem.isElementVisible())
                tabItem.LeftClick();
            return tabItem;
        }

        public static IWebElement TabSelect(this IWebElement tabControl, object tabNameOrIdx)
        {
            IWebElement tabItem = null;
            if (tabNameOrIdx.GetType() == typeof(string))
                tabItem = tabControl.TabSelect(tabNameOrIdx.ToString());
            else if (tabNameOrIdx.GetType() == typeof(int))
                tabItem = tabControl.TabSelect(int.Parse(tabNameOrIdx.ToString()));

            return tabItem;
        }

        #endregion

        #region Toolbar

        public static void ToolBarSelect(this IWebElement window, int index)
        {
            IWebElement toolbarItem = window.GetToolbarElement((e) => e.isElementVisible()).GetRdoBtnElement(index);
            if (toolbarItem.Enabled)
                toolbarItem.LeftClick();
        }

        #endregion
    }
}
