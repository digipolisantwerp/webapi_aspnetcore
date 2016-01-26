using System;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Formatters;
using Microsoft.Extensions.OptionsModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Toolbox.WebApi.Utilities;

namespace Toolbox.WebApi.Formatters
{
    public class RootObjectOutputFormatterOptionsSetup : IConfigureOptions<MvcOptions>
    {
        public void Configure(MvcOptions options)
        {
            var settings = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };

            ListHelper.RemoveTypes(options.OutputFormatters, typeof(JsonOutputFormatter));

            var outputFormatter = new RootObjectJsonOutputFormatter() { SerializerSettings = settings };
            options.OutputFormatters.Insert(0, outputFormatter);
        }
    }
}
