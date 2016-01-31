using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;

namespace Toolbox.WebApi.Swagger
{
    public class SwaggerUiRedirectMiddleware
    {
        public SwaggerUiRedirectMiddleware(RequestDelegate next, string url = null)
        {
            if ( next == null ) throw new ArgumentNullException(nameof(next), $"{nameof(next)} cannot be null.");
            NextDelegate = next;
            Url = String.IsNullOrWhiteSpace(url) ? Defaults.Swagger.Url : url;
        }

        internal RequestDelegate NextDelegate { get; private set; }
        internal string Url { get; private set; }

        public async Task Invoke(HttpContext httpContext)
        {
            if ( httpContext.Request.Path == "/" )
            {
                httpContext.Response.Redirect(Url);
                return;
            }

            await NextDelegate(httpContext);
            return;
        }
    }
}
