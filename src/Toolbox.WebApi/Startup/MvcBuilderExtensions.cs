using System;
using Microsoft.Extensions.DependencyInjection;
using  Microsoft.Extensions.DependencyInjection.Extensions;

namespace Toolbox.WebApi
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddRootObjectInputFormatter(this IMvcBuilder builder)
        {
            //builder.Services.TryAddEnumerable();

            return builder;
        }
    }
}
