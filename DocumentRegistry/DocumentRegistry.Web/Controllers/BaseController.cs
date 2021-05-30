using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DocumentRegistry.Web.Controllers
{
    public class BaseController : Controller
    {
        protected int GetUserIdFromSession() => HttpContext.Session.GetInt32("UserId").Value;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                context.Result = new RedirectResult("/");
                return;
            }
            
            base.OnActionExecuting(context);
        }
    }
}