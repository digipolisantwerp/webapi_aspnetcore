using System;
using Microsoft.AspNet.Mvc;
using Toolbox.WebApi.Filters;
using Xunit;

namespace Toolbox.WebApi.UnitTests.Filters
{
    public class ValidateModelStateAttributeTests
    {
        [Fact]
        private void ValidModelStateDoesNothing()
        {
            var context = ActionFilterFactory.CreateActionExecutingContext();

            var filter = new ValidateModelStateAttribute();
            filter.OnActionExecuting(context);

            Assert.Null(context.Result);
        }

        [Fact]
        private void InvalidModelStateSetsBadRequestResult()
        {
            var context = ActionFilterFactory.CreateActionExecutingContext();
            context.ModelState.AddModelError("key1", "error1");
            
            var filter = new ValidateModelStateAttribute();
            filter.OnActionExecuting(context);

            Assert.NotNull(context.Result);
            Assert.IsType<BadRequestObjectResult>(context.Result);
        }
    }
}
