using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.OptionsModel;
using Microsoft.Extensions.DependencyInjection;

namespace Toolbox.WebApi.Versioning
{
    public class WebApiVersioningOptionsSetup : IConfigureOptions<MvcOptions>
    {
        public WebApiVersioningOptionsSetup(IApplicationBuilder app)
        {
            OptionsServices = app.ApplicationServices;
        }

        internal IServiceProvider OptionsServices { get; private set; }

        public void Configure(MvcOptions options)
        {
            var versionOptions = OptionsServices.GetService<IOptions<WebApiVersioningOptions>>();
            //options.Conventions.Add(new WebApiVersioningConvention());
        }
    }
}
