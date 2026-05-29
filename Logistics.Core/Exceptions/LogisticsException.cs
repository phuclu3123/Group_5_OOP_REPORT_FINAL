using System;

namespace Logistics.Core.Exceptions
{
    public class LogisticsException : Exception
    {
        public LogisticsException() { }
        
        public LogisticsException(string message) : base(message) { }
        
        public LogisticsException(string message, Exception inner) : base(message, inner) { }
    }
}
