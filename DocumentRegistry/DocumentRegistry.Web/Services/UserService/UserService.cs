using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using DocumentRegistry.Web.ApiModels;
using DocumentRegistry.Web.Models.User;

namespace DocumentRegistry.Web.Services.UserService
{
    public class UserService : IUserService
    {
        private HttpClient _apiClient;

        public UserService()
        {
            _apiClient = ApiHelper.PrepareClient("User");
        }
        
        public IEnumerable<User> Search(int beginFrom, int rows, int userId)
        {
            var response = _apiClient.GetAsync($"GetList?beginFrom={beginFrom}&rows={rows}").Result.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<IEnumerable<User>>(response);
        }

        public IEnumerable<User> Search(User modifiedUser, int beginFrom, int rows, int userId)
        {
            var request = PrepareRequestModel(modifiedUser, beginFrom, rows, userId);
            
            var jsonRequest = JsonSerializer.Serialize(request);
            
            var response = _apiClient.PostAsync("Search", new StringContent(jsonRequest, Encoding.UTF8, "application/json")).Result.Content.ReadAsStringAsync().Result;
            
            return JsonSerializer.Deserialize<IEnumerable<User>>(response);
        }

        public User GetDetails(int id, int userId)
        {
            var response = _apiClient.GetAsync($"GetDetails?id={id}").Result.Content.ReadAsStringAsync().Result;

            return JsonSerializer.Deserialize<User>(response);
        }

        public void Create(User modifiedUser, int userId)
        {
            var request = PrepareRequestModel(modifiedUser, userId);

            var jsonRequest = JsonSerializer.Serialize(request);

            var result = _apiClient.PostAsync("Create", new StringContent(jsonRequest, Encoding.UTF8, "application/json")).Result;

            if (!result.IsSuccessStatusCode) throw new Exception("Error during creating an object");
        }

        public void Edit(User modifiedUser, int userId)
        {
            var request = PrepareRequestModel(modifiedUser, userId);

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
        
        private UserRequest PrepareRequestModel(int id, int userId)
        {
            return new()
            {
                UserId = userId,
                User = new User {Id = id}
            };
        }
        
        private UserRequest PrepareRequestModel(User user, int userId)
        {
            return new()
            {
                UserId = userId,
                User = user
            };
        }
        
        private UserRequest PrepareRequestModel(User user, int beginFrom, int rows, int userId)
        {
            return new()
            {
                UserId = userId,
                BeginFrom = beginFrom,
                Rows = rows,
                User = user
            };
        }
        
    }
}