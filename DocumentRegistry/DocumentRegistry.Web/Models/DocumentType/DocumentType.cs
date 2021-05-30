using System.ComponentModel;

namespace DocumentRegistry.Web.Models.DocumentType
{
    public class DocumentType
    {
        public int Id { get; set; }
        [DisplayName("Nazwa")]
        public string Name { get; set; }
    }
}