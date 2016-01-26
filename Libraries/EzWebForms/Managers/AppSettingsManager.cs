using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Runtime.CompilerServices;
using EzWebForms.Linq;

namespace EzWebForms.Managers
{
	public static class AppSettingsManager
	{
		private static readonly NameValueCollection AppSettings;

		static AppSettingsManager()
		{
			AppSettings = ConfigurationManager.AppSettings;
		}

		public static T GetItem<T>([CallerMemberName] string key = "", T defaultValue = default(T))
			where T : IConvertible
		{
			return AppSettings.GetItem(key, defaultValue);
		}

		public static IEnumerable<T> GetItems<T>([CallerMemberName] string key = "", string separator = ",",
			StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
			where T : IConvertible
		{
			return AppSettings.GetItems<T>(key, separator);
		}
	}
}