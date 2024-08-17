using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using DisableDevice;

namespace PP5AutoUITests
{
    [TestClass]
    public class UnitTest_HelperFunctionality
    {
        #region Dll Helper

        [TestInitialize]
        public void TestMethodSetup()
        {
            
        }

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
            DisableMemoryMonitorWindow();   // Disable Memory Monitor that shows memory warning window before starting the driver
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

        [TestMethod("Test GetProperty in Json config file, for this case use:\n" +
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
            string nodepathToUpdate = $"CommandGroupInfos[0]/Commands[0]@{propertyNameToUpdate}={expectedCmdName}";
            string nodepathToVerify = "CommandGroupInfos[0]/Commands";
            
            // Create a new node that cloned from the first command: "ABS" in the first group and update the property "CommandName"
            bool isJsonCreateNewNodeSuccess = FileProcessingExtension.JsonCreateNewNodeInList(filePath: $"{PowerPro5Config.ReleaseDataFolder}//SystemCommand.csx",
                                                                                              nodePath: nodepathToUpdate);

            // Check the new node created and the property value is updated
            Assert.IsTrue(isJsonCreateNewNodeSuccess);

            FileProcessingExtension.JsonCountNodesInList(filePath: $"{PowerPro5Config.ReleaseDataFolder}//SystemCommand.csx",
                                                         nodePath: nodepathToVerify,
                                                         out int nodeCount);

            nodepathToVerify = nodepathToVerify + $"[{nodeCount - 1}]@{propertyNameToUpdate}";
            FileProcessingExtension.JsonGetProperty(filePath: $"{PowerPro5Config.ReleaseDataFolder}//SystemCommand.csx",
                                                    nodePath: nodepathToVerify,
                                                    out string actualCmdName);
            expectedCmdName.ShouldEqualTo(actualCmdName);
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
            Assert.IsTrue(propertyValues.Count != 0);
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
