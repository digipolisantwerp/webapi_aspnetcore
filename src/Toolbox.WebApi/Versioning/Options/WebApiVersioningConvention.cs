using System;
using Microsoft.AspNet.Mvc.ApplicationModels;
using Microsoft.Extensions.OptionsModel;

namespace Toolbox.WebApi.Versioning
{
    public class WebApiVersioningConvention : IControllerModelConvention
    {
        public WebApiVersioningConvention(IOptions<WebApiVersioningOptions> options)
        {
            Options = options.Value;
        }

        internal WebApiVersioningOptions Options { get; private set; }

        public void Apply(ControllerModel controller)
        {
            if ( controller.ControllerType.FullName == "Toolbox.WebApi.Versioning.VersionController" && !String.IsNullOrWhiteSpace(Options.Route) )
            {
                controller.AttributeRoutes.Clear();
                controller.AttributeRoutes.Add(new AttributeRouteModel() { Name = "WebApiVersioningRoute", Order = 0, Template = Options.Route });
            }
        }
    }
}
