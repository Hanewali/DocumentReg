using System.Xml.Linq;
using DocumentRegistry.Web.Models;

namespace DocumentRegistry.Api.ApiModels.Login
{
    public class LoginRequest : RequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}