using System.Net;
using DocumentRegistry.Web.Services.CompanyService;
using Microsoft.AspNetCore.Mvc;

namespace DocumentRegistry.Web.Controllers
{
    [Route("[controller]")]
    public class CompanyController : Controller
    {
        private static ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = _companyService.PrepareIndexModel();
            return View(model);
        }
    }
}