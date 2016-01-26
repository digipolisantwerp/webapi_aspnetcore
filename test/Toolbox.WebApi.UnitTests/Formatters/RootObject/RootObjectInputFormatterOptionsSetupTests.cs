using System;
using Microsoft.AspNet.Mvc;
using Toolbox.WebApi.Formatters;
using Xunit;

namespace Toolbox.WebApi.UnitTests.Formatters.RootObject
{
    public class RootObjectInputFormatterOptionsSetupTests
    {
        [Fact]
        private void InputFormatterisRegistered()
        {
            var options = new MvcOptions();
            var setup = new RootObjectInputFormatterOptionsSetup();
            
            setup.Configure(options);

            Assert.Contains(options.InputFormatters, item => typeof(RootObjectJsonInputFormatter).IsInstanceOfType(item));
        }
    }
}
