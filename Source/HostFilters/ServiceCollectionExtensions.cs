using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace HostFilters
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHostFilter(this IServiceCollection services, Type type)
        {
            if (!typeof(IHostFilter).IsAssignableFrom(type))
            {
                throw new ArgumentException($"{type.Name} is not assignable to {typeof(IHostFilter).Name}.");
            }
            services.AddScoped(typeof(IHostFilter), type);
            return services;
        }

        public static IServiceCollection AddHostFilter<THostFilter>(this IServiceCollection services)  where THostFilter : IHostFilter
        {
            return services.AddHostFilter(typeof(THostFilter));
        }

        public static IServiceCollection AddHostFilters(this IServiceCollection services)
        {
            return AddHostFilters(services, new[] { Assembly.GetCallingAssembly() });
        }

        public static IServiceCollection AddHostFilters(this IServiceCollection services, params Assembly[] assemblies)
        {
            foreach (Assembly assembly in assemblies.ToList())
            {
                AddHostFilters(services, assembly);
            }
            return services;
        }

        private static void AddHostFilters(IServiceCollection services, Assembly assembly)
        {
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
