<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dropdowlisst_selectpicker_new.aspx.cs" ValidateRequest="false" EnableEventValidation="false" ViewStateEncryptionMode="Never" EnableViewStateMac="false" Inherits="VS.ECommerce_MVC.cms.PhatTrien.Dropdowlisst_selectpicker_new" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
            <link rel="stylesheet" type="text/css" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-alpha.6/css/bootstrap.min.css" />
            <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-alpha.6/js/bootstrap.min.js"></script>
            <link rel="stylesheet" type="text/css" href="https://www.jqueryscript.net/demo/Bootstrap-4-Dropdown-Select-Plugin-jQuery/dist/css/bootstrap-select.css" />
            <script type="text/javascript" src="https://www.jqueryscript.net/demo/Bootstrap-4-Dropdown-Select-Plugin-jQuery/dist/js/bootstrap-select.js"></script>
            
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Literal ID="Literal1" runat="server"></asp:Literal><br />
            <asp:DropDownList ID="drpCountry" runat="server" class="selectpicker"  
                AutoPostBack="true" data-live-search="true" OnSelectedIndexChanged="drpCountry_SelectedIndexChanged">
                <asp:ListItem Value="A">Accccc</asp:ListItem>
                <asp:ListItem Value="B">B5435</asp:ListItem>
                <asp:ListItem Value="C">C74757</asp:ListItem>
                <asp:ListItem Value="D">D8474</asp:ListItem>
            </asp:DropDownList>


        </div>
    </form>
</body>
</html>
