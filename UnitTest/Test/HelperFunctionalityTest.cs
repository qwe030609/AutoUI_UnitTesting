using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using DisableDevice;
using Chroma.UnitTest.Common;
using Newtonsoft.Json.Linq;

namespace PP5AutoUITests.Test
{
    [TestClass]
    public class HelperFunctionalityTest
    {
        #region Dll Helper

        //[AssemblyInitialize]
        //public static void BeforeClass(TestContext tc)
        //{

        //}

        //[TestInitialize]
        //public void TestMethodSetup()
        //{

        //}

        //[AssemblyCleanup]
        //public static void AfterClass()
        //{

        //}

        [TestMethod("Test Enable Mouse usage")]
        public void TestEnableMouseControl()
        {
            DllHelper.EnableMouse(true);   // Enable mouse usage
        }

        [TestMethod("Test Disable Mouse usage")]
        public void TestDisableMouseControl()
        {
            DllHelper.EnableMouse(false);   // Disable mouse usage
        }

        [TestMethod("Test Disable Memory Monitor Window of PP5")]
        public void DisableMemoryMonitorWindow()
        {
            DllHelper.DisableMemoryMonitorWindow();   // Disable Memory Monitor that shows memory warning window before starting the driver
        }

        [TestMethod("Test UpdateProperty in Json config file, for this case use:\n" +
                    "\rfilePath: \"SystemSetup.ssx\"\n" +
                    "\rnodePath: \"Datas\"\n" +
                    "\rpropertyName: \"IsStopOnFail\"\n" +
                    "\rnewValue: false")]
        public void JsonUpdateProperty()
        {
            // Update property "IsStopOnFail" with itemValue: false
            bool isJsonPropertyUpdated = FileProcessingExtension.JsonUpdateProperty(filePath: $"{PowerPro5Config.ReleaseDataFolder}//SystemSetup.ssx", 
                                                                                    nodePath: "Datas",
                                                                                    propertyName: "IsStopOnFail", 
                                                                                    newValue: false);

            // Check file has updated with the new property value
            Assert.IsTrue(isJsonPropertyUpdated);
        }

        [TestMethod("Test JsonCreateNewNodeInList in Json config file, for this case use:\n" +
                    "\rInputs:\n" + 
                    "\r\rfilePath: \"SystemCommand.csx\"\n" +
                    "\r\rnodePath: \"CommandGroupInfos\"\n" +
                    "\r\rpropertyName: \"IsDevice\"\n" +
                    "\rOutput:\n"+
                    "\r\rbool propertyValues")]
        public void JsonCreateNewNodeInList()
        {
            // Arrange
            string expectedCmdName = "testCmd1";
            string propertyNameToUpdate = "CommandName";
            string nodepathToUpdate = $"CommandGroupInfos[0]/Commands[0>-1]@{propertyNameToUpdate}={expectedCmdName}";
            string nodepathToVerify = "CommandGroupInfos[0]/Commands";
            string nodepathToDelete = "CommandGroupInfos[0]/Commands[-1]";
            string commandFilePath = TestBase.GetCommandFileFullPath();

            // Create a new node that cloned from the first command: "ABS" in the first group and update the property "CommandName"
            bool isJsonCreateNewNodeSuccess = FileProcessingExtension.JsonCreateNewNodeInList(filePath: commandFilePath,
                                                                                              nodePath: nodepathToUpdate);

            // Check the new node created and the property value is updated
            Assert.IsTrue(isJsonCreateNewNodeSuccess);

            FileProcessingExtension.JsonCountNodesInList(filePath: commandFilePath,
                                                         nodePath: nodepathToVerify,
                                                         out int nodeCount);

            nodepathToVerify = nodepathToVerify + $"[{nodeCount - 1}]@{propertyNameToUpdate}";
            FileProcessingExtension.JsonGetProperty(filePath: commandFilePath,
                                                    nodePath: nodepathToVerify,
                                                    out string actualCmdName);
            expectedCmdName.ShouldEqualTo(actualCmdName);

            // Delete the node
            FileProcessingExtension.JsonDeleteNode(filePath: commandFilePath,
                                                   nodePath: nodepathToDelete);
        }

        [TestMethod]
        public void TestCreateCleanNode()
        {
            string json = @"{""CommandGroupInfos"":[{""Commands"":[]}]}";
            string filePath = "test.json";
            File.WriteAllText(filePath, json);
            
            bool result = FileProcessingExtension.JsonCreateNewNodeInList(filePath, "CommandGroupInfos[0]/Commands[>-1]@CommandName=NewCmd,GroupID=300,IsDevice=true");

            Assert.IsTrue(result);
            string updatedJson = File.ReadAllText(filePath);
            JObject updatedObj = JObject.Parse(updatedJson);
            Assert.AreEqual("NewCmd", updatedObj["CommandGroupInfos"][0]["Commands"][0]["CommandName"].ToString());
            Assert.AreEqual("300", updatedObj["CommandGroupInfos"][0]["Commands"][0]["GroupID"].ToString());
            Assert.AreEqual("true", updatedObj["CommandGroupInfos"][0]["Commands"][0]["IsDevice"].ToString());
        }

        [TestMethod]
        public void TestCreateCleanNode_CheckNoSubNodeCount()
        {
            string json = @"{""CommandGroupInfos"":[{""Commands"":[{""SubNode"":[{""SubNodeProp1"":""Value""}]}]}]}";
            string filePath = "test.json";
            File.WriteAllText(filePath, json);

            bool result = FileProcessingExtension.JsonCreateNewNodeInList(filePath, "CommandGroupInfos[0]/Commands[>-1]@CommandName=NewCmd,GroupID=300,IsDevice=true");

            Assert.IsTrue(result);
            string updatedJson = File.ReadAllText(filePath);
            JObject updatedObj = JObject.Parse(updatedJson);
            Assert.AreEqual(0, updatedObj["CommandGroupInfos"][0]["Commands"][1]["SubNode"].Count());
        }

        [TestMethod]
        public void TestCreateCleanNode_CheckNoSubNodeIsCleared()
        {
            string json = @"{""CommandGroupInfos"":[{""Commands"":[{""SubNodeProp1"":""Value""}]}]}";
            string filePath = "test.json";
            File.WriteAllText(filePath, json);

            bool result = FileProcessingExtension.JsonCreateNewNodeInList(filePath, "CommandGroupInfos[0]/Commands[>-1]@CommandName=NewCmd,GroupID=300,IsDevice=true");

            Assert.IsTrue(result);
            string updatedJson = File.ReadAllText(filePath);
            JObject updatedObj = JObject.Parse(updatedJson);
            Assert.AreEqual(null, (updatedObj["CommandGroupInfos"][0]["Commands"][1]["SubNodeProp1"] as JValue).Value);
        }

        [TestMethod]
        public void TestCreateCleanNode_UpdateSubNodePropFromNullToInteger()
        {
            string json = @"{""CommandGroupInfos"":[{""Commands"":[{""SubNodeProp1"":null}]}]}";
            string filePath = "test.json";
            File.WriteAllText(filePath, json);

            bool result = FileProcessingExtension.JsonCreateNewNodeInList(filePath, $"CommandGroupInfos[0]/Commands[>-1]@SubNodeProp1=10");

            Assert.IsTrue(result);
            string updatedJson = File.ReadAllText(filePath);
            JObject updatedObj = JObject.Parse(updatedJson);
            Assert.AreEqual(10L, (updatedObj["CommandGroupInfos"][0]["Commands"][1]["SubNodeProp1"] as JValue).Value);
        }

        [TestMethod]
        public void TestCreateCleanNode_UpdateSubNodePropFromNullToBoolean()
        {
            string json = @"{""CommandGroupInfos"":[{""Commands"":[{""SubNodeProp1"":null}]}]}";
            string filePath = "test.json";
            File.WriteAllText(filePath, json);

            bool result = FileProcessingExtension.JsonCreateNewNodeInList(filePath, $"CommandGroupInfos[0]/Commands[>-1]@SubNodeProp1=true");

            Assert.IsTrue(result);
            string updatedJson = File.ReadAllText(filePath);
            JObject updatedObj = JObject.Parse(updatedJson);
            Assert.AreEqual(true, (updatedObj["CommandGroupInfos"][0]["Commands"][1]["SubNodeProp1"] as JValue).Value);
        }

        [TestMethod]
        [DataRow("true")]    // Boolean type
        [DataRow("10")]      // Integer type
        [DataRow("10.1")]    // Float type
        public void TestCreateCleanNode_UpdateSubNodePropToSimpleValueType(string SimpleTypeValue)
        {
            string json = @"{""CommandGroupInfos"":[{""Commands"":[{""SubNodeProp1"":null}]}]}";
            string filePath = "test.json";
            File.WriteAllText(filePath, json);

            bool result = FileProcessingExtension.JsonCreateNewNodeInList(filePath, $"CommandGroupInfos[0]/Commands[>-1]@SubNodeProp1={SimpleTypeValue}");

            Assert.IsTrue(result);
            string updatedJson = File.ReadAllText(filePath);
            JObject updatedObj = JObject.Parse(updatedJson);
            object SimpleTypeValueExpected = SimpleTypeValue.ParseStringToObjectOfType();
            Assert.AreEqual(SimpleTypeValueExpected, (updatedObj["CommandGroupInfos"][0]["Commands"][1]["SubNodeProp1"] as JValue).Value);
        }

        [TestMethod]
        public void TestCloneAndCreateNode()
        {
            string json = @"{""CommandGroupInfos"":[{""Commands"":[{""ExistingProp"":""Value""}]}]}";
            string filePath = "test.json";
            File.WriteAllText(filePath, json);

            bool result = FileProcessingExtension.JsonCreateNewNodeInList(filePath, "CommandGroupInfos[0]/Commands[0>-1]@CommandName=ClonedCmd,GroupID=301");

            Assert.IsTrue(result);
            string updatedJson = File.ReadAllText(filePath);
            JObject updatedObj = JObject.Parse(updatedJson);
            Assert.AreEqual("ClonedCmd", updatedObj["CommandGroupInfos"][0]["Commands"][1]["CommandName"].ToString());
            Assert.AreEqual("301", updatedObj["CommandGroupInfos"][0]["Commands"][1]["GroupID"].ToString());
            Assert.AreEqual("Value", updatedObj["CommandGroupInfos"][0]["Commands"][1]["ExistingProp"].ToString());
        }

        [TestMethod]
        public void TestNonExistentPath()
        {
            string json = @"{""CommandGroupInfos"":[]}";
            string filePath = "test.json";
            File.WriteAllText(filePath, json);

            bool result = FileProcessingExtension.JsonCreateNewNodeInList(filePath, "NonExistentPath[0]/Commands[>-1]@CommandName=NewCmd");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestInsertAtSpecificIndex()
        {
            string json = @"{""CommandGroupInfos"":[{""Commands"":[{""Cmd1"":""Value1""},{""Cmd2"":""Value2""}]}]}";
            string filePath = "test.json";
            File.WriteAllText(filePath, json);

            bool result = FileProcessingExtension.JsonCreateNewNodeInList(filePath, "CommandGroupInfos[0]/Commands[>1]@CommandName=InsertedCmd");

            Assert.IsTrue(result);
            string updatedJson = File.ReadAllText(filePath);
            JObject updatedObj = JObject.Parse(updatedJson);
            Assert.AreEqual("InsertedCmd", updatedObj["CommandGroupInfos"][0]["Commands"][1]["CommandName"].ToString());
            Assert.AreEqual(3, updatedObj["CommandGroupInfos"][0]["Commands"].Count());
        }

        [TestMethod]
        public void TestUpdateExistingProperty()
        {
            string json = @"{""CommandGroupInfos"":[{""Commands"":[{""CommandName"":""OldCmd"",""GroupID"":100}]}]}";
            string filePath = "test.json";
            File.WriteAllText(filePath, json);

            bool result = FileProcessingExtension.JsonCreateNewNodeInList(filePath, "CommandGroupInfos[0]/Commands[0>-1]@CommandName=UpdatedCmd,GroupID=200");

            Assert.IsTrue(result);
            string updatedJson = File.ReadAllText(filePath);
            JObject updatedObj = JObject.Parse(updatedJson);
            Assert.AreEqual("UpdatedCmd", updatedObj["CommandGroupInfos"][0]["Commands"][1]["CommandName"].ToString());
            Assert.AreEqual(200L, (updatedObj["CommandGroupInfos"][0]["Commands"][1]["GroupID"] as JValue).Value);
        }

        [TestMethod("Test JsonDeleteNode in Json config file, for this case use:\n" +
            "\rInputs:\n" +
            "\r\rfilePath: \"SystemCommand.csx\"\n" +
            "\r\rnodePath: \"CommandGroupInfos\"\n" +
            "\r\rpropertyName: \"IsDevice\"\n" +
            "\rOutput:\n" +
            "\r\rbool propertyValues")]
        public void JsonDeleteNode()
        {
            // Arrange
            string cmdName = "testCmd1";
            string propertyNameToUpdate = "CommandName";
            string nodepathToUpdate = $"CommandGroupInfos[0]/Commands[0>-1]@{propertyNameToUpdate}={cmdName}";
            string nodepathToCount = "CommandGroupInfos[0]/Commands";
            string nodepathToDelete = "CommandGroupInfos[0]/Commands[-1]";
            string commandFilePath = TestBase.GetCommandFileFullPath();

            // Create a new node that cloned from the first command: "ABS" in the first group and update the property "CommandName"
            bool isJsonCreateNewNodeSuccess = FileProcessingExtension.JsonCreateNewNodeInList(filePath: commandFilePath,
                                                                                              nodePath: nodepathToUpdate);

            // Check the new node created and the property value is updated
            Assert.IsTrue(isJsonCreateNewNodeSuccess);

            FileProcessingExtension.JsonCountNodesInList(filePath: commandFilePath,
                                                         nodePath: nodepathToCount,
                                                         out int nodeCountExpected);

            FileProcessingExtension.JsonDeleteNode(filePath: commandFilePath,
                                                    nodePath: nodepathToDelete);

            FileProcessingExtension.JsonCountNodesInList(filePath: commandFilePath,
                                                         nodePath: nodepathToCount,
                                                         out int nodeCountActual);

            nodeCountExpected.ShouldEqualTo(nodeCountActual + 1);
        }

        [TestMethod("Test GetProperty in Json config file, for this case use:\n" +
                    "\rInputs:\n" +
                    "\r\rfilePath: \"SystemCommand.csx\"\n" +
                    "\r\rnodePath: \"CommandGroupInfos\"\n" +
                    "\r\rpropertyName: \"IsDevice\"\n" +
                    "\rOutput:\n" +
                    "\r\rbool propertyValues")]
        public void JsonGetPropertyValues_singleNode()
        {
            // Get the value of property "IsDevice"
            bool isJsonPropertyValuesFetched = FileProcessingExtension.JsonGetProperty(filePath: $"{PowerPro5Config.ReleaseDataFolder}//SystemCommand.csx",
                                                                                      nodePath: "CommandGroupInfos@IsDevice",
                                                                                      out List<bool> propertyValues);

            // Check propertyValue is bool with "IsDevice" property
            Assert.IsTrue(isJsonPropertyValuesFetched);
            Assert.IsTrue(propertyValues.Count != 0);
        }

        [TestMethod("Test GetProperty in Json config file, for this case use:\n" +
                    "\rInputs:\n" +
                    "\r\rfilePath: \"SystemCommand.csx\"\n" +
                    "\r\rnodePath: \"CommandGroupInfos[0]/Commands[0]\"\n" +
                    "\r\rpropertyName: \"IsDevice\"\n" +
                    "\rOutput:\n" +
                    "\r\rbool propertyValues")]
        public void JsonGetPropertyValues_multiNodes()
        {
            // Get the value of property "IsDevice"
            bool isJsonPropertyValuesFetched = FileProcessingExtension.JsonGetProperty(filePath: $"{PowerPro5Config.ReleaseDataFolder}//SystemCommand.csx",
                                                                                      nodePath: "CommandGroupInfos[0]/Commands[0]@IsDevice",
                                                                                      out List<bool> propertyValues);

            // Check propertyValue is bool with "IsDevice" property
            Assert.IsTrue(isJsonPropertyValuesFetched);
            Assert.IsTrue(propertyValues.Count == 1);
        }

        [TestMethod("Test GetProperty in Json config file, for this case use:\n" +
                    "\rInputs:\n" +
                    "\r\rfilePath: \"SystemCommand.csx\"\n" +
                    "\r\rnodePath: \"CommandGroupInfos\"\n" +
                    "\r\rpropertyName: \"IsDevice\"\n" +
                    "\rOutput:\n" +
                    "\r\rbool propertyValue")]
        public void JsonGetPropertyValue_singleNode()
        {
            // Get the value of property "IsDevice"
            bool isJsonPropertyValuesFetched = FileProcessingExtension.JsonGetProperty(filePath: $"{PowerPro5Config.ReleaseDataFolder}//SystemCommand.csx",
                                                                                      nodePath: "CommandGroupInfos[0]@IsDevice",
                                                                                      //propertyName: "IsDevice",
                                                                                      out object propertyValue);

            // Check propertyValue is bool with "IsDevice" property
            Assert.IsTrue(isJsonPropertyValuesFetched);
            Assert.IsInstanceOfType(propertyValue, typeof(bool));
        }

        [TestMethod("Test GetProperty in Json config file, for this case use:\n" +
                    "\rInputs:\n" +
                    "\r\rfilePath: \"SystemCommand.csx\"\n" +
                    "\r\rnodePath: \"CommandGroupInfos[0]/Commands[0]\"\n" +
                    "\r\rpropertyName: \"IsDevice\"\n" +
                    "\rOutput:\n" +
                    "\r\rbool propertyValue")]
        public void JsonGetPropertyValue_multiNodes()
        {
            // Get the value of property "IsDevice"
            bool isJsonPropertyValuesFetched = FileProcessingExtension.JsonGetProperty(filePath: $"{PowerPro5Config.ReleaseDataFolder}//SystemCommand.csx",
                                                                                      nodePath: "CommandGroupInfos[0]/Commands[0]@IsDevice",
                                                                                      out object propertyValue);

            // Check propertyValue is bool with "IsDevice" property
            Assert.IsTrue(isJsonPropertyValuesFetched);
            Assert.IsInstanceOfType(propertyValue, typeof(bool));
        }

        #endregion
    }
}
