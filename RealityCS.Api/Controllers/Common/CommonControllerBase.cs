using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealityCS.Api.Controllers.Common
{
    //[ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/common/[controller]/[action]")]
    public class CommonControllerBase:ApiControllerBase
    {
    }
}
