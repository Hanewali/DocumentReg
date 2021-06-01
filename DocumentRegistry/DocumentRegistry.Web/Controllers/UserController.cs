using System;
using System.Collections.Generic;
using System.Text.Json;
using DocumentRegistry.Web.ApiModels;
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
            var searchResult = new List<User>();
            
            try
            {
                searchResult.AddRange(_userService.Search(model.User, model.BeginFrom, model.Rows, GetUserIdFromSession()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during company search");
                return Problem();
            }

            return Ok(JsonSerializer.Serialize(searchResult));
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var result = new User();

            try
            {
                result = _userService.GetDetails(id, GetUserIdFromSession());
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
            var model = new User();

            return View(model);
        }
        
        [HttpPost]
        public IActionResult Create(User company)
        {
            try
            {
                _userService.Create(company, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during company search");
                return Problem();
            }

            return RedirectToAction("Search", "User");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _userService.GetDetails(id, GetUserIdFromSession());

            return View(model);
        }
        
        [HttpPost]
        public IActionResult Edit(User user)
        {
            try
            {
                _userService.Edit(user, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during company search");
                return Problem();
            }

            return RedirectToAction("Details", "User", new {id = user.Id});
        }
        
        [HttpGet]
        public IActionResult ConfirmDelete(int id)
        {
            var model = _userService.GetDetails(id, GetUserIdFromSession());

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                _userService.Delete(id, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during company search");
                return Problem();
            }

            return RedirectToAction("Search", "User");
        }
    }
}