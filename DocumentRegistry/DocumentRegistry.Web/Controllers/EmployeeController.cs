using Microsoft.AspNetCore.Mvc;

namespace DocumentRegistry.Web.Controllers
{
    public class EmployeeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}