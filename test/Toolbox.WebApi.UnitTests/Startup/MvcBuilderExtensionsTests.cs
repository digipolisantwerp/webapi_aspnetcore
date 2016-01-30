using System;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;
using Toolbox.WebApi.ActionOverloading;
using Toolbox.WebApi.Formatters;
using Toolbox.WebApi.Versioning;
using Xunit;

namespace Toolbox.WebApi.UnitTests.Startup
{
    public class MvcBuilderExtensionsTests
    {
        [Fact]
        private void ApiActionOverloadingOptionsSetupIsRegisteredAsTransient()
        {
            var services = new ServiceCollection();
            var builder = new MvcBuilder(services);

            builder.AddActionOverloading();

            var registrations = services.Where(sd => sd.ServiceType == typeof(IConfigureOptions<MvcOptions>)
                                               && sd.ImplementationType == typeof(ApiActionOverloadingOptionsSetup))
                                        .ToArray();
            Assert.Equal(1, registrations.Count());
            Assert.Equal(ServiceLifetime.Transient, registrations[0].Lifetime);
        }

        [Fact]
        private void RootObjectInputFormatterOptionsSetupIsRegisteredAsTransient()
        {
            var services = new ServiceCollection();
            var builder = new MvcBuilder(services);

            builder.AddRootObjectInputFormatter();

            var registrations = services.Where(sd => sd.ServiceType == typeof(IConfigureOptions<MvcOptions>)
                                               && sd.ImplementationType == typeof(RootObjectInputFormatterOptionsSetup))
                                        .ToArray();
            Assert.Equal(1, registrations.Count());
            Assert.Equal(ServiceLifetime.Transient, registrations[0].Lifetime);
        }

        [Fact]
        private void RootObjectOutputFormatterOptionsSetupIsRegisteredAsTransient()
        {
            var services = new ServiceCollection();
            var builder = new MvcBuilder(services);

            builder.AddRootObjectOutputFormatter();

            var registrations = services.Where(sd => sd.ServiceType == typeof(IConfigureOptions<MvcOptions>)
                                               && sd.ImplementationType == typeof(RootObjectOutputFormatterOptionsSetup))
                                        .ToArray();
            Assert.Equal(1, registrations.Count());
            Assert.Equal(ServiceLifetime.Transient, registrations[0].Lifetime);
        }

        [Fact]
        private void WebApiVersioningOptionsSetupIsRegisteredAsTransient()
        {
            var services = new ServiceCollection();
            var builder = new MvcBuilder(services);
            
            builder.AddVersioning();

            var registrations = services.Where(sd => sd.ServiceType == typeof(IConfigureOptions<MvcOptions>)
                                               && sd.ImplementationType == typeof(WebApiVersioningOptionsSetup))
                                        .ToArray();
            Assert.Equal(1, registrations.Count());
            Assert.Equal(ServiceLifetime.Transient, registrations[0].Lifetime);
        }

        [Fact]
        private void WebApiVersioningOptionsIsRegisteredAsSingleton()
        {
            var services = new ServiceCollection();
            var builder = new MvcBuilder(services);

            builder.AddVersioning(options => options.Route = "myroute");

            var registrations = services.Where(sd => sd.ServiceType == typeof(IConfigureOptions<WebApiVersioningOptions>)).ToArray();
            Assert.Equal(1, registrations.Count());
            Assert.Equal(ServiceLifetime.Singleton, registrations[0].Lifetime);
        }

        [Fact]
        private void WebApiVersionProviderIsRegisteredAsSingleton()
        {
            var services = new ServiceCollection();
            var builder = new MvcBuilder(services);

            builder.AddVersioning();

            var registrations = services.Where(sd => sd.ServiceType == typeof(IVersionProvider)
                                               && sd.ImplementationType == typeof(WebApiVersionProvider))
                                        .ToArray();
            Assert.Equal(1, registrations.Count());
            Assert.Equal(ServiceLifetime.Singleton, registrations[0].Lifetime);
        }

    }
}
