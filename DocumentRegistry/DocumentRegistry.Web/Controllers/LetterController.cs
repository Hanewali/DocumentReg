using System;
using System.Collections.Generic;
using System.Text.Json;
using DocumentRegistry.Web.ApiModels;
using DocumentRegistry.Web.Models.Letter;
using DocumentRegistry.Web.Services.LetterService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DocumentRegistry.Web.Controllers
{
    public class LetterController : BaseController
    {
        private readonly ILogger<LetterController> _logger;
        private static ILetterService _companyService;

        public LetterController(ILetterService companyService, ILogger<LetterController> logger)
        {
            _companyService = companyService;
            _logger = logger;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(LetterRequest model)
        {
            var searchResult = new List<Letter>();
            
            try
            {
                searchResult.AddRange(_companyService.Search(model.Letter, model.BeginFrom, model.Rows, GetUserIdFromSession()));
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
            var result = new Letter();

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
            var model = new Letter();

            return View(model);
        }
        
        [HttpPost]
        public IActionResult Create(Letter company)
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

            return RedirectToAction("Search", "Letter");
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var model = new Letter();

            return View(model);
        }
        
        [HttpPost]
        public IActionResult Edit(Letter company)
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

            return RedirectToAction("Details", "Letter", company.Id);
        }
        
        [HttpGet]
        public IActionResult Delete()
        {
            var model = new Letter();

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

            return RedirectToAction("Search", "Letter");
        }
    }
}