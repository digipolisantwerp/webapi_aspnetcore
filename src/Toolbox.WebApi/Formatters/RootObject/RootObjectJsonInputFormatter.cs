using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNet.Mvc.Formatters;

namespace Toolbox.WebApi.Formatters
{
	public class RootObjectJsonInputFormatter : JsonInputFormatter
	{
		public override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
		{
			var reader = GetReader(context);
			try
			{
				var jsonObject = ReadFromStream(context, reader);
				return InputFormatterResult.SuccessAsync(jsonObject);
			}
			catch (Exception)
			{
				return InputFormatterResult.FailureAsync();
				throw;
			}
		}

		public StreamReader GetReader(InputFormatterContext context)
		{
			var request = context.HttpContext.Request;

			MediaTypeHeaderValue requestContentType = null;
			MediaTypeHeaderValue.TryParse(request.ContentType, out requestContentType);

			// Get the character encoding for the content
			// Never non-null since SelectCharacterEncoding() throws in error / not found scenarios
			var effectiveEncoding = SelectCharacterEncoding(context);
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
						throw new RootObjectMissingException($"Root object with name {rootName} is missing in the input parameter. If there is a root object, then check the accuracy of spelling and casing.", type.Name);

					var dataObject = JsonConvert.DeserializeObject(jsonToken.ToString(), type, SerializerSettings);
					if (dataObject == null)
						throw new BadRootObjectException("Input parameter does not follow the structure with the root object.", type.Name);

					return dataObject;
				}
				catch (RootObjectException rootEx)
				{
					context.ModelState.TryAddModelError(String.Empty, rootEx.Message);
					throw;
				}
				catch (JsonSerializationException jsonEx)
				{
					context.ModelState.TryAddModelError(String.Empty, jsonEx.Message);
					throw new BadRootObjectException(String.Format("Input parameter does not follow the structure with the root object : {0}", jsonEx.Message), type.Name);
				}
				catch (Exception ex)
				{
					context.ModelState.TryAddModelError(String.Empty, ex.Message);
					throw;
				}
			}
		}
	}
}