using System;
using Microsoft.AspNet.Builder;

namespace Toolbox.WebApi
{
    public static class WebApiAppBuilderExtensions
    {
        public static IApplicationBuilder UseWebApiVersioning(this IApplicationBuilder app)
        {
            // Todo : voeg hier startup logica toe, indien nodig

            return app;
        }
    }
}
