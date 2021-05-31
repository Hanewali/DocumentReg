using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using DocumentRegistry.Web.ApiModels;
using DocumentRegistry.Web.Models.PostCompany;

namespace DocumentRegistry.Web.Services.PostCompanyService
{
    public class PostCompanyService : IPostCompanyService
    {
        private HttpClient _apiClient;

        public PostCompanyService()
        {
            _apiClient = ApiHelper.PrepareClient("PostCompany");
        }

        public IEnumerable<PostCompany> Search(int beginFrom, int rows, int userId)
        {
            var response = _apiClient.GetAsync("GetList?beginFrom={beginFrom}&rows={rows}").Result.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<IEnumerable<PostCompany>>(response);
        }

        public IEnumerable<PostCompany> Search(PostCompany postCompany, int beginFrom, int rows, int userId)
        {
            var request = PrepareRequestModel(postCompany, beginFrom, rows, userId);
            
            var jsonRequest = JsonSerializer.Serialize(request);
            
            var response = _apiClient.PostAsync("Search", new StringContent(jsonRequest, Encoding.UTF8, "application/json")).Result.Content.ReadAsStringAsync().Result;
            
            return JsonSerializer.Deserialize<IEnumerable<PostCompany>>(response);
        }

        public PostCompany GetDetails(int postCompanyId, int userId)
        {
            var response = _apiClient.GetAsync("GetDetails").Result.Content.ReadAsStringAsync().Result;

            return JsonSerializer.Deserialize<PostCompany>(response);
        }

        public void Create(PostCompany postCompany, int userId)
        {
            var request = PrepareRequestModel(postCompany, userId);

            var jsonRequest = JsonSerializer.Serialize(request);

            var result = _apiClient.PostAsync("Create", new StringContent(jsonRequest, Encoding.UTF8, "application/json")).Result;

            if (!result.IsSuccessStatusCode) throw new Exception("Error during creating an object");
        }

        public void Edit(PostCompany postCompany, int userId)
        {
            var request = PrepareRequestModel(postCompany, userId);

            var jsonRequest = JsonSerializer.Serialize(request);

            var result = _apiClient.PostAsync("Edit", new StringContent(jsonRequest, Encoding.UTF8, "application/json")).Result;

            if (!result.IsSuccessStatusCode) throw new Exception("Error during editing an object");
        }

        public void Delete(int postCompanyId, int userId)
        {
            var request = PrepareRequestModel(postCompanyId, userId);

            var jsonRequest = JsonSerializer.Serialize(request);

            var result = _apiClient.PostAsync("Edit", new StringContent(jsonRequest, Encoding.UTF8, "application/json")).Result;

            if (!result.IsSuccessStatusCode) throw new Exception("Error during deleting an object");
        }
        
        private PostCompanyRequest PrepareRequestModel(int postCompanyId, int userId)
        {
            return new()
            {
                UserId = userId,
                PostCompany = new PostCompany {Id = postCompanyId}
            };
        }
        
        private PostCompanyRequest PrepareRequestModel(PostCompany postCompany, int userId)
        {
            return new()
            {
                UserId = userId,
                PostCompany = postCompany
            };
        }
        
        private PostCompanyRequest PrepareRequestModel(PostCompany postCompany, int beginFrom, int rows, int userId)
        {
            return new()
            {
                UserId = userId,
                BeginFrom = beginFrom,
                Rows = rows,
                PostCompany = postCompany
            };
        }
    }
}