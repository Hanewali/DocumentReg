using DocumentRegistry.Api.Exceptions;
using DocumentRegistry.Api.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace DocumentRegistry.Api.Controllers
{
    public class BaseController : Microsoft.AspNetCore.Mvc.Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (Request.IsAuthorized()) return;
            
            Log.Error("Wrong authorization token!");
            throw new NotAuthorizedException("Wrong authorization token");
        }
    }
}