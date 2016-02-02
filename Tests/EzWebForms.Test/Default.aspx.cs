using System;
using System.Web.UI;

namespace EzWebForms.Test
{
	public partial class Default : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Response.Redirect("~/Templates/Default.aspx");
		}
	}
}