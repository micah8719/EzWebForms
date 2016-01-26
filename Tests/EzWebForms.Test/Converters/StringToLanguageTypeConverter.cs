using System;
using System.Globalization;
using EzWebForms.Base;
using EzWebForms.Test.Controls;

namespace EzWebForms.Test.Converters
{
	public class StringToLanguageTypeConverter : ValueConverterBase<string, CodeBlock.LanguageType>
	{
		public override CodeBlock.LanguageType Convert(string value, object parameter, CultureInfo culture)
		{
			CodeBlock.LanguageType languageType;
			Enum.TryParse(value, true, out languageType);
			return languageType;
		}

		public override string ConvertBack(CodeBlock.LanguageType value, object parameter, CultureInfo culture)
		{
			switch (value)
			{
				default:
					return "html";
				case CodeBlock.LanguageType.AspDotNet:
					return "xml";
				case CodeBlock.LanguageType.CSharp:
					return "cs";
				case CodeBlock.LanguageType.JavaScript:
					return "js";
			}
		}
	}
}