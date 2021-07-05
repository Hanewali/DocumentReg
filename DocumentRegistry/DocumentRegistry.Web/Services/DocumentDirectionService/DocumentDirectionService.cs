using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using DocumentRegistry.Web.Controllers;
using DocumentRegistry.Web.Models.DocumentDirection;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DocumentRegistry.Web.Services.DocumentDirectionService
{
    public class DocumentDirectionService : IDocumentDirectionService
    {
        private readonly HttpClient _apiClient;

        public DocumentDirectionService()
        {
            _apiClient = ApiHelper.PrepareClient("DocumentDirection");
        }

        public IEnumerable<DocumentDirection> GetList(int userId)
        {
            var response = _apiClient.GetAsync("GetList").Result.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<IEnumerable<DocumentDirection>>(response); 
        }
    }
}