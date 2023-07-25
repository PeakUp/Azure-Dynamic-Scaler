using Microsoft.Azure.Management.AppService.Fluent;
using PEAKUP.Azure.Services.Entity;

namespace PEAKUP.Azure.Services.Services
{
    // Class: WebApplicationService
    /// <summary>
    /// This class represents a service for scaling Azure Web Apps. It inherits from the AzureResourceScalerBase
    /// class and provides methods for getting the Web App, checking its state, and scaling the App Service Plan
    /// to the desired pricing tier and instance count.
    /// </summary>
    public class WebApplicationService : AzureResourceScalerBase
    {
        // Property: Resources
        /// <summary>
        /// Gets or sets the Web App credentials and resource information required for scaling the Azure Web App.
        /// </summary>
        public WebAppCredentials Resources { get; set; }

        // Property: WebApp
        /// <summary>
        /// Gets or sets the Azure Web App instance representing the target Web App for scaling.
        /// </summary>
        public IWebApp WebApp { get; set; }

        // Property: AppServicePlan
        /// <summary>
        /// Gets or sets the Azure App Service Plan instance representing the target App Service Plan for scaling.
        /// </summary>
        public IAppServicePlan AppServicePlan { get; set; }

        // Constructor: WebApplicationService
        /// <summary>
        /// Initializes a new instance of the WebApplicationService class with the provided Azure application credentials
        /// and Web App credentials.
        /// </summary>
        /// <param name="credentials">The Azure application credentials for authentication.</param>
        /// <param name="resources">The Web App credentials and resource information required for scaling.</param>
        public WebApplicationService(AzureApplicationCredentials credentials, WebAppCredentials resources) : base(credentials)
        {
            Resources = resources;
        }

        // Method: GetWebApp
        /// <summary>
        /// Retrieves the Azure Web App instance based on the provided Web App name and resource group name.
        /// If the Web App is not in the running state, this method returns null.
        /// </summary>
        /// <returns>The Azure Web App instance if it exists and is running; otherwise, null.</returns>
        public IWebApp? GetWebApp()
        {
            // Get the Azure Web App instance using the provided Web App name and resource group name
            WebApp = AzureClient.WebApps.GetByResourceGroup(Resources.ResourceGroupName, Resources.WebAppName);

            // If the Web App is not running, return null
            if (WebApp.State.ToLower() != "running")
            {
                return null;
            }

            return WebApp;
        }

        // Method: Scale
        /// <summary>
        /// Scales the App Service Plan of the Azure Web App to the desired pricing tier and instance count.
        /// </summary>
        /// <param name="desiredPlan">The desired pricing tier for the App Service Plan.</param>
        /// <param name="instanceCount">The desired instance count for the App Service Plan. Default is 1.</param>
        /// <returns>The updated App Service Plan instance after scaling.</returns>
        public IAppServicePlan? Scale(PricingTier desiredPlan, int instanceCount = 1)
        {
            // Get the Azure App Service Plan instance using the provided resource group name and App Service Plan name
            AppServicePlan = AzureClient.AppServices.AppServicePlans.GetByResourceGroup(Resources.ResourceGroupName, Resources.WebAppServicePlanName);

            // If the App Service Plan exists, update it with the desired pricing tier and instance count
            if (AppServicePlan is not null)
            {
                AppServicePlan.Update().WithPricingTier(desiredPlan).WithCapacity(instanceCount).Apply();

                return AppServicePlan;
            }

            return null;
        }
    }
}
