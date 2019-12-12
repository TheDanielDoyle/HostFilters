﻿using System;
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
        private readonly IHost host;

        public HostFilterInvoker(IHost host)
        {
            this.host = host;
        }

        public async Task<IHost> RunAsync(CancellationToken cancellationToken)
        {
            using (IServiceScope scope = host.Services.CreateScope())
            {
                IEnumerable<IHostFilter> filters = scope.ServiceProvider.GetRequiredService<IEnumerable<IHostFilter>>();
                foreach (IHostFilter filter in filters.ToList())
                {
                    await Handle(filter, scope.ServiceProvider, cancellationToken).ConfigureAwait(false);
                }
            }
            await this.host.RunAsync(cancellationToken).ConfigureAwait(false);
            return host;
        }

        private Task Handle(IHostFilter hostFilter, IServiceProvider services, CancellationToken cancellationToken)
        {
            try
            {
                return hostFilter.Handle(services, cancellationToken);
            }
            catch (Exception exception)
            {
                throw new HostFilterException(exception.Message, exception);
            }
        }
    }
}