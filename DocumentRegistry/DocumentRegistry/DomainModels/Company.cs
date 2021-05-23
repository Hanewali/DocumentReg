using System;
using System.Reflection;
using Dapper.Contrib.Extensions;

namespace DocumentRegistry.DomainModels
{
    [Table("Company")]
    public class Company : IModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public User CreateUser { get; set; }
        public DateTime ModifyDate { get; set; }
        public User ModifyUser { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Branch { get; set; }
        public string PostalCode { get; set; }
        public string PostName { get; set; }
        public string PostStreet { get; set; }
        public string PostCity { get; set; }
        public string PostPostalCode { get; set; }
    }
}