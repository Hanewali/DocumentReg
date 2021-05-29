using DocumentRegistry.Web.Models.PostCompany;

namespace DocumentRegistry.Web.ApiModels
{
    public class PostCompanyRequest
    {
        public int UserId { get; set; }
        public int BeginFrom { get; set; }
        public int Rows { get; set; }
        public PostCompany PostCompany { get; set; }
    }
}