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

        public int CompanyId { get; set; }
        [DisplayName("Firma")]
        public string CompanyName { get; set; }
    }
}