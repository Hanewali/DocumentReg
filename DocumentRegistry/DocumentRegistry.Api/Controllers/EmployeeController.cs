using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using Dapper;
using DocumentRegistry.Api.ApiModels.Employee;
using DocumentRegistry.Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DocumentRegistry.Api.Controllers
{
    [ApiController] 
    [Route("[controller]/[action]")]  
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetList([FromQuery] int? beginFrom, [FromQuery] int? rows)
        {
            var result = new List<Employee>();

            try
            {
                var queryResult = DatabaseHelper.GetAll<DomainModels.Employee>();
                result.AddRange(queryResult
                    .Where(x => x.IsActive == true)
                    .Skip(beginFrom ?? 0)
                    .Take(rows ?? 10)
                    .Select(Employee.BuildFromDomainModel));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error during GetList");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }
      
            return Ok(JsonSerializer.Serialize(result));
        }

        [HttpPost]
        public IActionResult Search(EmployeeRequest model)
        {
            var result = new List<Employee>();

            try
            {
                var queryResult = DatabaseHelper.ExecProcedure<DomainModels.Employee>("CompanySearch", model.Employee.ToDynamicParameters());
                result.AddRange(queryResult
                    .Skip(model.BeginFrom ?? 0)
                    .Take(model.Rows ?? 10)
                    .Select(Employee.BuildFromDomainModel));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error during Search");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }

            return Ok(JsonSerializer.Serialize(result));
        }

        [HttpGet]
        public IActionResult Search(string employeeName)
        {
            var result = new Dictionary<string, string>();

            try
            {
                result = DatabaseHelper.GetAll<DomainModels.Employee>()
                    .Where(x => x.FullName.ToLower().Contains(employeeName))
                    .ToDictionary(employee => employee.Id.ToString(), employee => employee.FullName);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            return Ok(JsonSerializer.Serialize(result));
        }
        
        [HttpGet]
        public IActionResult GetDetails([FromQuery] int Id)
        {
            var result = new Employee();
            try
            {
                var queryResult = DatabaseHelper.Get<DomainModels.Employee>(Id);
                result = Employee.BuildFromDomainModel(queryResult);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error during Search");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }

            return Ok(JsonSerializer.Serialize(result));
        }
        
        [HttpPost]
        public IActionResult Create(EmployeeRequest model)
        {
            try
            {
                DatabaseHelper.Insert(model.Employee.ToDomainModel(model.UserId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error during Create");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult Edit(EmployeeRequest model)
        {
            try
            {
                var existingEmployee = new DomainModels.Employee();
                if (model.Employee.Id != null)
                {
                    existingEmployee = DatabaseHelper.Get<DomainModels.Employee>(model.Employee.Id.Value);
                }
                
                var result = DatabaseHelper.Update(model.Employee.ToDomainModel(model.UserId, existingEmployee));
                if (!result)
                    throw new Exception("Record wasn't updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error during Edit");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult Delete(EmployeeRequest model)
        {
            try
            {
                // var parameters = new DynamicParameters();
                //
                // parameters.Add("Id", model.Employee.Id);
                // parameters.Add("UserId", model.UserId);
                //
                // DatabaseHelper.ExecuteNoResult("DeleteCompany", parameters);

                var domainModel = DatabaseHelper.Get<DomainModels.Employee>(model.Employee.Id.Value);

                domainModel.IsActive = false;
                domainModel.ModifyDate = DateTime.Now;
                domainModel.ModifyUserId = model.UserId;

                DatabaseHelper.Update(domainModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error during Delete");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }

            return Ok();
        }
    }
}