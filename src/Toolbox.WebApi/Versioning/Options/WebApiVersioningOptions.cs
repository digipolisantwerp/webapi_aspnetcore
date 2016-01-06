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
        /// De route waar de versie kan opgevraagd worden (default = '/admin/version').
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// De assembly waar de versie controllers gedefinieerd zijn (default = de assembly waar de Startup class gevonden wordt).
        /// </summary>
        public Assembly ControllerAssembly { get; private set; }

        /// <summary>
        /// Default Route = 'admin/version' en default Assembly is diegene waar de Startup class in gevonden wordt.
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