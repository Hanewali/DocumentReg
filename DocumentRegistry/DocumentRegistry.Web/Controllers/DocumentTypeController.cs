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

            if (TempData["Error"] != null)
                ModelState.AddModelError("Error", TempData["Error"].ToString());

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
            var viewModel = new Search();
            
            if (TempData["Error"] != null)
                ModelState.AddModelError("Error", TempData["Error"].ToString());
            
            try
            {
                viewModel.DocumentTypes = _documentTypeService.Search(model.DocumentType, model.BeginFrom, model.Rows, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during document type search");
                ModelState.AddModelError("Error", "Wystąpił błąd podczas wyszukiwania typów dokumentów");
                return View(viewModel);
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var result = new DocumentType();

            try
            {
                result = _documentTypeService.GetDetails(id, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during document type search");
                //todo: dodać obsługę błędów i p rzekierowanie na search
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
        [ValidateAntiForgeryToken]
        public IActionResult Create(DocumentType documentType)
        {
            try
            {
                _documentTypeService.Create(documentType, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during document type search");
                return Problem();
            }

            return RedirectToAction("Search", "DocumentType");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = new DocumentType();
            
            try
            {
                model = _documentTypeService.GetDetails(id, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during document type edit");
                return RedirectToAction("Details", "DocumentType", new {id});
            }

            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DocumentType documentType)
        {
            try
            {
                _documentTypeService.Edit(documentType, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during document type search");
                //todo dodać obsługę błędó i przekierownaie na search
                return Problem();
            }

            return RedirectToAction("Details", "DocumentType", new {id = documentType.Id});
        }
        
        [HttpGet]
        public IActionResult ConfirmDelete(int id)
        {
            var model = _documentTypeService.GetDetails(id, GetUserIdFromSession());

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                _documentTypeService.Delete(id, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during document type search");
                return Problem();
            }

            return RedirectToAction("Search", "DocumentType");
        }
    }
}