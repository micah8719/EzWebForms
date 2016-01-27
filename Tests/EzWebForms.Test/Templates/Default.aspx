<%@ Page Language="C#" MasterPageFile="~/Masters/Standard.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EzWebForms.Test.Templates.Default" %>
<%@ Register TagPrefix="control" Namespace="EzWebForms.Test.Controls" Assembly="EzWebForms.Test" %>
<%@ Register TagPrefix="uc" TagName="CkEditor" Src="~/UserControls/CkEditor.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="Scripts">
	<script src="//code.jquery.com/jquery-1.12.0.min.js"></script>
	<script src="//code.jquery.com/jquery-1.12.0.min.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Styles">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Header">
	<h1>EzWebForms Framework</h1>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
	<div class="ui-body ui-body-a ui-corner-all" data-role="collapsible">
		<h3>EzJavaScriptLoader</h3>
		<control:CodeBlock runat="server" Language="AspDotNet">
&lt;ez:EzJavaScriptLoader runat="server"&gt;
	&lt;ScriptTemplate&gt;
		&lt;script src="//code.jquery.com/jquery-1.12.0.min.js"&gt;&lt;/script&gt;
		&lt;script type="text/javascript"&gt;
			$(document).ready(function(){
				alert("Hello, world!");
			});
		&lt;/script&gt;
	&lt;/ScriptTemplate&gt;
&lt;/ez:EzJavaScriptLoader&gt;
		</control:CodeBlock>
	</div>

	<div class="ui-body ui-body-a ui-corner-all" data-role="collapsible">
		<h3>EzStylesheetLoader</h3>
		<control:CodeBlock runat="server" Language="AspDotNet">
&lt;ez:EzStylesheetLoader runat="server"&gt;
	&lt;StyleTemplate&gt;
		&lt;link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/ui-lightness/jquery-ui.css"/&gt;
		&lt;style type="text/css"&gt;
			body {
				background-color: #000;
				color: #fff;
			}
		&lt;/style&gt;
	&lt;/StyleTemplate&gt;
&lt;/ez:EzStylesheetLoader&gt;
		</control:CodeBlock>
	</div>
	
	<div class="ui-body ui-body-a ui-corner-all" data-role="collapsible">
		<h3>EzUserControl</h3>
		<control:CodeBlock runat="server" Language="CSharp">
public class MyUserControl : EzUserControl
{
	protected void Page_Load(object sender, EventArgs e)
	{
		JavaScriptManager.RegisterClientScriptInclude("//code.jquery.com/jquery-1.12.0.min.js");
		StylesheetManager.RegisterStylesheet("//code.jquery.com/ui/1.11.4/themes/ui-lightness/jquery-ui.css");
	}
}
		</control:CodeBlock>
	</div>
	
	<div class="ui-body ui-body-a ui-corner-all" data-role="collapsible">
		<h3>EzWebControl</h3>
		<control:CodeBlock runat="server" Language="CSharp">
public class MyServerControl : EzWebControl
{
	public MyServerControl()
	{
		Load += MyServerControl_Load;
	}

	private void MyServerControl_Load(object sender, EventArgs e)
	{
		JavaScriptManager.RegisterClientScriptInclude("//code.jquery.com/jquery-1.12.0.min.js");
		StylesheetManager.RegisterStylesheet("//code.jquery.com/ui/1.11.4/themes/ui-lightness/jquery-ui.css");
	}
}
		</control:CodeBlock>
	</div>
	
	<div class="ui-body ui-body-a ui-corner-all" data-role="collapsible">
		<h3>LINQ Extensions (String Extensions)</h3>
		<control:CodeBlock runat="server" Language="CSharp">
var md5 = "Hello, world!".ToMd5(); // returns 6cd3556deb0da54bca060b4c39479839
var base64 = "Hello, world!".ToBase64(); // returns SGVsbG8sIHdvcmxkIQ==
var html = "&lt;p&gt;Hello, world!&lt;/p&gt;".HtmlEncode(); // returns &amp;lt;p&amp;gt;Hello, world!&amp;lt;/p&amp;gt;
var url = "Hello, world!".UrlEncode(); // returns Hello%2C%20world%21
var @long = "0".ConvertTo&lt;long&gt;(); // returns 0L
var @long = "test".ConvertTo&lt;long&gt;(-1); // returns -1, test cannot be converted to long, so default value set to -1
var @bool = "1".ConvertTo&lt;bool&gt;(); // returns true
var @bool = "y".ConvertTo&lt;bool&gt;(); // returns true
var @bool = "Yes".ConvertTo&lt;bool&gt;(); // returns true
var @bool = "t".ConvertTo&lt;bool&gt;(); // returns true
var @bool = "True".ConvertTo&lt;bool&gt;(); // returns true
var @bool = "tRuE".ConvertTo&lt;bool&gt;(); // returns true
var @bool = "0".ConvertTo&lt;bool&gt;(); // returns false
		</control:CodeBlock>
	</div>
	
	<div class="ui-body ui-body-a ui-corner-all" data-role="collapsible">
		<h3>LINQ Extensions (Object Extensions)</h3>
		<control:CodeBlock runat="server" Language="CSharp">
public class StringToHtmlTextWriterTagConverter : ValueConverterBase&lt;string, HtmlTextWriterTag&gt;
{
	public override HtmlTextWriterTag Convert(string value, object parameter, CultureInfo culture)
	{
		HtmlTextWriterTag tag;
		Enum.TryParse(value, out tag);
		return tag;
	}

	public override string ConvertBack(HtmlTextWriterTag value, object parameter, CultureInfo culture)
	{
		return value.ToString();
	}
}

var tag = "H1".ConvertTo&lt;HtmlTextWriterTag&gt;(new StringToHtmlTextWriterTagConverter()); // returns HtmlTextWriterTag.H1
		</control:CodeBlock>
	</div>
	
	<div class="ui-body ui-body-a ui-corner-all" data-role="collapsible">
		<h3>LINQ Extensions (Control Extensions)</h3>
		<control:CodeBlock runat="server" Language="CSharp">
var allEzUserControls = page.FindControlsOfType&lt;EzUserControl&gt;(); // returns IEnumerable&lt;EzUserControl&gt;
var isInUpdatePanel = control.IsInUpdatePanel(); // returns true if control is inside of an UpdatePanel
		</control:CodeBlock>
	</div>
	
	<div class="ui-body ui-body-a ui-corner-all" data-role="collapsible">
		<h3>LINQ Extensions (NameValueCollection Extensions)</h3>
		<control:CodeBlock runat="server" Language="CSharp">
// http://www.mywebsite.com/Default.aspx?id=123
var queryStringId = Request.QueryString.GetItem&lt;long&gt;("id"); // returns 123L;

// http://www.mywebsite.com/Default.aspx?id=test
var queryStringId = Request.QueryString.GetItem&lt;long&gt;("id", 456L); // returns 456L because test cannot be converted to a long

// &lt;add key="IsEnabled" value="1" /&gt;
var appSettingsBool = ConfigurationManager.GetItem&lt;bool&gt;("IsEnabled"); // returns true

// &lt;add key="ContentIds" value="1,2,3   , 4   ,5" /&gt;
var appSettingsIds = ConfigurationManager.GetItems&lt;long&gt;("ContentIds"); // returns IEnumerable&lt;long&gt; {1L,2L,3L,4L,5L}
		</control:CodeBlock>
	</div>
	
	<div class="ui-body ui-body-a ui-corner-all" data-role="collapsible">
		<h3>Example User Control (CKEditor)</h3>
		<control:CodeBlock runat="server" Language="AspDotNet">
&lt;%@ Register TagPrefix="uc" TagName="CkEditor" Src="~/UserControls/CkEditor.ascx" %&gt;
&lt;uc:CkEditor runat="server" ID="Editor" Tag="H1"/&gt;
		</control:CodeBlock>

		<uc:CkEditor runat="server" ID="Editor" Tag="H1"/>
	</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Footer">
	<h1>Thank you for using EzWebForms!</h1>
</asp:Content>
