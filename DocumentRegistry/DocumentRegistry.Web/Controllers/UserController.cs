using Microsoft.AspNetCore.Mvc;

namespace DocumentRegistry.Web.Controllers
{
    public class UserController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}