using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Toolbox.WebApi.CorrelationId;
using Toolbox.WebApi.UnitTests.Utilities;
using Xunit;

namespace Toolbox.WebApi.UnitTests.CorrelationId
{
    public class HttpRequestMessageExtensionsTests
    {
        [Fact]
        public void SetValuesOnRequest()
        {
            var options = new CorrelationOptions();
            var context = new CorrelationContext(Options.Create(options));
            var correlationId = Guid.NewGuid();
            var correlationSource = "TestSource";
            context.TrySetValues(correlationId, correlationSource);
            var request = new HttpRequestMessage();

            request.SetCorrelationValues(context);

            Assert.NotNull(request.Headers.Single(h => h.Key == options.IdHeaderKey));
            Assert.Equal(correlationId.ToString(), request.Headers.Single(h => h.Key == options.IdHeaderKey).Value.First());
            Assert.NotNull(request.Headers.Single(h => h.Key == options.SourceHeaderKey));
            Assert.Equal(correlationSource, request.Headers.Single(h => h.Key == options.SourceHeaderKey).Value.First());
        }

    }
}
