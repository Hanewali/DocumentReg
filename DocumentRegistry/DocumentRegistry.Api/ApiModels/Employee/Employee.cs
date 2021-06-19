using System;
using Dapper;
using DocumentRegistry.Api.Helpers;

namespace DocumentRegistry.Api.ApiModels.Employee
{
    public class Employee
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        
        public static Employee BuildFromDomainModel(DomainModels.Employee employee)
        {
            return new()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                CompanyId = employee.CompanyId,
                CompanyName = DatabaseHelper.Get<DomainModels.Company>(employee.CompanyId).Name
            };
        }

        public DomainModels.Employee ToDomainModel(int userId, DomainModels.Employee employee = null)
        {
            employee ??= new DomainModels.Employee();

            if (employee.Id == 0)
            {
                employee.CreateUserId = userId;
                employee.CreateDate = DateTime.Now;
                employee.IsActive = true;
            }

            employee.ModifyUserId = userId;
            employee.ModifyDate = DateTime.Now;
            employee.FirstName = FirstName;
            employee.LastName = LastName;
            employee.CompanyId = CompanyId;
            
            return employee;
        }

        public DynamicParameters ToDynamicParameters()
        {
            var parameters = new DynamicParameters();
            
            parameters.Add("Id", Id);
            parameters.Add("FirstName", FirstName);
            parameters.Add("LastName", LastName);
            parameters.Add("CompanyId", CompanyId);

            return parameters;
        }
    }
}