namespace DocumentRegistry.Api.ApiModels.Company
{
    public class RequestModel
    {
        public int UserId { get; set; }
        public int? BeginFrom { get; set; }
        public int? Rows { get; set; }
        public Company Company { get; set; }
    }
}