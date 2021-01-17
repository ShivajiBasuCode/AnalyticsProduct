using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RealityCS.Core;
using RealityCS.Core.Helper;
using RealityCS.DataLayer;
using RealityCS.SharedMethods;

namespace RealityCS.ServiceCollectionExtensions.Middlewares
{
    /// <summary>
    /// Represents middleware that checks whether request is for keep alive
    /// </summary>
    public class KeepAliveMiddleware
    {
        #region Fields

        private readonly RequestDelegate _next;

        #endregion

        #region Ctor

        public KeepAliveMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invoke middleware actions
        /// </summary>
        /// <param name="context">HTTP context</param>
        /// <param name="webHelper">Web helper</param>
        /// <returns>Task</returns>
        public async Task Invoke(HttpContext context, IWebHelper webHelper)
        {
            //whether database is installed
            if (DataSettingsManager.DatabaseIsInstalled)
            {
                //keep alive page requested (we ignore it to prevent creating a guest customer records)
                var keepAliveUrl = $"{webHelper.GetStoreLocation()}{RealitycsCommonDefaults.KeepAlivePath}";
                if (webHelper.GetCurrentRequestUrl(false).StartsWith(keepAliveUrl, StringComparison.InvariantCultureIgnoreCase))
                    return;
            }

            //or call the next middleware in the request pipeline
            await _next(context);
        }

        #endregion
    }
}