using System.ComponentModel;

namespace DocumentRegistry.Web.Models.Company
{
    public class Company : IModel
    {
        public int Id { get; set; }
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [DisplayName("Ulica")]
        public string Street { get; set; }
        [DisplayName("Miasto")]
        public string City { get; set; }
        [DisplayName("Oddział")]
        public string Branch { get; set; }
        [DisplayName("Kod pocztowy")]
        public string PostalCode { get; set; }
        [DisplayName("Nazwa")]
        public string PostName { get; set; }
        [DisplayName("Ulica")]
        public string PostStreet { get; set; }
        [DisplayName("Miasto")]        
        public string PostCity { get; set; }
        [DisplayName("Kod pocztowy")]
        public string PostPostalCode { get; set; }
    }
}