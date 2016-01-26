using System;
using System.Web.UI;
using EzWebForms.Linq;

namespace EzWebForms.Managers
{
	public sealed class EzJavaScriptManager
	{
		private readonly Page _page;
		private readonly Control _control;
		private readonly Type _type;
		private readonly bool _isInUpdatePanel;

		public EzJavaScriptManager(Page page)
		{
			_page = page;
			_type = page.GetType();
		}

		public EzJavaScriptManager(Control control) : this(control.Page)
		{
			_control = control;
			_type = control.GetType();
			_isInUpdatePanel = IsInUpdatePanel();
		}

		public void RegisterClientScriptBlock(string script, bool addScriptTags = true)
		{
			if (_isInUpdatePanel)
			{
				ScriptManager.RegisterClientScriptBlock(_control, _type, script.ToMd5(), script, addScriptTags);
			}
			else
			{
				_page.ClientScript.RegisterClientScriptBlock(_type, script.ToMd5(), script, addScriptTags);
			}
		}

		public void RegisterClientScriptInclude(string url)
		{
			if (_isInUpdatePanel)
			{
				ScriptManager.RegisterClientScriptInclude(_control, _type, url.ToMd5(), url);
			}
			else
			{
				_page.ClientScript.RegisterClientScriptInclude(_type, url.ToMd5(), url);
			}
		}

		public void RegisterClientScriptResource(Type type, string resourceName)
		{
			if (_isInUpdatePanel)
			{
				ScriptManager.RegisterClientScriptResource(_control, type, resourceName);
			}
			else
			{
				_page.ClientScript.RegisterClientScriptResource(type, resourceName);
			}
		}

		public void RegisterHiddenField(string fieldName, string fieldValue)
		{
			if (_isInUpdatePanel)
			{
				ScriptManager.RegisterHiddenField(_control, fieldName, fieldValue);
			}
			else
			{
				_page.ClientScript.RegisterHiddenField(fieldName, fieldValue);
			}
		}

		public void RegisterOnSubmitStatement(string script)
		{
			if (_isInUpdatePanel)
			{
				ScriptManager.RegisterOnSubmitStatement(_control, _type, script.ToMd5(), script);
			}
			else
			{
				_page.ClientScript.RegisterOnSubmitStatement(_type, script.ToMd5(), script);
			}
		}

		public void RegisterStartupScript(string script, bool addScriptTags = true)
		{
			if (_isInUpdatePanel)
			{
				ScriptManager.RegisterStartupScript(_control, _type, script.ToMd5(), script, addScriptTags);
			}
			else
			{
				_page.ClientScript.RegisterStartupScript(_type, script.ToMd5(), script, addScriptTags);
			}
		}

		private bool IsInUpdatePanel()
		{
			var currentParent = _control.Parent;

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
	}
}