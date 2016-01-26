using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc.ApplicationModels;
using Toolbox.WebApi.ActionOverloading;
using Xunit;

namespace Toolbox.WebApi.UnitTests.ActionOverloading
{
    public class ApiActionOverloadingConventionTests
    {
        [Fact]
        private void ActionNullRaisesArgumentNullException()
        {
            var convention = new ApiActionOverloadingConvention();

            var ex = Assert.Throws<ArgumentNullException>(() => convention.Apply(null));

            Assert.Equal("actionModel", ex.ParamName);
        }

        [Fact]
        private void ApiActionOverloadingConstraintIsRegistered()
        {
            var methodInfo = typeof(TestController).GetMethod("Get");
            var actionModel = new ActionModel(methodInfo, new List<object>() { });

            var convention = new ApiActionOverloadingConvention();
            convention.Apply(actionModel);

            Assert.Contains(actionModel.ActionConstraints, item => item.GetType().IsAssignableFrom(typeof(OverloadActionConstraint)));
        }
    }
}
