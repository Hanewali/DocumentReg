using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentRegistry.DomainModels;
using DocumentRegistry.Helpers;
using DocumentRegistry.Models;
using Microsoft.AspNetCore.Mvc;

namespace DocumentRegistry.Controllers
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