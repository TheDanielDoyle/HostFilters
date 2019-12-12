using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HostFilters.Samples.Console
{
    public class HappyEasterHostFilter : AsyncHostFilter
    {
        protected override async Task Handle(IServiceProvider services, CancellationToken cancellationToken)
        {
            await Task.Delay(1000, cancellationToken);
            ILoggerFactory loggerFactory = services.GetService<ILoggerFactory>();
            ILogger logger = loggerFactory.CreateLogger(typeof(HappyEasterHostFilter));
            logger.LogInformation("Happy Easter!!!");
        }
    }
}
