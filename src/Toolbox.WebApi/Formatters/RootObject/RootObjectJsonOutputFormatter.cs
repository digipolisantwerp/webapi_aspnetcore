using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Formatters;
using Newtonsoft.Json;

namespace Toolbox.WebApi.Formatters
{
    public class RootObjectJsonOutputFormatter : JsonOutputFormatter
    {
        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;

            var objectType = context.Object.GetType();

            var root = RootObjectHelper.GetRootFieldName(objectType, false, this.SerializerSettings.ContractResolver);
            var obj = new ExpandoObject() as IDictionary<string, Object>;
            obj[root] = context.Object;
            //context.Object = obj as object;

            var ctx = new OutputFormatterWriteContext(context.HttpContext, context.WriterFactory, objectType, obj);

            return base.WriteResponseBodyAsync(ctx);
        }
    }
}