using System;
using Toolbox.WebApi.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Toolbox.WebApi
{
    public static class WebApiServiceCollectionExtensions
    {
        public static IServiceCollection AddWebApiVersioning(this IServiceCollection services, WebApiVersioningOptions options)
        {
            RegisterControllerDiscovery(services, options);
            return services;
        }

        private static void RegisterControllerDiscovery(IServiceCollection services, WebApiVersioningOptions options)
        {
            services.AddSingleton<IVersionProvider, WebApiVersionProvider>();
            services.AddInstance(options);
        }
              
    }
}
