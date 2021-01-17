

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RealityCS.Web.Filters;
using RealityCS.Web.Models;

namespace RealityCS.Web.Utility
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [ValidateUser]
    [ResourceFilter]
    [ActionFilter]
    [ExceptionFilter]
    
    //[ServiceFilter(typeof(LoggingFilter))]
    public abstract class RestrictedControllerBase:Controller
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly HttpContext httpContext;
        internal ApiClient ApiClient { get; set; }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="httpContextAccessor"></param>
       /// <param name="configuration"></param>
       /// <param name="ApiBase"></param>
        public RestrictedControllerBase(IHttpContextAccessor httpContextAccessor, IConfiguration configuration,string ApiBase)
        {
            this.httpContextAccessor = httpContextAccessor;
             httpContext = httpContextAccessor.HttpContext;

           
            ApiClient = new ApiClient(new Uri(configuration[ApiBase]), httpContext.Request.Cookies["access_token"]);
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected LoggedinUser GetLoggedinUser()
        {
            LoggedinUser loggedinUser = new LoggedinUser();
            loggedinUser.UserId = Convert.ToInt32(this.GetUsersClaims().Find(x => x.Type == "userid").Value);
            loggedinUser.AssignRole = this.GetAssignedRole();

            return loggedinUser;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected Int32 GetAssignedRole()
        {
            List<UsersClaim> usersClaims = this.GetUsersClaims();
            int assignedrole =Convert.ToInt32(usersClaims.Where(x => x.Type == "AssignRole").Select(s => s.Value).SingleOrDefault());
            return assignedrole;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected List<UsersClaim> GetUsersClaims()
        {
            var identityClaims = (ClaimsIdentity)httpContext.User.Identity;            
            IEnumerable<Claim> claims = identityClaims.Claims;
           
            List<UsersClaim> usersClaims = new List<UsersClaim>();
            foreach (Claim claim in claims)
            {
                usersClaims.Add(new UsersClaim { Type = claim.Type, Value = claim.Value });
            }
            return usersClaims;

        }
       


    }
}
