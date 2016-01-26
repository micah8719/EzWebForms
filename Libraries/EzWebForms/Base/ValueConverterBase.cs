using System;
using System.Globalization;
using EzWebForms.Extensibility;

namespace EzWebForms.Base
{
	public abstract class ValueConverterBase<T1, T2> : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Convert((T1) value, parameter, culture);
		}

		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ConvertBack((T2) value, parameter, culture);
		}

		public abstract T2 Convert(T1 value, object parameter, CultureInfo culture);

		public T2 Convert(T1 value, object parameter)
		{
			return Convert(value, parameter, CultureInfo.CurrentCulture);
		}

		public T2 Convert(T1 value)
		{
			return Convert(value, null);
		}

		public abstract T1 ConvertBack(T2 value, object parameter, CultureInfo culture);

		public T1 ConvertBack(T2 value, object parameter)
		{
			return ConvertBack(value, parameter, CultureInfo.CurrentCulture);
		}

		public T1 ConvertBack(T2 value)
		{
			return ConvertBack(value, null);
		}
	}
}