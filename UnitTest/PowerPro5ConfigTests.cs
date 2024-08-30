using Microsoft.VisualStudio.TestTools.UnitTesting;
using PP5AutoUITests;

namespace PP5AutoUITests
{
    [TestClass]
    public class PowerPro5ConfigTests
    {
        [TestMethod]
        public void CheckReleaseDataPathUsingReleaseSubPathPattern()
        {
            // Arrange
            string releaseDataFolderExpected = PowerPro5Config.ReleaseDataFolder;
            
            // Act
            string releaseDataFolderActual = string.Format(PowerPro5Config.SubPathPattern, PowerPro5Config.ReleaseFolder, "Data");

            // Assert
            releaseDataFolderExpected.ShouldEqualTo(releaseDataFolderActual);
        }

        [TestMethod]
        public void CheckCommandFileFullPathUsingReleaseSubPathPattern()
        {
            // Arrange
            string commandFilePathExpected = $"{PowerPro5Config.ReleaseDataFolder}/SystemCommand.csx";

            // Act
            string commandFilePathActual = string.Format(PowerPro5Config.SubsubPathPattern, PowerPro5Config.ReleaseFolder, "Data", PowerPro5Config.SystemCommandFileName);

            // Assert
            commandFilePathExpected.ShouldEqualTo(commandFilePathActual);
        }
    }
}
