using System.ComponentModel;

namespace DocumentRegistry.Web.Models.Employee
{
    public class Employee
    {
        public int Id { get; set; }
        [DisplayName("Imię")]
        public string FirstName { get; set; }
        [DisplayName("Nazwisko")]
        public string LastName { get; set; }
        [DisplayName("Firma")]
        public Company.Company Company { get; set; }
    }
}