using System.Net.Http;

namespace Toolbox.WebApi.CorrelationId
{
    public static class HttpRequestMessageExtensions
    {
        public static void SetCorrelationValues(this HttpRequestMessage message, ICorrelationContext context)
        {
            context.SetValuesOnHttpRequest(message);
        }
    }
}
