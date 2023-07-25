using Microsoft.Azure.Management.AppService.Fluent;
using PEAKUP.Azure.Services.Entity;
using PEAKUP.Azure.Services.Services;
using System.Configuration;

namespace PEAKUP.Azure.Services.Application
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var clientId = ConfigurationManager.AppSettings["ClientId"];
            var clientSecret = ConfigurationManager.AppSettings["ClientSecret"];
            var tenantId = ConfigurationManager.AppSettings["TenantId"];
            var subscriptionId = ConfigurationManager.AppSettings["SubscriptionId"];

            // Create AzureApplicationCredentials object
            var credentials = new AzureApplicationCredentials()
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
                TenantId = tenantId,
                SubscriptionId = subscriptionId
            };

            var webAppResourceGroupName = ConfigurationManager.AppSettings["WebAppResourceGroupName"];
            var webAppName = ConfigurationManager.AppSettings["WebAppName"];
            var webAppServicePlanName = ConfigurationManager.AppSettings["WebAppServicePlanName"];

            // Create WebAppCredentials object
            var resources = new WebAppCredentials()
            {
                ResourceGroupName = webAppResourceGroupName,
                WebAppName = webAppName,
                WebAppServicePlanName = webAppServicePlanName
            };

            var webAppServiceScaler = new WebApplicationService(credentials, resources);

            webAppServiceScaler.CreateAzureClient();

            webAppServiceScaler.GetWebApp();

            webAppServiceScaler.Scale(PricingTier.StandardS1);
        }
    }
}