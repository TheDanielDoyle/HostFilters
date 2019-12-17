using System;
using System.Threading;
using System.Threading.Tasks;

namespace HostFilters
{
    public abstract class HostFilter : IHostFilter
    {
        Task IHostFilter.Handle(IServiceProvider services, CancellationToken cancellationToken)
        {
            if (CanHandle(services))
            {
                Handle(services);
            }
            return Task.CompletedTask;
        }

        protected virtual bool CanHandle(IServiceProvider services)
        {
            return true;
        }

        protected abstract void Handle(IServiceProvider services);
    }
}