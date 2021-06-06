using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using DocumentRegistry.Web.ApiModels;
using DocumentRegistry.Web.Models.Employee;
using DocumentRegistry.Web.Services.CompanyService;
using DocumentRegistry.Web.Services.EmployeeService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

            if (TempData["Error"] != null)
                ModelState.AddModelError("Error", TempData["Error"].ToString());

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
            var viewModel = new Search();
            
            if (TempData["Error"] != null)
                ModelState.AddModelError("Error", TempData["Error"].ToString());
            
            try
            {
                viewModel.Employees = _employeeService.Search(model.Employee, model.BeginFrom, model.Rows, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during employee search");
                ModelState.AddModelError("Error", "Wystąpił błąd podczas wyszukiwania pracowników");
                return View(viewModel);
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var result = new Employee();

            try
            {
                result = _employeeService.GetDetails(id, GetUserIdFromSession());
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

            var companies = _companyService.GetList(GetUserIdFromSession());
            model.Companies = companies.Select(company => new SelectListItem(company.Name, company.Id.ToString()));
            
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateEdit employee)
        {
            try
            {
                _employeeService.Create(employee.ToDomainModel(), GetUserIdFromSession());
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
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CreateEdit employee)
        {
            try
            {
                _employeeService.Edit(employee.ToDomainModel(), GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during employee search");
                return Problem();
            }

            return RedirectToAction("Details", "Employee", new {id = employee.Id});
        }
        
        [HttpGet]
        public IActionResult ConfirmDelete(int id)
        {
            var model = _employeeService.GetDetails(id, GetUserIdFromSession());

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                _employeeService.Delete(id, GetUserIdFromSession());
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