# Host Filters

A framework for executing code before your .NET Core / ASP.NET Core application runs.

Host filters will run before the IHost runs. It's useful for executing migrations or performing validation tasks before you allow the IHost to run.

## Quick start

1. Implement __IHostFilter__ in a class, or derive from the built-in __HostFilter__ or __AsyncHostFilter__ base classes.
2. Register all concrete types implementing __IHostFilter__ in dependency injection.
3. Replace RunAsync() with RunWithFiltersAsync() in your Program.cs Main method.
4. Run the application and laugh like Dr. Evil.

## Registering Host Filters

You can register host filters using the provided __AddHostFilters()__ method, with its overloads, or use your own Dependency Injection container to match up IHostFilter and concrete types.

__AddHostFilters__ is an extension available for IServiceProvider, which overloads to accomodate each need. See below.

````csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddHostFilters();
    services.AddHostFilters(typeof(Program).Assembly);
    services.AddHostFilter<HappyEasterHostFilter>();
    services.AddHostFilter(typeof(HappyHanukkahHostFilter));
}
````

## Running Host Filters at Host Startup

Below is a typical ASP.NET Core __Program.cs__ file. You can see __RunAsync()__ has been replaced with __RunWithFiltersAsync()__.

That is all that is required to run the host filters.

````csharp
public class Program
{
    public static async Task Main(string[] args)
    {
        //await CreateHostBuilder(args).Build().RunAsync();
        await CreateHostBuilder(args).Build().RunWithFiltersAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
````

## Create a Host Filter Examples

### Synchronous example using HostFilter base class

````csharp
public class HoldTheBeerHostFilter : HostFilter
{
    protected override async Task Handle(IServiceProvider services)
    {
        ILogger logger = services.GetService<ILogger<HoldTheBeerHostFilter>>();
        logger.LogInformation("Hold my beer!");
    }
}
````

### Asynchronous example using AsyncHostFilter base class

````csharp
public class GetHeadlinesHostFilter : AsyncHostFilter
{
    protected override async Task Handle(IServiceProvider services, CancellationToken cancellationToken)
    {
        await new SuperAmazesauceOperationAsync().JustDoItAsync(cancellationToken);
    }
}
````

### Direct Interface Implementation example

````csharp
public class BeatItHostFilter : IHostFilter
{
    public async Task Handle(IServiceProvider services, CancellationToken cancellationToken)
    {
        await new JustBeatItAsync().GimmeAHeeeHeeeAsync(cancellationToken);
    }
}
````

## Samples

See <https://github.com/TheDanielDoyle/HostFilters/tree/develop/Samples> for .NET Core or ASP.NET Core examples.
