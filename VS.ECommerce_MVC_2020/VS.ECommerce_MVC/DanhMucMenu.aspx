<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DanhMucMenu.aspx.cs" Inherits="VS.ECommerce_MVC.DanhMucMenu" ValidateRequest="false" EnableEventValidation="false" ViewStateEncryptionMode="Never" EnableViewStateMac="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:CheckBoxList ID="cblcat" runat="server"> </asp:CheckBoxList>
        <br />

        <asp:Literal ID="Message" runat="server"></asp:Literal>
                <br />
        <asp:Button ID="Button1" runat="server" Text="Lưu" OnClick="Button1_Click" />
    </div>
    </form>
</body>
</html>
