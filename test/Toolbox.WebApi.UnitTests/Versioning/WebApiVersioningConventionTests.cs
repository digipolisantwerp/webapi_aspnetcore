using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.Mvc.ApplicationModels;
using Microsoft.Extensions.OptionsModel;
using Moq;
using Toolbox.WebApi.Versioning;
using Xunit;

namespace Toolbox.WebApi.UnitTests.Versioning
{
    public class WebApiVersioningConventionTests
    {
        [Fact]
        private void OptionsIsSet()
        {
            var options = new WebApiVersioningOptions() { Route = "myroute" };
            var optionsMock = new Mock<IOptions<WebApiVersioningOptions>>();
            optionsMock.Setup(o => o.Value).Returns(options);

            var convention = new WebApiVersioningConvention(optionsMock.Object);

            Assert.Same(options, convention.Options);
        }

        [Fact]
        private void RouteIsSetForVersionControllerModel()
        {
            var options = new WebApiVersioningOptions() { Route = "myroute" };
            var optionsMock = new Mock<IOptions<WebApiVersioningOptions>>();
            optionsMock.Setup(o => o.Value).Returns(options);

            var convention = new WebApiVersioningConvention(optionsMock.Object);
            var model = new ControllerModel(typeof(VersionController).GetTypeInfo(), new List<object>());

            convention.Apply(model);

            Assert.Equal(1, model.AttributeRoutes.Count);
            Assert.Equal("myroute", model.AttributeRoutes.First().Template);
            Assert.Equal("WebApiVersioningRoute", model.AttributeRoutes.First().Name);
        }

        [Fact]
        private void RouteIsNotSetForNonVersionControllerModel()
        {
            var options = new WebApiVersioningOptions() { Route = "myroute" };
            var optionsMock = new Mock<IOptions<WebApiVersioningOptions>>();
            optionsMock.Setup(o => o.Value).Returns(options);

            var convention = new WebApiVersioningConvention(optionsMock.Object);
            var model = new ControllerModel(typeof(TestController).GetTypeInfo(), new List<object>());

            convention.Apply(model);

            Assert.Equal(0, model.AttributeRoutes.Count);
        }

        [Fact]
        private void RouteNullIsNotSetForVersionController()
        {
            var options = new WebApiVersioningOptions() { Route = null };
            var optionsMock = new Mock<IOptions<WebApiVersioningOptions>>();
            optionsMock.Setup(o => o.Value).Returns(options);

            var convention = new WebApiVersioningConvention(optionsMock.Object);
            var model = new ControllerModel(typeof(VersionController).GetTypeInfo(), new List<object>());

            convention.Apply(model);

            Assert.Equal(0, model.AttributeRoutes.Count);
        }

        [Fact]
        private void RouteEmptyIsNotSetForVersionController()
        {
            var options = new WebApiVersioningOptions() { Route = "" };
            var optionsMock = new Mock<IOptions<WebApiVersioningOptions>>();
            optionsMock.Setup(o => o.Value).Returns(options);

            var convention = new WebApiVersioningConvention(optionsMock.Object);
            var model = new ControllerModel(typeof(VersionController).GetTypeInfo(), new List<object>());

            convention.Apply(model);

            Assert.Equal(0, model.AttributeRoutes.Count);
        }

        [Fact]
        private void RouteWhitespaceIsNotSetForVersionController()
        {
            var options = new WebApiVersioningOptions() { Route = "   " };
            var optionsMock = new Mock<IOptions<WebApiVersioningOptions>>();
            optionsMock.Setup(o => o.Value).Returns(options);

            var convention = new WebApiVersioningConvention(optionsMock.Object);
            var model = new ControllerModel(typeof(VersionController).GetTypeInfo(), new List<object>());

            convention.Apply(model);

            Assert.Equal(0, model.AttributeRoutes.Count);
        }
    }
}
