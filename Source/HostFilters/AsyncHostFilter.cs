using System;
using System.Threading;
using System.Threading.Tasks;

namespace HostFilters
{
    public abstract class AsyncHostFilter : IHostFilter
    {
        Task IHostFilter.Handle(IServiceProvider services, CancellationToken cancellationToken)
        {
            return Handle(services, cancellationToken);
        }

        protected abstract Task Handle(IServiceProvider services, CancellationToken cancellationToken);
    }
}
