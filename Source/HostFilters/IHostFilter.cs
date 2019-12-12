using System;
using System.Threading;
using System.Threading.Tasks;

namespace HostFilters
{
    public interface IHostFilter
    {
        Task Handle(IServiceProvider services, CancellationToken cancellationToken);
    }
}
