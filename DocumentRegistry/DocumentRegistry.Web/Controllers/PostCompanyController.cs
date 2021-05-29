using Microsoft.AspNetCore.Mvc;

namespace DocumentRegistry.Web.Controllers
{
    public class PostCompanyController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}