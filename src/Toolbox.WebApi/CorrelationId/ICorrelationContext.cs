using Microsoft.AspNet.Http;
using System;

namespace Toolbox.WebApi.CorrelationId
{
    public interface ICorrelationContext
    {
        Guid CorrelationId { get; }
        string CorrelationSource { get; }
        string IdHeaderKey { get; }
        string SourceHeaderKey { get; }
    }
}