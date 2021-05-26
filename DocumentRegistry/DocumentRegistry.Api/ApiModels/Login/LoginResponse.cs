
using DocumentRegistry.Api.DomainModels;
using DocumentRegistry.Web.Models;

namespace DocumentRegistry.Api.ApiModels.Login
{
    public class LoginResponse : ResponseModel
    {
        public bool Verified { get; set; }
        public int UserId { get; set; }

        public static LoginResponse BuildFromUser(User user)
        {
            return new()
            {
                Verified = true,
                UserId = user.Id
            };
        }
    }
}