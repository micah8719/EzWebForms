using System;
using System.Linq;
using System.Web.UI;
using EzWebForms.Controls;
using EzWebForms.Test.Converters;

namespace EzWebForms.Test.Controls
{
	public class CodeBlock : EzWebControl
	{
		private static readonly StringToLanguageTypeConverter Converter;

		static CodeBlock()
		{
			Converter = new StringToLanguageTypeConverter();
		}

		public enum LanguageType
		{
			Html,
			JavaScript,
			AspDotNet,
			CSharp,
		}

		public LanguageType Language
		{
			get { return ViewState["Language"] as LanguageType? ?? LanguageType.Html; }
			set { ViewState["Language"] = value; }
		}

		public override HtmlTextWriterTag Tag
		{
			get
			{
				return HtmlTextWriterTag.Pre;
			}
			set { }
		}

		public CodeBlock()
		{
			Init += CodeBlock_Init;
		}

		private void CodeBlock_Init(object sender, EventArgs e)
		{
			CssClass = $"prettyprint lang-{Converter.ConvertBack(Language)} linenums";
		}
	}
}