using System.Collections.Generic;
using DocumentRegistry.Web.Models.PostCompany;

namespace DocumentRegistry.Web.Services.PostCompanyService
{
    public interface IPostCompanyService
    {
        IEnumerable<PostCompany> Search(int beginFrom, int rows, int userId);
        IEnumerable<PostCompany> Search(PostCompany postCompany, int beginFrom, int rows, int userId);
        PostCompany GetDetails(int postCompanyId, int userId); 
        void Create(PostCompany model, int userId);
        void Edit(PostCompany model, int userId);
        void Delete(int postCompanyId, int userId);
    }
}