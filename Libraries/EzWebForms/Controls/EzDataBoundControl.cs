using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EzWebForms.Controls
{
	public class EzDataBoundControl : CompositeDataBoundControl
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

		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID);

			if (!string.IsNullOrWhiteSpace(CssClass))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, CssClass);
			}

			foreach (string attribute in Attributes.Keys)
			{
				writer.AddAttribute(attribute, Attributes[attribute]);
			}
		}

		protected override int CreateChildControls(IEnumerable dataSource, bool dataBinding)
		{
			var count = 0;
			return CreateChildControlsImpl(dataSource, dataBinding, ref count);
		}

		protected virtual int CreateChildControlsImpl(IEnumerable dataSource, bool dataBinding, ref int controlCount)
		{
			return controlCount;
		}
	}
}