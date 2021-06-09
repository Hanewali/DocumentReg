using System;
using System.Collections.Generic;
using System.Text.Json;
using DocumentRegistry.Web.ApiModels;
using DocumentRegistry.Web.Exceptions;
using DocumentRegistry.Web.Models.User;
using DocumentRegistry.Web.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DocumentRegistry.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class UserController : BaseController
    {
        private readonly ILogger<UserController> _logger;
        private static IUserService _userService;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
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
                model.Users = _userService.Search(0, 10, GetUserIdFromSession());
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
        public IActionResult Search(UserRequest model)
        {
            var viewModel = new Search();
            
            try
            {
                viewModel.Users = _userService.Search(model.User, model.BeginFrom, model.Rows, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during user search");
                ModelState.AddModelError("Error", "Wystąpił błąd podczas wyszukiwania użytkowników");
                return View(viewModel);
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var result = new User();

            if (id == 0)
            {
                throw new ObjectNotFoundException("Nie ma takiej firmy!");
            }
            
            try
            {
                result = _userService.GetDetails(id, GetUserIdFromSession());
                
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
                TempData.Add("Error", "Wystąpił błąd podczas pobierania danych użytkownika");
                return RedirectToAction("Search","User");
            }

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new User();

            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User company)
        {
            try
            {
                _userService.Create(company, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during company search");
                TempData.Add("Error", "Wystąpił błąd podczas dodawania użytkownika");
                return RedirectToAction("Search", "User");
            }

            return RedirectToAction("Search", "User");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _userService.GetDetails(id, GetUserIdFromSession());

            if (TempData["Error"] != null)
                ModelState.AddModelError("Error", TempData["Error"].ToString());

            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User user)
        {
            try
            {
                _userService.Edit(user, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during company search");
                TempData.Add("Error", "Wystąpił błąd podczas edycji użytkownika");
                return RedirectToAction("Edit", "User", new {id = user.Id});
            }

            return RedirectToAction("Details", "User", new {id = user.Id});
        }
        
        [HttpGet]
        public IActionResult ConfirmDelete(int id)
        {
            var model = _userService.GetDetails(id, GetUserIdFromSession());

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
                _userService.Delete(id, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "User.Delete Error");
                TempData.Add("Error", "Wystąpił błąd podczas usuwania użytkownika");
                return RedirectToAction("ConfirmDelete", "User", new {id});
            }

            return RedirectToAction("Search", "User");
        }
    }
}