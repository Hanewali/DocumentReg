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
        [Computed]
        public string EmployeeFullName { get; set; }

        public int CompanyId { get; set; }
        public string CompanyName { get; set; }

        public int DocumentTypeId { get; set; }
        [Computed]
        public string DocumentTypeName { get; set; }

        public int DocumentDirectionId { get; set; }
        [Computed]
        public string DocumentDirectionName { get; set; }

        public string Other { get; set; }
        
        public bool PR { get; set; }
        public bool PO { get; set; }
        public string CompanyStreet { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyPostalCode { get; set; }
        public string CompanyPostName { get; set; }
        public string CompanyPostStreet { get; set; }
        public string CompanyPostCity { get; set; }
        public string CompanyPostPostalCode { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }

    }
}