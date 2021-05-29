using Microsoft.AspNetCore.Mvc;

namespace DocumentRegistry.Web.Controllers
{
    public class LetterController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}