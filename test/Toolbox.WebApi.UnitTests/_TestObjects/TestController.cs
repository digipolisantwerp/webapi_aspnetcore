using System;
using Microsoft.AspNet.Mvc;

namespace Toolbox.WebApi.UnitTests
{
    public class TestController
    {
        public IActionResult Get()
        {
            return new HttpOkResult();
        }
    }
}
