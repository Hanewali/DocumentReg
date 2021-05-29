namespace DocumentRegistry.Api.ApiModels.DocumentType
{
    public class RequestModel
    {
        public int UserId { get; set; }
        public int? BeginFrom { get; set; }
        public int? Rows { get; set; }
        public DocumentType DocumentType { get; set; }
    }
}