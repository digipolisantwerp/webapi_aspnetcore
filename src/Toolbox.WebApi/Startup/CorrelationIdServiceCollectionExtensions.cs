using Microsoft.Extensions.DependencyInjection;
using System;
using Toolbox.WebApi.CorrelationId;

namespace Toolbox.WebApi
{
    public static class CorrelationIdServiceCollectionExtensions
    {
        public static IServiceCollection AddCorrelationId(this IServiceCollection services, Action<CorrelationOptions> setupAction = null)
        {
            if (setupAction == null)
                setupAction = options => { };

            services.Configure(setupAction);
            services.AddScoped<ICorrelationContext, CorrelationContext>();

            return services;
        }
    }
}
