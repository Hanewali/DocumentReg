using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentRegistry.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return HttpContext.Session.GetInt32("UserId") != null 
                ? RedirectToAction("Search", "Letter") 
                : RedirectToAction("Login", "Login");
        }
    }
}