using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Internal;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toolbox.WebApi.CorrelationId;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;
using Toolbox.WebApi.UnitTests.Utilities;

namespace Toolbox.WebApi.UnitTests.CorrelationId
{
    public class CorrelationIdMiddlewareTests
    {
        private readonly string _externalSource = "externalApplication";
        private readonly string _applicationSource = "thisApplication";

        [Fact]
        private void ThrowExceptionWhenNextDelegateIsNull()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new CorrelationIdMiddleware(null, "src"));
            Assert.Equal("next", ex.ParamName);
        }

        [Fact]
        private void ThrowExceptionWhenSourceIsNull()
        {
            var requestDelegate = CreateRequestDelegate();
            var ex = Assert.Throws<ArgumentNullException>(() => new CorrelationIdMiddleware(requestDelegate, null));
            Assert.Equal("source", ex.ParamName);
        }

        [Fact]
        private void ThrowExceptionWhenSourceIsEmpty()
        {
            var requestDelegate = CreateRequestDelegate();
            var ex = Assert.Throws<ArgumentNullException>(() => new CorrelationIdMiddleware(requestDelegate, String.Empty));
            Assert.Equal("source", ex.ParamName);
        }

        [Fact]
        public async void ThrowExceptionWhenCorrelationContextNotAvailable()
        {
            var requestDelegate = CreateRequestDelegate();
             var middleware = new CorrelationIdMiddleware(requestDelegate, _applicationSource);

            Exception x = await Assert.ThrowsAsync<ArgumentNullException>(() => middleware.Invoke(new DefaultHttpContext()));
        }

        [Fact]
        private async void NextDelegateInvoked()
        {
            var httpContext = new DefaultHttpContext();
            var options = new CorrelationOptions();

            httpContext.RequestServices = CreateServiceProvider(CreateContext(options), options);

            var isInvoked = false;
            var requestDelegate = CreateRequestDelegate(() => isInvoked = true);

             var middleware = new CorrelationIdMiddleware(requestDelegate, _applicationSource);

            await middleware.Invoke(httpContext);

            Assert.True(isInvoked);
        }

        [Fact]
        private async void CreateNewCorrelationValuesIfNotPresent()
        {
            var httpContext = new DefaultHttpContext();
            var options = new CorrelationOptions();
            var correlationContext = CreateContext(options);

            httpContext.RequestServices = CreateServiceProvider(correlationContext, options);
            var requestDelegate = CreateRequestDelegate();
             var middleware = new CorrelationIdMiddleware(requestDelegate, _applicationSource);

            await middleware.Invoke(httpContext);

            Assert.NotEqual(new Guid(), correlationContext.CorrelationId);
            Assert.Equal(_applicationSource, correlationContext.CorrelationSource);
        }

        [Fact]
        private async void LeaveCorrelationValuesIfPresent()
        {
            var initialId = Guid.NewGuid();
            var httpContext = new DefaultHttpContext();
            var options = new CorrelationOptions();
            var correlationContext = CreateContext(options);

            httpContext.RequestServices = CreateServiceProvider(correlationContext, options);
            httpContext.Request.Headers.Add(options.IdHeaderKey, initialId.ToString());
            httpContext.Request.Headers.Add(options.SourceHeaderKey, _externalSource);

            var requestDelegate = CreateRequestDelegate();
             var middleware = new CorrelationIdMiddleware(requestDelegate, _applicationSource);

            await middleware.Invoke(httpContext);

            Assert.Equal(initialId, correlationContext.CorrelationId);
            Assert.Equal(_externalSource, correlationContext.CorrelationSource);
        }

        private ICorrelationContext CreateContext(CorrelationOptions options)
        {
            var context = new CorrelationContext(Options.Create(options));
            return context;
        }

        private IServiceProvider CreateServiceProvider(ICorrelationContext context, CorrelationOptions options = null)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(p => p.GetService(typeof(ICorrelationContext))).Returns(context);

            if (options != null)
                serviceProviderMock.Setup(p => p.GetService(typeof(IOptions<CorrelationOptions>))).Returns(Options.Create(options));

            return serviceProviderMock.Object;
        }

        private RequestDelegate CreateRequestDelegate(Action action = null)
        {
            return new RequestDelegate(ctx =>
            {
                if (action != null) action.Invoke();
                return Task.FromResult(false);
            });
        }

        private Task CreateDefaultHttpContext(HttpContext HttpContext)
        {
            return Task.FromResult(false);
        }
    }
}
