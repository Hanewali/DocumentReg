using System;
using System.Collections.Generic;
using System.Text.Json;
using DocumentRegistry.Web.ApiModels;
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

            try
            {
                model.PostCompanies = _postCompanyService.Search(0, 10, GetUserIdFromSession());
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
        public IActionResult Search(PostCompanyRequest model)
        {
            var searchResult = new List<PostCompany>();
            
            try
            {
                searchResult.AddRange(_postCompanyService.Search(model.PostCompany, model.BeginFrom, model.Rows, GetUserIdFromSession()));
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
            var result = new PostCompany();

            try
            {
                result = _postCompanyService.GetDetails(companyId, GetUserIdFromSession());
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
            var model = new PostCompany();

            return View(model);
        }
        
        [HttpPost]
        public IActionResult Create(PostCompany company)
        {
            try
            {
                _postCompanyService.Create(company, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during company search");
                return Problem();
            }

            return RedirectToAction("Search", "PostCompany");
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var model = new PostCompany();

            return View(model);
        }
        
        [HttpPost]
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

            return RedirectToAction("Details", "PostCompany", company.Id);
        }
        
        [HttpGet]
        public IActionResult Delete()
        {
            var model = new PostCompany();

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int companyId)
        {
            try
            {
                _postCompanyService.Delete(companyId, GetUserIdFromSession());
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