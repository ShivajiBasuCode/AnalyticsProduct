
namespace RealityCS.SharedMethods
{
    /// <summary>
    /// Represents default values related to common services
    /// </summary>
    public static partial class RealitycsCommonDefaults
    {
        /// <summary>
        /// Gets a request path to the keep alive URL
        /// </summary>
        public static string KeepAlivePath => "keepalive/index";

        #region Address attributes

        /// <summary>
        /// Gets a name of the custom address attribute control
        /// </summary>
        /// <remarks>
        /// {0} : address attribute id
        /// </remarks>
        public static string AddressAttributeControlName => "address_attribute_{0}";

        #endregion

        #region Maintenance

        /// <summary>
        /// Gets a default timeout (in milliseconds) before restarting the application
        /// </summary>
        public static int RestartTimeout => 3000;

        /// <summary>
        /// Gets a path to the database backup files
        /// </summary>
        public static string DbBackupsPath => "db_backups\\";

        /// <summary>
        /// Gets a database backup file extension
        /// </summary>
        public static string DbBackupFileExtension => "bak";

        #endregion

        #region Favicon and app icons

        /// <summary>
        /// Gets a name of the file with code for the head element
        /// </summary>
        public static string HeadCodeFileName => "html_code.html";

        /// <summary>
        ///  Gets a head link for the favicon
        /// </summary>
        public static string SingleFaviconHeadLink => "<link rel=\"shortcut icon\" href=\"\\icons\\icons_{0}\\{1}\">";

        /// <summary>
        /// Gets a path to the favicon and app icons
        /// </summary>
        public static string FaviconAndAppIconsPath => "icons\\icons_{0}";

        /// <summary>
        /// Gets a name of the old favicon icon for current store
        /// </summary>
        public static string OldFaviconIconName => "favicon-{0}.ico";

        #endregion

        #region Localization client-side validation

        /// <summary>
        /// Gets a path to the localization client-side validation 
        /// </summary>
        public static string LocalePatternPath => "lib\\cldr-data\\main\\{0}";

        /// <summary>
        /// Gets a name of the archive with localization of templates
        /// </summary>
        public static string LocalePatternArchiveName => "main.zip";

        /// <summary>
        /// Gets a name of the default pattern locale
        /// </summary>
        public static string DefaultLocalePattern => "en";

        #endregion

        

    
    }
}