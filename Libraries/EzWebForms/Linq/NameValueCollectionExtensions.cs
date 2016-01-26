using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace EzWebForms.Linq
{
	public static class NameValueCollectionExtensions
	{
		public static T GetItem<T>(this NameValueCollection collection, string key, T defaultValue = default(T))
			where T : IConvertible
		{
			if (collection == null)
			{
				throw new ArgumentNullException(nameof(collection));
			}

			if (key == null)
			{
				throw new ArgumentNullException(nameof(key));
			}

			var item = collection[key];

			return item != null
				? item.Trim().ConvertTo(defaultValue)
				: defaultValue;
		}

		public static IEnumerable<T> GetItems<T>(this NameValueCollection collection, string key, string separator = ",",
			StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
			where T : IConvertible
		{
			if (collection == null)
			{
				throw new ArgumentNullException(nameof(collection));
			}

			if (key == null)
			{
				throw new ArgumentNullException(nameof(key));
			}

			var item = collection[key];

			if (item == null)
			{
				return Enumerable.Empty<T>();
			}

			var items = item.Trim().Split(new[] {separator}, splitOptions);

			return items.Where(i => i != null).Select(i => i.ConvertTo<T>());
		}
	}
}