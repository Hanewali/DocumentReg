using System;
using System.Collections.Generic;
using System.Text.Json;
using DocumentRegistry.Web.ApiModels;
using DocumentRegistry.Web.Models.DocumentType;
using DocumentRegistry.Web.Services.DocumentTypeService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DocumentRegistry.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class DocumentTypeController : BaseController
    {
        private readonly ILogger<DocumentTypeController> _logger;
        private static IDocumentTypeService _companyService;

        public DocumentTypeController(IDocumentTypeService companyService, ILogger<DocumentTypeController> logger)
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
        public IActionResult Search(DocumentTypeRequest model)
        {
            var searchResult = new List<DocumentType>();
            
            try
            {
                searchResult.AddRange(_companyService.Search(model.DocumentType, model.BeginFrom, model.Rows, GetUserIdFromSession()));
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
            var result = new DocumentType();

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
            var model = new DocumentType();

            return View(model);
        }
        
        [HttpPost]
        public IActionResult Create(DocumentType company)
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

            return RedirectToAction("Search", "DocumentType");
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var model = new DocumentType();

            return View(model);
        }
        
        [HttpPost]
        public IActionResult Edit(DocumentType company)
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

            return RedirectToAction("Details", "DocumentType", company.Id);
        }
        
        [HttpGet]
        public IActionResult Delete()
        {
            var model = new DocumentType();

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

            return RedirectToAction("Search", "DocumentType");
        }
    }
}