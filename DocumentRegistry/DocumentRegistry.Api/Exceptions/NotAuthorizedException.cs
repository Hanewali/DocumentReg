using System;

namespace DocumentRegistry.Api.Exceptions
{
    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException()
        {
        }

        public NotAuthorizedException(string message)
            : base(message)
        {
        }
    }
}