using DocumentRegistry.Web.Models.Home;
using Microsoft.AspNetCore.Http;

namespace DocumentRegistry.Web.Services.HomeService
{
    public interface ILoginService
    {
        LoginResponse Verify(Login request);
        void Login(ISession session, LoginResponse request);
    }
}