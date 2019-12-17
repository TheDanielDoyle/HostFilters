using System;
using System.Threading;
using System.Threading.Tasks;

namespace HostFilters
{
    public abstract class AsyncHostFilter : IHostFilter
    {
        Task IHostFilter.Handle(IServiceProvider services, CancellationToken cancellationToken)
        {
            if (CanHandle(services, cancellationToken))
            {
                return Handle(services, cancellationToken);
            }
            return Task.CompletedTask;
        }

        protected virtual bool CanHandle(IServiceProvider services, CancellationToken cancellationToken)
        { 
            return true;
        }

        protected abstract Task Handle(IServiceProvider services, CancellationToken cancellationToken);
    }
}
