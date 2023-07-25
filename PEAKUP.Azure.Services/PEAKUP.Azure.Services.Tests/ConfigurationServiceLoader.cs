using PEAKUP.Azure.Services.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PEAKUP.Azure.Services.Tests
{

    public class ConfigurationServiceLoader
    {
        public AzureApplicationCredentials GETCredentials() => new AzureApplicationCredentials()
        {
            ClientId = ConfigurationManager.AppSettings["ClientId"],
            ClientSecret = ConfigurationManager.AppSettings["ClientSecret"],
            TenantId = ConfigurationManager.AppSettings["TenantId"],
            SubscriptionId = ConfigurationManager.AppSettings["SubscriptionId"]
        };


        public SQLCredentials GETSQLCredentials() => new SQLCredentials()
        {
            ResourceGroupName = ConfigurationManager.AppSettings["SQLResourceGroupName"],
            SqlServerName = ConfigurationManager.AppSettings["SqlServerName"],
            DatabaseName = ConfigurationManager.AppSettings["DatabaseName"],
        };


        public WebAppCredentials GETWebAppCredentials() => new WebAppCredentials()
        {
            ResourceGroupName = ConfigurationManager.AppSettings["WebAppResourceGroupName"],
            WebAppName = ConfigurationManager.AppSettings["WebAppName"],
            WebAppServicePlanName = ConfigurationManager.AppSettings["WebAppServicePlanName"],
        };
    }
}
