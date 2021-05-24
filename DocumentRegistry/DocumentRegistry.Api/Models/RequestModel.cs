using System;
using DocumentRegistry.Api.Exceptions;
using DocumentRegistry.Api.Infrastructure;

namespace DocumentRegistry.Api.Models
{
    public abstract class RequestModel
    {
        protected RequestModel(int userId, string authorizationToken)
        {
            UserId = userId;
            AuthorizationToken = authorizationToken;

            if (authorizationToken != Configuration.GetConfigurationField("AuthorizationToken"))
            {
                throw new NotAuthorizedException("Wrong authorization token");
            }
        }

        public int UserId { get; set; }
        public string AuthorizationToken { get; set; }
    }
}