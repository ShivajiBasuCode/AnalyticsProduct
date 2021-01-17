namespace RealityCS.DataLayer
{
    /// <summary>
    /// Represents default values related to Nop data
    /// </summary>
    public static partial class RealitycsDataDefaults
    {
        /// <summary>
        /// Gets a path to the file that contains script to create SQL Server stored procedures
        /// </summary>
        public static string SqlServerStoredProceduresFilePath => "~/App_Data/Install/SqlServer.StoredProcedures.sql";

        /// <summary>
        /// Gets a path to the file that contains script to create MySQL stored procedures
        /// </summary>
        public static string MySQLStoredProceduresFilePath => "~/App_Data/Install/MySQL.StoredProcedures.sql";


        /// <summary>
        /// Gets a path to the file that contains script to drop all the SQL Server tables of the database
        /// </summary>
        public static string SqlServerDropTablesFilePath => "~/App_Data/Install/SqlServer.DropTables.sql";

        /// <summary>
        /// Gets a path to the folder that contains script to create SQL Server functions
        /// </summary>
        public static string SqlServerFunctionsFolderPath => "~/App_Data/Install/Functions";

        /// <summary>
        /// Gets a path to the folder that contains script to alter SQL Server tables
        /// </summary>
        public static string SqlServerAlterTableFolderPath => "~/App_Data/Install/AlterScript";



        /// <summary>
        /// Gets a path to the folder that contains script to create SQL Server Triggers
        /// </summary>
        public static string SqlServerTriggersFolderPath => "~/App_Data/Install/Triggers";


        /// <summary>
        /// Gets a path to the file that contains script to create SQL Server stored procedures
        /// </summary>
        public static string SqlServerStoreProceduresFolderPath => "~/App_Data/Install/StoredProcedures";


        public static string SqlServerCountryFilePath => "~/App_Data/Install/Country.sql";

        public static string SqlServerStateProvinceFilePath => "~/App_Data/Install/States.sql";

        public static string SqlServerCityFilePath => "~/App_Data/Install/City.sql";

        public static string SqlServerCommunityMasterDataFilePath => "~/App_Data/Install/SqlServer.CommunityMasterData.sql";



    }
}
