using DocumentRegistry.Web.Models.User;

namespace DocumentRegistry.Web.ApiModels
{
    public class UserRequest
    {
        public int UserId { get; set; }
        public int BeginFrom { get; set; }
        public int Rows { get; set; }
        public User User { get; set; }
    }
}