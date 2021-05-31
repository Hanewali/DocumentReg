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
    [Route("[controller]/[action]")]
    public class LetterController : BaseController
    {
        private readonly ILogger<LetterController> _logger;
        private static ILetterService _letterService;

        public LetterController(ILetterService letterService, ILogger<LetterController> logger)
        {
            _letterService = letterService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Search()
        {
            var model = new Search();

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
            var searchResult = new List<Letter>();
            
            try
            {
                searchResult.AddRange(_letterService.Search(model.Letter, model.BeginFrom, model.Rows, GetUserIdFromSession()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during letter search");
                return Problem();
            }

            return Ok(JsonSerializer.Serialize(searchResult));
        }

        [HttpGet]
        public IActionResult Details(int letterId)
        {
            var result = new Letter();

            try
            {
                result = _letterService.GetDetails(letterId, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during letter search");
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
        public IActionResult Create(Letter letter)
        {
            try
            {
                _letterService.Create(letter, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during letter search");
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
        public IActionResult Edit(Letter letter)
        {
            try
            {
                _letterService.Edit(letter, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during letter search");
                return Problem();
            }

            return RedirectToAction("Details", "Letter", letter.Id);
        }
        
        [HttpGet]
        public IActionResult Delete()
        {
            var model = new Letter();

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int letterId)
        {
            try
            {
                _letterService.Delete(letterId, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during letter search");
                return Problem();
            }

            return RedirectToAction("Search", "Letter");
        }
    }
}