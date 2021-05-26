namespace DocumentRegistry.Web.Models.Home
{
    public class Login : RequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}