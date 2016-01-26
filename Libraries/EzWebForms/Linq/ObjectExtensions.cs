using System.Globalization;
using EzWebForms.Extensibility;

namespace EzWebForms.Linq
{
	public static class ObjectExtensions
	{
		public static T ConvertTo<T>(this object value, object parameter, CultureInfo culture, IValueConverter converter)
		{
			return (T) converter.Convert(value, typeof (T), parameter, culture);
		}

		public static T ConvertTo<T>(this object value, object parameter, IValueConverter converter)
		{
			return ConvertTo<T>(value, parameter, CultureInfo.CurrentCulture, converter);
		}

		public static T ConvertTo<T>(this object value, IValueConverter converter)
		{
			return ConvertTo<T>(value, null, converter);
		}
	}
}