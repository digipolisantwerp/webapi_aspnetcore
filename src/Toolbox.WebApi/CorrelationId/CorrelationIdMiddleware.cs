using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;

namespace Toolbox.WebApi.CorrelationId
{
    internal class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private string _source;

        public CorrelationIdMiddleware(RequestDelegate next, string source)
        {
            if (next == null) throw new ArgumentNullException(nameof(next), $"{nameof(next)} cannot be null.");
            if (String.IsNullOrWhiteSpace(source)) throw new ArgumentNullException(nameof(source), $"{nameof(source)} cannot be null or empty.");

            _next = next;
            _source = source;
        }

        public Task Invoke(HttpContext context)
        {
            Guid correlationId = Guid.Empty;
            string correlationSource = String.Empty;

            var correlationContext = context.RequestServices.GetService<ICorrelationContext>() as CorrelationContext;
            var correlationIdHeader = context.Request.Headers[correlationContext.IdHeaderKey];

            if (StringValues.IsNullOrEmpty(correlationIdHeader))
            {
                correlationId =  Guid.NewGuid();
                correlationSource = _source;
            }
            else
            {
                correlationId = Guid.Parse(correlationIdHeader);
                correlationSource = context.Request.Headers[correlationContext.SourceHeaderKey];
            }

            correlationContext.TrySetValues(correlationId, correlationSource);

            return _next.Invoke(context);
        }
    }
}
