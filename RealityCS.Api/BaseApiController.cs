using RealityCS.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RealityCS.SharedMethods;
using RealityCS.Api.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RealityCS.DTO;
using Newtonsoft.Json;

namespace RealityCS.Api
{
    //[Route("")]
    [ApiController]
    [ExceptionFilter]
    //[ServiceFilter(typeof(LoggingFilter))]
    public class ApiControllerBase : ControllerBase
    {


        /// <summary>
        /// 
        /// </summary>
        public ApiControllerBase()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //protected async Task<ApplicationUser> GetCurrentUserIdentity()
        //{

        //    UserManager<ApplicationUser> userManager = this.HttpContext.RequestServices.GetService<UserManager<ApplicationUser>>();
        //    var identityClaims = (ClaimsIdentity)User.Identity;
        //    IEnumerable<Claim> claims = identityClaims.Claims;
        //    //if (string.IsNullOrEmpty(identityClaims.AuthenticationType) == false && identityClaims.AuthenticationType.ToLower() == "bearer")
        //    //{
        //    Int32 UserID;
        //    int.TryParse(identityClaims.FindFirst("userid").Value, out UserID);

        //    var user = await userManager.FindByIdAsync(UserID.ToString());

        //    return user;

        //    //}
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //protected async Task<ApplicationUser> GetCurrentUser()
        //{
        //    // Task<ApplicationUser> user = Task.Run(async () => await this.GetCurrentUserIdentity());

        //    //var result = task.Result;

        //    //return result;

        //    return await this.GetCurrentUserIdentity();

        //}
        public override OkObjectResult Ok(object value)
        {
            var data = CreateResponse(value, StatusCodes.Status200OK);
            return base.Ok(data);
        }
        public override BadRequestObjectResult BadRequest(ModelStateDictionary modelState)
        {
            return RealitycsBadRequest(modelState: modelState);
        }

        protected ObjectResult AccessDenied(string value = null)
        {
            return StatusCode(StatusCodes.Status403Forbidden, "You are not allowed to access this resource!");
        }
        public override ObjectResult StatusCode(int statusCode, object value)
        {
            var data = CreateResponse(value, statusCode);
            return base.StatusCode(statusCode, data);
        }
        public override BadRequestObjectResult BadRequest(object value)
        {
            var data = CreateResponse(value, StatusCodes.Status400BadRequest);
            return base.BadRequest(data);
        }
        public override UnauthorizedObjectResult Unauthorized(object value)
        {
            var data = CreateResponse(value, StatusCodes.Status401Unauthorized);
            return base.Unauthorized(data);
        }
        public override NotFoundObjectResult NotFound(object value)
        {
            var data = CreateResponse(value, StatusCodes.Status404NotFound);
            return base.NotFound(data);
        }
        public override OkResult Ok()
        {
            return base.Ok();
        }

        #region Exception return 
        public override BadRequestResult BadRequest()
        {
            throw new RealitycsException("Not supported");
        }
        public override ForbidResult Forbid()
        {
            throw new RealitycsException("Not supported");
        }
        public override NoContentResult NoContent()
        {
            throw new RealitycsException("Not supported");
        }
        public override NotFoundResult NotFound()
        {
            throw new RealitycsException("Not supported");
        }
        #endregion

        public static BadRequestObjectResult RealitycsBadRequest(ModelStateDictionary modelState)
        {
            var problems = CreateResponse(null, StatusCodes.Status400BadRequest, modelState);
            return new BadRequestObjectResult(problems)
            {
                ContentTypes = { RealitycsConstants.MimeTypes.ApplicationProblemJson, RealitycsConstants.MimeTypes.ApplicationProblemXml }
            };
        }
        
        private static ApiResponse<object> CreateResponse(
            object result, int statusCode, ModelStateDictionary modelState = null)
        {
            //  var notificationService = CommunityEngineContext.Current.Resolve<ICommunityResponseMessageService>();
            //var messages = notificationService.GetMessages();
            List<ValidationError> messages = null;
            if (modelState != null)
            {
                messages = new List<ValidationError>();
                var errors = modelState.Keys
                .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                .ToList();
                messages.AddRange(errors);
            }
            return new ApiResponse<object>()
            {
                StatusCode=statusCode,
                Data = result,
                IsSuccess = statusCode == StatusCodes.Status200OK ? true : false,
                Error = messages
            };
        }
    }
    public class ValidationError
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }

        public string Message { get; }

        public ValidationError(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;

        }
    }
}

