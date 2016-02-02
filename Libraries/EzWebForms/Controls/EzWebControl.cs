using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EzWebForms.Controls
{
	[ParseChildren(false)]
	[PersistChildren(false)]
	[Themeable(true)]
	public class EzWebControl : WebControl, INamingContainer
	{
		[Themeable(true)]
		public virtual HtmlTextWriterTag Tag
		{
			get { return ViewState["Tag"] as HtmlTextWriterTag? ?? HtmlTextWriterTag.Div; }
			set { ViewState["Tag"] = value; }
		}

		[Themeable(true)]
		public override string CssClass
		{
			get { return base.CssClass; }
			set { base.CssClass = value; }
		}

		protected override HtmlTextWriterTag TagKey => Tag;

		public override ControlCollection Controls
		{
			get
			{
				EnsureChildControls();
				return base.Controls;
			}
		}

		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID);

			if (!String.IsNullOrWhiteSpace(CssClass))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, CssClass);
			}

			foreach (string attribute in Attributes.Keys)
			{
				writer.AddAttribute(attribute, Attributes[attribute]);
			}
		}
	}
}
