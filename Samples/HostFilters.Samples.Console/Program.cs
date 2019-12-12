using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HostFilters.Samples.Console
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            IHost host = CreateBuilder(args).Build();
            await host.RunWithFiltersAsync();
        }

        private static IHostBuilder CreateBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddHostFilters();
                    //services.AddHostFilters(typeof(Program).Assembly);
                    //services.AddHostFilter<HappyEasterHostFilter>();
                    //services.AddHostFilter(typeof(HappyHanukkahHostFilter));

                    services.AddLogging(builder =>
                    {
                        builder.AddConsole();
                        builder.SetMinimumLevel(LogLevel.Information);
                    });

                    services.AddHostedService<MerryChristmasHostedService>();
                });
        }
    }
}
