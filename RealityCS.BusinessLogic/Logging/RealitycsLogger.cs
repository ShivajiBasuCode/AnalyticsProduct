using NLog;
using RealityCS.Core.Helper;
using RealityCS.Core.Infrastructure;
using RealityCS.DataLayer;
using RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels;
using RealityCS.DataLayer.Context.RealitycsShared.ContextModels;
using RealityCS.SharedMethods;
using System;
using System.Collections.Generic;

namespace RealityCS.BusinessLogic.Logging
{

    public class RealitycsLogger
    {
        #region Fields 
        
       // private readonly RealitycsCommonSettings _commonSettings;
        private readonly IWebHelper _webHelper;
        
        #endregion
        
        #region Ctor          
        public RealitycsLogger(/*RealitycsCommonSettings commonSettings,*/ IWebHelper webHelper)
        {
            //this._commonSettings = commonSettings;
            this._webHelper = webHelper;
        }
        #endregion
        #region Utilities       
        /// <summary>  
        /// Determines whether a log level is enabled 
        /// </summary> 
        /// <param name="level">Log level</param> 
        /// <returns>Result</returns>        
        public bool IsEnabled(EnumerationLogLevelType level)
        {
            switch (level)
            {
                case EnumerationLogLevelType.Debug:
                    return false;
                case EnumerationLogLevelType.Warning:
                    return false;
                default:
                    return true;
            }
        }
        #endregion Utilities     
        #region Methods 

        /// <summary> 
        ///  
        /// </summary> 
        /// <param name="logLevel"></param> 
        /// <param name="shortMessage"></param> 
        /// <param name="fullMessage"></param> 
        private void InsertLog(EnumerationLogLevelType logLevel, string shortMessage, string requestParameters = "", string fullMessage = "")
        {
            //TODO 
            var log = new RealitycsLog
            {
                LogLevel = logLevel,
                ShortMessage = shortMessage,
                FullMessage = fullMessage,
                IpAddress = _webHelper.GetCurrentIpAddress(),
                UserId = 0,
                EndPoint = _webHelper.GetCurrentRequestUrl(includeQueryString: true),
                CreatedOnUtc = DateTime.UtcNow,
                RequestParameters = requestParameters
            }; 
            if (true/*commonSettings.RDBMSLogginEnabled*/)
            {
                var logRepository = RealitycsEngineContext.Current.Resolve<IGenericRepository<RealitycsLog>>();
                logRepository.Insert(log);
            }
            if (true/*_commonSettings.FileSystemLoggingEnabled*/)
            {
                ILogger nLogger = LogManager.GetCurrentClassLogger();
                LogEventInfo logEvent = new LogEventInfo(LogLevel.Info, string.Empty, string.Empty);
                logEvent.Properties["Id"] = log.PK_Id;
                logEvent.Properties["LogLevel"] = log.LogLevel;
                logEvent.Properties["IpAddress"] = log.IpAddress;
                logEvent.Properties["ShortMessage"] = log.ShortMessage;
                logEvent.Properties["RequestParameters"] = log.RequestParameters;
                logEvent.Properties["EndPoint"] = log.EndPoint;
                logEvent.Properties["CommunityUserId"] = 0;//log.CommunityUserId;
                logEvent.Properties["CreatedOnUtc"] = log.CreatedOnUtc;
                nLogger.Error(logEvent);
            }
        }                   
        /// <summary> 
        /// Information 
        /// </summary> 
        /// <param name="message">Message</param> 
        /// <param name="exception">Exception</param> 
        public void Information(string message, Exception exception = null)
        {
            //don't log thread abort exception 
            if (exception is System.Threading.ThreadAbortException)
                return; if (IsEnabled(EnumerationLogLevelType.Information))
                InsertLog(EnumerationLogLevelType.Information, message, null, exception?.ToString() ?? string.Empty);
        }
        /// <summary> 
        /// Warning 
        /// </summary> 
        /// <param name="message">Message</param> 
        /// <param name="exception">Exception</param> 
        public void Warning(string message, Exception exception = null)
        {
            //don't log thread abort exception 
            if (exception is System.Threading.ThreadAbortException)
                return; if (IsEnabled(EnumerationLogLevelType.Warning))
                InsertLog(EnumerationLogLevelType.Warning, message, null, exception?.ToString() ?? string.Empty);
        }
        /// <summary> 
        /// Error 
        /// </summary> 
        /// <param name="message">Message</param> 
        /// <param name="exception">Exception</param> 
        public void Error(string message, Exception exception = null)
        {
            //don't log thread abort exception 
            if (exception is System.Threading.ThreadAbortException)
                return; var requestParameters = string.Empty;
            if (exception != null && exception.Data.Count > 0)
            {
                for (int i = 0; i < exception.Data.Keys.Count; i++)
                {
                    if (exception.Data[RealitycsConstants.LogRequestParameters] != null)
                    {
                        requestParameters = exception.Data[RealitycsConstants.LogRequestParameters]?.ToString();
                        break;
                    }
                }
            }
            if (IsEnabled(EnumerationLogLevelType.Error))
                InsertLog(EnumerationLogLevelType.Error, message, requestParameters, exception?.ToString() ?? string.Empty);
        }
        #endregion
    }
}
