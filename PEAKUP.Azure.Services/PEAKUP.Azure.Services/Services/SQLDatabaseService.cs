using Microsoft.Azure.Management.Sql.Fluent;
using Microsoft.Azure.Management.Sql.Fluent.Models;
using PEAKUP.Azure.Services.Entity;

namespace PEAKUP.Azure.Services.Services
{
    // Class: SQLDatabaseService
    /// <summary>
    /// This class represents a service for scaling Azure SQL databases. It inherits from the AzureResourceScalerBase
    /// class and provides methods for getting SQL servers, getting SQL databases, and scaling the SQL database to
    /// the desired service objective.
    /// </summary>
    public class SQLDatabaseService : AzureResourceScalerBase
    {
        // Property: Resources
        /// <summary>
        /// Gets or sets the SQL credentials and resource information required for scaling the SQL database.
        /// </summary>
        public SQLCredentials Resources { get; set; }

        // Property: SQLServer
        /// <summary>
        /// Gets or sets the SQL server instance representing the target SQL server for scaling.
        /// </summary>
        public ISqlServer SQLServer { get; set; }

        // Property: SqlDatabase
        /// <summary>
        /// Gets or sets the SQL database instance representing the target SQL database for scaling.
        /// </summary>
        public ISqlDatabase SqlDatabase { get; set; }

        // Constructor: SQLDatabaseService
        /// <summary>
        /// Initializes a new instance of the SQLDatabaseService class with the provided Azure application credentials
        /// and SQL credentials.
        /// </summary>
        /// <param name="credentials">The Azure application credentials for authentication.</param>
        /// <param name="resources">The SQL credentials and resource information required for scaling.</param>
        public SQLDatabaseService(AzureApplicationCredentials credentials, SQLCredentials resources) : base(credentials)
        {
            Resources = resources;
        }

        // Method: GetSQLServer
        /// <summary>
        /// Retrieves the SQL server instance based on the provided SQL server name and resource group name.
        /// If the SQL server does not exist or its name is available, this method returns null.
        /// </summary>
        /// <returns>The SQL server instance if it exists; otherwise, null.</returns>
        public ISqlServer? GetSQLServer()
        {
            // Check if the SQL server name is available
            if (AzureClient.SqlServers.CheckNameAvailability(Resources.SqlServerName).IsAvailable)
            {
                return null;
            }

            // Get the SQL server instance using the provided SQL server name and resource group name
            SQLServer = AzureClient.SqlServers.GetByResourceGroup(Resources.ResourceGroupName, Resources.SqlServerName);

            return SQLServer;
        }

        // Method: GETSQLDatabase
        /// <summary>
        /// Retrieves the SQL database instance based on the provided SQL server name, resource group name, and database name.
        /// If the SQL database does not exist, this method returns null.
        /// </summary>
        /// <returns>The SQL database instance if it exists; otherwise, null.</returns>
        public ISqlDatabase? GETSQLDatabase()
        {
            // Get the SQL database instance using the provided SQL server name, resource group name, and database name
            SqlDatabase = AzureClient.SqlServers.Databases.GetBySqlServer(Resources.ResourceGroupName, Resources.SqlServerName, Resources.DatabaseName);

            // If the SQL database exists, return the instance; otherwise, return null
            if (SqlDatabase is not null)
            {
                return SqlDatabase;
            }

            return null;
        }

        // Method: Scale
        /// <summary>
        /// Scales the SQL database to the desired service objective (performance level).
        /// </summary>
        /// <param name="desiredPlan">The desired service objective (performance level) to which the database should be scaled.</param>
        /// <returns>The updated SQL database instance after scaling.</returns>
        public ISqlDatabase Scale(ServiceObjectiveName desiredPlan)
        {
            // Update the SQL database with the desired service objective and apply the changes
            return SqlDatabase.Update().WithServiceObjective(desiredPlan).Apply();
        }
    }
}
