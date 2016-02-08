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
    public class CorrelationContextTests
    {
        [Fact]
        private void ThrowExceptionWhenOptionsIsNull()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new CorrelationContext(Options.Create<CorrelationOptions>(null)));
        }

        [Fact]
        private void SetHeaderKeysFromOptions()
        {
            var options = new CorrelationOptions();

            var context = new CorrelationContext(Options.Create(options));

            Assert.Equal(options.IdHeaderKey, context.IdHeaderKey);
            Assert.Equal(options.SourceHeaderKey, context.SourceHeaderKey);
        }

        [Fact]
        public void SetValuesFirstTime()
        {
            var options = new CorrelationOptions();
            var context = new CorrelationContext(Options.Create(options));
            var correlationId = Guid.NewGuid();
            var correlationSource = "TestSource";

            var result = context.TrySetValues(correlationId, correlationSource);

            Assert.True(result);
            Assert.Equal(correlationId, context.CorrelationId);
            Assert.Equal(correlationSource, context.CorrelationSource);
        }

        [Fact]
        public void KeepFirstTimeValues()
        {
            var options = new CorrelationOptions();
            var context = new CorrelationContext(Options.Create(options));
            var correlationId = Guid.NewGuid();
            var correlationSource = "TestSource";

            context.TrySetValues(correlationId, correlationSource);
            var result = context.TrySetValues(Guid.NewGuid(), "otherSource");

            Assert.False(result);
            Assert.Equal(correlationId, context.CorrelationId);
            Assert.Equal(correlationSource, context.CorrelationSource);
        }

        [Fact]
        public void AddHeaders()
        {
            var options = new CorrelationOptions();
            var context = new CorrelationContext(Options.Create(options));
            var correlationId = Guid.NewGuid();
            var correlationSource = "TestSource";
            context.TrySetValues(correlationId, correlationSource);
            var request = new HttpRequestMessage();

            context.SetValuesOnHttpRequest(request);

            Assert.NotNull(request.Headers.Single(h => h.Key == options.IdHeaderKey));
            Assert.Equal(correlationId.ToString(), request.Headers.Single(h => h.Key == options.IdHeaderKey).Value.First());
            Assert.NotNull(request.Headers.Single(h => h.Key == options.SourceHeaderKey));
            Assert.Equal(correlationSource, request.Headers.Single(h => h.Key == options.SourceHeaderKey).Value.First());
        }
    }
}
