using System;
using DocumentRegistry.Exceptions;
using DocumentRegistry.Infrastructure;

namespace DocumentRegistry.Models
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