using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealityCS.Web.Utility
{
    public class CompanyNameConstraint : IRouteConstraint
    {
        private readonly string[] validOptions;
        public CompanyNameConstraint(string options)
        {
            validOptions = options.Split('|');
        }

        public bool Match(HttpContext httpContext, IRouter route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            object value;
            if (values.TryGetValue(parameterName, out value) && value != null)
            {
                return !validOptions.Contains(value.ToString(), StringComparer.OrdinalIgnoreCase);
            }
            return true;
        }       
    }

    public class LoginTypeConstraint : IRouteConstraint
    {
        private readonly string[] validOptions;
        public LoginTypeConstraint(string options)
        {
            validOptions = options.Split('|');
        }

        public bool Match(HttpContext httpContext, IRouter route, string parameterName, RouteValueDictionary LoginType, RouteDirection routeDirection)
        {
            object value;
            if (LoginType.TryGetValue(parameterName, out value) && value != null)
            {
                return validOptions.Contains(value.ToString(), StringComparer.OrdinalIgnoreCase);
            }
            return false;
        }
    }
}
