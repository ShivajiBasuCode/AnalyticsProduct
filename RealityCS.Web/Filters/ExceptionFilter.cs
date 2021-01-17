using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealityCS.Web.Filters
{
    public class ExceptionFilter: ExceptionFilterAttribute
    {       
      
        public override void OnException(ExceptionContext context)
        {           
            base.OnException(context);
        }
    }
}
