using System;

namespace Logistics.Core.Exceptions
{
    public class InsufficientCapacityException : Exception
    {
        public InsufficientCapacityException() { }
        
        public InsufficientCapacityException(string message) : base(message) { }
        
        public InsufficientCapacityException(string message, Exception inner) : base(message, inner) { }
    }
}
