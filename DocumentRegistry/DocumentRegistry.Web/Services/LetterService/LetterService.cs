using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using DocumentRegistry.Web.ApiModels;
using DocumentRegistry.Web.Models.Letter;

namespace DocumentRegistry.Web.Services.LetterService
{
    public class LetterService : ILetterService
    {
        private readonly HttpClient _apiClient;

        public LetterService()
        {
            _apiClient = ApiHelper.PrepareClient("Letter");
        }

        public IEnumerable<Letter> Search(int beginFrom, int rows, int userId)
        {
            var response = _apiClient.GetAsync("Search").Result.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<IEnumerable<Letter>>(response);
        }

        public IEnumerable<Letter> Search(Letter letter, int beginFrom, int rows, int userId)
        {
            var request = PrepareRequestModel(letter, beginFrom, rows, userId);
            
            var jsonRequest = JsonSerializer.Serialize(request);
            
            var response = _apiClient.PostAsync("Search", new StringContent(jsonRequest)).Result.Content.ReadAsStringAsync().Result;
            
            return JsonSerializer.Deserialize<IEnumerable<Letter>>(response);
        }

        public Letter GetDetails(int letterId, int userId)
        {
            var response = _apiClient.GetAsync("GetDetails").Result.Content.ReadAsStringAsync().Result;

            return JsonSerializer.Deserialize<Letter>(response);
        }

        public void Create(Letter letter, int userId)
        {
            var request = PrepareRequestModel(letter, userId);

            var jsonRequest = JsonSerializer.Serialize(request);

            var result = _apiClient.PostAsync("Create", new StringContent(jsonRequest)).Result;

            if (!result.IsSuccessStatusCode) throw new Exception("Error during creating an object");
        }

        public void Edit(Letter letter, int userId)
        {
            var request = PrepareRequestModel(letter, userId);

            var jsonRequest = JsonSerializer.Serialize(request);

            var result = _apiClient.PostAsync("Edit", new StringContent(jsonRequest)).Result;

            if (!result.IsSuccessStatusCode) throw new Exception("Error during editing an object");
        }

        public void Delete(int letterId, int userId)
        {
            var request = PrepareRequestModel(letterId, userId);

            var jsonRequest = JsonSerializer.Serialize(request);

            var result = _apiClient.PostAsync("Edit", new StringContent(jsonRequest)).Result;

            if (!result.IsSuccessStatusCode) throw new Exception("Error during deleting an object");
        }
        
        private LetterRequest PrepareRequestModel(int letterId, int userId)
        {
            return new()
            {
                UserId = userId,
                Letter = new Letter {Id = letterId}
            };
        }
        
        private LetterRequest PrepareRequestModel(Letter letter, int userId)
        {
            return new()
            {
                UserId = userId,
                Letter = letter
            };
        }
        
        private LetterRequest PrepareRequestModel(Letter letter, int beginFrom, int rows, int userId)
        {
            return new()
            {
                UserId = userId,
                BeginFrom = beginFrom,
                Rows = rows,
                Letter = letter
            };
        }
    }
}