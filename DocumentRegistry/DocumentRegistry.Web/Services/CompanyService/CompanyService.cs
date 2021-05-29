using System;
using System.Collections.Generic;
using System.Net.Http;
using DocumentRegistry.Web.Models.Company;
using System.Text.Json;
using DocumentRegistry.Web.ApiModels;

namespace DocumentRegistry.Web.Services.CompanyService
{
    public class CompanyService : ICompanyService
    {
        private HttpClient _apiClient;

        public CompanyService()
        {
            _apiClient = ApiHelper.PrepareClient("Company/");
        }
        
        public IEnumerable<Company> Search(int beginFrom, int rows, int userId)
        {
            var response = _apiClient.GetAsync("Search").Result.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<IEnumerable<Company>>(response);
        }

        public IEnumerable<Company> Search(Company company, int beginFrom, int rows, int userId)
        {
            var request = PrepareRequestModel(company, beginFrom, rows, userId);
            
            var jsonRequest = JsonSerializer.Serialize(request);
            
            var response = _apiClient.PostAsync("Search", new StringContent(jsonRequest)).Result.Content.ReadAsStringAsync().Result;
            
            return JsonSerializer.Deserialize<IEnumerable<Company>>(response);
        }

        public Company GetDetails(int companyId, int userId)
        {
            var response = _apiClient.GetAsync("GetDetails").Result.Content.ReadAsStringAsync().Result;

            return JsonSerializer.Deserialize<Company>(response);
        }

        public void Create(Company company, int userId)
        {
            var request = PrepareRequestModel(company, userId);

            var jsonRequest = JsonSerializer.Serialize(request);

            var result = _apiClient.PostAsync("Create", new StringContent(jsonRequest)).Result;

            if (!result.IsSuccessStatusCode) throw new Exception("Error during creating an object");
        }

        public void Edit(Company company, int userId)
        {
            var request = PrepareRequestModel(company, userId);

            var jsonRequest = JsonSerializer.Serialize(request);

            var result = _apiClient.PostAsync("Edit", new StringContent(jsonRequest)).Result;

            if (!result.IsSuccessStatusCode) throw new Exception("Error during editing an object");
        }

        public void Delete(int companyId, int userId)
        {
            var request = PrepareRequestModel(companyId, userId);

            var jsonRequest = JsonSerializer.Serialize(request);

            var result = _apiClient.PostAsync("Edit", new StringContent(jsonRequest)).Result;

            if (!result.IsSuccessStatusCode) throw new Exception("Error during deleting an object");
        }

        private CompanyRequest PrepareRequestModel(int companyId, int userId)
        {
            return new CompanyRequest
            {
                UserId = userId,
                Company = new Company {Id = companyId}
            };
        }
        
        private CompanyRequest PrepareRequestModel(Company company, int userId)
        {
            return new CompanyRequest
            {
                UserId = userId,
                Company = company
            };
        }
        
        private CompanyRequest PrepareRequestModel(Company company, int beginFrom, int rows, int userId)
        {
            return new CompanyRequest
            {
                UserId = userId,
                BeginFrom = beginFrom,
                Rows = rows,
                Company = company
            };
        }
    }
}