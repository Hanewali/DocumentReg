using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using DocumentRegistry.Api.DomainModels;
using DocumentRegistry.Api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DocumentRegistry.Api.Controllers
{
    [ApiController, Route("[controller]")]   
    public class CompanyController : Extensions.Controller
    {
        [HttpGet]
        public IActionResult GetList()
        {
            var result = new List<ApiModels.Company>();
            
            var queryResult = DatabaseHelper.ExecProcedure<Company>("GetCompanies");
            result.AddRange(queryResult.Select(ApiModels.Company.BuildFromDomainModel));
      
            return Ok(JsonSerializer.Serialize(result));
        }
    }
}