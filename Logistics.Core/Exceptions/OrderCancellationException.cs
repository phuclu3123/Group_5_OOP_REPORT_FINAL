using System;

namespace Logistics.Core.Exceptions
{
    public class OrderCancellationException : Exception
    {
        public OrderCancellationException() { }
        
        public OrderCancellationException(string message) : base(message) { }
        
        public OrderCancellationException(string message, Exception inner) : base(message, inner) { }
    }
}
