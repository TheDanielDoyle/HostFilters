using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HostFilters.Samples.Console
{
    public class HappyHanukkahHostFilter : HostFilter
    {
        protected override void Handle(IServiceProvider services)
        {
            ILoggerFactory loggerFactory = services.GetService<ILoggerFactory>();
            ILogger logger = loggerFactory.CreateLogger(typeof(HappyHanukkahHostFilter));
            logger.LogInformation("Happy Hanukkah!!!");
        }
    }
}