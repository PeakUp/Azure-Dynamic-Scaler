# PEAKUP.Azure.Services

PEAKUP.Azure.Services is a .NET Core project that provides a set of services for scaling Azure resources such as Web Apps and SQL databases. It leverages the Azure Management Fluent SDK to interact with Azure resources.

## Getting Started

1. **Create an Azure Application**
   - Go to the Azure portal (https://portal.azure.com).
   - Navigate to "Azure Active Directory" in the left-hand menu.
   - Click on "App registrations" and then click "New registration".
   - Provide a name for the application, select the supported account types, and specify the redirect URI (if required).

2. **Assign Access Control (IAM) Permissions**
   - For Web App Management:
     1. Navigate to the desired resource group that contains the target Web App.
     2. Click on "Access control (IAM)" in the left-hand menu.
     3. Click "Add a role assignment".
     4. In the "Role" dropdown, search for and select "Web Plan Contributor" and "Website Contributor".
     5. In the "Select" dropdown, search and select the application you created in Step 1.
     6. Click "Save" to assign the roles.

   - For SQL Server and Database Management:
     1. Navigate to the desired resource group that contains the target SQL Server and database.
     2. Click on "Access control (IAM)" in the left-hand menu.
     3. Click "Add a role assignment".
     4. In the "Role" dropdown, search for and select "SQL DB Contributor", "SQL Managed Instance Contributor", and "SQL Server Contributor".
     5. In the "Select" dropdown, search and select the application you created in Step 1.
     6. Click "Save" to assign the roles.

3. **Update Application Credentials in the Code**

   In the `Program.cs` file of the `PEAKUP.Azure.Services` project, update the following variables with the values obtained from Step 1:

   ```csharp
   var credentials = new AzureApplicationCredentials()
   {
       ClientId = "Your_Client_Id",        // Replace with the Application (client) ID
       ClientSecret = "Your_Client_Secret",    // Replace with the Client secret
       TenantId = "Your_Tenant_Id",        // Replace with the Directory (tenant) ID
       SubscriptionId = "Your_Subscription_Id"    // Replace with your Azure subscription ID
   };


### Prerequisites

- .NET 6.0 LTS or later
- Azure subscription with appropriate permissions and credentials

### Installation

1. Clone the repository or download the source code.
2. Open the solution in Visual Studio or your preferred code editor.

### Configuration

Before running the application, make sure to update the Azure application credentials and resource information in the `Program.cs` file.

```csharp
// Update these values with your actual Azure application credentials
var credentials = new AzureApplicationCredentials()
{
    ClientId = "Your_Client_Id",
    ClientSecret = "Your_Client_Secret",
    TenantId = "Your_Tenant_Id",
    SubscriptionId = "Your_Subscription_Id"
};

// Update these values with your actual Azure resource information
var webAppResources = new WebAppCredentials()
{
    ResourceGroupName = "Your_Resource_Group",
    WebAppName = "Your_Web_App_Name",
    WebAppServicePlanName = "Your_Web_App_Service_Plan_Name"
};

var sqlResources = new SQLCredentials()
{
    ResourceGroupName = "Your_Resource_Group",
    SqlServerName = "Your_SQL_Server_Name",
    DatabaseName = "Your_Database_Name"
};
```

### Usage

#### Scaling Azure Web App

To scale an Azure Web App, follow these steps:

1. Create an instance of `WebApplicationService` using the provided credentials and resource information, same applies for the SQL Server!


```csharp
var webAppServiceScaler = new WebApplicationService(credentials, webAppResources);

webAppServiceScaler.CreateAzureClient();

IWebApp webApp = webAppServiceScaler.GetWebApp();


//Provide how many scaled instances desired with this pricing tier!

webAppServiceScaler.Scale(PricingTier.StandardS1, 2);
````

### Scaling Azure SQL Server

To scale an Azure SQL Server, follow these steps:

1. Create an instance of `SQLDatabaseService` using the provided credentials and resource information:

```csharp
// Update these values with your actual Azure application credentials
var credentials = new AzureApplicationCredentials()
{
    ClientId = "Your_Client_Id",
    ClientSecret = "Your_Client_Secret",
    TenantId = "Your_Tenant_Id",
    SubscriptionId = "Your_Subscription_Id"
};

// Update these values with your actual Azure resource information for the SQL Server
var sqlResources = new SQLCredentials()
{
    ResourceGroupName = "Your_Resource_Group",
    SqlServerName = "Your_SQL_Server_Name",
    DatabaseName = "Your_Database_Name"
};

var sqlDatabaseService = new SQLDatabaseService(credentials, sqlResources);

sqlDatabaseService.CreateAzureClient();

var desiredPlan = ServiceObjectiveName.S1;

ISqlDatabase updatedSqlDatabase = sqlDatabaseService.Scale(desiredPlan);

```