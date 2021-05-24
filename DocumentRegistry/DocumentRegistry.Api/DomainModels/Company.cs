using Dapper.Contrib.Extensions;

namespace DocumentRegistry.Api.DomainModels
{
    [Table("Company")]
    public class Company : ModelBase
    {
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