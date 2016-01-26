using System;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;
using Toolbox.WebApi.ActionOverloading;
using Toolbox.WebApi.Formatters;
using Xunit;

namespace Toolbox.WebApi.UnitTests.Startup
{
    public class MvcBuilderExtensionsTests
    {
        [Fact]
        private void ApiActionOverloadingOptionsSetupIsRegistered()
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
        private void RootObjectInputFormatterOptionsSetupIsRegistered()
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
        private void RootObjectOutputFormatterOptionsSetupIsRegistered()
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
    }
}
