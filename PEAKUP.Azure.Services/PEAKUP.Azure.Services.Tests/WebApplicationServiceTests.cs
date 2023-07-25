using Microsoft.Azure.Management.AppService.Fluent;
using PEAKUP.Azure.Services.Entity;
using PEAKUP.Azure.Services.Services;
using System.Configuration;

namespace PEAKUP.Azure.Services.Tests
{
    [TestClass]
    public class WebApplicationServiceTests
    {
        public ConfigurationServiceLoader configurationServiceLoader { get; set; }

        // Create AzureApplicationCredentials object
        AzureApplicationCredentials Credentials = new AzureApplicationCredentials() { };

        WebAppCredentials Resources = new WebAppCredentials() { };

        public WebApplicationServiceTests()
        {
            configurationServiceLoader = new ConfigurationServiceLoader();

            Credentials = configurationServiceLoader.GETCredentials();

            Resources = configurationServiceLoader.GETWebAppCredentials();
        }

        [TestMethod]
        public void TestWebApplicationService_GetWebApp()
        {
            // Arrange
            var webAppServiceScaler = new WebApplicationService(Credentials, Resources);

            // Act
            webAppServiceScaler.CreateAzureClient();
            IWebApp webApp = webAppServiceScaler.GetWebApp();

            // Assert
            Assert.IsNotNull(webApp, "Web App instance should not be null.");
            Assert.AreEqual("running", webApp.State.ToLower(), "Web App should be in the running state.");
        }

        [TestMethod]
        public void TestWebApplicationService_Scale()
        {
            // Arrange
            var webAppServiceScaler = new WebApplicationService(Credentials, Resources);
            webAppServiceScaler.CreateAzureClient();
            IAppServicePlan appServicePlan = webAppServiceScaler.Scale(PricingTier.StandardS1);

            // Act (Optional: Perform additional actions if needed before assertion)

            // Assert
            Assert.IsNotNull(appServicePlan, "App Service Plan instance should not be null.");
            // Add more assertions here if needed based on the scaling logic.
        }
    }
}
