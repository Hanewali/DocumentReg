﻿using System;
using System.Collections.Generic;
using System.Text.Json;
using DocumentRegistry.Web.ApiModels;
using DocumentRegistry.Web.Exceptions;
using DocumentRegistry.Web.Models.Letter;
using DocumentRegistry.Web.Services.CompanyService;
using DocumentRegistry.Web.Services.DocumentTypeService;
using DocumentRegistry.Web.Services.EmployeeService;
using DocumentRegistry.Web.Services.LetterService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DocumentRegistry.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class LetterController : BaseController
    {
        private readonly ILogger<LetterController> _logger;
        private static ILetterService _letterService;
        private static ICompanyService _companyService;
        private static IEmployeeService _employeeService;
        private static IDocumentTypeService _documentTypeService;

        public LetterController(ILetterService letterService, ILogger<LetterController> logger, ICompanyService companyService, IEmployeeService employeeService, IDocumentTypeService documentTypeService)
        {
            _letterService = letterService;
            _logger = logger;
            _companyService = companyService;
            _employeeService = employeeService;
            _documentTypeService = documentTypeService;
        }

        [HttpGet]
        public IActionResult Search()
        {
            var model = new Search();

            if (TempData["Error"] != null)
                ModelState.AddModelError("Error", TempData["Error"].ToString());

            try
            {
                model.Letters = _letterService.Search(0, 10, GetUserIdFromSession());
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
        public IActionResult Search(LetterRequest model)
        {
            var viewModel = new Search();
            
            if (TempData["Error"] != null)
                ModelState.AddModelError("Error", TempData["Error"].ToString());
            
            try
            {
                viewModel.Letters = _letterService.Search(model.Letter, model.BeginFrom, model.Rows, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during letter search");
                ModelState.AddModelError("Error", "Wystąpił błąd podczas wyszukiwania dokumentów");
                return View(viewModel);
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var result = new Letter();

            if (id == 0)
                throw new ObjectNotFoundException("Nie ma takiego dokumentu!");

            if (TempData["Error"] != null) 
                ModelState.AddModelError("Error", TempData["Error"].ToString());
             
            try
            {
                result = _letterService.GetDetails(id, GetUserIdFromSession());
                
                if (result == null)
                    throw new ObjectNotFoundException("Nie ma takiej firmy!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during letter search");
                TempData.Add("Error", "Wystąpił błąd podczas pobierania danych dokumentu");
                return RedirectToAction("Search","Letter");
            }

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            // var model = new CreateEdit();
            //          
            //
            // if (TempData["Error"] != null)
            //     ModelState.AddModelError("Error", TempData["Error"].ToString());
            //
            //
            // return View(model);
            TempData.Add("Error", "Strona będzie dostępna w przyszłości!");
            return RedirectToAction("Search", "Letter");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateEdit model)
        {
            // try
            // {
            //     _letterService.Create(model, GetUserIdFromSession());
            // }
            // catch (Exception ex)
            // {
            //     _logger.LogError(ex, "There was an error during letter search");
            //     TempData.Add("Error", "Wystąpił błąd podczas tworzenia dokumentu");
            //     return View(model);
            // }
            TempData.Add("Error", "Strona będzie dostępna w przyszłości!");
            return RedirectToAction("Search", "Letter");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            // var letter = _letterService.GetDetails(id, GetUserIdFromSession());
            //
            // if (TempData["Error"] != null)
            //     ModelState.AddModelError("Error", TempData["Error"].ToString());
            //
            // var model = CreateEdit.BuildFromModel(letter);
            // return View(model);

            TempData.Add("Error", "Strona będzie dostępna w przyszłości!");
            return RedirectToAction("Search", "Letter");

        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Letter letter)
        {
            // try
            // {
            //     _letterService.Edit(letter, GetUserIdFromSession());
            // }
            // catch (Exception ex)
            // {
            //     _logger.LogError(ex, "There was an error during letter search");
            //     TempData.Add("Error", "Wystąpił błąd podczas edycji dokumentu");
            //      return RedirectToAction("ConfirmDelete", "User", new {id});
            // }
            //
            // return RedirectToAction("Details", "Letter", letter.Id);
            TempData.Add("Error", "Strona będzie dostępna w przyszłości!");
            return RedirectToAction("Search", "Letter");
        }
        
        [HttpGet]
        public IActionResult ConfirmDelete(int id)
        {
            var model = _letterService.GetDetails(id, GetUserIdFromSession());
            
            if (TempData["Error"] != null)
                ModelState.AddModelError("Error", TempData["Error"].ToString());
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                _letterService.Delete(id, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during letter delete");
                
                TempData.Add("Error", "Wystąpił błąd podczas usuwania dokumentu");
                return RedirectToAction("ConfirmDelete", "User", new {id});
            }

            return RedirectToAction("Search", "Letter");
        }
    }
}