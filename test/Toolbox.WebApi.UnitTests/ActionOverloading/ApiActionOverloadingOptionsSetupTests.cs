using System;
using Microsoft.AspNet.Mvc;
using Toolbox.WebApi.ActionOverloading;
using Xunit;

namespace Toolbox.WebApi.UnitTests.ActionOverloading
{
    public class ApiActionOverloadingOptionsSetupTests
    {
        [Fact]
        private void ApiActionOverloadingConventionIsRegistered()
        {
            var options = new MvcOptions();
            var setup = new ApiActionOverloadingOptionsSetup();

            Assert.Equal(0, options.Conventions.Count);

            setup.Configure(options);

            Assert.Equal(1, options.Conventions.Count);           
        }
    }
}
