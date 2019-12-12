using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HostFilters.Samples.Console
{
    public class MerryChristmasHostedService : IHostedService
    {
        private Timer timer;
        private readonly ILogger logger;

        public MerryChristmasHostedService(ILogger<MerryChristmasHostedService> logger)
        {
            this.logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.timer = new Timer(MerryChristmas, null, 0, 2000);
            return Task.CompletedTask;
        }

        private void MerryChristmas(object state)
        {
            this.logger.LogInformation("Merry Christmas!!");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
