using System;
using EzWebForms.Pages;

namespace EzWebForms.Test
{
	public partial class Default : EzPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Response.Redirect("~/Templates/Default.aspx");
		}
	}
}