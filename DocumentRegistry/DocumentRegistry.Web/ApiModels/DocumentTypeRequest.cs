using DocumentRegistry.Web.Models.DocumentType;

namespace DocumentRegistry.Web.ApiModels
{
    public class DocumentTypeRequest
    {
        public int UserId { get; set; }
        public int BeginFrom { get; set; }
        public int Rows { get; set; }
        public DocumentType DocumentType { get; set; }
    }
}