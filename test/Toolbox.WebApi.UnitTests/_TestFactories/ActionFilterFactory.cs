using System;
using System.Collections.Generic;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Abstractions;
using Microsoft.AspNet.Mvc.Filters;
using Microsoft.AspNet.Routing;
using Moq;

namespace Toolbox.WebApi.UnitTests
{
    public static class ActionFilterFactory
    {
        public static ActionExecutingContext CreateActionExecutingContext()
        {
            var mockFilter = new Mock<ActionFilterAttribute>();

            return new ActionExecutingContext(
                CreateActionContext(),
                new IFilterMetadata[] { mockFilter.As<IFilterMetadata>().Object, },
                new Dictionary<string, object>(),
                controller: new object());
        }

        public static ActionContext CreateActionContext()
        {
            return new ActionContext(Mock.Of<HttpContext>(), new RouteData(), new ActionDescriptor());
        }
    }
}
