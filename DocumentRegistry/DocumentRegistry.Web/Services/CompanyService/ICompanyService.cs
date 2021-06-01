using System.Collections.Generic;
using DocumentRegistry.Web.Models;
using DocumentRegistry.Web.Models.Company;

namespace DocumentRegistry.Web.Services.CompanyService
{
    public interface ICompanyService
    {
        IEnumerable<Company> GetList(int userId);
        IEnumerable<Company> Search(int beginFrom, int rows, int userId);
        IEnumerable<Company> Search(Company company, int beginFrom, int rows, int userId);
        Company GetDetails(int companyId, int userId); 
        void Create(Company company, int userId);
        void Edit(Company company, int userId);
        void Delete(int companyId, int userId);
    }
}