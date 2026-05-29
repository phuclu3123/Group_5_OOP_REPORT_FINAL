using System;

namespace Logistics.Core.Exceptions
{
    public class InvalidAddressException : Exception
    {
        public InvalidAddressException() { }
        
        public InvalidAddressException(string message) : base(message) { }
        
        public InvalidAddressException(string message, Exception inner) : base(message, inner) { }
    }
}
