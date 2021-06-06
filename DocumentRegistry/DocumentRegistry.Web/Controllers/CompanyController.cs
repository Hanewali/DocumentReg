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

            if (TempData["Error"] != null)
                ModelState.AddModelError("Error", TempData["Error"].ToString());

            try
            {
                model.Companies = _companyService.Search(0, 10, GetUserIdFromSession());
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "There was an error during company search");
                return Problem();
            }
            
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(CompanyRequest model)
        {
            var viewModel = new Search();
            
            if (TempData["Error"] != null)
                ModelState.AddModelError("Error", TempData["Error"].ToString());
            
            try
            {
                viewModel.Companies = _companyService.Search(model.Company, model.BeginFrom, model.Rows, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during post company search");
                ModelState.AddModelError("Error", "Wystąpił błąd podczas wyszukiwania firm pocztowych");
                return View(viewModel);
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Details([FromQuery] int id)
        {
            var result = new Company();

            try
            {
                result = _companyService.GetDetails(id, GetUserIdFromSession());
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
        [ValidateAntiForgeryToken]
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
        public IActionResult Edit(int id)
        {
            var model = _companyService.GetDetails(id, GetUserIdFromSession());

            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
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

            return RedirectToAction("Details", "Company", new {id = company.Id});
        }
        
        [HttpGet]
        public IActionResult ConfirmDelete(int id)
        {
            var model = _companyService.GetDetails(id, GetUserIdFromSession());

            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int Id)
        {
            try
            {
                _companyService.Delete(Id, GetUserIdFromSession());
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