using System;
using System.Web.UI;

namespace EzWebForms.Controls
{
	[ParseChildren(false)]
	[PersistChildren(false)]
	[Themeable(true)]
	public class EzUserControl : UserControl
	{
		[Themeable(true)]
		public virtual HtmlTextWriterTag Tag
		{
			get { return ViewState["Tag"] as HtmlTextWriterTag? ?? HtmlTextWriterTag.Div; }
			set { ViewState["Tag"] = value; }
		}

		[Themeable(true)]
		public virtual string CssClass
		{
			get { return ViewState["CssClass"] as string ?? String.Empty; }
			set { ViewState["CssClass"] = value; }
		}

		public virtual bool RenderContainer => true;

		public override ControlCollection Controls
		{
			get
			{
				EnsureChildControls();
				return base.Controls;
			}
		}

		protected virtual void AddAttributesToRender(HtmlTextWriter writer)
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

		protected override void Render(HtmlTextWriter writer)
		{
			if (!RenderContainer)
			{
				base.Render(writer);
				return;
			}

			AddAttributesToRender(writer);
			writer.RenderBeginTag(Tag);
			base.Render(writer);
			writer.RenderEndTag();
		}
	}
}