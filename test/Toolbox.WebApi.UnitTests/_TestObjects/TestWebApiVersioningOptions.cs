using System;
using Microsoft.Extensions.OptionsModel;

namespace Toolbox.WebApi.UnitTests
{
    public class TestWebApiVersioningOptions : IOptions<WebApiVersioningOptions>
    {
        public TestWebApiVersioningOptions(WebApiVersioningOptions options)
        {
            Value = options;
        }

        public WebApiVersioningOptions Value { get; private set; }
    }
}
