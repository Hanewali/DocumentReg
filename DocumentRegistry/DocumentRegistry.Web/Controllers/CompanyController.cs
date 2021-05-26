using System;
using DocumentRegistry.Web.Services.CompanyService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DocumentRegistry.Web.Controllers
{
    [Route("[controller]")]
    public class CompanyController : Controller
    {
        private ILogger<CompanyController> _logger;
        private static ICompanyService _companyService;

        public CompanyController(ICompanyService companyService, ILogger<CompanyController> logger)
        {
            _companyService = companyService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new Models.Company.Index();
            try
            {
                 model = _companyService.PrepareIndexModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error during API request");
                Response.StatusCode = 500;
                
                return View();
            }
            
            return View(model);
        }
    }
}