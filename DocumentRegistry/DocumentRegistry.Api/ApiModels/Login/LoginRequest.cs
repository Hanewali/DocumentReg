using System.Xml.Linq;

namespace DocumentRegistry.Api.ApiModels.Login
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}