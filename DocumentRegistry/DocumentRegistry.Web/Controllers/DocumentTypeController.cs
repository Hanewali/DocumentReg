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
        private static IDocumentTypeService _documentTypeService;

        public DocumentTypeController(IDocumentTypeService documentTypeService, ILogger<DocumentTypeController> logger)
        {
            _documentTypeService = documentTypeService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Search()
        {
            var model = new Search();

            try
            {
                model.DocumentTypes = _documentTypeService.Search(0, 10, GetUserIdFromSession());
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
        public IActionResult Search(DocumentTypeRequest model)
        {
            var searchResult = new List<DocumentType>();
            
            try
            {
                searchResult.AddRange(_documentTypeService.Search(model.DocumentType, model.BeginFrom, model.Rows, GetUserIdFromSession()));
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
                result = _documentTypeService.GetDetails(companyId, GetUserIdFromSession());
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
                _documentTypeService.Create(company, GetUserIdFromSession());
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
                _documentTypeService.Edit(company, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during company search");
                return Problem();
            }

            return RedirectToAction("Details", "DocumentType", company.Id);
        }
        
        [HttpGet]
        public IActionResult ConfirmDelete(int id)
        {
            var model = _documentTypeService.GetDetails(id, GetUserIdFromSession());

            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int companyId)
        {
            try
            {
                _documentTypeService.Delete(companyId, GetUserIdFromSession());
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