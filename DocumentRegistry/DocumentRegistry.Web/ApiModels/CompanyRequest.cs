using DocumentRegistry.Web.Models.Company;

namespace DocumentRegistry.Web.ApiModels
{
    public class CompanyRequest
    {
        public int UserId { get; set; }
        public int BeginFrom { get; set; }
        public int Rows { get; set; }
        public Company Company { get; set; }
    }
}