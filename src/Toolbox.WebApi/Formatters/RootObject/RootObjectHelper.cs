using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using Newtonsoft.Json.Serialization;
using System.Text.RegularExpressions;

namespace Toolbox.WebApi.Formatters
{
    public static class RootObjectHelper
    {

		public static string GetRootFieldName(Type type, bool inList, IContractResolver resolver)
		{
			if (typeof(IEnumerable).IsAssignableFrom(type) && !inList)
			{
				var underlyingType = type.GetGenericArguments().FirstOrDefault();
				if (underlyingType == null) return GetRootFieldName(type, true, resolver);

				var rootName = GetRootFieldName(underlyingType, true, resolver);
				return rootName;
			}

			var result = String.Empty;
			var typeName = type.Name;

			var attributeType = inList ? typeof(RootListObjectAttribute) : typeof(RootObjectAttribute);
			var attributes = type.CustomAttributes.Where(x => x.AttributeType == attributeType).ToList();

			if (attributes.Count == 0)
			{
				if (inList) result = "ListOf";
				result += typeName;
			}
			else
			{
				var name = attributes.First().ConstructorArguments.First();
				result = name.Value.ToString();
			}

			return ToCamelCaseIfNeeded(result, resolver);
		}

		private static string ToCamelCaseIfNeeded(string input, IContractResolver resolver)
		{
			if (resolver == null) return input;

			if (resolver.GetType().IsAssignableFrom(typeof(CamelCasePropertyNamesContractResolver)))
				return ToCamelCase(input);
			else
				return input;
		}

		internal static string ToCamelCase(string input)
		{
			if (String.IsNullOrWhiteSpace(input)) return input;

			if (input.Length < 2)
			{
				if (!Char.IsUpper(input[0]))
				{
					return input;
				}
				else
				{
					return Camelize(input);
				}
			}

			var clean = Regex.Replace(input, @"[\W]", " ");

			var words = clean.Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);
			var result = "";
			for (int i = 0; i < words.Length; i++)
			{
				var currentWord = words[i];
				if (i == 0)
					result += Camelize(currentWord);
				else
					result += Pascalize(currentWord);

				if (currentWord.Length > 1) result += currentWord.Substring(1);
			}

			return result;
		}

		private static string Camelize(string input)
		{
			return Char.ToLower(input[0], CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
		}

		private static string Pascalize(string input)
		{
			return Char.ToUpper(input[0], CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
		}

	}
}