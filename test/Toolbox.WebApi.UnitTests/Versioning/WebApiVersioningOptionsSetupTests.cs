using System;
using Microsoft.AspNet.Mvc;
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

            var optionsSetup = new WebApiVersioningOptionsSetup(serviceProviderMock.Object);

            Assert.Same(serviceProviderMock.Object, optionsSetup.OptionsServices);
        }

        [Fact]
        private void ConventionIsAdded()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            var options = new TestWebApiVersioningOptions(new WebApiVersioningOptions() { Route = "myroute" });
            serviceProviderMock.Setup(svp => svp.GetService(typeof(IOptions<WebApiVersioningOptions>))).Returns(options);

            var mvcOptions = new MvcOptions();
            var setup = new WebApiVersioningOptionsSetup(serviceProviderMock.Object);

            Assert.Equal(0, mvcOptions.Conventions.Count);

            setup.Configure(mvcOptions);

            Assert.Equal(1, mvcOptions.Conventions.Count);
        }
    }
}
