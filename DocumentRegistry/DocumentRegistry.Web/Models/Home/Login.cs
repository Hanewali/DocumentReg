using System.ComponentModel;

namespace DocumentRegistry.Web.Models.Home
{
    public class Login
    {
        [DisplayName("Login")]
        public string Username { get; set; }
        [DisplayName("Hasło")]
        public string Password { get; set; }
    }
}