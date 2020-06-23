using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HostFilters
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHostFilter(this IServiceCollection services, Type type)
        {
            if (!typeof(IHostFilter).IsAssignableFrom(type))
            {
                throw new ArgumentException($"{type.Name} is not assignable to {nameof(IHostFilter)}.");
            }
            AddArgumentReader(services);
            services.AddScoped(typeof(IHostFilter), type);
            return services;
        }

        public static IServiceCollection AddHostFilter<THostFilter>(this IServiceCollection services)  where THostFilter : IHostFilter
        {
            AddArgumentReader(services);
            return services.AddHostFilter(typeof(THostFilter));
        }

        public static IServiceCollection AddHostFilters(this IServiceCollection services)
        {
            AddArgumentReader(services);
            return AddHostFilters(services, new[] { Assembly.GetCallingAssembly() });
        }

        public static IServiceCollection AddHostFilters(this IServiceCollection services, params Assembly[] assemblies)
        {
            AddArgumentReader(services);
            foreach (Assembly assembly in assemblies.ToList())
            {
                AddHostFilters(services, assembly);
            }
            return services;
        }

        private static void AddArgumentReader(IServiceCollection services)
        {
            services.TryAddSingleton<IApplicationArgumentReader, CommandlineApplicationArgumentReader>();
        }

        private static void AddHostFilters(IServiceCollection services, Assembly assembly)
        {
            AddArgumentReader(services);
            foreach (Type type in GetHostFilterTypes(assembly).ToList())
            {
                services.AddScoped(typeof(IHostFilter), type);
            }
        }

        private static IEnumerable<Type> GetHostFilterTypes(Assembly assembly)
        {
            return assembly
                .GetExportedTypes()
                .Where(type => !type.IsAbstract && !type.IsInterface && typeof(IHostFilter).IsAssignableFrom(type));
        }
    }
}
