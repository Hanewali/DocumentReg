using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using Dapper;
using DocumentRegistry.Api.ApiModels.Letter;
using DocumentRegistry.Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DocumentRegistry.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]  
    public class LetterController : Controller
    {
        private readonly ILogger<LetterController> _logger;

        public LetterController(ILogger<LetterController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetList([FromQuery] int? beginFrom, [FromQuery] int? rows)
        {
            var result = new List<Letter>();

            try
            {
                var queryResult = DatabaseHelper.GetAll<DomainModels.Letter>();
                result.AddRange(queryResult
                    .Skip(beginFrom ?? 0)
                    .Take(rows ?? 10)
                    .Select(Letter.BuildFromDomainModel));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error during GetList");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }
      
            return Ok(JsonSerializer.Serialize(result));
        }

        [HttpPost]
        public IActionResult Search(LetterRequest model)
        {
            var result = new List<Letter>();

            try
            {
                var queryResult = DatabaseHelper.ExecProcedure<DomainModels.Letter>("CompanySearch", model.Letter.ToDynamicParameters());
                result.AddRange(queryResult
                    .Skip(model.BeginFrom ?? 0)
                    .Take(model.Rows ?? 10)
                    .Select(Letter.BuildFromDomainModel));
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
            var result = new Letter();
            try
            {
                result = DatabaseHelper.Get<Letter>(Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error during Search");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }

            return Ok(JsonSerializer.Serialize(result));
        }
        
        [HttpPost]
        public IActionResult Create(LetterRequest model)
        {
            try
            {
                DatabaseHelper.Insert(model.Letter.ToDomainModel(model.UserId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error during Create");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult Edit(LetterRequest model)
        {
            try
            {
                var existingLetter = new DomainModels.Letter();
                if (model.Letter.Id != null)
                {
                    existingLetter = DatabaseHelper.Get<DomainModels.Letter>(model.Letter.Id.Value);
                }
                
                var result = DatabaseHelper.Update(model.Letter.ToDomainModel(model.UserId, existingLetter));
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
        public IActionResult Delete(LetterRequest model)
        {
            try
            {
                var parameters = new DynamicParameters();
                
                parameters.Add("Id", model.Letter.Id);
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