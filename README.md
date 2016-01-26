# EzWebForms Framework

EzWebForms is an ASP.NET framework designed to ease the pains of WebForms development. Have you ever wrote an image carousel control, included the necessary JavaScript or JQuery code in the control, just to realize that more than one instance of the control causes a scripting error? Why? Because the control, as well as the JavaScript, were duplicated on the page, which confused the browser. Maybe you've had a reference to JQuery in your master page, just to realize you've also added a reference to JQuery again in your user control. You know the drill! Well, NO MORE! There's a control for that! As a bonus, there's also a control to deal with stylesheets the same way!

```asp.net
<control:EzJavaScriptLoader runat="server">
    <ScriptTemplate>
        <%-- Of the 3, only 1 will get loaded because the "src" --%>
        <%-- attribute is MD5 checksum'd. It's important to note --%>
        <%-- that if a loader in another control, template, or --%>
        <%-- master page, registers JQuery, it  will still only be --%>
        <%-- loaded once! Pretty darn cool, huh? --%>
        <script src="//code.jquery.com/jquery-1.12.0.min.js"></script>
        <script src="//code.jquery.com/jquery-1.12.0.min.js"></script>
        <script src="//code.jquery.com/jquery-1.12.0.min.js"></script>
        
        <script type="text/javascript">
            // this line will happen just once no matter how many controls
            // the code is MD5 checksum'd per <script> block
            alert("Hello, world!");
        </script>
        
        <script type="text/javascript">
            // This line will execute per control, because ClientID changes
            // per control
            alert("<%= ClientID %>");
        </script>
    </ScriptTemplate>
</control:EzJavaScriptLoader>
```

> **NOTE:** *When scripts are loaded, <script src="..."></script> tags are rendered after the opening body, and <script type="text/javascript">// code here</script> tags render before the closing body. Script registration also accounts for being loaded inside of an UpdatePanel versus not.*

The same goes for the **EzStylesheetLoader** control:
```asp.net
<control:EzStylesheetLoader runat="server">
	<StyleTemplate>
		<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/ui-lightness/jquery-ui.css"/>
		<style type="text/css">
			body { background-color: black; color: white; }
		</style>
	</StyleTemplate>
</control:EzStylesheetLoader>
```

> **NOTE:** *Styles get rendered in the head of the page.*

> **NOTE:** *Scripts and styles alike render in their respective places as noted above regardless where your **EzJavaScriptLoader** or **EzStylesheetLoader** controls are placed. This means scripts and/or styles that belong with a particular control can now be included WITH the control greatly increasing the maintainability of each.*

Inherit your pages from **EzPage**, user controls from **EzUserControl**, and server controls from **EzWebControl**, and each will have an instance of **JavaScriptManager** and **StylesheetManager** to allow script and stylesheet loading from within the code-behind.

> **NOTE:** *All controls are "Themeable" and have a special "Tag" attribute to control how that control renders on the page. "Tag" is "Div" by default and is strong-typed also. User controls inheriting **EzUserControl** render a tag also, unless "RenderContainer" is overriden to "false" in your control.*

As well as some nifty and useful controls, there's a plethora of useful LINQ extensions to help with things like creating MD5 hashes from a string, HTML encoding and decoding, Base64 encoding and decoding, strong-typing NameValueCollection values, and object conversion.

Take this line of code for example, which many of us have done:
```c#
public long ContentId
{
    get
    {
        long contentId;
        long.TryParse(Request.QueryString["id"], out contentId);
        return contentId;
    }
}
```

This begins to take up precious bytes of code and destroys any sense of DRY principles we strived to achieve to make our code clean for fellow developers. Try this on for size:
```c#
public long ContentId
{
    get { return Request.QueryString.GetItem<long>("id"); }
}
```

That's all well and good, it returns the default value for the type (in this case, zero), but what if I want to return a negative one instead? No problem:
```c#
public long ContentId
{
    get { return Request.QueryString.GetItem<long>("id", -1); }
}
```

That can be a huge improvement, not only for code size but for readability as well. Now, you're probably thinking, "Hey, that's pretty sweet! Except, what if I have a delimited string and want the values of each returned as an enumerable of longs?" Gotcha covered there as well:
```c#
public IEnumerable<long> ContentIds
{
    // 1,2,   3,       4,5,6,   7, 8,9, 0 (Yeah, it trims the values too!)
    get { return Request.QueryString.GetItems<long>("ids"); }
}
```

"Sure, but what if my list is delimited by pipes instead of commas?" Glad you asked!
```c#
public IEnumerable<long> ContentIds
{
    get { return Request.QueryString.GetItems<long>("ids", "|"); }
}
```

Another optional parameter to the **NameValueCollection.GetItems<>** function determines how to deal with empties. By default, this is set to **StringSplitOptions.RemoveEmptyEntries**. Just set it to **StringSplitOptions.None** to change the behavior.

And, there's so much more! Dig in to the test solution to see **EzWebForms** in action! All code has been thoroughly unit tested, and in some cases, integration tested (in the case of controls). Please feel free to use, modify, or enhance to your liking, and by all means, suggest, comment, and report bugs. Also, let me know how **EzWebForms** has benefited you or your company!