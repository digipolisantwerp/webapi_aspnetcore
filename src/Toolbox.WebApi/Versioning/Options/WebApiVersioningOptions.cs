using System;
using System.Linq;
using System.Reflection;
using Toolbox.Common.Helpers;
using Toolbox.WebApi.Routing;

namespace Toolbox.WebApi
{
    public class WebApiVersioningOptions
    {
       
        public WebApiVersioningOptions(Assembly controllerAssembly = null)
        {
            if (controllerAssembly == null)
                ControllerAssembly = FindControllerAssembly();
            else
                ControllerAssembly = controllerAssembly;
            this.Route = Routes.VersionController;
        }

        /// <summary>
        /// The route wwhere the version can be requested (default = '/admin/version').
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// De assembly where the version controllers are defined (default = the assembly where the Startup class is found).
        /// </summary>
        public Assembly ControllerAssembly { get; private set; }

        /// <summary>
        /// Default Route = 'admin/version' and default Assembly is the one where the Startup class is fount in.
        /// </summary>
        public static WebApiVersioningOptions Default { get { return new WebApiVersioningOptions(); } }

        private Assembly FindControllerAssembly()
        {
            var startup = ReflectionHelper.GetTypesFromAppDomain("startup");
            //if ( startup.Count() != 1 ) return;   // ToDo (SVB) :  < 1
            var controllerAssembly = startup.First().Assembly;
            return controllerAssembly;
        }
    }
}