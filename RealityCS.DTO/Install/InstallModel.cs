using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealityCS.DataLayer;

namespace RealityCS.DTO.Install
{
    public partial class InstallModel 
    {
        public InstallModel()
        {
            AvailableLanguages = new List<SelectListItem>();
        }

   
        public string AdminEmail { get; set; }

        [DataType(DataType.Password)]
        public string AdminPassword { get; set; }

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string DatabaseConnectionString { get; set; }
        public DataProviderType DataProvider { get; set; }
        public string SqlConnectionInfo { get; set; }
        public string SqlServerName { get; set; }
        public string SqlDatabaseName { get; set; }
        public string SqlServerUsername { get; set; }
        [DataType(DataType.Password)]
        public string SqlServerPassword { get; set; }
        public string SqlAuthenticationType { get; set; }
        public bool SqlServerCreateDatabase { get; set; }
        public bool UseCustomCollation { get; set; }
        public string Collation { get; set; }
        public bool DisableSampleDataOption { get; set; }
        public bool InstallSampleData { get; set; }
        public List<SelectListItem> AvailableLanguages { get; set; }
        public string RestartUrl { get; set; }



        //public bool CreateDatabaseIfNotExists { get; set; }
        //public bool ConnectionStringRaw { get; set; }

        //public string DatabaseName { get; set; }
        //public string ServerName { get; set; }

        //public bool IntegratedSecurity { get; set; }

        //public string Username { get; set; }

        //[DataType(DataType.Password)]
        //public string Password { get; set; }
        //public string ConnectionString { get; set; }


        //public List<SelectListItem> AvailableDataProviders { get; set; }
        //public IDictionary<string, string> RawDataSettings => new Dictionary<string, string>();

        //public string RestartUrl { get; set; }

    }
}