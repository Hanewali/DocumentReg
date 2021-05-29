using System;
using System.Collections.Generic;
using System.Text.Json;
using DocumentRegistry.Web.ApiModels;
using DocumentRegistry.Web.Models;
using DocumentRegistry.Web.Models.Company;
using DocumentRegistry.Web.Services.CompanyService;
using Microsoft.AspNetCore.Http;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(CompanyRequest model)
        {
            var searchResult = new List<Company>();
            
            try
            {
                searchResult.AddRange(_companyService.Search(model.Company, model.BeginFrom, model.Rows, HttpContext.Session.GetInt32("UserId").Value));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during company search");
                return Problem();
            }

            return Ok(JsonSerializer.Serialize(searchResult));
        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            var result = new Company();

            try
            {
                result = _companyService.GetDetails(Id, HttpContext.Session.GetInt32("UserId").Value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during company search");
                return Problem();
            }

            return View(result);
        }
        
        [HttpPost]
        public IActionResult Create(Company company)
        {
            try
            {
                _companyService.Create(company, HttpContext.Session.GetInt32("UserId").Value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during company search");
                return Problem();
            }

            return RedirectToAction("Search", "Company");
        }
    }
}