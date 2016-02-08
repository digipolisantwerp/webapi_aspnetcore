using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toolbox.WebApi.CorrelationId;
using Xunit;

namespace Toolbox.WebApi.UnitTests.CorrelationId
{
    public class CorrelationOptionsTests
    {
        [Fact]
        public void SetDefaultValuesOnCreation()
        {
            var options = new CorrelationOptions();

            Assert.Equal(CorrelationHeaders.IdHeaderKey, options.IdHeaderKey);
            Assert.Equal(CorrelationHeaders.SourceHeaderKey, options.SourceHeaderKey);
        }
    }
}
