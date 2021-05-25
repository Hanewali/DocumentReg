using DocumentRegistry.Web.Models.Company;

namespace DocumentRegistry.Web.Services.CompanyService
{
    public interface ICompanyService
    {
        Index PrepareIndexModel();
    }
}