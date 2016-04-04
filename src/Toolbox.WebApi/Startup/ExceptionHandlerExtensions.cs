using Microsoft.AspNet.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toolbox.WebApi.Exceptions;

namespace Toolbox.WebApi.Startup
{
    public static class ExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app, Action<HttpStatusCodeMappings> setupAction)
        {
            var logger = app.ApplicationServices.GetService<ILogger<ExceptionHandler>>();

            var mappings = new HttpStatusCodeMappings();
            setupAction.Invoke(mappings);

            var handler = new ExceptionHandler(mappings, logger);

            return app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context => await handler.HandleAsync(context));
            });
        }
    }
}
