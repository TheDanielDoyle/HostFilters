using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HostFilters
{
    public class HostFilterInvoker : IHostFilterInvoker
    {
        public const string ExitFlag = "exit";
        private readonly IHost host;

        public HostFilterInvoker(IHost host)
        {
            this.host = host;
        }

        public async Task<IHost> RunAsync(CancellationToken cancellationToken)
        {
            using (IServiceScope scope = host.Services.CreateScope())
            {
                foreach (IHostFilter hostFilter in CreateHostFilters(scope).ToList())
                {
                    await Handle(hostFilter, scope.ServiceProvider, cancellationToken).ConfigureAwait(false);
                }
                if (ShouldRunHost(scope))
                {
                    await this.host.RunAsync(cancellationToken).ConfigureAwait(false);
                }
            }
            return host;
        }

        private IEnumerable<IHostFilter> CreateHostFilters(IServiceScope scope)
        {
            try
            {
                return scope.ServiceProvider.GetRequiredService<IEnumerable<IHostFilter>>();
            }
            catch (InvalidOperationException exception)
            {
               throw new HostFilterException("Unable to create Host Filters. Please ensure you have registred all dependencies correctly.", exception);
            }
        }

        private async Task Handle(IHostFilter hostFilter, IServiceProvider services, CancellationToken cancellationToken)
        {
            try
            {
                await hostFilter.Handle(services, cancellationToken);
            }
            catch (Exception exception)
            {
                throw new HostFilterException(exception.Message, exception);
            }
        }

        private bool ShouldRunHost(IServiceScope scope)
        {
            IApplicationArgumentReader argumentReader =
                scope.ServiceProvider.GetService<IApplicationArgumentReader>()
                ?? new CommandlineApplicationArgumentReader();
            return !argumentReader.HasArgument(ExitFlag);
        }
    }
}