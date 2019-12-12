using System;

namespace HostFilters
{
    public class HostFilterException : Exception
    {
        public HostFilterException(string message) : base(message)
        {
        }

        public HostFilterException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}
