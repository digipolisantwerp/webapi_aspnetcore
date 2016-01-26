using System;
using Microsoft.AspNet.Mvc;
using Toolbox.WebApi.Formatters;
using Xunit;

namespace Toolbox.WebApi.UnitTests.Formatters.RootObject
{
    public class RootObjectOutputFormatterOptionsSetupTests
    {
        [Fact]
        private void OutputFormatterisRegistered()
        {
            var options = new MvcOptions();
            var setup = new RootObjectOutputFormatterOptionsSetup();

            setup.Configure(options);

            Assert.Contains(options.OutputFormatters, item => typeof(RootObjectJsonOutputFormatter).IsInstanceOfType(item));
        }
    }
}
