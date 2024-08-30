using Microsoft.VisualStudio.TestTools.UnitTesting;
using PP5AutoUITests;
using System;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace PP5AutoUITests
{
    [TestClass]
    public class TestBaseTests
    {
        //static TestBase testBase;
        //[AssemblyInitialize]
        //public new static void BeforeClass(TestContext tc)
        //{
        //    testBase = new TestBase();
        //}

        //[TestInitialize]
        //public new void TestMethodSetup()
        //{

        //}

        //[AssemblyCleanup]
        //public new static void AfterClass()
        //{
        //    testBase = null;
        //}

        [TestMethod]
        public void BeforeClass_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            TestContext tc = null;

            // Act
            TestBase.BeforeClass(tc);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void AfterClass_StateUnderTest_ExpectedBehavior()
        {
            // Act
            TestBase.AfterClass();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void TestMethodSetup_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();

            // Act
            testBase.TestMethodSetup();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void AfterTest_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();

            // Act
            testBase.AfterTest();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void TryTestBase_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();

            // Act
            testBase.TryTestBase();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void TestEnvSetup_StateUnderTest_ExpectedBehavior()
        {
            // Act
            TestBase.TestEnvSetup();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetGroupNames_StateUnderTest_ExpectedBehavior()
        {
            // Act
            var result = TestBase.GetGroupNames();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void LoadCommandGroupTypeInfos_StateUnderTest_ExpectedBehavior()
        {
            // Act
            TestBase.LoadCommandGroupInfos();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void LoadCommandGroupCommandNames_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            string groupName = null;

            // Act
            TestBase.LoadCommandGroupCommandNames(
                groupName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void QueryCommandNames_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            string groupName = null;

            // Act
            var result = testBase.QueryCommandNames(
                groupName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void AddCommandInCGIList_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            string groupName = null;
            string cmdName = null;

            // Act
            var result = TestBase.AddCommandInCGIList(
                groupName,
                cmdName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void PP5LogIn_StateUnderTest_ExpectedBehavior()
        {
            // Act
            TestBase.PP5LogIn();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void OpenNewTIEditorWindow_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();

            // Act
            testBase.OpenNewTIEditorWindow();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void OpenNewTPEditorWindow_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();

            // Act
            testBase.OpenNewTPEditorWindow();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void OpenDefaultGUIEditorWindow_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();

            // Act
            testBase.OpenDefaultGUIEditorWindow();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void OpenDefaultReportWindow_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();

            // Act
            testBase.OpenDefaultReportWindow();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void OpenDefaultManagementWindow_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();

            // Act
            testBase.OpenDefaultManagementWindow();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void OpenDefaultExecutionWindow_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            string tpName = null;

            // Act
            testBase.OpenDefaultExecutionWindow(
                tpName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void PerformOpenTPFile_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            string TPName = null;

            // Act
            testBase.PerformOpenTPFile(
                TPName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void PerformOpenNewTP_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            bool tpNotSaved = false;

            // Act
            testBase.PerformOpenNewTP(
                tpNotSaved);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void PerformOpenNewTI_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            bool tiNotSaved = false;

            // Act
            testBase.PerformOpenNewTI(
                tiNotSaved);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void PerformOpenNewTI_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var testBase = new TestBase();
            TestItemType tiType = default(global::PP5AutoUITests.TestItemType);
            TestItemRunType tiRunType = default(global::PP5AutoUITests.TestItemRunType);

            // Act
            testBase.PerformOpenNewTI(
                tiType,
                tiRunType);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void PerformLoadOldTI_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            string tiName = null;

            // Act
            testBase.PerformLoadOldTI(
                tiName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void PerformLoadOldTI_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var testBase = new TestBase();
            string tiName = null;
            TestItemType type = default(global::PP5AutoUITests.TestItemType);
            TestItemRunType runType = default(global::PP5AutoUITests.TestItemRunType);

            // Act
            testBase.PerformLoadOldTI(
                tiName,
                type,
                runType);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void PerformLoadOldTI_StateUnderTest_ExpectedBehavior2()
        {
            // Arrange
            var testBase = new TestBase();
            string tiName = null;
            string desc = null;

            // Act
            testBase.PerformLoadOldTI(
                tiName,
                out desc);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void PerformLoadTIBySearchingTIName_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            string tiName = null;

            // Act
            testBase.PerformLoadTIBySearchingTIName(
                tiName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void SaveAsNewTI_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            string tiName = null;
            bool isInputDescription = false;
            string desc = null;

            // Act
            testBase.SaveAsNewTI(
                tiName,
                isInputDescription,
                desc);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ChangeGroupAndSaveAsNewTI_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            string tiName = null;
            string group = null;

            // Act
            testBase.ChangeGroupAndSaveAsNewTI(
                tiName,
                group);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void VariableTabNavi_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            VariableTabType tabType = default(global::PP5AutoUITests.VariableTabType);

            // Act
            testBase.VariableTabNavi(
                tabType);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void TestItemTabNavi_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            TestItemTabType tabType = default(global::PP5AutoUITests.TestItemTabType);

            // Act
            testBase.TestItemTabNavi(
                tabType);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void TestItemListNavi_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            TestItemSourceType tiSType = default(global::PP5AutoUITests.TestItemSourceType);

            // Act
            testBase.TestItemListNavi(
                tiSType);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void TestProgramTestTypeNavi_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            TestItemRunType tiRunType = default(global::PP5AutoUITests.TestItemRunType);

            // Act
            testBase.TestProgramTestTypeNavi(
                tiRunType);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void TPExecuteAction_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            TPAction tpAction = default(global::PP5AutoUITests.TPAction);

            // Act
            testBase.TPExecuteAction(
                tpAction);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void TestProgramAllSettingPageNavi_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            TestProgramSettingTabType tpSettingType = default(global::PP5AutoUITests.TestProgramSettingTabType);

            // Act
            testBase.TestProgramAllSettingPageNavi(
                tpSettingType);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void TestProgramParamInfoNavi_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            TestProgramParameterTabType tiParamType = default(global::PP5AutoUITests.TestProgramParameterTabType);

            // Act
            testBase.TestProgramParamInfoNavi(
                tiParamType);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ReportFormatNavi_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            ReportFormatTabType rptFrmtType = default(global::PP5AutoUITests.ReportFormatTabType);

            // Act
            testBase.ReportFormatNavi(
                rptFrmtType);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void CreateNewVariable1_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            VariableTabType tabType = default(global::PP5AutoUITests.VariableTabType);
            string ShowName = null;
            string CallName = null;
            VariableDataType DataType = default(global::PP5AutoUITests.VariableDataType);
            VariableEditType EditType = default(global::PP5AutoUITests.VariableEditType);
            OrderedDictionary enumItems = null;
            int enumItemSelectionIndex = 0;

            // Act
            testBase.CreateNewVariable1(
                tabType,
                ShowName,
                CallName,
                DataType,
                EditType,
                enumItems,
                enumItemSelectionIndex);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void CreateNewVariable2_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            VariableTabType tabType = default(global::PP5AutoUITests.VariableTabType);
            string ShowName = null;
            string CallName = null;
            VariableDataType DataType = default(global::PP5AutoUITests.VariableDataType);

            // Act
            testBase.CreateNewVariable2(
                tabType,
                ShowName,
                CallName,
                DataType);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetCommandBy_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            string cmdName = null;
            string GroupNameToSearch = null;
            bool findExactSameCommand = false;

            // Act
            var result = testBase.GetCommandBy(
                cmdName,
                GroupNameToSearch,
                findExactSameCommand);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void SaveCommandMap_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            string cmdNameToSearch = null;
            string GroupNameToSearch = null;
            bool findExactSameCommand = false;

            // Act
            testBase.SaveCommandMap(
                cmdNameToSearch,
                GroupNameToSearch,
                findExactSameCommand);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetCellBy_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            string DataGridAutomationID = null;
            int rowNo = 0;
            string colName = null;

            // Act
            var result = testBase.GetCellBy(
                DataGridAutomationID,
                rowNo,
                colName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetColumnBy_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            DataTableAutoIDType DataGridType = default(global::PP5AutoUITests.DataTableAutoIDType);
            int colNo = 0;
            bool excludeLastRow = false;

            // Act
            var result = testBase.GetColumnBy(
                DataGridType,
                colNo,
                excludeLastRow);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetRowCellElementsBy_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            DataTableAutoIDType DataGridType = default(global::PP5AutoUITests.DataTableAutoIDType);
            int rowNo = 0;

            // Act
            var result = testBase.GetRowCellElementsBy(
                DataGridType,
                rowNo);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetCellValue_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            string DataGridAutomationID = null;
            int rowIdx = 0;
            string colName = null;

            // Act
            var result = testBase.GetCellValue(
                DataGridAutomationID,
                rowIdx,
                colName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetRowCount_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            DataTableAutoIDType DataGridType = default(global::PP5AutoUITests.DataTableAutoIDType);

            // Act
            var result = testBase.GetRowCount(
                DataGridType);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetRowCount_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var testBase = new TestBase();
            string DataGridAutomationID = null;

            // Act
            var result = testBase.GetRowCount(
                DataGridAutomationID);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetSingleColumnValues_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            DataTableAutoIDType DataGridType = default(global::PP5AutoUITests.DataTableAutoIDType);
            int colNo = 0;
            bool excludeLastRow = false;

            // Act
            var result = testBase.GetSingleColumnValues(
                DataGridType,
                colNo,
                excludeLastRow);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetSingleRowValues_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            DataTableAutoIDType DataGridType = default(global::PP5AutoUITests.DataTableAutoIDType);
            int rowNo = 0;

            // Act
            var result = testBase.GetSingleRowValues(
                DataGridType,
                rowNo);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void AddCommandBy_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            string groupName = null;
            int cmdNumber = 0;
            int addCount = 0;

            // Act
            testBase.AddCommandBy(
                groupName,
                cmdNumber,
                addCount);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void AddCommandBy_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var testBase = new TestBase();
            string groupName = null;
            string cmdName = null;
            int addCount = 0;

            // Act
            testBase.AddCommandBy(
                groupName,
                cmdName,
                addCount);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void AddCommandsBy_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            string groupName = null;
            string[] cmdNames = null;

            // Act
            testBase.AddCommandsBy(
                groupName,
                cmdNames);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void AddCommandsBy_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var testBase = new TestBase();
            string groupName = null;
            int[] cmdNumbers = null;

            // Act
            testBase.AddCommandsBy(
                groupName,
                cmdNumbers);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void AddCommandsBy_StateUnderTest_ExpectedBehavior2()
        {
            // Arrange
            var testBase = new TestBase();
            IWebElement groupTreeItem = null;
            int[] cmdNumbers = null;
            IWebElement cmdToAdd = null;

            // Act
            testBase.AddCommandsBy(
                groupTreeItem,
                cmdNumbers,
                out cmdToAdd);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void AddCommandsBy_StateUnderTest_ExpectedBehavior3()
        {
            // Arrange
            var testBase = new TestBase();
            IWebElement groupTreeItem = null;
            string[] cmdNames = null;
            IWebElement cmdToAdd = null;

            // Act
            testBase.AddCommandsBy(
                groupTreeItem,
                cmdNames,
                out cmdToAdd);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void AddCommandBy_StateUnderTest_ExpectedBehavior2()
        {
            // Arrange
            var testBase = new TestBase();
            IWebElement groupTreeItem = null;
            string cmdName = null;
            int addCount = 0;
            IWebElement cmdToAdd = null;

            // Act
            testBase.AddCommandBy(
                groupTreeItem,
                cmdName,
                addCount,
                out cmdToAdd);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void AddCommandBy_StateUnderTest_ExpectedBehavior3()
        {
            // Arrange
            var testBase = new TestBase();
            IWebElement groupTreeItem = null;
            int cmdNumber = 0;
            int addCount = 0;
            IWebElement cmdToAdd = null;

            // Act
            testBase.AddCommandBy(
                groupTreeItem,
                cmdNumber,
                addCount,
                out cmdToAdd);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void AddCommands_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            ReadOnlyCollection<IWebElement> cmdTreeItems = null;
            int[] cmdNumbers = null;

            // Act
            testBase.AddCommands(
                cmdTreeItems,
                cmdNumbers);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindCommandBy_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            string groupName = null;
            string cmdName = null;

            // Act
            var result = testBase.GetCommandBy(
                groupName,
                cmdName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetCommandTreeByGroupName_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            string groupName = null;

            // Act
            var result = testBase.GetCommandTreeByGroupName(
                groupName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void CommandTreeViewScrollToTop_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            IWebElement commandTree = null;

            // Act
            testBase.CommandTreeViewScrollToTop(
                commandTree);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void SearchForCommandGroup_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            IWebElement commandTree = null;
            string groupName = null;
            IWebElement groupTreeItem = null;

            // Act
            testBase.SearchForCommandGroup(
                commandTree,
                groupName,
                out groupTreeItem);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ExpandCommandGroup_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            IWebElement commandTree = null;
            string groupName = null;
            IWebElement groupTreeItem = null;

            // Act
            testBase.ExpandCommandGroup(
                commandTree,
                groupName,
                out groupTreeItem);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void AddTIBy_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            string groupName = null;
            int tiIndex = 0;
            TestItemSourceType tiType = default(global::PP5AutoUITests.TestItemSourceType);
            int addCount = 0;

            // Act
            testBase.AddTIBy(
                groupName,
                tiIndex,
                tiType,
                addCount);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void AddTIBy_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var testBase = new TestBase();
            string groupName = null;
            int tiIndex = 0;
            int addCount = 0;

            // Act
            testBase.AddTIBy(
                groupName,
                tiIndex,
                addCount);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void SelectColorSettingItem_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            IWebElement ColorSettingPage = null;
            ColorSettingPageType csPageType = default(global::PP5AutoUITests.ColorSettingPageType);
            string groupName = null;
            int idx = 0;

            // Act
            testBase.SelectColorSettingItem(
                ColorSettingPage,
                csPageType,
                groupName,
                idx);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetComboBoxItems_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            string comboBoxID = null;
            ReadOnlyCollection<IWebElement> cmbItems = null;

            // Act
            testBase.GetComboBoxItems(
                comboBoxID,
                out cmbItems);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ComboBoxSelectByName_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            string comboBoxID = null;
            string name = null;
            bool supportKeyInputSearch = false;

            // Act
            testBase.ComboBoxSelectByName(
                comboBoxID,
                name);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ComboBoxSelectByIndex_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            string comboBoxID = null;
            int index = 0;
            bool supportKeyInputSearch = false;

            // Act
            testBase.ComboBoxSelectByIndex(
                comboBoxID,
                index,
                supportKeyInputSearch);

            // Assert
            Assert.Fail();
        }

        //[TestMethod]
        //public void GetComboBoxSelectionIndex_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var testBase = new TestBase();
        //    string comboBoxID = null;

        //    // Act
        //    var result = testBase.GetComboBoxSelectionIndex(
        //        comboBoxID);

        //    // Assert
        //    Assert.Fail();
        //}

        [TestMethod]
        public void GetDataTableElement_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            string DataGridAutomationID = null;

            // Act
            var result = testBase.GetDataTableElement(
                DataGridAutomationID);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetDataTableElement_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var testBase = new TestBase();
            DataTableAutoIDType DataGridType = default(global::PP5AutoUITests.DataTableAutoIDType);

            // Act
            var result = testBase.GetDataTableElement(
                DataGridType);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetCommandFileFullPath_StateUnderTest_ExpectedBehavior()
        {
            // Act
            var result = TestBase.GetCommandFileFullPath();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void SetColor_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var testBase = new TestBase();
            IWebElement colorTabItem = null;
            ColorSettingType colorSettingType = default(global::PP5AutoUITests.ColorSettingType);
            string colorCodeFont = null;

            // Act
            testBase.SetColor(
                colorTabItem,
                colorSettingType,
                colorCodeFont);

            // Assert
            Assert.Fail();
        }
    }
}
