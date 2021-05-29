using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using Dapper;
using DocumentRegistry.Api.ApiModels.Company;
using DocumentRegistry.Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DocumentRegistry.Api.Controllers
{
    [ApiController] 
    [Route("[controller]/[action]")]  
    public class CompanyController : BaseController
    {
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ILogger<CompanyController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetList([FromQuery] int? beginFrom, [FromQuery] int? rows)
        {
            var result = new List<Company>();

            try
            {
                var queryResult = DatabaseHelper.GetAll<DomainModels.Company>();
                result.AddRange(queryResult
                    .Skip(beginFrom ?? 0)
                    .Take(rows ?? 10)
                    .Select(Company.BuildFromDomainModel));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error during GetList");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }
      
            return Ok(JsonSerializer.Serialize(result));
        }

        [HttpPost]
        public IActionResult Search(CompanyRequest model)
        {
            var result = new List<Company>();

            try
            {
                var queryResult = DatabaseHelper.ExecProcedure<DomainModels.Company>("CompanySearch", model.Company.ToDynamicParameters());
                result.AddRange(queryResult
                    .Skip(model.BeginFrom ?? 0)
                    .Take(model.Rows ?? 10)
                    .Select(Company.BuildFromDomainModel));
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
            var result = new DomainModels.Company();
            try
            {
                result = DatabaseHelper.Get<DomainModels.Company>(Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error during Search");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }

            return Ok(JsonSerializer.Serialize(result));
        }
        
        [HttpPost]
        public IActionResult Create(CompanyRequest model)
        {
            try
            {
                DatabaseHelper.Insert(model.Company.ToDomainModel(model.UserId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error during Create");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult Edit(CompanyRequest model)
        {
            try
            {
                var existingCompany = new DomainModels.Company();
                if (model.Company.Id != null)
                {
                    existingCompany = DatabaseHelper.Get<DomainModels.Company>(model.Company.Id.Value);
                }
                
                var result = DatabaseHelper.Update(model.Company.ToDomainModel(model.UserId, existingCompany));
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
        public IActionResult Delete(CompanyRequest model)
        {
            try
            {
                var parameters = new DynamicParameters();
                
                parameters.Add("Id", model.Company.Id);
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