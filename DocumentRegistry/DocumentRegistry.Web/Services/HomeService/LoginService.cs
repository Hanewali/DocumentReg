using System;
using System.Buffers.Binary;
using System.Diagnostics.Eventing.Reader;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using DocumentRegistry.Web.Models.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;

namespace DocumentRegistry.Web.Services.HomeService
{
    public class LoginService : ILoginService
    {
        private HttpClient _client;
        private ISessionStore _session;
        
        public LoginService(ISessionStore session)
        {
            _client = ApiHelper.PrepareClient("");
            _session = session;
        }

        public LoginResponse Verify(Login request)
        {
            var response = _client
                .PostAsync("Login", new StringContent(JsonSerializer.Serialize(request)))
                .Result.Content.ReadAsStringAsync().Result;
            
             return JsonSerializer.Deserialize<LoginResponse>(response);
        }

        public void Login(ISession session, LoginResponse response)
        {
            if (session.IsAvailable)
                session.SetInt32("UserId", response.UserId);
            
            throw new Exception("Session is not available!");
        }
    }
}