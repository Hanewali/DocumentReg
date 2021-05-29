using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using Dapper;
using DocumentRegistry.Api.ApiModels.PostCompany;
using DocumentRegistry.Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DocumentRegistry.Api.Controllers
{
    [ApiController] 
    [Route("[controller]/[action]")]  
    public class PostCompanyController : Controller
    {
        private readonly ILogger<PostCompanyController> _logger;

        public PostCompanyController(ILogger<PostCompanyController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetList([FromQuery] int? beginFrom, [FromQuery] int? rows)
        {
            var result = new List<PostCompany>();

            try
            {
                var queryResult = DatabaseHelper.GetAll<DomainModels.PostCompany>();
                result.AddRange(queryResult
                    .Skip(beginFrom ?? 0)
                    .Take(rows ?? 10)
                    .Select(PostCompany.BuildFromDomainModel));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error during GetList");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }
      
            return Ok(JsonSerializer.Serialize(result));
        }

        [HttpPost]
        public IActionResult Search(PostCompanyRequest model)
        {
            var result = new List<PostCompany>();

            try
            {
                var queryResult = DatabaseHelper.ExecProcedure<DomainModels.PostCompany>("CompanySearch", model.PostCompany.ToDynamicParameters());
                result.AddRange(queryResult
                    .Skip(model.BeginFrom ?? 0)
                    .Take(model.Rows ?? 10)
                    .Select(PostCompany.BuildFromDomainModel));
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
            var result = new DomainModels.PostCompany();
            try
            {
                result = DatabaseHelper.Get<DomainModels.PostCompany>(Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error during Search");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }

            return Ok(JsonSerializer.Serialize(result));
        }
        
        [HttpPost]
        public IActionResult Create(PostCompanyRequest model)
        {
            try
            {
                DatabaseHelper.Insert(model.PostCompany.ToDomainModel(model.UserId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error during Create");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult Edit(PostCompanyRequest model)
        {
            try
            {
                var existingPostCompany = new DomainModels.PostCompany();
                if (model.PostCompany.Id != null)
                {
                    existingPostCompany = DatabaseHelper.Get<DomainModels.PostCompany>(model.PostCompany.Id.Value);
                }
                
                var result = DatabaseHelper.Update(model.PostCompany.ToDomainModel(model.UserId, existingPostCompany));
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
        public IActionResult Delete(PostCompanyRequest model)
        {
            try
            {
                var parameters = new DynamicParameters();
                
                parameters.Add("Id", model.PostCompany.Id);
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