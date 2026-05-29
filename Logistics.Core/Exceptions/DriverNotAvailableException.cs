using System;

namespace Logistics.Core.Exceptions
{
    public class DriverNotAvailableException : Exception
    {
        public DriverNotAvailableException() { }
        
        public DriverNotAvailableException(string message) : base(message) { }
        
        public DriverNotAvailableException(string message, Exception inner) : base(message, inner) { }
    }
}
