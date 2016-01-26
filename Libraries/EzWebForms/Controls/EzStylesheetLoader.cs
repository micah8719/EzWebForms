using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Xml.Serialization;

namespace EzWebForms.Controls
{
	public sealed class EzStylesheetLoader : EzWebControl
	{
		public EzStylesheetLoader()
		{
			Init += EzStylesheetLoader_Init;
			PreRender += EzStylesheetLoader_PreRender;
		}

		[TemplateContainer(typeof (EzJavaScriptLoader))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate StyleTemplate { get; set; }

		private void EzStylesheetLoader_Init(object sender, EventArgs e)
		{
			StyleTemplate?.InstantiateIn(this);
		}

		private void EzStylesheetLoader_PreRender(object sender, EventArgs e)
		{
			if (!Visible)
			{
				return;
			}

			var stringBuilder = new StringBuilder();

			using (var textWriter = new StringWriter(stringBuilder))
			{
				using (var scriptWriter = new HtmlTextWriter(textWriter))
				{
					RenderContents(scriptWriter);
				}
			}

			var regex = new Regex(@"<style[^>]*>(?<Code>[\s\S]*?)</style>");
			var styleText = $"{stringBuilder}";

			foreach (var match in regex.Matches(styleText).Cast<Match>().Where(match => match.Groups["Code"].Value.Length > 0))
			{
				styleText = styleText.Replace(match.Groups["Code"].Value, $"<![CDATA[{match.Groups["Code"].Value.Trim()}]]>");
			}

			var styleCollection = StylesheetCollection.Deserialize(styleText);

			foreach (var stylesheet in styleCollection.Stylesheets)
			{
				StylesheetManager.RegisterStylesheet(stylesheet.HRef);
			}

			foreach (var cssStyle in styleCollection.CssStyles)
			{
				StylesheetManager.RegisterCss(cssStyle.Code);
			}
		}

		protected override void Render(HtmlTextWriter writer)
		{
		}

		[XmlRoot("StyleTemplate")]
		[Serializable]
		public sealed class StylesheetCollection
		{
			public StylesheetCollection()
			{
				Stylesheets = new List<Stylesheet>();
				CssStyles = new List<CssStyle>();
			}

			[XmlElement("link")]
			public List<Stylesheet> Stylesheets { get; set; }

			[XmlElement("style")]
			public List<CssStyle> CssStyles { get; set; }

			public static StylesheetCollection Deserialize(string xml)
			{
				var serializer = new XmlSerializer(typeof (StylesheetCollection));

				using (var textReader = new StringReader(xml))
				{
					try
					{
						return serializer.Deserialize(textReader) as StylesheetCollection ?? new StylesheetCollection();
					}
					catch
					{
						return new StylesheetCollection();
					}
				}
			}
		}

		[Serializable]
		public sealed class Stylesheet
		{
			[XmlAttribute("href")]
			public string HRef { get; set; }
		}

		[Serializable]
		public sealed class CssStyle
		{
			[XmlText]
			public string Code { get; set; }
		}
	}
}