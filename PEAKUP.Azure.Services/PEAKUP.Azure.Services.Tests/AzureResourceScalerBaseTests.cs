using Microsoft.Azure.Management.Fluent;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using PEAKUP.Azure.Services.Entity;
using PEAKUP.Azure.Services.Services;
using System.Configuration;

namespace PEAKUP.Azure.Services.Tests
{
    [TestClass]
    public class AzureResourceScalerBaseTests
    {
        public ConfigurationServiceLoader configurationServiceLoader { get; set; }

        // Create AzureApplicationCredentials object
        AzureApplicationCredentials Credentials = new AzureApplicationCredentials() { };

        public AzureResourceScalerBaseTests()
        {
            configurationServiceLoader = new ConfigurationServiceLoader();

            Credentials = configurationServiceLoader.GETCredentials();
        }

        [TestMethod]
        public void TestAzureResourceScalerBase_CreateAzureClient()
        {
            // Arrange
            var azureResourceScalerBase = new AzureResourceScalerBase(Credentials);

            // Act
            IAzure azureClient = azureResourceScalerBase.CreateAzureClient();

            // Assert
            Assert.IsNotNull(azureClient, "Azure client instance should not be null.");
            // Add more assertions here if needed based on the Azure client logic.
        }
    }
}