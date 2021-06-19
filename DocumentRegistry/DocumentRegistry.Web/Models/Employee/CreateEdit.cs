using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DocumentRegistry.Web.Models.Employee
{
    public class CreateEdit
    {
        public int Id { get; set; }
        [DisplayName("Imię")]
        public string FirstName { get; set; }
        [DisplayName("Nazwisko")]
        public string LastName { get; set; }
        [DisplayName("Firma")]
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public Employee ToDomainModel()
        {
            return new()
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                CompanyId = CompanyId
            };
        }
        
        public static CreateEdit FromDomainModel(Employee employee, IEnumerable<Company.Company> companies)
        {
            var model = new CreateEdit
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                CompanyId = employee.CompanyId,
                CompanyName = employee.CompanyName
            };

            return model;
        }
    }
}