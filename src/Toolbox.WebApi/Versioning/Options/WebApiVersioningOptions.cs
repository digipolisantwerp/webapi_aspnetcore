using System;

namespace Toolbox.WebApi
{
    public class WebApiVersioningOptions
    {
        /// <summary>
        /// The route wwhere the version can be requested (default = '/admin/version').
        /// </summary>
        public string Route { get; set; }
    }
}