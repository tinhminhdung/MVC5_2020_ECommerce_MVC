<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Close.aspx.cs" Inherits="VS.ECommerce_MVC.Close" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script language="javascript" type="text/javascript">
            window.close();
            if (window.opener && !window.opener.closed) {
                window.opener.location.reload();
            }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
