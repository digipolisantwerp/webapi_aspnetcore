using System;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;

namespace Toolbox.WebApi.ActionOverloading
{
    public class ApiActionOverloadingOptionsSetup : IConfigureOptions<MvcOptions>
    {
        public void Configure(MvcOptions options)
        {
            options.Conventions.Add(new ApiActionOverloadingConvention());
        }
    }
}
