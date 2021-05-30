using System.Collections.Generic;

namespace DocumentRegistry.Web.Models.Employee
{
    public class Search
    {
        public Employee SearchParameters { get; set; }
        public IEnumerable<Employee> Employees { get; set; }

        public Search()
        {
            SearchParameters = new Employee();
            Employees = new List<Employee>();
        }
    }
}