using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using DocumentRegistry.Web.ApiModels;
using DocumentRegistry.Web.Exceptions;
using DocumentRegistry.Web.Models.PostCompany;
using DocumentRegistry.Web.Services.PostCompanyService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DocumentRegistry.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class PostCompanyController : BaseController
    {
        private readonly ILogger<PostCompanyController> _logger;
        private static IPostCompanyService _postCompanyService;

        public PostCompanyController(IPostCompanyService postCompanyService, ILogger<PostCompanyController> logger)
        {
            _postCompanyService = postCompanyService;
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
                model.PostCompanies = _postCompanyService.Search(0, 10, GetUserIdFromSession());
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "There was an error during company search");
                ModelState.AddModelError("Error", "Wystąpił błąd podczas wyszukiwania firmy");
                return View(model);
            }

            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(PostCompanyRequest model)
        {
            var viewModel = new Search();
            
            if (TempData["Error"] != null)
                ModelState.AddModelError("Error", TempData["Error"].ToString());
            
            try
            {
                viewModel.PostCompanies = _postCompanyService.Search(model.PostCompany, model.BeginFrom, model.Rows, GetUserIdFromSession());
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
        public IActionResult Details(int id)
        {
            var result = new PostCompany();

            if (id == 0)
            {
                throw new ObjectNotFoundException("Nie ma takiej firmy!");
            }

            try
            {
                result = _postCompanyService.GetDetails(id, GetUserIdFromSession());

                if (result == null)
                    throw new ObjectNotFoundException("Nie ma takiej firmy!");
            }
            catch (ObjectNotFoundException ex)
            {
                TempData.Add("Error", ex.Message);
                return RedirectToAction("Search", "PostCompany");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during company search");
                TempData.Add("Error", "Wystąpił błąd podczas pobierania danych firmy");
                return RedirectToAction("Search","PostCompany");
            }

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new PostCompany();

            model.ContractDate = DateTime.Now;
            
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PostCompany company)
        {
            try
            {
                _postCompanyService.Create(company, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during company search");
                TempData.Add("Error", "Wystąpił błąd podczas tworzenia firmy");
                return View(company);
            }

            return RedirectToAction("Search", "PostCompany");
        }

        [HttpGet]
        public IActionResult Edit([FromQuery] int id)
        {
            var model = _postCompanyService.GetDetails(id, GetUserIdFromSession());

            if (TempData["Error"] != null)
                ModelState.AddModelError("Error", TempData["Error"].ToString());

            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PostCompany company)
        {
            try
            {
                _postCompanyService.Edit(company, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during company search");
                return Problem();
            }

            return RedirectToAction("Details", "PostCompany", new {id = company.Id});
        }
        
        [HttpGet]
        public IActionResult ConfirmDelete(int id)
        {
            var model = _postCompanyService.GetDetails(id, GetUserIdFromSession());

            return View(model);
        } 
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                _postCompanyService.Delete(id, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during company search");
                return Problem();
            }

            return RedirectToAction("Search", "PostCompany");
        }
    }
}