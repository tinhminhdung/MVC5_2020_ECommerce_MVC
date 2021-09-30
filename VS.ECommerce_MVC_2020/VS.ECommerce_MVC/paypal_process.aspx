<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="paypal_process.aspx.cs" Inherits="VS.ECommerce_MVC.paypal_process" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="/Resources/js/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            setTimeout(function () {
                //alert('this form is about to be submitted...');
                $('#frmPaypal').submit();
            }, 500);
        });
    </script>

    <style type="text/css">
        .auto-style1
        {
            width: 100%;
        }

        .ajax-loading-box 
        {
            z-index: 100001;
            position: fixed;
            top: 50%;
            padding: 0px;
            left: 50%;
            width: 52px;
            min-height: 52px;
            margin-top: -26px;
            margin-left: -26px;
            background: url('../css/images/ajax-loader.gif') no-repeat 10px 10px #0d0d0d;
            opacity: .8;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            border-radius: 5px;
            overflow: visible;
            right: 50%;
        }

        .auto-style2
        {
            width: 486px;
        }
    </style>
</head>
<body>
   <form id="frmPaypal" method="post" action="https://www.paypal.com/cgi-bin/webscr" runat="server">
        <input type="hidden" name="cmd" value="_xclick" />
        <input type="hidden" name="business" value="<%= this.BusinessValue %>" />
        <input type="hidden" name="item_name" value="<%= this.ItemNameValue %>" />
        <input type="hidden" name="item_number" value="<%= this.ItemNumberValue %>" />
        <input type="hidden" name="amount" value="<%= this.AmountValue %>" />
        <input type="hidden" name="no_shipping" value="<%= this.NoShippingValue %>" />
        <input type="hidden" name="quantity" value="<%= this.QuantityValue %>" />
        <input type="hidden" name="os0" value="<%= this.OS2Value %>" />
        <input type="hidden" name="return" value="<%= this.UrlReturn %>" />
        <input type="hidden" name="cancel_return" value="<%= this.CancelUrlReturn %>" />
    </form>

    <table class="auto-style1">
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td>
                <div id="ajax_loading_box" class="ajax-loading-box"></div>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</body>
</html>