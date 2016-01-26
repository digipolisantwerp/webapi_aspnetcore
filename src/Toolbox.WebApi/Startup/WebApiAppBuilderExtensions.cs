using System;
using Microsoft.AspNet.Builder;

namespace Toolbox.WebApi
{
    public static class WebApiAppBuilderExtensions
    {
        public static IApplicationBuilder UseWebApiVersioning(this IApplicationBuilder app)
        {
            // Todo : add startup logic here, if neccesary.

            return app;
        }
    }
}
