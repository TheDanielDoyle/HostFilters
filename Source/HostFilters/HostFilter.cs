using System;
using System.Threading;
using System.Threading.Tasks;

namespace HostFilters
{
    public abstract class HostFilter : IHostFilter
    {
        Task IHostFilter.Handle(IServiceProvider services, CancellationToken cancellationToken)
        {
            Handle(services);
            return Task.CompletedTask;
        }

        protected abstract void Handle(IServiceProvider services);
    }
}