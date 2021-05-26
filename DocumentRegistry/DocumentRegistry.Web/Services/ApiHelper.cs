using System;
using System.Net.Http;
using DocumentRegistry.Web.Infrastructure;

namespace DocumentRegistry.Web.Services
{
    public static class ApiHelper
    {
        public static HttpClient PrepareClient()
        {
            return new()
            {
                BaseAddress = new Uri(Configuration.Api.Url)
            };
        }
    }
}