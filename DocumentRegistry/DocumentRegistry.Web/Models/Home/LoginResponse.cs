namespace DocumentRegistry.Web.Models.Home
{
    public class LoginResponse : ResponseModel
    {
        public bool Verified { get; set; }
        public int UserId { get; set; }
    }
}