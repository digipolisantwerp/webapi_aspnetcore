using System;
using Toolbox.WebApi.Routing;
using Toolbox.WebApi.Versioning.Models;
using Microsoft.AspNet.Mvc;

namespace Toolbox.WebApi.Versioning
{
    [Route(Routes.VersionController)]
    public class VersionController : Controller
    {
        private IVersionProvider _versionProvider;

        public VersionController(IVersionProvider versionProvider)
        {
            _versionProvider = versionProvider;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
               
                return new ObjectResult(_versionProvider.GetCurrentVersion()) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                var error = new Error(ex.Message);
                return new ObjectResult(error) { StatusCode = 500 };
            }
        }
    }
}
