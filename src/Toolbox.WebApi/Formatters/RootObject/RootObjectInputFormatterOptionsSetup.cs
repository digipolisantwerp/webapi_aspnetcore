using System;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Formatters;
using Microsoft.Extensions.OptionsModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Toolbox.WebApi.Utilities;

namespace Toolbox.WebApi.Formatters
{
    public class RootObjectInputFormatterOptionsSetup : IConfigureOptions<MvcOptions>
    {
        public void Configure(MvcOptions options)
        {
            var settings = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };

            ListHelper.RemoveTypes(options.InputFormatters, typeof(JsonInputFormatter));

            var inputFormatter = new RootObjectJsonInputFormatter() { SerializerSettings = settings };
            options.InputFormatters.Insert(0, inputFormatter);
        }
    }
}
