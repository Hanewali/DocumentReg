using System;
using System.Net.Http;
using DocumentRegistry.Web.Infrastructure;

namespace DocumentRegistry.Web.Services
{
    public static class ApiHelper
    {
        public static HttpClient PrepareClient(string targetArea)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(Configuration.Api.Url + targetArea + "/")
            };
            
            client.DefaultRequestHeaders.Add("AuthorizationToken", Configuration.Api.AuthorizationToken);

            return client;
        }
    }
}