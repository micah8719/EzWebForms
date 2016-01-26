using System;
using System.Web.UI;
using EzWebForms.Managers;

namespace EzWebForms.Pages
{
	public class EzPage : Page
	{
		public EzPage()
		{
			Init += WebPartPage_Init;
		}

		protected EzJavaScriptManager JavaScriptManager { get; private set; }
		protected EzStylesheetManager StylesheetManager { get; private set; }

		private void WebPartPage_Init(object sender, EventArgs e)
		{
			JavaScriptManager = new EzJavaScriptManager(this);
			StylesheetManager = new EzStylesheetManager(this);
		}
	}
}