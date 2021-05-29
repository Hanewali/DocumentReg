using DocumentRegistry.Api.DomainModels;

namespace DocumentRegistry.Api.ApiModels.Login
{
    public class LoginResponse
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