using System;
using System.Web.UI;
using EzWebForms.EzWebParts.Data;

namespace EzWebForms.EzWebParts
{
	public class EzWebPartPage : Page
	{
		public EzWebPartPage()
		{
			Data = new WebPartPageData();

			PreInit += EzWebPartPage_PreInit;
		}

		public WebPartPageData Data
		{
			get { return Items["WebPartPageData"] as WebPartPageData; }
			private set { Items["WebPartPageData"] = value; }
		}

		private void EzWebPartPage_PreInit(object sender, EventArgs e)
		{
			// todo: Set Data object through dependency injection
		}
	}
}