<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportExel.aspx.cs" Inherits="VS.ECommerce_MVC.Views.PhatTrienDangKy.ImportExel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

   Mẫu code:     F:\Congty_APhuc\Nam2021\Thang08\VS.CongDong_MVC_DaiHocDongDO\VS.E_Commerce\cms\Admin\Member
          <div>
                                    <asp:Label ID="ltmsgs" runat="server" Font-Bold="True" ForeColor="Red" Visible="True"></asp:Label>
                                </div>

<div style=" border:1px solid #ed1c24; padding:10px; border-radius:3px;">
<div style=" color:red"><b style=" font-size:13px">Lưu ý</b>:
<br />
    - Exel phải được lưu ở định dạng (excel 97-2003 workbook (*.xls))
<br />
- Hệ thống sẽ lấy số điện thoại để cập nhật nếu có sự thay đổi nội dung trong File Exel
<br />- Phải chọn nhóm ngành nghề trước khi Import Exel
 
</div>
<asp:FileUpload ID="FileUpload1" runat="server" />
<asp:LinkButton ID="Import" CssClass="vadd toolbar btn btn-info" OnClick="Import_Click" runat="server"> Import Exel</asp:LinkButton>   
    
     <span><a style=" color:#008100; font-weight:bold" href="/Views/PhatTrienDangKy/HocVien.xls">Tải File Mẫu Exel về máy</a></span>
</div>
                                
<div style=" clear:both; height:10px"></div>
    </div>
    </form>
</body>
</html>
