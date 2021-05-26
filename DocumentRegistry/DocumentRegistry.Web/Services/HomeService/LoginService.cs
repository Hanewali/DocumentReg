using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using DocumentRegistry.Web.Models.Home;

namespace DocumentRegistry.Web.Services.HomeService
{
    public class LoginService : ILoginService
    {
        private HttpClient _client;
        
        public LoginService()
        {
            _client = ApiHelper.PrepareClient();
        }

        public LoginResponse Verify(Login request)
        {
            var response = _client
                .PostAsync("Login/Verify", new StringContent(request.ToJson(), Encoding.UTF8, "application/json"))
                .Result.Content.ReadAsStringAsync().Result;
            
             return LoginResponse.FromJson<LoginResponse>(response);
        }

        public void Login(LoginResponse request)
        {
            throw new System.NotImplementedException();
        }
    }
}