using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using DocumentRegistry.Api.DomainModels;
using DocumentRegistry.Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DocumentRegistry.Api.Controllers
{
    [ApiController, Route("[controller]")]   
    public class CompanyController : BaseController
    {
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ILogger<CompanyController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            var result = new List<ApiModels.Company>();

            try
            {
                var queryResult = DatabaseHelper.ExecProcedure<Company>("GetCompanies");
                result.AddRange(queryResult.Select(ApiModels.Company.BuildFromDomainModel));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error during CompanyController.GetList", ex);
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }
      
            return Ok(JsonSerializer.Serialize(result));
        }
    }
}