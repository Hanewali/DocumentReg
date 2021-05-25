
using DocumentRegistry.Api.Infrastructure;

namespace DocumentRegistry.Api.Extensions
{
    public static class HttpRequest
    {
        public static bool IsAuthorized(this Microsoft.AspNetCore.Http.HttpRequest request)
        {
            return request.Headers["AuthorizationToken"] == Configuration.Api.AuthorizationToken;
        }
    }
}