using System;
using System.Collections.Generic;
using System.Net.Http;
using DocumentRegistry.Web.Infrastructure;
using DocumentRegistry.Web.Models.Company;
using Index = DocumentRegistry.Web.Models.Company.Index;
using System.Text.Json;


namespace DocumentRegistry.Web.Services.CompanyService
{
    public class CompanyService : ICompanyService
    {
        public Index PrepareIndexModel()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(Configuration.Api.Url)
            };

            client.DefaultRequestHeaders.Add("AuthorizationToken", Configuration.Api.AuthorizationToken);
            var response = client.GetAsync("Company").Result.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<IEnumerable<Company>>(response);
            return new Index(result);
        }
    }
}