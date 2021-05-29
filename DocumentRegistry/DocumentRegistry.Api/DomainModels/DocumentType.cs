using Dapper.Contrib.Extensions;

namespace DocumentRegistry.Api.DomainModels
{
    [Table("DocumentType")]
    public class DocumentType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}