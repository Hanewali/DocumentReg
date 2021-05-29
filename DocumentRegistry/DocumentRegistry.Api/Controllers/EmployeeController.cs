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
        public IActionResult Search(RequestModel model)
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
        public IActionResult GetDetails([FromQuery] int Id)
        {
            var result = new Employee();
            try
            {
                result = DatabaseHelper.Get<Employee>(Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error during Search");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }

            return Ok(JsonSerializer.Serialize(result));
        }
        
        [HttpPost]
        public IActionResult Create(RequestModel model)
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
        public IActionResult Edit(RequestModel model)
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
        public IActionResult Delete(RequestModel model)
        {
            try
            {
                var parameters = new DynamicParameters();
                
                parameters.Add("Id", model.Employee.Id);
                parameters.Add("UserId", model.UserId);

                DatabaseHelper.ExecuteNoResult("DeleteCompany", parameters);
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