using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using EzWebForms.Linq;

namespace EzWebForms.Managers
{
	public sealed class EzStylesheetManager
	{
		private static readonly object ThreadLock = new object();
		private static readonly Dictionary<string, string> Stylesheets;
		private static readonly Dictionary<string, string> Styles;

		static EzStylesheetManager()
		{
			Stylesheets = new Dictionary<string, string>();
			Styles = new Dictionary<string, string>();
		}

		private readonly Page _page;

		public EzStylesheetManager(Page page)
		{
			_page = page;
		}

		public EzStylesheetManager(Control control) : this(control.Page)
		{
		}

		public void RegisterStylesheet(string url)
		{
			lock (ThreadLock)
			{
				string stylesheetUrl;

				if (!Stylesheets.TryGetValue(url.ToMd5(), out stylesheetUrl))
				{
					Stylesheets.Add(url.ToMd5(), url);
				}
				else
				{
					var existingStylesheet = _page.Header.Controls
						.OfType<HtmlLink>()
						.FirstOrDefault(stylesheet => stylesheet.Href == url);

					if (existingStylesheet != null)
					{
						_page.Header.Controls.Remove(existingStylesheet);
					}
				}
			}

			var htmlLink = new HtmlLink { Href = url };

			htmlLink.Attributes.Add("rel", "stylesheet");
			htmlLink.Attributes.Add("type", "text/css");

			_page.Header.Controls.Add(htmlLink);
		}

		public void RegisterStylesheetResource(Type type, string resourceName)
		{
			var stylesheet = _page.ClientScript.GetWebResourceUrl(type, resourceName);

			if (!String.IsNullOrWhiteSpace(stylesheet))
			{
				RegisterStylesheet(stylesheet);
			}
		}

		public void RegisterCss(string css)
		{
			lock (ThreadLock)
			{
				string styleCss;

				if (!Styles.TryGetValue(css.ToMd5(), out styleCss))
				{
					Styles.Add(css.ToMd5(), css);
				}
				else
				{
					var stylesheet = _page.Header.Controls
						.OfType<HtmlGenericControl>()
						.FirstOrDefault(style => style.TagName == "style" && style.InnerText == css);

					if (stylesheet != null)
					{
						_page.Header.Controls.Remove(stylesheet);
					}
				}
			}

			var cssStyle = new HtmlGenericControl {TagName = "style", InnerText = css};

			cssStyle.Attributes.Add("type", "text/css");

			_page.Header.Controls.Add(cssStyle);
		}
	}
}