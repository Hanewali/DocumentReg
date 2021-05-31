using System;
using Dapper.Contrib.Extensions;

namespace DocumentRegistry.Api.DomainModels
{
    [Table("Letter")]
    public class Letter : IBaseModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime ModifyDate { get; set; }
        public int? ModifyUserId { get; set; }
        public bool? IsActive { get; set; }
        public int Number { get; set; }
        public int PostCompanyId { get; set; }
        public DateTime Date { get; set; }
        public DateTime ReceiveDate { get; set; }
        public string Content { get; set; }
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public int DocumentTypeId { get; set; }
    }
}