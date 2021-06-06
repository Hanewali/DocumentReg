using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using Dapper;
using DocumentRegistry.Api.ApiModels.DocumentType;
using DocumentRegistry.Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DocumentRegistry.Api.Controllers
{
    [ApiController] 
    [Route("[controller]/[action]")]  
    public class DocumentTypeController : Controller
    {
        private readonly ILogger<DocumentTypeController> _logger;

        public DocumentTypeController(ILogger<DocumentTypeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetList([FromQuery] int? beginFrom, [FromQuery] int? rows)
        {
            var result = new List<DocumentType>();

            try
            {
                var queryResult = DatabaseHelper.GetAll<DomainModels.DocumentType>();
                result.AddRange(queryResult
                    .Where(x => x.IsActive == true)
                    .Skip(beginFrom ?? 0)
                    .Take(rows ?? 10)
                    .Select(DocumentType.BuildFromDomainModel));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error during GetList");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }
      
            return Ok(JsonSerializer.Serialize(result));
        }

        [HttpPost]
        public IActionResult Search(DocumentTypeRequest model)
        {
            var result = new List<DocumentType>();

            try
            {
                var queryResult = DatabaseHelper.ExecProcedure<DomainModels.DocumentType>("CompanySearch", model.DocumentType.ToDynamicParameters());
                result.AddRange(queryResult
                    .Skip(model.BeginFrom ?? 0)
                    .Take(model.Rows ?? 10)
                    .Select(DocumentType.BuildFromDomainModel));
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
            var result = new DomainModels.DocumentType();
            try
            {
                result = DatabaseHelper.Get<DomainModels.DocumentType>(Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error during Search");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }

            return Ok(JsonSerializer.Serialize(result));
        }
        
        [HttpPost]
        public IActionResult Create(DocumentTypeRequest model)
        {
            try
            {
                DatabaseHelper.Insert(model.DocumentType.ToDomainModel(model.UserId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error during Create");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult Edit(DocumentTypeRequest model)
        {
            try
            {
                var existingDocumentType = new DomainModels.DocumentType();
                if (model.DocumentType.Id != null)
                {
                    existingDocumentType = DatabaseHelper.Get<DomainModels.DocumentType>(model.DocumentType.Id.Value);
                }
                
                var result = DatabaseHelper.Update(model.DocumentType.ToDomainModel(model.UserId, existingDocumentType));
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
        public IActionResult Delete(DocumentTypeRequest model)
        {
            try
            {
                // var parameters = new DynamicParameters();
                //
                // parameters.Add("Id", model.DocumentType.Id);
                // parameters.Add("UserId", model.UserId);
                //
                // DatabaseHelper.ExecuteNoResult("DeleteCompany", parameters);

                var domainModel = DatabaseHelper.Get<DomainModels.DocumentType>(model.DocumentType.Id.Value);

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