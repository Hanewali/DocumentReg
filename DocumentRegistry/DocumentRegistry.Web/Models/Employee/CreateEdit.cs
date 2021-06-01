using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DocumentRegistry.Web.Models.Employee
{
    public class CreateEdit
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<SelectListItem> Companies { get; set; }
        public int SelectedCompanyId { get; set; }

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