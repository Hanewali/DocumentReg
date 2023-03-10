using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using DocumentRegistry.Web.ApiModels;
using DocumentRegistry.Web.Models.Employee;

namespace DocumentRegistry.Web.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        
        private readonly HttpClient _apiClient;

        public EmployeeService()
        {
            _apiClient = ApiHelper.PrepareClient("Employee");
        }
        
        public IEnumerable<Employee> Search(int beginFrom, int rows, int userId)
        {
            var response = _apiClient.GetAsync($"GetList?beginFrom={beginFrom}&rows={rows}").Result.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<IEnumerable<Employee>>(response);
        }

        public IEnumerable<NameSearchResponse> Search(string employeeName, int userId)
        {
            var response = _apiClient.GetAsync($"Search?employeeName={employeeName}").Result.Content.ReadAsStringAsync().Result;
            return NameSearchResponse.BuildFromDictionaryJson(response);
        }
        
        public IEnumerable<Employee> Search(Employee employee, int beginFrom, int rows, int userId)
        {
            var request = PrepareRequestModel(employee, beginFrom, rows, userId);
            
            var jsonRequest = JsonSerializer.Serialize(request);
            
            var response = _apiClient.PostAsync("Search", new StringContent(jsonRequest, Encoding.UTF8, "application/json")).Result.Content.ReadAsStringAsync().Result;
            
            return JsonSerializer.Deserialize<IEnumerable<Employee>>(response);
        }

        public Employee GetDetails(int id, int userId)
        {
            var response = _apiClient.GetAsync($"GetDetails?Id={id}").Result.Content.ReadAsStringAsync().Result;

            return JsonSerializer.Deserialize<Employee>(response);
        }

        public void Create(Employee employee, int userId)
        {
            var request = PrepareRequestModel(employee, userId);

            var jsonRequest = JsonSerializer.Serialize(request);

            var result = _apiClient.PostAsync("Create", new StringContent(jsonRequest, Encoding.UTF8, "application/json")).Result;

            if (!result.IsSuccessStatusCode) throw new Exception("Error during creating an object");
        }

        public void Edit(Employee employee, int userId)
        {
            var request = PrepareRequestModel(employee, userId);

            var jsonRequest = JsonSerializer.Serialize(request);

            var result = _apiClient.PostAsync("Edit", new StringContent(jsonRequest, Encoding.UTF8, "application/json")).Result;

            if (!result.IsSuccessStatusCode) throw new Exception("Error during editing an object");
        }

        public void Delete(int id, int userId)
        {
            var request = PrepareRequestModel(id, userId);

            var jsonRequest = JsonSerializer.Serialize(request);

            var result = _apiClient.PostAsync("Delete", new StringContent(jsonRequest, Encoding.UTF8, "application/json")).Result;

            if (!result.IsSuccessStatusCode) throw new Exception("Error during deleting an object");
        }
        
        private EmployeeRequest PrepareRequestModel(int id, int userId)
        {
            return new()
            {
                UserId = userId,
                Employee = new Employee {Id = id}
            };
        }
        
        private EmployeeRequest PrepareRequestModel(Employee employee, int userId)
        {
            return new()
            {
                UserId = userId,
                Employee = employee
            };
        }
        
        private EmployeeRequest PrepareRequestModel(Employee employee, int beginFrom, int rows, int userId)
        {
            return new()
            {
                UserId = userId,
                BeginFrom = beginFrom,
                Rows = rows,
                Employee = employee
            };
        }
    }
}