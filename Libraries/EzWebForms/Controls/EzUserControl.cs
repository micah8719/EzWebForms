using System;
using System.Linq;
using System.Web.UI;
using EzWebForms.Managers;

namespace EzWebForms.Controls
{
	[ParseChildren(false)]
	[PersistChildren(false)]
	[Themeable(true)]
	public class EzUserControl : UserControl
	{
		protected EzJavaScriptManager JavaScriptManager { get; private set; }
		protected EzStylesheetManager StylesheetManager { get; private set; }

		public EzUserControl()
		{
			Init += WebPartUserControl_Init;
		}

		private void WebPartUserControl_Init(object sender, EventArgs e)
		{
			JavaScriptManager = new EzJavaScriptManager(this);
			StylesheetManager = new EzStylesheetManager(this);
		}

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