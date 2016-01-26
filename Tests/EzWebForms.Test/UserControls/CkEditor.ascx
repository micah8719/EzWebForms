<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CkEditor.ascx.cs" Inherits="EzWebForms.Test.UserControls.CkEditor" %>
<%@ Register TagPrefix="ez" Namespace="EzWebForms.Controls" Assembly="EzWebForms" %>

<ez:EzWebControl runat="server" ID="Editor" CssClass="Editor" contenteditable="true"/>

<asp:LinkButton runat="server" Text="Save" ID="SaveButton" CssClass="ui-btn ui-btn-inline ui-icon-edit ui-btn-icon-left" OnClick="SaveButton_OnClick"/>

<asp:HiddenField runat="server" ID="EditorData"/>

<ez:EzJavaScriptLoader runat="server">
	<ScriptTemplate>
		<script src="//code.jquery.com/jquery-1.12.0.min.js"></script>
		<script src="//cdn.ckeditor.com/4.5.6/standard/ckeditor.js"></script>
		<script type="text/javascript">
			var Editor = Editor || {
				HtmlEncode: function(input) {
					return String(input)
						.replace(/&/g, '&amp;')
						.replace(/'/g, "&#39;")
						.replace(/"/g, '&quot;')
						.replace(/</g, "&lt;")
						.replace(/>/g, "&gt;");
				},
				Base64Encode: function(input) {
					return window.btoa(unescape(encodeURIComponent(input)));
				},
				RegisterOnSubmit: function(editorId, hiddenFieldId) {
					if (jQuery == null || CKEDITOR == null) {
						return;
					}

					var editor = CKEDITOR.instances[editorId];
					var $hiddenField = jQuery('#' + hiddenFieldId);

					$hiddenField.val(this.Base64Encode(this.HtmlEncode(editor.getData())));
				}
			};
		</script>
		<script type="text/javascript">
			jQuery(document.forms[0]).submit(function() {
				Editor.RegisterOnSubmit('<%= Editor.ClientID %>', '<%= EditorData.ClientID %>');
			});
		</script>
	</ScriptTemplate>
</ez:EzJavaScriptLoader>

<ez:EzStylesheetLoader runat="server">
	<StyleTemplate>
		<style type="text/css">
			.Editor { border: 1px dashed #c0c0c0; }
			.Editor:hover { border: 2px dashed #0094ff; }
		</style>
	</StyleTemplate>
</ez:EzStylesheetLoader>