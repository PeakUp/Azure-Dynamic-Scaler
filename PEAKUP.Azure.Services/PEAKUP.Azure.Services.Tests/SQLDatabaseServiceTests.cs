using Microsoft.Azure.Management.Sql.Fluent;
using Microsoft.Azure.Management.Sql.Fluent.Models;
using PEAKUP.Azure.Services.Entity;
using PEAKUP.Azure.Services.Services;
using System.Configuration;

namespace PEAKUP.Azure.Services.Tests
{
    [TestClass]
    public class SQLDatabaseServiceTests
    {
        public ConfigurationServiceLoader configurationServiceLoader { get; set; }

        // Create AzureApplicationCredentials object
        AzureApplicationCredentials Credentials = new AzureApplicationCredentials() { };

        SQLCredentials Resources = new SQLCredentials() { };

        public SQLDatabaseServiceTests()
        {
            configurationServiceLoader = new ConfigurationServiceLoader();

            Credentials = configurationServiceLoader.GETCredentials();

            Resources = configurationServiceLoader.GETSQLCredentials();
        }

        [TestMethod]
        public void TestSQLDatabaseService_GetSQLServer()
        {
            // Arrange
            var sqlDatabaseService = new SQLDatabaseService(Credentials, Resources);

            // Act
            sqlDatabaseService.CreateAzureClient();

            ISqlServer sqlServer = sqlDatabaseService.GetSQLServer();

            // Assert
            Assert.IsNotNull(sqlServer, "SQL Server instance should not be null.");
            // Add more assertions here if needed based on the SQL Server logic.
        }

        [TestMethod]
        public void TestSQLDatabaseService_GETSQLDatabase()
        {
            // Arrange
            var sqlDatabaseService = new SQLDatabaseService(Credentials, Resources);

            // Act
            sqlDatabaseService.CreateAzureClient();
            ISqlDatabase sqlDatabase = sqlDatabaseService.GETSQLDatabase();

            // Assert
            Assert.IsNotNull(sqlDatabase, "SQL Database instance should not be null.");
            // Add more assertions here if needed based on the SQL Database logic.
        }

        [TestMethod]
        public void TestSQLDatabaseService_Scale()
        {
            // Arrange
            var sqlDatabaseService = new SQLDatabaseService(Credentials, Resources);
            sqlDatabaseService.CreateAzureClient();
            ISqlDatabase sqlDatabase = sqlDatabaseService.GETSQLDatabase();

            // Act
            if (sqlDatabase is not null)
            {
                // Perform additional actions if needed before scaling
                var desiredPlan = ServiceObjectiveName.S1;
                ISqlDatabase updatedSqlDatabase = sqlDatabaseService.Scale(desiredPlan);

                // Assert
                Assert.IsNotNull(updatedSqlDatabase, "Updated SQL Database instance should not be null.");
                // Add more assertions here if needed based on the scaling logic.
            }
            else
            {
                Assert.Fail("SQL Database instance is null. Unable to perform scaling.");
            }
        }
    }
}
