using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace HostFilters
{
    public interface IHostFilterInvoker
    {
        Task<IHost> RunAsync(CancellationToken cancellationToken);
    }
}
