using System;
using Microsoft.AspNet.Builder;
using Toolbox.WebApi.Swagger;

namespace Toolbox.WebApi
{
    public static class WebApiAppBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerUiRedirect(this IApplicationBuilder app, string url = null)
        {
            if ( url == null )
                app.UseMiddleware<SwaggerUiRedirectMiddleware>();
            else
                app.UseMiddleware<SwaggerUiRedirectMiddleware>(url);

            return app;
        }
    }
}
