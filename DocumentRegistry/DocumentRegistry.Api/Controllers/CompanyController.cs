using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentRegistry.Api.DomainModels;
using DocumentRegistry.Api.Helpers;
using DocumentRegistry.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace DocumentRegistry.Api.Controllers
{
    [ApiController, Route("[controller]")]   
    public class CompanyController : Controller
    {
        [HttpGet]
        public IEnumerable<Company> GetList(GetCompaniesRequest request)
        {
            
            
            return DatabaseHelper.ExecProcedure<Company>("GetCompanies");
        }
    }
}