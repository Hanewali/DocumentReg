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
        public IEnumerable<SelectListItem> Companies { get; set; }
        [DisplayName("Firma")]
        public int SelectedCompanyId { get; set; }

        public Employee ToDomainModel()
        {
            return new()
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                KeyCompanyId = SelectedCompanyId
            };
        }
        
        public static CreateEdit FromDomainModel(Employee employee, IEnumerable<Company.Company> companies)
        {
            var model = new CreateEdit
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Companies = companies.ToList().ConvertAll(x => new SelectListItem
                {
                    Text = x.Name, Value = x.Id.ToString(), Selected = false
                }),
                SelectedCompanyId = employee.KeyCompanyId
            };

            model.Companies.First(x => x.Value == model.SelectedCompanyId.ToString()).Selected = true;

            return model;
        }
    }
}