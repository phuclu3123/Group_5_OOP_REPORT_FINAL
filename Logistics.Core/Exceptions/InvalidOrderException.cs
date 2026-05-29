using System;

namespace Logistics.Core.Exceptions
{
    public class InvalidOrderException : Exception
    {
        public InvalidOrderException() { }
        
        public InvalidOrderException(string message) : base(message) { }
        
        public InvalidOrderException(string message, Exception inner) : base(message, inner) { }
    }
}
