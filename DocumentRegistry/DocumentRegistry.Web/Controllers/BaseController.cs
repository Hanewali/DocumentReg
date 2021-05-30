using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentRegistry.Web.Controllers
{
    public class BaseController : Controller
    {
        protected int GetUserIdFromSession() => HttpContext.Session.GetInt32("UserId").Value;
    }
}