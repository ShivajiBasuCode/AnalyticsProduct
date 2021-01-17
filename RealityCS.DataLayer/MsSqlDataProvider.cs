using System;
using System.Data.Common;
using RealityCS.Core.Infrastructure;
using RealityCS.DataLayer.Context.BaseContext;
using RealityCS.SharedMethods.FileProvider;
using RealityCS.DataLayer.BaseContext;
using System.IO;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace RealityCS.DataLayer
{
    /// <summary>
    /// Represents the MS SQL Server data provider
    /// </summary>
    public partial class MsSqlDataProvider : IRealitycsDataProvider
    {
        #region Utils

        /// <summary>
        /// Get SQL commands from the script
        /// </summary>
        /// <param name="sql">SQL script</param>
        /// <returns>List of commands</returns>
        //private static IList<string> GetCommandsFromScript(string sql)
        //{
        //    var commands = new List<string>();

        //    //origin from the Microsoft.EntityFrameworkCore.Migrations.SqlServerMigrationsSqlGenerator.Generate method
        //    sql = Regex.Replace(sql, @"\\\r?\n", string.Empty);
        //    var batches = Regex.Split(sql, @"^\s*(GO[ \t]+[0-9]+|GO)(?:\s+|$)", RegexOptions.IgnoreCase | RegexOptions.Multiline);

        //    for (var i = 0; i < batches.Length; i++)
        //    {
        //        if (string.IsNullOrWhiteSpace(batches[i]) || batches[i].StartsWith("GO", StringComparison.OrdinalIgnoreCase))
        //            continue;

        //        var count = 1;
        //        if (i != batches.Length - 1 && batches[i + 1].StartsWith("GO", StringComparison.OrdinalIgnoreCase))
        //        {
        //            var match = Regex.Match(batches[i + 1], "([0-9]+)");
        //            if (match.Success)
        //                count = int.Parse(match.Value);
        //        }

        //        var builder = new StringBuilder();
        //        for (var j = 0; j < count; j++)
        //        {
        //            builder.Append(batches[i]);
        //            if (i == batches.Length - 1)
        //                builder.AppendLine();
        //        }

        //        commands.Add(builder.ToString());
        //    }

        //    return commands;
        //}

        //protected virtual SqlConnectionStringBuilder GetConnectionStringBuilder()
        //{
        //    var connectionString = DataSettingsManager.LoadSettings().ConnectionString;

        //    return new SqlConnectionStringBuilder(connectionString);
        //}

        #endregion

        #region Methods

        ///// <summary>
        ///// Gets a connection to the database for a current data provider
        ///// </summary>
        ///// <param name="connectionString">Connection string</param>
        ///// <returns>Connection to a database</returns>
        //protected override IDbConnection GetInternalDbConnection(string connectionString)
        //{
        //    if(string.IsNullOrEmpty(connectionString))
        //        throw new ArgumentException(nameof(connectionString));

        //    return new SqlConnection(connectionString);
        //}

        ///// <summary>
        ///// Create the database
        ///// </summary>
        ///// <param name="collation">Collation</param>
        ///// <param name="triesToConnect">Count of tries to connect to the database after creating; set 0 if no need to connect after creating</param>
        //public void CreateDatabase(string collation, int triesToConnect = 10)
        //{
        //    if (DatabaseExists())
        //        return;

        //    var builder = GetConnectionStringBuilder();

        //    //gets database name
        //    var databaseName = builder.InitialCatalog;

        //    //now create connection string to 'master' dabatase. It always exists.
        //    builder.InitialCatalog = "master";

        //    using (var connection = new SqlConnection(builder.ConnectionString))
        //    {
        //        var query = $"CREATE DATABASE [{databaseName}]";
        //        if (!string.IsNullOrWhiteSpace(collation))
        //            query = $"{query} COLLATE {collation}";

        //        var command = new SqlCommand(query, connection);
        //        command.Connection.Open();

        //        command.ExecuteNonQuery();
        //    }

        //    //try connect
        //    if (triesToConnect <= 0)
        //        return;

        //    //sometimes on slow servers (hosting) there could be situations when database requires some time to be created.
        //    //but we have already started creation of tables and sample data.
        //    //as a result there is an exception thrown and the installation process cannot continue.
        //    //that's why we are in a cycle of "triesToConnect" times trying to connect to a database with a delay of one second.
        //    for (var i = 0; i <= triesToConnect; i++)
        //    {
        //        if (i == triesToConnect)
        //            throw new Exception("Unable to connect to the new database. Please try one more time");

        //        if (!DatabaseExists())
        //            Thread.Sleep(1000);
        //        else
        //            break;
        //    }
        //}

        ///// <summary>
        ///// Checks if the specified database exists, returns true if database exists
        ///// </summary>
        ///// <returns>Returns true if the database exists.</returns>
        //public bool DatabaseExists()
        //{
        //    try
        //    {
        //        using (var connection = new SqlConnection(GetConnectionStringBuilder().ConnectionString))
        //        {
        //            //just try to connect
        //            connection.Open();
        //        }

        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// Execute commands from a file with SQL script against the context database
        ///// </summary>
        ///// <param name="fileProvider">File provider</param>
        ///// <param name="filePath">Path to the file</param>
        //protected void ExecuteSqlScriptFromFile(IRealitycsFileProvider fileProvider, string filePath)
        //{
        //    filePath = fileProvider.MapPath(filePath);
        //    if (!fileProvider.FileExists(filePath))
        //        return;

        //    ExecuteSqlScript(fileProvider.ReadAllText(filePath, Encoding.Default));
        //}

        ///// <summary>
        ///// Execute commands from the SQL script
        ///// </summary>
        ///// <param name="sql">SQL script</param>
        //public void ExecuteSqlScript(string sql)
        //{
        //    var sqlCommands = GetCommandsFromScript(sql);

        //    using var currentConnection = CreateDataConnection();
        //    foreach (var command in sqlCommands)
        //        currentConnection.Execute(command);
        //}

        /// <summary>
        /// Initialize database
        /// </summary>
        public void InitializeDatabase()
        {
            var context1 = RealitycsEngineContext.Current.ResolveAll<IRealitycsBaseContext>();
            var context=context1.FirstOrDefault();

            var fileProvider = RealitycsEngineContext.Current.Resolve<IRealitycsFileProvider>();
            try
            {
                foreach (var cont in context1)
                {
                    //Drop all existing tables if any
                    cont.ExecuteSqlScriptFromFile(fileProvider.MapPath(RealitycsDataDefaults.SqlServerDropTablesFilePath));

                    //ReCreate all the tables
                    cont.ExecuteSqlScript(cont.GenerateCreateScript());
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            //Create functions
            try
            {
                if (Directory.Exists(fileProvider.MapPath(RealitycsDataDefaults.SqlServerFunctionsFolderPath)))
                {
                    var functionfiles = Directory.GetFiles(fileProvider.MapPath(RealitycsDataDefaults.SqlServerFunctionsFolderPath));
                    if (functionfiles != null)
                    {
                        foreach (var filePath in functionfiles)
                        {
                            try
                            {
                                context.ExecuteSqlScriptFromFile(filePath);
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }
                }
                //Create procedures
                if (Directory.Exists(fileProvider.MapPath(RealitycsDataDefaults.SqlServerStoreProceduresFolderPath)))
                {
                    var procedurefiles = Directory.GetFiles(fileProvider.MapPath(RealitycsDataDefaults.SqlServerStoreProceduresFolderPath));
                    if (procedurefiles != null)
                    {
                        foreach (var filePath in Directory.GetFiles(fileProvider.MapPath(RealitycsDataDefaults.SqlServerStoreProceduresFolderPath)))
                        {
                            try
                            {
                                context.ExecuteSqlScriptFromFile(filePath);
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }

                }
                //Create Triggers
                if (Directory.Exists(fileProvider.MapPath(RealitycsDataDefaults.SqlServerTriggersFolderPath)))
                {
                    //Create Triggers
                    var triggersfiles = Directory.GetFiles(fileProvider.MapPath(RealitycsDataDefaults.SqlServerTriggersFolderPath));
                    if (triggersfiles != null)
                    {
                        foreach (var filePath in Directory.GetFiles(fileProvider.MapPath(RealitycsDataDefaults.SqlServerTriggersFolderPath)))
                        {
                            try
                            {
                                context.ExecuteSqlScriptFromFile(filePath);
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }
                }
                context.ExecuteSqlScriptFromFile(fileProvider.MapPath(RealitycsDataDefaults.SqlServerCountryFilePath));

                context.ExecuteSqlScriptFromFile(fileProvider.MapPath(RealitycsDataDefaults.SqlServerStateProvinceFilePath));

                context.ExecuteSqlScriptFromFile(fileProvider.MapPath(RealitycsDataDefaults.SqlServerCityFilePath));

                //  context.ExecuteSqlScriptFromFile(fileProvider.MapPath(RealitycsDataDefaults.SqlServerCommunityMasterDataFilePath));
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        public DbParameter GetParameter()
        {
            return new SqlParameter();
        }
        #endregion

        #region Properties

        /// <summary>
        /// Sql server data provider
        /// </summary>
      //  protected override IDataProvider LinqToDbDataProvider => new SqlServerDataProvider(ProviderName.SqlServer, SqlServerVersion.v2008);

        /// <summary>
        /// Gets allowed a limit input value of the data for hashing functions, returns 0 if not limited
        /// </summary>
        public int SupportedLengthOfBinaryHash { get; } = 8000;

        /// <summary>
        /// Gets a value indicating whether this data provider supports backup
        /// </summary>
        public virtual bool BackupSupported => true;

        #endregion
    }
}
