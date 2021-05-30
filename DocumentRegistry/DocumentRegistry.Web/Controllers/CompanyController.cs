using System;
using System.Collections.Generic;
using System.Text.Json;
using DocumentRegistry.Web.ApiModels;
using DocumentRegistry.Web.Models.Company;
using DocumentRegistry.Web.Services.CompanyService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DocumentRegistry.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class CompanyController : BaseController
    {
        private readonly ILogger<CompanyController> _logger;
        private static ICompanyService _companyService;

        public CompanyController(ICompanyService companyService, ILogger<CompanyController> logger)
        {
            _companyService = companyService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Search()
        {
            var model = new Search();

            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(CompanyRequest model)
        {
            var searchResult = new List<Company>();
            
            try
            {
                searchResult.AddRange(_companyService.Search(model.Company, model.BeginFrom, model.Rows, GetUserIdFromSession()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during company search");
                return Problem();
            }

            return Ok(JsonSerializer.Serialize(searchResult));
        }

        [HttpGet]
        public IActionResult Details(int companyId)
        {
            var result = new Company();

            try
            {
                result = _companyService.GetDetails(companyId, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during company search");
                return Problem();
            }

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new Company();

            return View(model);
        }
        
        [HttpPost]
        public IActionResult Create(Company company)
        {
            try
            {
                _companyService.Create(company, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during company search");
                return Problem();
            }

            return RedirectToAction("Search", "Company");
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var model = new Company();

            return View(model);
        }
        
        [HttpPost]
        public IActionResult Edit(Company company)
        {
            try
            {
                _companyService.Edit(company, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during company search");
                return Problem();
            }

            return RedirectToAction("Details", "Company", company.Id);
        }
        
        [HttpGet]
        public IActionResult Delete()
        {
            var model = new Company();

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int companyId)
        {
            try
            {
                _companyService.Delete(companyId, GetUserIdFromSession());
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