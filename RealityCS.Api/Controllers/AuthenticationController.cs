using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace RealityCS.Api.Controllers
{
    
    
    [Route("api/[controller]/[action]")]
    public class AuthenticationController : ApiControllerBase
    {
        
        private readonly IIdentityManager identityManager;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identityManager"></param>
        public AuthenticationController(IIdentityManager  identityManager)
        {
           
            this.identityManager = identityManager;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Genarate Auth Token",
            Description = "Genarate token for authorozation",
            OperationId = "token",
            Tags = new[] { "Auth" }
        )]
        [SwaggerResponse(201, "Token Created", typeof(string))]
        [SwaggerResponse(404, "Invalid User Information", typeof(string))]
        /// <summary>
        /// Genarate Auth Token
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> GenarateToken(string username, string password)
        {
            string token =await this.identityManager.GenarateToken(username, password);
           
            return Ok(new { access_token=token});
        }
        [SwaggerOperation(
            Summary = "Get user claims",
            Description = "Get claims by user",
            OperationId = "GetClaims",
            Tags = new[] { "Auth" }
        )]
        [SwaggerResponse(201, "Claims served", typeof(string))]
        [SwaggerResponse(404, "Invalid token", typeof(string))]
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public  IActionResult GetClaims()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identityClaims.Claims;

            return Ok(claims);

        }


    }
}