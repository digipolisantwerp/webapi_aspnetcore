using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toolbox.WebApi.CorrelationId;
using Xunit;

namespace Toolbox.WebApi.UnitTests.CorrelationIdServiceCollectionExtensions
{
    public class AddCorrelationIdTests
    {
        [Fact]
        private void CorrelationContextIsRegistratedAsScoped()
        {
            var services = new ServiceCollection();
            services.AddCorrelationId();

            var registrations = services.Where(sd => sd.ServiceType == typeof(ICorrelationContext) && 
                                                     sd.ImplementationType == typeof(CorrelationContext))
                                        .ToArray();

            Assert.Equal(1, registrations.Count());
            Assert.Equal(ServiceLifetime.Scoped, registrations[0].Lifetime);
        }

        [Fact]
        private void CorrelationOptionsIsRegistratedAsSingleton()
        {
            var services = new ServiceCollection();
            services.AddCorrelationId();

            var registrations = services.Where(sd => sd.ServiceType == typeof(IConfigureOptions<CorrelationOptions>))
                                        .ToArray();

            Assert.Equal(1, registrations.Count());
            Assert.Equal(ServiceLifetime.Singleton, registrations[0].Lifetime);
        }
    }
}
