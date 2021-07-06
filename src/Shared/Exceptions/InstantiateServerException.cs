using System;

namespace Shared.Exceptions
{
    public class InstantiateServerException : Exception
    {
        public InstantiateError Error;
        public InstantiateServerException() : base()
        {
            
        }
        
        public InstantiateServerException(string message) : base(message)
        {
            
        }
    }

    public enum InstantiateError
    {
        NotFoundServerId, BadRequest, Unknown
    }
}