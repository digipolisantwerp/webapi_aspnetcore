using Microsoft.AspNet.Builder;
using Toolbox.WebApi.CorrelationId;

namespace Toolbox.WebApi
{
    public static class CorrelationIdAppBuilderExtensions
    {
        public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app, string source)
        {
            app.UseMiddleware<CorrelationIdMiddleware>(source);

            return app;
        }
    }
}
