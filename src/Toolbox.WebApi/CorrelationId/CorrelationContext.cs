using Microsoft.AspNet.Http;
using Microsoft.Extensions.OptionsModel;
using System;

namespace Toolbox.WebApi.CorrelationId
{
    public class CorrelationContext : ICorrelationContext
    {
        private readonly CorrelationOptions _options;

        public CorrelationContext(IOptions<CorrelationOptions> options)
        {
            if (options.Value == null) throw new ArgumentNullException(nameof(CorrelationOptions), $"{nameof(CorrelationOptions)} cannot be null.");

            _options = options.Value;

            IdHeaderKey = _options.IdHeaderKey;
            SourceHeaderKey = _options.SourceHeaderKey;
        }

        public Guid CorrelationId { get; private set; }
        public string CorrelationSource { get; private set; }
        public string IdHeaderKey { get; private set; } 
        public string SourceHeaderKey { get; private set; }

        internal bool TrySetValues(Guid id, string source)
        {
            if (CorrelationId == Guid.Empty)
            {
                CorrelationId = id;
                CorrelationSource = source;
                return true;
            }

            return false;
        }
    }
}
