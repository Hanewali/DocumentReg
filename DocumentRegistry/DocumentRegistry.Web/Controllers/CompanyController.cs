using System;
using System.Collections.Generic;
using System.Text.Json;
using DocumentRegistry.Web.ApiModels;
using DocumentRegistry.Web.Exceptions;
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
                ModelState.AddModelError("Error", "Wystąpił błąd podczas wyszukiwania firm");
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
                _logger.LogError(ex, "There was an error during company search");
                ModelState.AddModelError("Error", "Wystąpił błąd podczas wyszukiwania firm");
                return View(viewModel);
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Details([FromQuery] int id)
        {
            var result = new Company();

            if (id == 0)
                throw new ObjectNotFoundException("Nie ma takiego dokumentu!");
            
            if (TempData["Error"] != null)
                ModelState.AddModelError("Error", TempData["Error"].ToString());

            try
            {
                result = _companyService.GetDetails(id, GetUserIdFromSession());

                if (result == null)
                    throw new ObjectNotFoundException("Nie ma takiej firmy!");
            }
            catch (ObjectNotFoundException ex)
            {
                ModelState.AddModelError("Error", ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during company search");
                TempData.Add("Error", "Wystąpił błąd podczas pobierania danych dokumentu");
                return RedirectToAction("Search","Company");
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
                _logger.LogError(ex, "There was an error during company create");
                TempData.Add("Error", "Wystąpił błąd podczas tworzenia firmy");
                return View(company);
            }

            return RedirectToAction("Search", "Company");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = new Company();

            try
            {
                model = _companyService.GetDetails(id, GetUserIdFromSession());
                if (model == null) throw new ObjectNotFoundException("Nie znaleziono firmy!");
            }
            catch (ObjectNotFoundException ex)
            {
                TempData.Add("Error", ex.Message);
                return RedirectToAction("Search", "Company");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was na error during company search");
                ModelState.AddModelError("Error", "Wystąpił błąd podczas pobierania danych firmy");
                return RedirectToAction("Search", "Company");
            }

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
                _logger.LogError(ex, "There was an error during company edit");
                ModelState.AddModelError("Error", "Wystąpił błąd podczas edycji danych firmy");
                return View(company);
            }

            return RedirectToAction("Search", "Company");
        }
        
        [HttpGet]
        public IActionResult ConfirmDelete(int id)
        {
            var model = new Company();
            try
            {
                model = _companyService.GetDetails(id, GetUserIdFromSession());
                if (model == null) throw new ObjectNotFoundException("Nie znaleziono danych firmy");
            }
            catch (ObjectNotFoundException ex)
            {
                TempData.Add("Error", ex.Message);
                return RedirectToAction("Search", "Company");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during company confirmDelete");
                TempData.Add("Error", "Wystąpił błąd podczas pobierania danych firmy");
                return RedirectToAction("Details", "Company", new {id});
            }

            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                _companyService.Delete(id, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during company delete");
                TempData.Add("Error", "Wystąpił błąd podczas usuwania firmy");
                return RedirectToAction("ConfirmDelete", "Company", new {id});
            }

            return RedirectToAction("Search", "Company");
        }
    }
}