using System.Collections.Generic;
using DocumentRegistry.Web.ApiModels;
using DocumentRegistry.Web.Models.Employee;

namespace DocumentRegistry.Web.Services.EmployeeService
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> Search(int beginFrom, int rows, int userId);
        IEnumerable<NameSearchResponse> Search(string employeeName, int userId);
        IEnumerable<Employee> Search(Employee employee, int beginFrom, int rows, int userId);
        Employee GetDetails(int employeeId, int userId); 
        void Create(Employee employee, int userID);
        void Edit(Employee employee, int userId);
        void Delete(int employeeID, int userId);
    }
}