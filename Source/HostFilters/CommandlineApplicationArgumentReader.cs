using System;
using System.Linq;

namespace HostFilters
{
    public class CommandlineApplicationArgumentReader : IApplicationArgumentReader
    {
        public bool HasArgument(string argument)
        {
            return Environment.GetCommandLineArgs().ToList().Contains(argument);
        }
    }
}