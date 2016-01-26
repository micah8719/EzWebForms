using System;
using System.Collections.Specialized;
using System.Web.UI;
using EzWebForms.Controls;
using EzWebForms.Linq;

namespace EzWebForms.Test.UserControls
{
	public partial class CkEditor : EzUserControl, IPostBackDataHandler
	{
		private readonly LiteralControl _content;

		public CkEditor()
		{
			_content = new LiteralControl();

			Init += CkEditor_Init;
			PreRender += CkEditor_PreRender;
		}

		public string Text
		{
			get { return ViewState["Text"] as string ?? string.Empty; }
			set { ViewState["Text"] = value; }
		}

		public override bool RenderContainer => false;

		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			var data = postCollection[postDataKey];

			if (data == null)
			{
				return false;
			}

			var decodedHtml = data.FromBase64().HtmlDecode();

			if (decodedHtml == Text)
			{
				return false;
			}

			Text = decodedHtml;

			return true;
		}

		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			OnTextChanged();
		}

		private void CkEditor_Init(object sender, EventArgs e)
		{
			Editor.Tag = Tag;
			Editor.Controls.Add(_content);
		}

		private void CkEditor_PreRender(object sender, EventArgs e)
		{
			_content.Text = Text;
			EditorData.Value = Text.HtmlEncode().ToBase64();
		}

		public event EventHandler TextChanged;

		protected virtual void OnTextChanged()
		{
			TextChanged?.Invoke(this, EventArgs.Empty);
		}

		protected void SaveButton_OnClick(object sender, EventArgs e)
		{
			// place breakpoint here to see value
			var text = Text;
		}
	}
}