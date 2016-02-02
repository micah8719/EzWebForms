using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace EzWebForms.Linq
{
	public static class ControlExtensions
	{
		private static readonly object ThreadLock = new object();
		private static readonly Dictionary<string, string> Stylesheets;
		private static readonly Dictionary<string, string> Styles;

		static ControlExtensions()
		{
			Stylesheets = new Dictionary<string, string>();
			Styles = new Dictionary<string, string>();
		}

		public static IEnumerable<TControl> FindControlsOfType<TControl>(this Control currentControl)
			where TControl : Control
		{
			foreach (Control control in currentControl.Controls)
			{
				var tControl = control as TControl;

				if (tControl != null)
				{
					yield return tControl;
				}

				foreach (var innerControl in FindControlsOfType<TControl>(control))
				{
					yield return innerControl;
				}
			}
		}

		public static bool IsInUpdatePanel(this Control control)
		{
			if (control == null)
			{
				throw new ArgumentNullException(nameof(control));
			}

			var currentParent = control.Parent;

			while (currentParent != null && !(currentParent is Page))
			{
				if (currentParent is UpdatePanel)
				{
					return true;
				}

				currentParent = currentParent.Parent;
			}

			return false;
		}

		public static void RegisterClientScriptBlock(this Control control, string script, bool addScriptTags = true)
		{
			if (control.IsInUpdatePanel())
			{
				ScriptManager.RegisterClientScriptBlock(control, control.GetType(), script.ToMd5(), script, addScriptTags);
			}
			else
			{
				(control is Page ? ((Page) control) : control.Page).ClientScript.RegisterClientScriptBlock(control.GetType(), script.ToMd5(), script, addScriptTags);
			}
		}

		public static void RegisterClientScriptInclude(this Control control, string url)
		{
			if (control.IsInUpdatePanel())
			{
				ScriptManager.RegisterClientScriptInclude(control, control.GetType(), url.ToMd5(), url);
			}
			else
			{
				(control is Page ? ((Page) control) : control.Page).ClientScript.RegisterClientScriptInclude(control.GetType(), url.ToMd5(), url);
			}
		}

		public static void RegisterClientScriptResource(this Control control, Type type, string resourceName)
		{
			if (control.IsInUpdatePanel())
			{
				ScriptManager.RegisterClientScriptResource(control, type, resourceName);
			}
			else
			{
				(control is Page ? ((Page) control) : control.Page).ClientScript.RegisterClientScriptResource(type, resourceName);
			}
		}

		public static void RegisterHiddenField(this Control control, string fieldName, string fieldValue)
		{
			if (control.IsInUpdatePanel())
			{
				ScriptManager.RegisterHiddenField(control, fieldName, fieldValue);
			}
			else
			{
				(control is Page ? ((Page) control) : control.Page).ClientScript.RegisterHiddenField(fieldName, fieldValue);
			}
		}

		public static void RegisterOnSubmitStatement(this Control control, string script)
		{
			if (control.IsInUpdatePanel())
			{
				ScriptManager.RegisterOnSubmitStatement(control, control.GetType(), script.ToMd5(), script);
			}
			else
			{
				(control is Page ? ((Page) control) : control.Page).ClientScript.RegisterOnSubmitStatement(control.GetType(), script.ToMd5(), script);
			}
		}

		public static void RegisterStartupScript(this Control control, string script, bool addScriptTags = true)
		{
			if (control.IsInUpdatePanel())
			{
				ScriptManager.RegisterStartupScript(control, control.GetType(), script.ToMd5(), script, addScriptTags);
			}
			else
			{
				(control is Page ? ((Page) control) : control.Page).ClientScript.RegisterStartupScript(control.GetType(), script.ToMd5(), script, addScriptTags);
			}
		}

		public static void RegisterStylesheet(this Control control, string url)
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
					var existingStylesheet = (control is Page ? ((Page) control) : control.Page).Header.Controls
						.OfType<HtmlLink>()
						.FirstOrDefault(stylesheet => stylesheet.Href == url);

					if (existingStylesheet != null)
					{
						(control is Page ? ((Page) control) : control.Page).Header.Controls.Remove(existingStylesheet);
					}
				}
			}

			var htmlLink = new HtmlLink { Href = url };

			htmlLink.Attributes.Add("rel", "stylesheet");
			htmlLink.Attributes.Add("type", "text/css");

			(control is Page ? ((Page) control) : control.Page).Header.Controls.Add(htmlLink);
		}

		public static void RegisterStylesheetResource(this Control control, Type type, string resourceName)
		{
			var stylesheet = (control is Page ? ((Page) control) : control.Page).ClientScript.GetWebResourceUrl(type, resourceName);

			if (!String.IsNullOrWhiteSpace(stylesheet))
			{
				control.RegisterStylesheet(stylesheet);
			}
		}

		public static void RegisterCss(this Control control, string css)
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
					var stylesheet = (control is Page ? ((Page) control) : control.Page).Header.Controls
						.OfType<HtmlGenericControl>()
						.FirstOrDefault(style => style.TagName == "style" && style.InnerText == css);

					if (stylesheet != null)
					{
						(control is Page ? ((Page) control) : control.Page).Header.Controls.Remove(stylesheet);
					}
				}
			}

			var cssStyle = new HtmlGenericControl { TagName = "style", InnerText = css };

			cssStyle.Attributes.Add("type", "text/css");

			(control is Page ? ((Page) control) : control.Page).Header.Controls.Add(cssStyle);
		}
	}
}