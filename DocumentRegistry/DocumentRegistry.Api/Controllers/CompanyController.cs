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
            var result = DatabaseHelper.ExecProcedure<Company>("GetCompanies");

            return Ok(result);
        }
    }
}