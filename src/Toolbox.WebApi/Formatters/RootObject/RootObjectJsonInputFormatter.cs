using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using Microsoft.Net.Http.Headers;

namespace Toolbox.WebApi.Formatters
{
	public class RootObjectJsonInputFormatter : JsonInputFormatter
	{
		public override Task<object> ReadRequestBodyAsync(InputFormatterContext context)
		{
			var reader = GetReader(context);
			return Task.FromResult(ReadFromStream(context, reader));
		}

		public StreamReader GetReader(InputFormatterContext context)
		{
			var request = context.HttpContext.Request;

			MediaTypeHeaderValue requestContentType = null;
			MediaTypeHeaderValue.TryParse(request.ContentType, out requestContentType);

			// Get the character encoding for the content
			// Never non-null since SelectCharacterEncoding() throws in error / not found scenarios
			var effectiveEncoding = SelectCharacterEncoding(requestContentType);
			return new StreamReader(request.Body, effectiveEncoding);
		}

		public object ReadFromStream(InputFormatterContext context, StreamReader reader)
		{
			SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
			var type = context.ModelType;

			using (reader)
			{
				try
				{
					var rootName = RootObjectHelper.GetRootFieldName(type, false, SerializerSettings.ContractResolver);
					var jsonString = reader.ReadToEnd();
					var jsonObject = JObject.Parse(jsonString);
					var jsonToken = jsonObject.SelectToken(rootName);
					if (jsonToken == null)
						throw new RootObjectMissingException($"Root object met naam {rootName} ontbreekt in de input parameter. Als er toch een root object is, check dan of de spelling en casing kloppen.", type.Name);

					var dataObject = JsonConvert.DeserializeObject(jsonToken.ToString(), type, SerializerSettings);
					if (dataObject == null)
						throw new BadRootObjectException("Input parameter volgt structuur met Root object niet.", type.Name);

					return dataObject;
				}
				catch (RootObjectException rootEx)
				{
					context.ModelState.TryAddModelError(String.Empty, rootEx);
					throw;
				}
				catch (JsonSerializationException jsonEx)
				{
					context.ModelState.TryAddModelError(String.Empty, jsonEx);
					throw new BadRootObjectException(String.Format("Input parameter volgt structuur met Root object niet : {0}", jsonEx.Message), type.Name);
				}
				catch (Exception ex)
				{
					context.ModelState.TryAddModelError(String.Empty, ex);
					throw;
				}
			}
		}
	}
}