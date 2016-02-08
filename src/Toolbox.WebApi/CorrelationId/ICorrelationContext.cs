using System;
using System.Net.Http;

namespace Toolbox.WebApi.CorrelationId
{
    public interface ICorrelationContext
    {
        Guid CorrelationId { get; }
        string CorrelationSource { get; }
        string IdHeaderKey { get; }
        string SourceHeaderKey { get; }

        void SetValuesOnHttpRequest(HttpRequestMessage request);
    }
}