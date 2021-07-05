using Dapper.Contrib.Extensions;

namespace DocumentRegistry.Api.DomainModels
{
    [Table("DocumentDirection")]
    public class DocumentDirection
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}