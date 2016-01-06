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
            //services.AddSingleton(typeof(IServiceCollection), (o) => { return services; });
            services.AddSingleton<IVersionProvider, WebApiVersionProvider>();
            services.AddInstance(options);

        }

        //Voorlopig niet gebruikt rc01831

        //public static IServiceCollection AddRootObjectFormatters(this IServiceCollection services, WebApiFormattingOptions options)
        //{
        //    RegisterFormattingControllerDiscovery(services, options);
        //    return services;
        //}

        //private static void RegisterFormattingControllerDiscovery(IServiceCollection services, WebApiFormattingOptions options)
        //{
        //  services.AddInstance(options);
        //}

        //Voorlopig niet gebruikt rc01831
        //public static IServiceCollection AddQueryStringMappings(this IServiceCollection services, WebApiQuerystringOptions options)
        //{
        //    RegisterQueryStringControllerDiscovery(services, options);
        //    return services;
        //}

        //private static void RegisterQueryStringControllerDiscovery(IServiceCollection services, WebApiQuerystringOptions options)
        //{
        //    services.AddInstance(options);
        //}

    }
}
