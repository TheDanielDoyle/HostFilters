using System.Threading;
using System.Threading.Tasks;
using HostFilters;

namespace Microsoft.Extensions.Hosting
{
    public static class HostExtensions
    {
        public static Task<IHost> RunWithFiltersAsync(this IHost host, CancellationToken cancellationToken = default)
        {
            return new HostFilterInvoker(host).RunAsync(cancellationToken);
        }
    }
}
