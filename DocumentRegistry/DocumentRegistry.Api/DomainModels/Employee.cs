using System;
using Dapper.Contrib.Extensions;

namespace DocumentRegistry.Api.DomainModels
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime ModifyDate { get; set; }
        public int? ModifyUserId { get; set; }
        public bool? IsActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Computed]
        public string FullName => string.Join(" ", FirstName, LastName);
        public int CompanyId { get; set; }
    }
}