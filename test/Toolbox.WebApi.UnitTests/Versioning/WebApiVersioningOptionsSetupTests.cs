using System;
using Microsoft.AspNet.Builder.Internal;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;
using Moq;
using Toolbox.WebApi.Versioning;
using Xunit;

namespace Toolbox.WebApi.UnitTests.Versioning
{
    public class WebApiVersioningOptionsSetupTests
    {
        [Fact]
        private void OptionsServicesIsSet()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            var appBuilder = new ApplicationBuilder(serviceProviderMock.Object);

            var optionsSetup = new WebApiVersioningOptionsSetup(appBuilder);

            Assert.Same(serviceProviderMock.Object, optionsSetup.OptionsServices);
        }

        [Fact]
        private void ConventionIsAdded()
        {
            var serviceProvider = new Mock<IServiceProvider>();
            var appBuilder = new ApplicationBuilder(serviceProvider.Object);

            var options = new TestWebApiVersioningOptions(new WebApiVersioningOptions() { Route = "myroute" });
            serviceProvider.Setup(svp => svp.GetService(typeof(IOptions<WebApiVersioningOptions>))).Returns(options);

            var mvcOptions = new MvcOptions();
            var setup = new WebApiVersioningOptionsSetup(appBuilder);

            Assert.Equal(0, mvcOptions.Conventions.Count);

            setup.Configure(mvcOptions);

            Assert.Equal(1, mvcOptions.Conventions.Count);
        }
    }
}
