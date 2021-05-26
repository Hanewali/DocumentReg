using DocumentRegistry.Web.Models.Home;

namespace DocumentRegistry.Web.Services.HomeService
{
    public interface ILoginService
    {
        LoginResponse Verify(Login request);
        void Login(LoginResponse request);
    }
}