using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using DocumentRegistry.Web.ApiModels;
using DocumentRegistry.Web.Exceptions;
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
                ModelState.AddModelError("Error", "Wystąpił błąd podczas wyszukiwania typów dokumentów");
                return View(model);
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

            if (id == 0)
                throw new ObjectNotFoundException("Nie ma takiego dokumentu!");
            
            if (TempData["Error"] != null)
                ModelState.AddModelError("Error", TempData["Error"].ToString());

            try
            {
                result = _documentTypeService.GetDetails(id, GetUserIdFromSession());
                
                if (result == null)
                    throw new ObjectNotFoundException("Nie ma takiej firmy!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during document type search");
                TempData.Add("Error", "Wystąpił błąd podczas pobierania danych dokumentu");
                return RedirectToAction("Search","DocumentType");
            }

            return View(result);
        }

        [HttpGet]
        public IActionResult SearchNames([FromQuery(Name = "q")]string companyName)
        {
            var companies = new List<NameSearchResponse>();

            try
            {
                companies = _documentTypeService.Search(companyName, GetUserIdFromSession()).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            return Ok(companies);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            var model = new DocumentType();
            
            if (TempData["Error"] != null)
                ModelState.AddModelError("Error", TempData["Error"].ToString());


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
            catch (ObjectNotFoundException ex)
            {
                TempData.Add("Error", ex.Message);
                return RedirectToAction("Search", "PostCompany");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during document type search");
                ModelState.AddModelError("Error", "Wystąpił błąd podczas tworzenia typu dokumentu");
                return View(documentType);
            }

            return RedirectToAction("Search", "DocumentType");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = new DocumentType();
            
            if (TempData["Error"] != null)
                ModelState.AddModelError("Error", TempData["Error"].ToString());
            
            try
            {
                model = _documentTypeService.GetDetails(id, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during document type edit");
                TempData.Add("Error", "Wystąpił błąd podczas edycji typu dokumentu");
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
                ModelState.AddModelError("Error", TempData["Error"].ToString());
                return View(documentType);
            }

            return RedirectToAction("Details", "DocumentType", new {id = documentType.Id});
        }
        
        [HttpGet]
        public IActionResult ConfirmDelete(int id)
        {
            var model = new DocumentType();

            if (id == 0)
                throw new ObjectNotFoundException("Nie znaleziono typu dokumentu");
            
            if (TempData["Error"] != null)
                ModelState.AddModelError("Error", TempData["Error"].ToString());

            try
            {
                model = _documentTypeService.GetDetails(id, GetUserIdFromSession());
                if (model == null)
                    throw new ObjectNotFoundException("Nie znaleziono typu dokumentu");
            }
            catch (ObjectNotFoundException ex)
            {
                TempData.Add("Error", ex.Message);
                return RedirectToAction("Details", "DocumentType", new {id});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during document type search.");
                TempData.Add("Error", "Wystąpił błąd podczas pobierania danych typu dokumentu");
                return RedirectToAction("Details", "DocumentType", new {id});
            }

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
                TempData.Add("Error", "Wystąpił błąd podczas pobierania danych typu dokumentu");
                return RedirectToAction("ConfirmDelete", "DocumentType", new {id});
            }

            return RedirectToAction("Search", "DocumentType");
        }
        
        [HttpGet]
        public IActionResult GetDetails([FromQuery] int id)
        {
            var result = new DocumentType();
            try
            {
                result = _documentTypeService.GetDetails(id, GetUserIdFromSession());
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "There was an error during document type getDetails");
                TempData.Add("Error", "Wystąpił błąd podczas pobierania danych typu dokumentu");
            }

            return Ok(result);
        }

    }
}