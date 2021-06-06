using System;
using Dapper.Contrib.Extensions;

namespace DocumentRegistry.Api.DomainModels
{
    [Table("DocumentType")]
    public class DocumentType
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime ModifyDate { get; set; }
        public int? ModifyUserId { get; set; }
        public bool? IsActive { get; set; }
        public string Name { get; set; }
    }
}