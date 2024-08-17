using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace PP5AutoUITests
{
    [TestClass]
    public class UnitTest_TIEditor : TestBase
    {
        #region Combo Action Tests

        [TestInitialize]
        public void TIEditor_TestMethodSetup()
        {
            OpenNewTIEditorWindow();
        }

        [TestMethod]
        public void TestCopyAndPaste_SearchBoxFromTestCommandToTestEditCommand()
        {
            string testString = "testCopyAndPaste";

            IWebElement testCmdSearchTextBox = CurrentDriver.GetElement(By.ClassName("CmdTreeView"))
                                                            .GetElement(MobileBy.AccessibilityId("searchText"));

            IWebElement testCmdEditSearchTextBox = CurrentDriver.GetElement(By.ClassName("SettingAeraView"))
                                                                .GetElement(MobileBy.AccessibilityId("searchText"));

            // Clean up text in both search box
            testCmdSearchTextBox.ClearContent();
            testCmdEditSearchTextBox.ClearContent();

            // Input random text in testCmdSearchBox
            testCmdSearchTextBox.SendSingleKeys(testString);

            // Copy and paste text from command list searchbox to command edit searchbox
            testCmdSearchTextBox.CopyAndPaste(testCmdEditSearchTextBox);

            // Check input text same as expected
            testString.ShouldEqualTo(testCmdEditSearchTextBox.Text);
        }

        [TestMethod]
        public void TestCutAndPaste_SearchBoxFromTestCommandToTestEditCommand()
        {
            string testString = "testCutAndPaste";

            IWebElement testCmdSearchTextBox = CurrentDriver.GetElement(By.ClassName("CmdTreeView"))
                                                            .GetElement(MobileBy.AccessibilityId("searchText"));

            IWebElement testCmdEditSearchTextBox = CurrentDriver.GetElement(By.ClassName("SettingAeraView"))
                                                                .GetElement(MobileBy.AccessibilityId("searchText"));

            // Clean up text in both search box
            testCmdSearchTextBox.ClearContent();
            testCmdEditSearchTextBox.ClearContent();

            // Input random text in testCmdSearchBox
            testCmdSearchTextBox.SendSingleKeys(testString);

            // Cut and paste text from command list searchbox to command edit searchbox
            testCmdSearchTextBox.CutAndPaste(testCmdEditSearchTextBox);

            // Check input text same as expected
            testString.ShouldEqualTo(testCmdEditSearchTextBox.Text);
        }

        [TestMethod]
        [TestCategory("ByCommandName")]
        public void TestAddCommand_CommandVisibleInList_CheckSameCommandABS()
        {
            // Switch to test item context window
            TestItemTabNavi(TestItemTabType.TIContext);

            // Input Command name: "ABS", first command in Group: "Arithmetic"
            string CommandName = "ABS";
            string GroupName = "Arithmetic";

            // Add the command
            AddCommandBy(GroupName, CommandName);

            // Check command name the same
            CommandName.ShouldEqualTo(GetCellValue("PGGrid", 0, "Test Command"));
        }

        [TestMethod]
        [TestCategory("ByCommandName")]
        public void TestAddCommand_CommandNotVisibleInList_CheckSameCommandXOR()
        {
            // Switch to test item context window
            TestItemTabNavi(TestItemTabType.TIContext);

            // Input Command name: "XOR", last command in Group: "Arithmetic"
            string CommandName = "XOR";
            string GroupName = "Arithmetic";

            // Add the command
            AddCommandBy(GroupName, CommandName);

            // Check command name the same
            CommandName.ShouldEqualTo(GetCellValue("PGGrid", 0, "Test Command"));
        }

        [TestMethod]
        [TestCategory("ByCommandNumber")]
        [DataRow("ABS")]
        public void TestAddCommand_CommandVisibleInList_CheckSameCommandABSInNumber1(string commandName)
        {
            // Switch to test item context window
            TestItemTabNavi(TestItemTabType.TIContext);

            // Input Command name: "ABS", first command in Group: "Arithmetic"
            int CommandNumber = 1;
            string GroupName = "Arithmetic";

            // Add the command
            AddCommandBy(GroupName, CommandNumber);

            // Check command name the same
            commandName.ShouldEqualTo(GetCellValue("PGGrid", 0, "Test Command"));
        }

        [TestMethod]
        [TestCategory("ByCommandNumber")]
        [DataRow("XOR")]
        public void TestAddCommand_CommandNotVisibleInList_CheckSameCommandXORInNumber25(string commandName)
        {
            // Switch to test item context window
            TestItemTabNavi(TestItemTabType.TIContext);

            // Input Command name: "XOR", last command in Group: "Arithmetic"
            int CommandNumber = 25;
            string GroupName = "Arithmetic";

            // Add the command
            AddCommandBy(GroupName, CommandNumber);

            // Check command name the same
            commandName.ShouldEqualTo(GetCellValue("PGGrid", 0, "Test Command"));
        }

        [TestMethod]
        [TestCategory("ByCommandNumber")]
        public void TestAddCommand_GroupNotVisibleInList_CheckSameCommandABSInNumber1()
        {
            // Switch to test item context window
            TestItemTabNavi(TestItemTabType.TIContext);

            // Input Command name: "GetTDL_Information", first command in Group: "Data Logger"
            // Where group is not visible in command list
            int CommandNumber = 1;
            string GroupName = "Data Logger";

            // Add the command
            AddCommandBy(GroupName, CommandNumber);

            // Check command name the same
            "GetTDL_Information".ShouldEqualTo(GetCellValue("PGGrid", 0, "Test Command"));
        }

        [TestMethod]
        [TestCategory("ByCommandName")]
        public void TestAddCommand_NotInListByCommandName_ShouldReturnCommandNameNotExistedException()
        {
            //// Arrange
            // Input Command name that is not in Group: "Arithmetic"
            string CommandName = "XXXXXXXXXX";
            string GroupName = "Arithmetic";

            //// Act + Assert
            // Add the command, check exception message
            Exception exp = Assert.ThrowsException<CommandNameNotExistedException>(() => AddCommandBy(GroupName, CommandName));
            CommandName.ShouldEqualTo(exp.Message);
        }

        [TestMethod]
        [TestCategory("ByCommandNumber")]
        public void TestAddCommand_NotInListByCommandNumber_ShouldReturnCommandNumberNotExistedException()
        {
            //// Arrange
            // Input Command number that is not in Group: "Arithmetic"
            int CommandNumber = 26;
            string GroupName = "Arithmetic";

            //// Act + Assert
            // Add the command, check exception message
            Exception exp = Assert.ThrowsException<CommandNumberNotExistedException>(() => AddCommandBy(GroupName, CommandNumber));
            CommandNumber.ToString().ShouldEqualTo(exp.Message);
        }

        [TestMethod]
        [TestCategory("ByCommandName")]
        public void TestAddCommands_HasCommandNameNotInList_ShouldReturnCommandNameNotExistedException()
        {
            //// Arrange
            // Input Command name that is not in Group: "Arithmetic"
            string WrongCommandName1 = "XXXXXXXXXX";
            string WrongCommandName2 = "YYYYYYYYYY";
            string GroupName = "Arithmetic";

            //// Act + Assert
            // Add the commands, check exception message
            Exception exp = Assert.ThrowsException<CommandNameNotExistedException>(() => AddCommandsBy(GroupName, WrongCommandName1, WrongCommandName2, "MOD"));
            WrongCommandName1.ShouldEqualTo(exp.Message);
        }

        [TestMethod]
        [TestCategory("ByCommandNumber")]
        public void TestAddCommands_WrongCommandNumber0_ShouldReturnCommandNumberNotExistedException()
        {
            //// Arrange
            // The group name
            string GroupName = "Arithmetic";

            //// Act + Assert
            // Add the commands with No: 0, 1, 10
            // Command number: 0 is not in Group: "Arithmetic"
            Exception exp = Assert.ThrowsException<CommandNumberNotExistedException>(() => AddCommandsBy(GroupName, 0, 1, 10));

            // check exception message
            "0".ShouldEqualTo(exp.Message);
        }

        [TestMethod]
        [TestCategory("ByCommandNumber")]
        public void TestAddCommands_WrongCommandNumber26_ShouldReturnCommandNumberNotExistedException()
        {
            //// Arrange
            // The group name
            string GroupName = "Arithmetic";

            //// Act + Assert
            // Add the commands with No: 1, 10, 26
            // Command number: 26 is not in Group: "Arithmetic"
            Exception exp = Assert.ThrowsException<CommandNumberNotExistedException>(() => AddCommandsBy(GroupName, 1, 10, 26));

            // check exception message
            "26".ShouldEqualTo(exp.Message);
        }

        [TestMethod]
        [TestCategory("ByCommandName")]
        public void TestAddCommand_GroupNameNotInList_ShouldReturnGroupNameNotExistedException()
        {
            //// Arrange
            // The group name not existed
            string GroupName = "xxxxxxxxxx";
            string CommandName = "ABS";

            //// Act + Assert
            // Add the command, check exception message
            Exception exp = Assert.ThrowsException<GroupNameNotExistedException>(() => AddCommandBy(GroupName, CommandName));
            GroupName.ShouldEqualTo(exp.Message);
        }

        [TestMethod]
        [TestCategory("ByCommandNumber")]
        public void TestAddCommand2_GroupNameNotInList_ShouldReturnGroupNameNotExistedException()
        {
            //// Arrange
            // The group name not existed
            string GroupName = "xxxxxxxxxx";
            int CommandNumber = 26;

            //// Act + Assert
            // Add the command, check exception message
            Exception exp = Assert.ThrowsException<GroupNameNotExistedException>(() => AddCommandBy(GroupName, CommandNumber));
            GroupName.ShouldEqualTo(exp.Message);
        }

        [TestMethod]
        [TestCategory("ByCommandName")]
        public void TestAddCommands_Add3CommandsByCommandName_ShouldAddToCommandEditPage()
        {
            // Switch to test item context window
            TestItemTabNavi(TestItemTabType.TIContext);

            // Command Group
            string GroupName = "AC Source";

            // Add the command with command names: "ABS", "MOD", "XOR"
            // { "AC Source",  new[] { "ReadAC_Current", "ReadAC_Frequency" } },
            AddCommandsBy(GroupName, "ReadAC_Current", "ReadAC_Frequency", "SetACDev_CPPHParameter2");

            // Check commands are correctly added
            CollectionAssert.AreEqual(new List<string> { "ReadAC_Current", "ReadAC_Frequency", "SetACDev_CPPHParameter2" }, GetSingleColumnValues(DataTableAutoIDType.PGGrid, 5/*Test Command*/));
        }

        [TestMethod]
        [TestCategory("ByCommandNumber")]
        public void TestAddCommands_Add3CommandsByCommandNumber_ShouldAddToCommandEditPage()
        {
            // Switch to test item context window
            TestItemTabNavi(TestItemTabType.TIContext);

            // Command Group
            string GroupName = "Arithmetic";

            // Add the commands with indexces: 1, 10, 25 (ABS, LOG, XOR)
            AddCommandsBy(GroupName, 1, 10, 25);

            // Check commands are correctly added
            CollectionAssert.AreEqual(new List<string>{ "ABS", "LOG", "XOR" }, GetSingleColumnValues(DataTableAutoIDType.PGGrid, 5/*Test Command*/));
        }

        #endregion
    }
}
