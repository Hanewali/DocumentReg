namespace DocumentRegistry.Api.ApiModels.PostCompany
{
    public class PostCompanyRequest
    {
        public int UserId { get; set; }
        public int? BeginFrom { get; set; }
        public int? Rows { get; set; }
        public PostCompany PostCompany { get; set; }
    }
}