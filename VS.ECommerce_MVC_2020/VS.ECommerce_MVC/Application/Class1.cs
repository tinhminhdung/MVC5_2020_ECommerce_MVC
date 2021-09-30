using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.WebPages.Html;

public static class UserControlHelper
{
    public static string RenderControlToHtml(Control ControlToRender)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        System.IO.StringWriter stWriter = new System.IO.StringWriter(sb);
        System.Web.UI.HtmlTextWriter htmlWriter = new System.Web.UI.HtmlTextWriter(stWriter);
        ControlToRender.RenderControl(htmlWriter);
        return sb.ToString();
    }
}