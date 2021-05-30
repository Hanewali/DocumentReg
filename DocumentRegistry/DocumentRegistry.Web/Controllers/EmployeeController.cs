using System;
using System.Collections.Generic;
using System.Text.Json;
using DocumentRegistry.Web.ApiModels;
using DocumentRegistry.Web.Models.Employee;
using DocumentRegistry.Web.Services.EmployeeService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DocumentRegistry.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class EmployeeController : BaseController
    {
        private readonly ILogger<EmployeeController> _logger;
        private static IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
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
        public IActionResult Search(EmployeeRequest model)
        {
            var searchResult = new List<Employee>();
            
            try
            {
                searchResult.AddRange(_employeeService.Search(model.Employee, model.BeginFrom, model.Rows, GetUserIdFromSession()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during employee search");
                return Problem();
            }

            return Ok(JsonSerializer.Serialize(searchResult));
        }

        [HttpGet]
        public IActionResult Details(int employeeId)
        {
            var result = new Employee();

            try
            {
                result = _employeeService.GetDetails(employeeId, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during employee search");
                return Problem();
            }

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new Employee();

            return View(model);
        }
        
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            try
            {
                _employeeService.Create(employee, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during employee search");
                return Problem();
            }

            return RedirectToAction("Search", "Employee");
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var model = new Employee();

            return View(model);
        }
        
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            try
            {
                _employeeService.Edit(employee, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during employee search");
                return Problem();
            }

            return RedirectToAction("Details", "Employee", employee.Id);
        }
        
        [HttpGet]
        public IActionResult Delete()
        {
            var model = new Employee();

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int employeeId)
        {
            try
            {
                _employeeService.Delete(employeeId, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during employee search");
                return Problem();
            }

            return RedirectToAction("Search", "Employee");
        }
    }
}