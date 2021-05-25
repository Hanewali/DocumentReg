using DocumentRegistry.Api.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DocumentRegistry.Api.Extensions
{
    public class Controller : Microsoft.AspNetCore.Mvc.Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!Request.IsAuthorized())
                throw new NotAuthorizedException("Wrong authorization token");
        }
    }
}