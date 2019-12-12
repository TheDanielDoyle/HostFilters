using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HostFilters.Samples.AspNetCore.Mvc.HostFilters
{
    public class MerryChristmasHostFilter : AsyncHostFilter
    {
        protected override Task Handle(IServiceProvider services, CancellationToken cancellationToken)
        {
            ILoggerFactory loggerFactory = services.GetService<ILoggerFactory>();
            ILogger logger = loggerFactory.CreateLogger(typeof(MerryChristmasHostFilter));
            logger.LogInformation("Merry Christmas!!");
            return Task.CompletedTask;
        }
    }
}
