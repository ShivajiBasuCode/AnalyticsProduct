using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealityCS.Core;
using RealityCS.SharedMethods.FileProvider;
using RealityCS.DataLayer;
using RealityCS.Core.Infrastructure;
using RealityCS.Core.Helper;
using RealityCS.BusinessLogic.Installation;
using RealityCS.DTO.Install;
using Microsoft.Data.SqlClient;
using System.Threading;
using Microsoft.AspNetCore.Hosting;

namespace RealityCS.Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi =true)]
    public partial class InstallController : Controller
    {
        #region Fields

        private readonly IInstallationLocalizationService _locService;
        private readonly IRealitycsFileProvider _fileProvider;
        private readonly IApplicationLifetime applicationLifeTime;
        #endregion

        #region Ctor

        public InstallController(IInstallationLocalizationService locService,
            IRealitycsFileProvider fileProvider, IApplicationLifetime applicationLifeTime)
        {
            _locService = locService;
            _fileProvider = fileProvider;
            this.applicationLifeTime = applicationLifeTime;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// A value indicating whether we use MARS (Multiple Active Result Sets)
        /// </summary>
        protected virtual bool UseMars => false;

        /// <summary>
        /// Checks if the specified database exists, returns true if database exists
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <returns>Returns true if the database exists.</returns>
        protected virtual bool SqlServerDatabaseExists(string connectionString)
        {
            try
            {
                //just try to connect
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Creates a database on the server.
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <param name="collation">Server collation; the default one will be used if not specified</param>
        /// <param name="triesToConnect">
        /// Number of times to try to connect to database. 
        /// If connection cannot be open, then error will be returned. 
        /// Pass 0 to skip this validation.
        /// </param>
        /// <returns>Error</returns>
        protected virtual string CreateDatabase(string connectionString, string collation, int triesToConnect = 10)
        {
            try
            {
                //parse database name
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
                string databaseName = builder.InitialCatalog;
                //now create connection string to 'master' dabatase. It always exists.
                builder.InitialCatalog = "master";
                string masterCatalogConnectionString = builder.ToString();
                string query = $"CREATE DATABASE [{databaseName}]";
                if (!string.IsNullOrWhiteSpace(collation))
                {
                    query = $"{query} COLLATE {collation}";
                }

                using (SqlConnection conn = new SqlConnection(masterCatalogConnectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                //try connect
                if (triesToConnect > 0)
                {
                    //Sometimes on slow servers (hosting) there could be situations when database requires some time to be created.
                    //But we have already started creation of tables and sample data.
                    //As a result there is an exception thrown and the installation process cannot continue.
                    //That's why we are in a cycle of "triesToConnect" times trying to connect to a database with a delay of one second.
                    for (int i = 0; i <= triesToConnect; i++)
                    {
                        if (i == triesToConnect)
                        {
                            throw new Exception("Unable to connect to the new database. Please try one more time");
                        }

                        if (!this.SqlServerDatabaseExists(connectionString))
                        {
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Format(_locService.GetResource("DatabaseCreationError"), ex.Message);
            }
        }

        /// <summary>
        /// Create contents of connection strings used by the SqlConnection class
        /// </summary>
        /// <param name="trustedConnection">Avalue that indicates whether User ID and Password are specified in the connection (when false) or whether the current Windows account credentials are used for authentication (when true)</param>
        /// <param name="serverName">The name or network address of the instance of SQL Server to connect to</param>
        /// <param name="databaseName">The name of the database associated with the connection</param>
        /// <param name="userName">The user ID to be used when connecting to SQL Server</param>
        /// <param name="password">The password for the SQL Server account</param>
        /// <param name="timeout">The connection timeout</param>
        /// <returns>Connection string</returns>
        protected virtual string CreateConnectionString(bool trustedConnection, string serverName, string databaseName, string userName, string password, int timeout = 0)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                IntegratedSecurity = trustedConnection,
                DataSource = serverName,
                InitialCatalog = databaseName
            };

            if (!trustedConnection)
            {
                builder.UserID = userName;
                builder.Password = password;
            }
            builder.PersistSecurityInfo = false;
            if (this.UseMars)
            {
                builder.MultipleActiveResultSets = true;
            }
            if (timeout > 0)
            {
                builder.ConnectTimeout = timeout;
            }
            return builder.ConnectionString;
        }

        #endregion

        #region Methods

        public virtual IActionResult Index()
        {
            if (DataSettingsManager.DatabaseIsInstalled)
                return RedirectToRoute("Homepage");

            var model = new InstallModel
            {
                AdminEmail = "admin@yourStore.com",
                InstallSampleData = false,
                SqlServerName = "192.168.1.103",
                SqlServerUsername = "RelaityCS",
                SqlServerPassword = "relaitycs@123",
                SqlDatabaseName = "Realitycs_piyush",
                SqlAuthenticationType = "sqlauthentication",
                SqlConnectionInfo = "sqlconnectioninfo_values",
                //fast installation service does not support SQL compact
                DataProvider = DataProviderType.SqlServer
            };


            foreach (var lang in _locService.GetAvailableLanguages())
            {
                model.AvailableLanguages.Add(new SelectListItem
                {
                    Value = Url.Action("ChangeLanguage", "Install", new { language = lang.Code }),
                    Text = lang.Name,
                    Selected = _locService.GetCurrentLanguage().Code == lang.Code
                });
            }

            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Index(InstallModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (DataSettingsManager.DatabaseIsInstalled)
                return RedirectToRoute("Account");

            if (model.DatabaseConnectionString != null)
            {
                model.DatabaseConnectionString = model.DatabaseConnectionString.Trim();
            }

            foreach (var lang in _locService.GetAvailableLanguages())
            {
                model.AvailableLanguages.Add(new SelectListItem
                {
                    Value = Url.Action("ChangeLanguage", "Install", new { language = lang.Code }),
                    Text = lang.Name,
                    Selected = _locService.GetCurrentLanguage().Code == lang.Code
                });
            }
            model.DisableSampleDataOption = true;

            if (model.DataProvider == DataProviderType.SqlServer)
            {
                if (model.SqlConnectionInfo.Equals("sqlconnectioninfo_raw", StringComparison.InvariantCultureIgnoreCase))
                {
                    //raw connection string
                    if (string.IsNullOrEmpty(model.DatabaseConnectionString))
                        ModelState.AddModelError("", _locService.GetResource("ConnectionStringRequired"));

                    try
                    {
                        //try to create connection string
                        new SqlConnectionStringBuilder(model.DatabaseConnectionString);
                    }
                    catch
                    {
                        ModelState.AddModelError("", _locService.GetResource("ConnectionStringWrongFormat"));
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(model.SqlServerName))
                        ModelState.AddModelError("", _locService.GetResource("SqlServerNameRequired"));

                    if (string.IsNullOrEmpty(model.SqlDatabaseName))
                        ModelState.AddModelError("", _locService.GetResource("DatabaseNameRequired"));

                    if (model.SqlAuthenticationType.Equals("sqlauthentication", StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (string.IsNullOrEmpty(model.SqlServerUsername))
                            ModelState.AddModelError("", _locService.GetResource("SqlServerUsernameRequired"));

                        if (string.IsNullOrEmpty(model.SqlServerPassword))
                            ModelState.AddModelError("", _locService.GetResource("SqlServerPasswordRequired"));
                    }
                }
            }

            //Consider granting access rights to the resource to the ASP.NET request identity. 
            //ASP.NET has a base process identity 
            //(typically {MACHINE}\ASPNET on IIS 5 or Network Service on IIS 6 and IIS 7, 
            //and the configured application pool identity on IIS 7.5) that is used if the application is not impersonating.
            //If the application is impersonating via <identity impersonate="true"/>, 
            //the identity will be the anonymous user (typically IUSR_MACHINENAME) or the authenticated request user.
            IWebHelper webHelper = RealitycsEngineContext.Current.Resolve<IWebHelper>();
            //validate permissions
            //IEnumerable<string> dirsToCheck = CommunityFilePermissionHelper.GetDirectoriesWrite();
            //foreach (string dir in dirsToCheck)
            //{
            //    if (!CommunityFilePermissionHelper.CheckPermissions(dir, checkRead: false, checkWrite: true, checkModify: true, checkDelete: false))
            //        ModelState.AddModelError("", string.Format(localizationService.GetResource("ConfigureDirectoryPermissions"), WindowsIdentity.GetCurrent().Name, dir));
            //}

            //IEnumerable<string> filesToCheck = CommunityFilePermissionHelper.GetFilesWrite();
            //foreach (string file in filesToCheck)
            //{
            //    if (!CommunityFilePermissionHelper.CheckPermissions(file, checkRead: false, checkWrite: true, checkModify: true, checkDelete: true))
                 //   ModelState.AddModelError("", string.Format(localizationService.GetResource("ConfigureFilePermissions"), WindowsIdentity.GetCurrent().Name, file));
           // }

            if (ModelState.IsValid)
            {
                try
                {
                    string connectionString = string.Empty;
                    if (model.DataProvider == DataProviderType.SqlServer)
                    {
                        if (model.SqlConnectionInfo.Equals("sqlconnectioninfo_raw", StringComparison.InvariantCultureIgnoreCase))
                        {
                            //raw connection string
                            //we know that MARS option is required when using Entity Framework
                            //let's ensure that it's specified
                            SqlConnectionStringBuilder sqlCsb = new SqlConnectionStringBuilder(model.DatabaseConnectionString);
                            if (this.UseMars)
                            {
                                sqlCsb.MultipleActiveResultSets = true;
                            }
                            connectionString = sqlCsb.ToString();
                        }
                        else
                        {
                            connectionString = CreateConnectionString(model.SqlAuthenticationType == "windowsauthentication",
                                model.SqlServerName, model.SqlDatabaseName,
                                model.SqlServerUsername, model.SqlServerPassword);
                        }

                        if (model.SqlServerCreateDatabase)
                        {
                            if (!SqlServerDatabaseExists(connectionString))
                            {
                                //create database
                                string collation = model.UseCustomCollation ? model.Collation : "";
                                string errorCreatingDatabase = CreateDatabase(connectionString, collation);
                                if (!string.IsNullOrEmpty(errorCreatingDatabase))
                                {
                                    throw new Exception(errorCreatingDatabase);
                                }
                            }
                        }
                        else
                        {
                            //check whether database exists
                            if (!SqlServerDatabaseExists(connectionString))
                            {
                                throw new Exception(_locService.GetResource("DatabaseNotExists"));
                            }
                        }
                    }

                    DataSettingsManager.SaveSettings(new DataSettings
                    {
                        DataProvider = model.DataProvider,
                        ConnectionString = connectionString
                    }, _fileProvider);

                    var dataProvider = RealitycsEngineContext.Current.Resolve<IRealitycsDataProvider>();
                    dataProvider.InitializeDatabase();

                    IInstallationService installationService = RealitycsEngineContext.Current.Resolve<IInstallationService>();
                    installationService.InstallRequiredData(model.AdminEmail, model.AdminPassword);


                    DataSettingsManager.ResetCache();

                   // ICommunityPermissionProvider provider = new StandardPermissionProvider();
                    //RealitycsEngineContext.Current.Resolve<ICommunityPermissionService>().InstallPermissions(provider);


                    //restart application
                    webHelper.RestartAppDomain();

                    applicationLifeTime.StopApplication();

                    //Redirect to home page
                    return null;
                }
                catch (Exception exception)
                {
                    //reset cache
                    DataSettingsManager.ResetCache();

                    //var staticCacheManager = RealitycsEngineContext.Current.Resolve<IStaticCacheManager>();
                    //staticCacheManager.Clear();

                    //clear provider settings if something got wrong
                    DataSettingsManager.SaveSettings(new DataSettings(), _fileProvider);

                    ModelState.AddModelError(string.Empty, string.Format(_locService.GetResource("SetupFailed"), exception.Message));
                }
            }
            return View(model);
        }

        public virtual IActionResult ChangeLanguage(string language)
        {
            if (DataSettingsManager.DatabaseIsInstalled)
                return RedirectToRoute("Homepage");

            _locService.SaveCurrentLanguage(language);

            //Reload the page
            return RedirectToAction("Index", "Install");
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public virtual IActionResult RestartInstall()
        {
            if (DataSettingsManager.DatabaseIsInstalled)
                return RedirectToRoute("Homepage");

            return View("Index", new InstallModel { RestartUrl = Url.Action("Index", "Install") });
        }

        public virtual IActionResult RestartApplication()
        {
            if (DataSettingsManager.DatabaseIsInstalled)
                return RedirectToRoute("Homepage");

            //restart application
            RealitycsEngineContext.Current.Resolve<IWebHelper>().RestartAppDomain();

            return new EmptyResult();
        }

        #endregion
    }
}