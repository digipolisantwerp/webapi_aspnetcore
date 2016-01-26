using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNet.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace Toolbox.WebApi.ActionOverloading
{
	public class QueryStringParameterCollection : IReadOnlyDictionary<string, StringValues>
	{
		public QueryStringParameterCollection(string queryString)
		{
			if ( queryString == null ) throw new ArgumentNullException(nameof(queryString), $"{nameof(queryString)} is null.");
			_queryParts = new Dictionary<string, StringValues>(QueryHelpers.ParseQuery(queryString), StringComparer.OrdinalIgnoreCase);
		}

		private readonly IDictionary<string, StringValues> _queryParts;

		public IEnumerable<string> Keys
		{
			get { return _queryParts.Keys; }
		}

		public IEnumerable<StringValues> Values
		{
			get { return _queryParts.Values; }
		}

		public int Count
		{
			get { return _queryParts.Count; }
		}

        public StringValues this[string key]
        {
            get { return _queryParts[key]; }
        }

        public bool ContainsKey(string key)
		{
			return _queryParts.ContainsKey(key);
		}

		public bool TryGetValue(string key, out StringValues value)
		{
			return _queryParts.TryGetValue(key, out value);
		}

		public IEnumerator<KeyValuePair<string, StringValues>> GetEnumerator()
		{
			return _queryParts.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _queryParts.GetEnumerator();
		}
	}
}