using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using DocumentRegistry.Web.ApiModels;
using DocumentRegistry.Web.Models.DocumentType;

namespace DocumentRegistry.Web.Services.DocumentTypeService
{
    public class DocumentTypeService : IDocumentTypeService
    {
        
        private readonly HttpClient _apiClient;

        public DocumentTypeService()
        {
            _apiClient = ApiHelper.PrepareClient("DocumentType");
        }
        
        public IEnumerable<DocumentType> Search(int beginFrom, int rows, int userId)
        {
            var response = _apiClient.GetAsync($"GetList?beginFrom={beginFrom}&rows={rows}").Result.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<IEnumerable<DocumentType>>(response);
        }

        public IEnumerable<DocumentType> Search(DocumentType documentType, int beginFrom, int rows, int userId)
        {
            var request = PrepareRequestModel(documentType, beginFrom, rows, userId);
            
            var jsonRequest = JsonSerializer.Serialize(request);
            
            var response = _apiClient.PostAsync("Search", new StringContent(jsonRequest, Encoding.UTF8, "application/json")).Result.Content.ReadAsStringAsync().Result;
            
            return JsonSerializer.Deserialize<IEnumerable<DocumentType>>(response);
        }

        public DocumentType GetDetails(int id, int userId)
        {
            var response = _apiClient.GetAsync($"GetDetails?id={id}").Result.Content.ReadAsStringAsync().Result;

            return JsonSerializer.Deserialize<DocumentType>(response);
        }

        public void Create(DocumentType documentType, int userId)
        {
            var request = PrepareRequestModel(documentType, userId);

            var jsonRequest = JsonSerializer.Serialize(request);

            var result = _apiClient.PostAsync("Create", new StringContent(jsonRequest, Encoding.UTF8, "application/json")).Result;

            if (!result.IsSuccessStatusCode) throw new Exception("Error during creating an object");
        }

        public void Edit(DocumentType documentType, int userId)
        {
            var request = PrepareRequestModel(documentType, userId);

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
        
        private DocumentTypeRequest PrepareRequestModel(int id, int userId)
        {
            return new()
            {
                UserId = userId,
                DocumentType = new DocumentType {Id = id}
            };
        }
        
        private DocumentTypeRequest PrepareRequestModel(DocumentType company, int userId)
        {
            return new()
            {
                UserId = userId,
                DocumentType = company
            };
        }
        
        private DocumentTypeRequest PrepareRequestModel(DocumentType documentType, int beginFrom, int rows, int userId)
        {
            return new()
            {
                UserId = userId,
                BeginFrom = beginFrom,
                Rows = rows,
                DocumentType = documentType
            };
        }
    }
}