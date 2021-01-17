using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using RealityCS.BusinessLogic.Customer;
using RealityCS.Core.Helper;
using RealityCS.Core.Http;
using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;

namespace RealityCS.BusinessLogic
{
    /// <summary>
    /// Represents work context for web application
    /// </summary>
    public partial class WorkContext : IWorkContext
    {
        #region Fields

        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IWebHelper webHelper;
        private readonly ITokenManager tokenManager;


        private User _cachedCustomer;
        #endregion

        #region Ctor

        public WorkContext(
            IHttpContextAccessor httpContextAccessor,
            IWebHelper webHelper, ITokenManager tokenManager)
        {
           
            this.httpContextAccessor = httpContextAccessor;
            this.webHelper = webHelper;
            this.tokenManager = tokenManager;
        }

        #endregion

        #region Utilities

       
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current customer
        /// </summary>
        public virtual User CurrentCustomer
        {
            get
            {
                //whether there is a cached value
                if (_cachedCustomer != null)
                    return _cachedCustomer;

                User customer = null;
#warning Get email from token
                string token = httpContextAccessor.HttpContext.Request.Headers["Authorization"];
                if (string.IsNullOrEmpty(token)) 
                    return _cachedCustomer;

                var bearerToken = token.ToString().Split(' ')[1];
                customer = tokenManager.GetUserByToken(bearerToken);
                if(customer!=null && customer.IsActive && !customer.IsDeleted)
                {
                    _cachedCustomer = customer;
                }
               
                return _cachedCustomer;
            }
            set
            {
                _cachedCustomer = value;
            }
        }
        /// <summary>
        /// Gets or sets value indicating whether we're in admin area
        /// </summary>
        public virtual bool IsAdmin { get; set; }

        #endregion
    }
}