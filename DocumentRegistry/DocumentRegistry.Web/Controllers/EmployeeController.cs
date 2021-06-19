using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using DocumentRegistry.Web.ApiModels;
using DocumentRegistry.Web.Exceptions;
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
                _logger.LogError(ex, "There was an error during employee search");
                ModelState.AddModelError("Error", "Wystąpił błąd podczas wyszukiwania pracowników");
                return View(model);
            }

            return View(model);
        }
        
        [HttpGet]
        public IActionResult SearchNames([FromQuery(Name = "q")]string employeeName)
        {
            var employees = new List<NameSearchResponse>();

            try
            {
                employees = _employeeService.Search(employeeName, GetUserIdFromSession()).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            return Ok(employees);
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
        public IActionResult GetDetails([FromQuery] int id)
        {
            var result = new Employee();
            try
            {
                result = _employeeService.GetDetails(id, GetUserIdFromSession());
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "There was an error during employee getDetails");
                TempData.Add("Error", "Wystąpił błąd podczas pobierania danych pracownika");
            }

            return Ok(result);
        }
        
        
        
        [HttpGet]
        public IActionResult Details(int id)
        {
            var result = new Employee();

            if (TempData["Error"] != null)
                ModelState.AddModelError("Error", TempData["Error"].ToString());
            
            if (id == 0)
                throw new ObjectNotFoundException("Nie ma takiego pracownika!");

            try
            {
                result = _employeeService.GetDetails(id, GetUserIdFromSession());

                if (result == null) throw new ObjectNotFoundException("Nie ma takiego pracownika!");
            }
            catch (ObjectNotFoundException ex)
            {
                TempData.Add("Error", ex.Message);
                return RedirectToAction("Search","Employee");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during employee search");
                TempData.Add("Error", "Wystąpił błąd podczas pobierania danych pracownika");
                return RedirectToAction("Search","Employee");
            }

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateEdit();

            if (TempData["Error"] != null)
                ModelState.AddModelError("Error", TempData["Error"].ToString());
            
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
                ModelState.AddModelError("Error", "Wystąpił błąd podczas tworzenia pracownika");
                return View(employee);
            }

            return RedirectToAction("Search", "Employee");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _employeeService.GetDetails(id, GetUserIdFromSession());

            if (TempData["Error"] != null)
                ModelState.AddModelError("Error", TempData["Error"].ToString());
            
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
                TempData.Add("Error", "Wystąpił błąd podczas edycji pracownika");
                return View(employee);
            }

            return RedirectToAction("Details", "Employee", new {id = employee.Id});
        }
        
        [HttpGet]
        public IActionResult ConfirmDelete(int id)
        {
            var model = _employeeService.GetDetails(id, GetUserIdFromSession());
            
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
                _employeeService.Delete(id, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during employee search");
                TempData.Add("Error", "Wystąpił błąd podczas edycji pracownika");
                return RedirectToAction("ConfirmDelete", "Employee", new {id});
            }

            return RedirectToAction("Search", "Employee");
        }
    }
}