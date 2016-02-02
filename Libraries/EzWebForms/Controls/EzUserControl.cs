using System;
using System.Linq;
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
			get
			{
				return Attributes.Keys
					.Cast<string>()
					.ToDictionary(attribute => Attributes[attribute])
					.Where(attribute => attribute.Key.Equals("class", StringComparison.InvariantCultureIgnoreCase))
					.Select(attribute => attribute.Value)
					.FirstOrDefault() ?? String.Empty;
			}
			set
			{
				var existingAttribute = Attributes.Keys
					.Cast<string>()
					.ToDictionary(attribute => Attributes[attribute])
					.Where(attribute => attribute.Key.Equals("class", StringComparison.InvariantCultureIgnoreCase))
					.Select(attribute => attribute.Value)
					.FirstOrDefault();

				if (existingAttribute != null)
				{
					Attributes.Remove("class");
				}

				if (!String.IsNullOrWhiteSpace(value))
				{
					Attributes.Add("class", value);
				}
			}
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