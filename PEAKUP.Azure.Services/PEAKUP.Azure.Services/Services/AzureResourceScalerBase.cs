using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using PEAKUP.Azure.Services.Entity;

namespace PEAKUP.Azure.Services.Services
{
    // Class: AzureResourceScalerBase
    /// <summary>
    /// Base class for Azure resource scaling operations. This class provides methods for creating an Azure client
    /// using application credentials and initializes the Azure client for further resource scaling operations.
    /// </summary>
    public class AzureResourceScalerBase
    {
        // Property: Credentials
        /// <summary>
        /// Gets or sets the Azure application credentials used for authentication with Azure services.
        /// </summary>
        public AzureApplicationCredentials Credentials { get; set; }

        // Property: AzureClient
        /// <summary>
        /// Gets or sets the Azure client instance, which allows interaction with Azure resources.
        /// </summary>
        public IAzure AzureClient { get; set; }

        // Constructor: AzureResourceScalerBase
        /// <summary>
        /// Initializes a new instance of the AzureResourceScalerBase class with the provided Azure application credentials.
        /// </summary>
        /// <param name="credentials">The Azure application credentials for authentication.</param>
        public AzureResourceScalerBase(AzureApplicationCredentials credentials)
        {
            Credentials = credentials;
        }

        // Method: CreateAzureClient
        /// <summary>
        /// Creates and configures an Azure client using the provided application credentials for authentication.
        /// </summary>
        /// <returns>The initialized IAzure client instance.</returns>
        public IAzure CreateAzureClient()
        {
            // Create Azure credentials from the provided application credentials
            var credentials = new AzureApplicationCredentials()
            {
                ClientId = Credentials.ClientId,
                ClientSecret = Credentials.ClientSecret,
                TenantId = Credentials.TenantId,
                SubscriptionId = Credentials.SubscriptionId
            };

            // Create a service principal credential using AzureCredentialsFactory
            var servicePrincipalCredential = SdkContext.AzureCredentialsFactory.FromServicePrincipal(credentials.ClientId, credentials.ClientSecret, credentials.TenantId, Microsoft.Azure.Management.ResourceManager.Fluent.AzureEnvironment.AzureGlobalCloud);

            // Configure and authenticate the Azure client using the service principal credentials
            AzureClient = Microsoft.Azure.Management.Fluent.Azure.Configure()
                 .Authenticate(servicePrincipalCredential)
                 .WithSubscription(credentials.SubscriptionId);

            // Return the initialized Azure client
            return AzureClient;
        }
    }
}
