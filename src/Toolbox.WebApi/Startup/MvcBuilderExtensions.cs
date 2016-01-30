using System;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.OptionsModel;
using Toolbox.WebApi.ActionOverloading;
using Toolbox.WebApi.Formatters;
using Toolbox.WebApi.Versioning;

namespace Toolbox.WebApi
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddActionOverloading(this IMvcBuilder builder)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<MvcOptions>, ApiActionOverloadingOptionsSetup>());

            return builder;
        }

        public static IMvcBuilder AddRootObjectInputFormatter(this IMvcBuilder builder)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<MvcOptions>, RootObjectInputFormatterOptionsSetup>());

            return builder;
        }

        public static IMvcBuilder AddRootObjectOutputFormatter(this IMvcBuilder builder)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<MvcOptions>, RootObjectOutputFormatterOptionsSetup>());

            return builder;
        }

        public static IMvcBuilder AddVersioning(this IMvcBuilder builder, Action<WebApiVersioningOptions> setupAction = null)
        {
            if ( setupAction != null )
            {
                builder.Services.Configure(setupAction);
            }

            builder.Services.TryAddSingleton<IVersionProvider, WebApiVersionProvider>();
            builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<MvcOptions>, WebApiVersioningOptionsSetup>());

            return builder;
        }
    }
}
