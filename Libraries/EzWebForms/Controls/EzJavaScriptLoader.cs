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
	public sealed class EzJavaScriptLoader : EzWebControl
	{
		public EzJavaScriptLoader()
		{
			Init += JavaScriptLoader_Init;
			PreRender += JavaScriptLoader_PreRender;
		}

		[TemplateContainer(typeof (EzJavaScriptLoader))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate ScriptTemplate { get; set; }

		private void JavaScriptLoader_Init(object sender, EventArgs e)
		{
			ScriptTemplate?.InstantiateIn(this);
		}

		private void JavaScriptLoader_PreRender(object sender, EventArgs e)
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

			var regex = new Regex(@"<script[^>]*>(?<Code>[\s\S]*?)</script>");
			var scriptText = $"{stringBuilder}";

			foreach (var match in regex.Matches(scriptText).Cast<Match>().Where(match => match.Groups["Code"].Value.Length > 0))
			{
				scriptText = scriptText.Replace(match.Groups["Code"].Value, $"<![CDATA[{match.Groups["Code"].Value.Trim()}]]>");
			}

			var scripts = JavaScriptCollection.Deserialize(scriptText);

			foreach (var script in scripts)
			{
				if (!string.IsNullOrWhiteSpace(script.Code))
				{
					JavaScriptManager.RegisterStartupScript(script.Code);
					continue;
				}

				if (!string.IsNullOrWhiteSpace(script.Source))
				{
					JavaScriptManager.RegisterClientScriptInclude(script.Source);
				}
			}
		}

		protected override void Render(HtmlTextWriter writer)
		{
		}

		[XmlRoot("ScriptTemplate")]
		[Serializable]
		public sealed class JavaScriptCollection
		{
			public JavaScriptCollection()
			{
				JavaScripts = new List<JavaScript>();
			}

			[XmlElement("script")]
			public List<JavaScript> JavaScripts { get; set; }

			public static List<JavaScript> Deserialize(string xml)
			{
				var serializer = new XmlSerializer(typeof (JavaScriptCollection));

				using (var textReader = new StringReader(xml))
				{
					try
					{
						var collection = serializer.Deserialize(textReader) as JavaScriptCollection;

						return collection == null
							? new List<JavaScript>()
							: collection.JavaScripts;
					}
					catch
					{
						return new List<JavaScript>();
					}
				}
			}
		}

		[Serializable]
		public sealed class JavaScript
		{
			[XmlAttribute("src")]
			public string Source { get; set; }

			[XmlText]
			public string Code { get; set; }
		}
	}
}