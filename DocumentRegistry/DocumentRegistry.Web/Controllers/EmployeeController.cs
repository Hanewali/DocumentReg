using System;
using System.Collections.Generic;
using System.Text.Json;
using DocumentRegistry.Web.ApiModels;
using DocumentRegistry.Web.Models.Employee;
using DocumentRegistry.Web.Services.CompanyService;
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
        private static ICompanyService _companyService;

        public EmployeeController(IEmployeeService employeeService, ICompanyService companyService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _companyService = companyService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Search()
        {
            var model = new Search();

            try
            {
                model.Employees = _employeeService.Search(0, 10, GetUserIdFromSession());
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
            var model = new CreateEdit();

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
        public IActionResult Edit(int id)
        {
            var employee = _employeeService.GetDetails(id, GetUserIdFromSession());

            var model = CreateEdit.FromDomainModel(employee, _companyService.GetList(GetUserIdFromSession())); 

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
        public IActionResult ConfirmDelete(int id)
        {
            var model = _employeeService.GetDetails(id, GetUserIdFromSession());

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