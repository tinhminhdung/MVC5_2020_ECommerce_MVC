using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sqllinq
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        private string lang = "VIE";
        protected void Page_Load(object sender, EventArgs e)
        {
              var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId into ppic
                        from pic in ppic.DefaultIfEmpty()
                        join c in _context.Categories on pic.CategoryId equals c.Id into picc
                        from c in picc.DefaultIfEmpty()
                        join pi in _context.ProductImages on p.Id equals pi.ProductId into ppi
                        from pi in ppi.DefaultIfEmpty()
                        where pt.LanguageId == request.LanguageId && pi.IsDefault == true
                        select new { p, pt, pic, pi };


				// Xóa cột
				ALTER TABLE products DROP COLUMN IDMonHoc



				alter table GiaiBaiTap add IDMonHoc int
				update GiaiBaiTap set IDMonHoc=0
				
				

			update News set Chekdata=@Chekdata,Create_Date=@Create_Date,Modified_Date=@Modified_Date where inid= @inid
			
			select * from News  where icid in (" + icid + ")  and lang='" + lang + "'
			
			SELECT * FROM News WHERE Title LIKE N'" + Exec(keyWord) + "'
			
			DELETE FROM News WHERE icid in (" + icid + ")
			
			select top 5 * from tbProduct order by proId desc
			
			select  top 5 * from tbProduct order by proId desc
		


			alter table Products add Price1 nvarchar(50)
			alter table Products add Price2 nvarchar(50)
			alter table Products add Price3 nvarchar(50)
			alter table Products add Price4 nvarchar(50)


		
		 // if (Page.IsValid)
       // {
            //Insert
            //YahooMessenger obj = new YahooMessenger();
            //obj.Title = "1111111";
            //obj.Nick = "ddd";
            //obj.Phone = "7777";
            //obj.Status = 1;
            //obj.Email = "";
            //db.YahooMessengers.InsertOnSubmit(obj);
            //db.SubmitChanges();
            ////Update

            //YahooMessenger abc = db.YahooMessengers.SingleOrDefault(p => p.inick == 30);
            //abc.inick = 30;
            //abc.Title = "---------";
            //abc.Nick = "ddd";
            //abc.Phone = "7777";
            //abc.Status = 1;
            //abc.Email = "";
            //db.SubmitChanges();
			//}
			
			
			
			GiaCuocThanhToan abc = db.GiaCuocThanhToans.SingleOrDefault(p => p.IDQuanHuyen == int.Parse(ID));
            if (abc != null)
            {
                abc.IDQuanHuyen = int.Parse(ID);
                abc.GiaCuocVanChuyenCODNhanh = GiaCuocVanChuyenCODNhanh.Text;
                db.SubmitChanges();
                lbl_msg.Text = "Cập nhật thành công.";
            }
            else
            {
                GiaCuocThanhToan obj = new GiaCuocThanhToan();
                obj.IDQuanHuyen = int.Parse(ID);
                obj.GiaCuocVanChuyenCODNhanh = GiaCuocVanChuyenCODNhanh.Text;
                db.GiaCuocThanhToans.InsertOnSubmit(obj);
                db.SubmitChanges();
                lbl_msg.Text = "Thêm mới thành công.";
            }

        }
        public void abc()
        {

        }
        public static List<New> LoadAllANew(DataClasses1DataContext db, string lang)
        {
		// var res = (from s in db.Histories where s.StaffID == int.Parse(hd_StaffID.Text) select new { s.FromDate }).ToList();
		// List<Staff> list = db.Staffs.Where(o => o.StaffCode.StartsWith(txtManhanvien) && o.StaffCode.EndsWith(txtManhanvien)).ToList<Staff>();
		// user its = db.users.SingleOrDefault(p => p.vuserun == Session["Members"].ToString() && p.istatus.ToString() == "1");// lay ra 1 bai viet

//History list = db.Histories.Where(s => s.StaffID == ID).OrderByDescending(s => s.HistoryID).FirstOrDefault(); lấy ra ID cuối cùng
            List<New> list = (from p in db.News where p.lang == lang orderby p.Create_Date descending select p).ToList();
			
			
			      List<Luutamgiohang> del = db.Luutamgiohangs.Where(s => s.cus_id == int.Parse("0")).ToList();
				  
				  
				  
            return list;
            // KQ Goi ham List<New> list = LoadAllANew(db, lang);
        }
		public string Diploma(string id)// lay ra 1 bai viet
        {
            string Chuoi = "";
            if (id != null)
            {
                Diploma list = db.Diplomas.SingleOrDefault(p => p.DiplomaID == int.Parse(id));
                if (list!=null)
                {
                    Chuoi += list.DiplomaName.ToString();
                }
            }
            return Chuoi;
        }
        protected string MenuPro()
        {
            string str = "";
            List<New> list = GetNewWithOption(db).ToList();
            // List<New> list = LoadAllANew(db, lang);
            // List<New> list = (from p in db.News where p.lang == lang orderby p.Create_Date descending select p).ToList();
            if (list.Count > 0)
            {
                foreach (New item in list)
                {
                    str += item.Title.ToString();
                }
            }
            return str.ToString();
        }

        protected string Menu_Pro()
        {
            string str = "";
            var cc = from p in db.News select p;
            foreach (New item in cc)
            {
                str += item.Title.ToString();
            }
            return str.ToString();
        }

        protected string KieuIntrongsql()
        {
            string str = "";
            //where multiple id linq
            // cách so 1:
            var cc = from News in db.News where (new int[] { 336 }).Contains(News.icid.GetValueOrDefault()) select News;
            // cách so 2:
            //IEnumerable<int> list = new List<int>() { 336 };
            //List<New> cc = (from e in db.News where list.Contains(e.icid.GetValueOrDefault()) select e).ToList();
            foreach (var item in cc)
            {
                str += item.Title.ToString() + "</br>--";
            }
            return str.ToString();
        }

        protected string Danhsachtin()
        {
            string str = "";
            var cc = from News in db.News
                     where (new int[] { 336 }).Contains(News.icid.GetValueOrDefault()) && News.lang == "VIE" && News.Status == 1 && (News.Create_Date <= DateTime.Now && DateTime.Now <= News.Modified_Date)
                     orderby News.Create_Date descending
                     select News;
            foreach (var item in cc)
            {
                str += item.Title.ToString() + "</br>-==-";
            }
            return str.ToString();
        }

        public void DeleteNews()
        {
		 NganHangMaThe del = db.NganHangMaThes.Where(s => s.ID == int.Parse(Id)).FirstOrDefault();// xóa 1
                        db.NganHangMaThes.DeleteOnSubmit(del);
                        db.SubmitChanges();
						
						
            List<Luutamgiohang> del = db.Luutamgiohangs.Where(s => s.cus_id == int.Parse("0")).ToList();// xóa nhiều
            db.Luutamgiohangs.DeleteAllOnSubmit(del);
            db.SubmitChanges();
			
			
						
            //var queryNews = from News in db.News where (new int[] { 1 }).Contains(News.inid)  select News;
            //foreach (var del in queryNews)
            //{
            //    db.News.DeleteOnSubmit(del);
            //}
            //db.SubmitChanges();

            //updateNew
            var queryNews = from News in db.News where News.inid == 0 select News;
            foreach (var News in queryNews)
            {
                News.Status = (System.Byte?)1;
            }
            db.SubmitChanges();

            // NEWS_OTHERFIRST
            (from News in db.News where News.Create_Date > ((from News0 in db.News where News0.inid == 00 select new { News0.Create_Date }).First().Create_Date) && News.lang == "VIE" && News.icid == 00 && (News.Create_Date <= DateTime.Now && DateTime.Now <= News.Modified_Date) orderby News.Create_Date descending select News).Take(11);

        }

        public static IEnumerable<New> GetNewWithOption(DataClasses1DataContext db)
        {
            string comand = "Select * from News";
			
            return db.ExecuteQuery<New>(@"" + comand + ""); 
			hoặc 
			return db.ExecuteQuery<New>(@"" + comand + "").ToList();
			
            // KQ Goi ham List<New> list = GetNewWithOption(db).ToList();
        }
		
		// List<Ship> list = (from p in db.Ships where p.Quocgia == ship orderby p.ID descending select p).ToList();
       // IEnumerable<Ship> list = (from p in db.Ships where p.Quocgia == ship orderby p.ID descending select p).ToList();
       // IEnumerable<Ship> list = db.ExecuteQuery<Ship>(@"select * from Ship");
	   
	    List<CommonDic> list = db.ExecuteQuery<CommonDic>(@"select Description0, Description1 from CommonDic where CommonDicType='PARAMATER' and CommonDicCode= 'LBC_TRANINGPOLICY'").ToList();
            if (list.Count() > 0)
            {
                txtOther.Text = list[0].Description0;
                txtOther1.Text = list[0].Description1;
            }
}

  protected IEnumerable<tbNew> NewCate(string icid)
        {
            string comand = "select top 2 * from  tbNews where newActive=0 and grnID in (" + icid + ")  and newLang ='" + lang + "' order by newDate desc";
            return db.ExecuteQuery<tbNew>(@"" + comand + "");
        }
        protected IEnumerable<tbNew> NewIn(string icid)
        {
            string comand = "SELECT * FROM (select top 5 row_number() OVER (order by newDate desc) AS rowindex ,* from tbNews  WHERE grnID in (" + icid + ") and newLang='" + lang + "')    As A where A.rowindex > 2";
            return db.ExecuteQuery<tbNew>(@"" + comand + "");
        }
\\\\

     protected IEnumerable<tbNew> NewCate(string icid)
        {
            string comand = "select top 2 * from  tbNews where newActive=0 and grnID in (" + icid + ")  and newLang =" + lang + " order by newDate desc";
            return db.ExecuteQuery<tbNew>(@"" + comand + "");
        }

(from tbNews in db.tbNews
where
  tbNews.newActive == 0 &&
  (new int[] {123 }).Contains(tbNews.grnID) &&
  tbNews.newLang == "vi"
orderby
  tbNews.newDate descending
select new {
  tbNews.newid,
  tbNews.newName,
  tbNews.newImage,
  tbNews.newimg,
  tbNews.skId,
  tbNews.newFile,
  tbNews.newContent,
  tbNews.newDetail,
  tbNews.newDate,
  tbNews.newCreateDate,
  tbNews.newEndDate,
  tbNews.newTitle,
  tbNews.newKeyword,
  tbNews.newDescription,
  tbNews.newPriority,
  tbNews.newIndex,
  tbNews.newVisit,
  tbNews.grnID,
  tbNews.usrID,
  tbNews.newAuthor,
  tbNews.newType,
  tbNews.newActive,
  tbNews.newLang,
  tbNews.ishowdesc,
  tbNews.UrlOriginal,
  tbNews.vkey,
  tbNews.vUrl,
  tbNews.newTag,
  tbNews.newAlt,
  tbNews.newTagName
}).Take(2)

    //
public string Famyly(object id)// <%#Famyly(Eval("id"))%>
{
	string Chuoi = "";
	if (id != null)
	{
		var list = db.Famyly_Relation_GetInfo(int.Parse(id.ToString())).ToList();
		if (list.Count() > 0)
		{
			Chuoi += list[1].Description0.ToString();
		}
	}
	return Chuoi;
}

// dau vao vidu: 009-999-9987-099// se dung doan code nay de cat chuoi
string Search = txtTenNV.Text;
    string[] idSearch = Search.Split('-');
    sqlsearch += "  AND Staff.FullName LIKE N'%" + Search[1].ToString() + "%'";//lay thu tu so 2

if (txtSearch.Text != "")
{
    string Search = txtSearch.Text;
    string[] idSearch = Search.Split('-');
    list = list.Where(s => s.FullName.ToLower().Contains(idSearch[0].ToString().ToLower()));//lay thu tu so 1
	
			
//	bắt lỗi DropDownList	
<asp:DropDownList ID="ddkynang" class="form-control input-sm"  style="border-radius: 0px !important" runat="server"></asp:DropDownList>
<asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="*" Text="*" InitialValue="0" ControlToValidate="ddkynang" ValidationGroup="GInfo"></asp:RequiredFieldValidator> 

									 
						
//cách lấy dữ liệu từ 1 bảng khác			 
if (list[0].ManagerID != 0)
{
Staff d = db.Staffs.FirstOrDefault(l => l.ManagerID == list[0].ManagerID);
if (d != null) txtngxacnhan.Text = d.FullName;
}

//                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="GInfo" ControlToValidate="txttungay" ErrorMessage="*" ></asp:RequiredFieldValidator>

//daus . vaf ,

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit"  Assembly="AjaxControlToolkit" %>
 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtInsuranceSalary"    FilterType="Custom, Numbers" ValidChars="+-=/*()." />
     
Response.Write("<script type=\"text/javascript\">alert('Đăng ký nghỉ thành công'); window.location.href='" + href + "'</script>");

 <iframe width=960px height=250px style=" overflow:hidden" scrolling=no  frameborder=no src="<%=WebURL %>Banner/Home/index.html" marginheight=0 marginwidth=0></iframe>

 <iframe width=960px height=250px style=" overflow:hidden" scrolling=no  frameborder=no src="<%=WebURL %>Banner/Home/index.html?ip=<%#Eval("id")%>" marginheight=0 marginwidth=0></iframe>

 <div class=\"cat-calendar\"><span class=\"cat-thang\">" + MyWeb.Global.GetLangKey("Thang") +" "+ DateTime.Parse(list[i].newDate.ToString()).Month + "</span><span class=\"cat-ngay\">" + DateTime.Parse(list[i].newDate.ToString()).Day + "</span><span class=\"cat-nam\">" + DateTime.Parse(list[i].newDate.ToString()).Year + "</span></div>
 
  <iframe width=100% height=100% style=" overflow:hidden" scrolling=no  frameborder=no src="http://hatinhplus.vn/" marginheight=0 marginwidth=0></iframe>

 
 // cap nhật ảnh bị chết trong menu
 
 protected void lbtDelimg_Click(object sender, EventArgs e)
    {
        tbPage abc = db.tbPages.SingleOrDefault(p => p.pagId == int.Parse(HidId.Value));
        abc.pagImage = "";
        db.SubmitChanges();
        lbtDelimg.Visible = false;
        lblImg.ImageUrl = "";
        ltrImg.Text = "";
    }
	
	
	
	

<div id="Hotliness"><%=Dienthoai() %></div>
<script runat="server">
 private static string Dienthoai()
    {
        System.Collections.Generic.List<tbConfigDATA> list = new System.Collections.Generic.List<tbConfigDATA>();
        list = tbConfigDB.tbConfig_GetLang(MyWeb.Global.GetLang());
        return "<a id=\"callnowbutton\" href=\"tel:" + list[0].conTel.Replace(".", "").Replace(",", "") + "\">" + list[0].conTel + "</a>";
    }
</script>
<div id="Hotliness"><%=Dienthoai() %> </div>
<style>
#Hotliness a {
    background: url("data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHhtbG5zOnhsaW5rPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5L3hsaW5rIiB2aWV3Qm94PSIwIDAgNjAgNjAiPjxwYXRoIGQ9Ik03LjEwNCAxNC4wMzJsMTUuNTg2IDEuOTg0YzAgMC0wLjAxOSAwLjUgMCAwLjk1M2MwLjAyOSAwLjc1Ni0wLjI2IDEuNTM0LTAuODA5IDIuMSBsLTQuNzQgNC43NDJjMi4zNjEgMy4zIDE2LjUgMTcuNCAxOS44IDE5LjhsMTYuODEzIDEuMTQxYzAgMCAwIDAuNCAwIDEuMSBjLTAuMDAyIDAuNDc5LTAuMTc2IDAuOTUzLTAuNTQ5IDEuMzI3bC02LjUwNCA2LjUwNWMwIDAtMTEuMjYxIDAuOTg4LTI1LjkyNS0xMy42NzRDNi4xMTcgMjUuMyA3LjEgMTQgNy4xIDE0IiBmaWxsPSIjMDA2NzAwIi8+PHBhdGggZD0iTTcuMTA0IDEzLjAzMmw2LjUwNC02LjUwNWMwLjg5Ni0wLjg5NSAyLjMzNC0wLjY3OCAzLjEgMC4zNWw1LjU2MyA3LjggYzAuNzM4IDEgMC41IDIuNTMxLTAuMzYgMy40MjZsLTQuNzQgNC43NDJjMi4zNjEgMy4zIDUuMyA2LjkgOS4xIDEwLjY5OWMzLjg0MiAzLjggNy40IDYuNyAxMC43IDkuMSBsNC43NC00Ljc0MmMwLjg5Ny0wLjg5NSAyLjQ3MS0xLjAyNiAzLjQ5OC0wLjI4OWw3LjY0NiA1LjQ1NWMxLjAyNSAwLjcgMS4zIDIuMiAwLjQgMy4xMDVsLTYuNTA0IDYuNSBjMCAwLTExLjI2MiAwLjk4OC0yNS45MjUtMTMuNjc0QzYuMTE3IDI0LjMgNy4xIDEzIDcuMSAxMyIgZmlsbD0iI2ZmZiIvPjwvc3ZnPg==") no-repeat scroll center 2px / 58px 58px #009900;
    border-bottom-right-radius: 40px;
    border-top: 2px solid #2dc62d;
    border-top-right-radius: 40px;
    bottom: -20px;
    box-shadow: 0 0 5px #888;
    color: #009900;
    font-size: 0;
    height: 80px;
    left: 0;
    position: fixed;
    text-decoration: inherit;
    width: 100px;
    z-index: 9999;
}
</style>



// quảng cáo trên Mobile giật

<%  System.Web.HttpBrowserCapabilities myBrowserCaps = Request.Browser;
if (((System.Web.Configuration.HttpCapabilitiesBase)myBrowserCaps).IsMobileDevice)
{ }
else
{%>
<asp:Literal ID="FooterBody" runat="server"></asp:Literal>
<asp:Literal ID="ltrAdv" runat="server"></asp:Literal>
<asp:Literal ID="ltrPopup" runat="server"></asp:Literal>
<asp:Literal ID="ltrBottomLayer" runat="server"></asp:Literal>
<asp:Literal ID="ltrlivechat" runat="server"></asp:Literal>
<%} %>



Google_Analytics_Id.Text += " <script>     $(document).ready(function () { (function (i, s, o, g, r, a, m) { i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () { (i[r].q = i[r].q || []).push(arguments) }, i[r].l = 1 * new Date(); a = s.createElement(o), m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m) })(window, document, 'script', 'js/ga.js', 'ga'); ga('create', '" + GlobalClass.Google_Analytics_Id + "', 'auto'); ga('send', 'pageview'); });</script>";



// sql update tu /p/ --> thanh /san-pham/
select * from tbPage where pagLink like N'/san-pham/%'
select SUBSTRING( pagLink,3,LEN(pagLink) )from tbPage where pagLink like N'/p/%'
update tbPage set pagLink='/san-pham' +SUBSTRING( pagLink,3,LEN(pagLink) )  where pagLink like N'/p/%' 

// css giamgia

.save_price {background: #ea2e49 none repeat scroll 0 0;color: #fff !important;display: block;font-weight: bold;padding: 2px 10px;position: absolute;right: -1px;text-align: center;text-decoration: none !important;top: 0;transition: opacity 0.2s ease 0s;width: 44px;z-index: 999;}#col-mid .box-body .div_img {position: relative;}



Response.Write("<script type=\"text/javascript\">alert('Đăng ký nghỉ thành công'); window.location.href='" + href + "'</script>");






//http://thenhuavieta.com/ 0 đ thành liên hệ
<script>
 ChangeMoney();
 function ChangeMoney(){
  for(i=0;i < $(".prohome > .gia").length;i++){
   if($(".prohome > .gia")[i].innerHTML.indexOf(" 0 ")>0){
    $(".prohome > .gia")[i].innerHTML="Giá bán: liên hệ";
   }
  }
 }
</script>
<script>
 ChangeMoneydt();
 function ChangeMoneydt(){
 if($(".prodetailright > .proPrice")[0].innerHTML.indexOf(">0 đ")>0) $(".prodetailright > .proPrice")[0].innerHTML=$(".prodetailright > .proPrice")[0].innerHTML.replace(">0 đ","> Liên hệ")
 }
</script>



Hien thi BDS
 dt.Columns.Add("vDiaChi", typeof(string));
 RowSua["vDiaChi"] = Row["vDiaChi"];
   <asp:Repeater ID="rpt" runat="server">
        <HeaderTemplate>
            <table class="list_raovat">
        </HeaderTemplate>
        <ItemTemplate>
            <tr id="<%#DataBinder.Eval(Container.DataItem, "ID")%>" class="display-body">
                <td class="col4">
                    <div class="display-image" >
                       <a class="display-title thumbnail bold" href="<%#DataBinder.Eval(Container.DataItem,"URL")%>"> <img src="<%#DataBinder.Eval(Container.DataItem,"Anh")%>" /></a>
                    </div>
                </td>
                <td class="col1">
                    <h2><a class="display-title thumbnail bold" href="<%#DataBinder.Eval(Container.DataItem,"URL")%>"><%#DataBinder.Eval(Container.DataItem,"TieuDe")%>
                    <%#DataBinder.Eval(Container.DataItem,"imgHot")%><br>
                    </a></h2>
                    <table class="display-detail">
                        <tr>
                           <td class="display-col1">Giá</td> 
                            <td class="display-col2">:</td> 
                            <td class="display-col3"><%#DataBinder.Eval(Container.DataItem,"Gia")%></td> 
                        </tr>
                        <tr>
                           <td class="display-col1">Diện tích</td> 
                            <td class="display-col2">:</td> 
                            <td class="display-col3"><%#DataBinder.Eval(Container.DataItem,"Dientich")%></td> 
                        </tr>
                        <tr>
                           <td class="display-col1">Địa chỉ</td> 
                            <td class="display-col2">:</td> 
                            <td class="display-col3 vDiaChi"><%#DataBinder.Eval(Container.DataItem,"vDiaChi")%></td> 
                        </tr>
                        <tr>
                            <td class="display-col1">Ngày đăng</td> 
                            <td class="display-col2">:</td> 
                            <td class="display-col3" style="text-align:right;"><%#DataBinder.Eval(Container.DataItem,"NgayDang")%></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>   

	
	
.display-ngaydang{
    float:right;
}
.display-detail{
    float:left;
    width:100%;
}
.display-col1{
    width:25%;
}
.display-col2{
    width:2%;
}
.display-col3{
    width:80%;
}
.display-image{
    width: 100%;
    height: 150px;
    text-align: center;
}
.display-image img{
    max-height: 150px;
    background-size: cover;
    margin: 0 auto;
}
.display-title{
    padding-left:1px;
    font-weight:600;
}

.vDiaChi{color:#276da7}
.col1 h2 a.display-title.thumbnail.bold{color:#276da7}
.display-body {border-bottom: 1px solid #276da7;float: left;width: 100%;}




Mobile

http://sanghuyevent.vn/  http://p24001.vmms.vn/
http://leseldelaterrecompany.com/ http://p16001.vmms.vn/
http://xehoanamdinh.com/   P07002.vmms.vn
http://mynghesungthuyung.com/    p05003
http://khacdau68.com/ http://p03009.vmms.vn/
http://breakout.com.vn/    http://p37001.vmms.vn/ 
http://mancuangoclinh.com/  p27001.vmms.vn
http://xetoyotabacninh.vmms.vn/
http://p34001.vmms.vn/   n7gaming.vn,http://vnphucan.com/,http://iphoneninhbinh.vn/
http://p38001.vmms.vn/     http://thegioikedehang.com/




chữ chạy:
	<marquee onmouseover="this.stop();" onmouseout="this.start();" behavior="scroll" direction="left" scrollamount="3"><%=GlobalClass.conContact%></marquee>
	
	
placeholder="Search"  thay bằng ---> txtSearch.Attributes.Add("placeholder", MyWeb.Global.GetLangKey("search_text"));

txtSearch.Attributes.Add("placeholder", MyWeb.Global.GetLangKey("search_text"));


//css chống coppy  http://www.ducanhplus.com/2013/07/code-chong-copy-va-click-chuot-phai-song-kiem-hop-bich-css-va-javascript/

body{ -webkit-touch-callout: none; -webkit-user-select: none; -moz-user-select: none; -ms-user-select: none; -o-user-select: none; user-select: none; }


Response.Write("<script type=\"text/javascript\">alert('Đăng ký nghỉ thành công'); window.location.href='" + href + "'</script>");


2021/10/21 18:00

// chuỗi @
string htmlbodystr = @"<body style='margin: 0px;'>
<div style='width: 500px; height:100px; padding:10px; border-radius: 5px; border:solid 1px #C0C0C0;'>
   <span>Name: </span>
   <span>" + nametex.Text + @"</span><br />
   <span>Email:</span>
   <span>" + emailtex.Text + @"</span><br />
   <span>Mobile:</span>
   <span>" + mobiletex.Text + @"</span><br /><br />
   <span>Message:</span><br />
   <span>" + messagetex.Text + @"</span>
</div>";


bật trình duyệt lên và dán vào CONSOLE , vậy là có thể sửa được các nội dung trực tiếp trên web rồi.
document.designMode = "on";




overflow: hidden;
display: -webkit-box;
-webkit-line-clamp: 4;
-webkit-box-orient: vertical;






public ActionResult Category(string hp)
{
	

	int Tongsobanghi = 0;
	Int16 pages = 1;
	int Tongsotrang = int.Parse("20");
	if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
	{
		pages = Convert.ToInt16(Request.QueryString["page"].Trim());
	}
	string chuoi = More.SubCheckList(More.PR, More.TangNameicid(hp));

	List<Entity.Category_Product> iitem = SProducts.Name_Text_Rg(@"SELECT " + Commond.Sql_Product() + " FROM products where lang='" + language + "' and Status=1 " + chuoi + " order by Create_Date desc");
	if (iitem.Count() > 0)
	{
		Tongsobanghi = iitem.Count();
	}
	int PageIndex = (pages - 1);
	int Tongpage = Tongsotrang;
	int StartRecord = PageIndex * Tongpage;
	int EndRecord = StartRecord + Tongpage + 1;
	List<Entity.Category_Product> dt = SProducts.Name_Text_Rg(@"SELECT " + Commond.Sql_Product() + " FROM (SELECT ROW_NUMBER() OVER (ORDER BY Create_Date DESC) AS rowindex ,*  FROM  products where lang='" + language + "' and Status=1  " + chuoi + " ) AS A WHERE  ( A.rowindex >  " + StartRecord.ToString() + " AND A.rowindex < " + EndRecord + ")");
	if (dt.Count >= 1)
	{
		ViewBag.ShowList = Commond.LoadProductList(dt);
	}

	if (Tongsobanghi % Tongsotrang > 0)
	{
		Tongsobanghi = (Tongsobanghi / Tongsotrang) + 1;
	}
	else
	{
		Tongsobanghi = Tongsobanghi / Tongsotrang;
	}
	ViewBag.Phantrang = Commond.Phantrang("/danh-muc/" + hp + ".html", Tongsobanghi, pages);
	return View();
}






  public ActionResult Index()
        {
            ViewBag.ShowDropdowlist = LoadDropdowlist();// show lên html Dropdowlist

            ViewBag.LoadTrinhDo = LoadTrinhDo();// show lên html Dropdowlist
            string MonHoc = "";
            string Category = "0";
            string TuNgay = "";
            string DenNgay = "";
            string loc = "";
            string TrinhDo = "0";
            if ((Request.QueryString["Category"] != null) && (Request.QueryString["Category"] != ""))
            {
                Category = Request.QueryString["Category"].Trim();
                loc += "&Category=" + Category + "";
            }
            if ((Request.QueryString["MonHoc"] != null) && (Request.QueryString["MonHoc"] != ""))
            {
                MonHoc = Request.QueryString["MonHoc"].Trim();
                loc += "&MonHoc=" + MonHoc + "";
            }
            if ((Request.QueryString["TrinhDo"] != null) && (Request.QueryString["TrinhDo"] != ""))
            {
                TrinhDo = Request.QueryString["TrinhDo"].Trim();
                loc += "&TrinhDo=" + TrinhDo + "";
            }
            if ((Request.QueryString["TuNgay"] != null) && (Request.QueryString["TuNgay"] != ""))
            {
                TuNgay = Request.QueryString["TuNgay"].Trim();
                loc += "&TuNgay=" + TuNgay + "";
            }
            if ((Request.QueryString["DenNgay"] != null) && (Request.QueryString["DenNgay"] != ""))
            {
                DenNgay = Request.QueryString["DenNgay"].Trim();
                loc += "&DenNgay=" + DenNgay + "";
            }




            if (MoreAll.MoreAll.GetCookies("TeacherID").ToString() == "")
            {
                return Redirect("/login-teacher.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
            }
            if (MoreAll.MoreAll.GetCookies("TeacherID") != "")
            {

                string sql = "";
                if (TuNgay != "" && DenNgay != "")
                {
                    sql += " AND Create_Date IS NOT NULL AND ((DATEADD(dd,-31,Create_Date)>='" + Commond.FormatDate(Commond.ConvertStringToDate(TuNgay, "dd/MM/yyyy").Date) + " 00:00:00.000' OR Create_Date>='" + Commond.FormatDate(Commond.ConvertStringToDate(TuNgay, "dd/MM/yyyy").Date) + " 00:00:00.000') AND Create_Date <='" + Commond.FormatDate(Commond.ConvertStringToDate(DenNgay, "dd/MM/yyyy").Date) + " 23:59:59.999')";
                }
                else if (TuNgay == "" && DenNgay != "")
                {
                    sql += " AND Create_Date IS NOT NULL AND Create_Date <='" + Commond.FormatDate(Commond.ConvertStringToDate(DenNgay, "dd/MM/yyyy").Date) + " 23:59:59.999'";
                }
                else if (TuNgay != "" && DenNgay == "")
                {
                    sql += " AND Create_Date IS NOT NULL AND (DATEADD(dd,-31,Create_Date)>='" + Commond.FormatDate(Commond.ConvertStringToDate(TuNgay, "dd/MM/yyyy").Date) + " 00:00:00.000' OR Create_Date>='" + Commond.FormatDate(Commond.ConvertStringToDate(TuNgay, "dd/MM/yyyy").Date) + " 00:00:00.000')";
                }
                if (Category != "0")
                {
                    sql += " and icid in (" + More.Sub_Menu(More.PR, Category) + ")";
                }
                if (TrinhDo != "0")
                {
                    sql += " and IDTrinhDo in (" + More.Sub_Menu(More.TD, TrinhDo) + ")";
                }

                if (MonHoc != "")
                {
                    sql += " and IDMonHoc = " + MonHoc + "";
                }

                sql += " and IDGiangVien =" + MoreAll.MoreAll.GetCookies("TeacherID") + "  ";

                int Tongsobanghi = 0;
                Int16 pages = 1;
                int Tongsotrang = int.Parse("20");
                if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
                {
                    pages = Convert.ToInt16(Request.QueryString["page"].Trim());
                }
                //  Response.Write("SELECT * FROM TracNghiem where 1=1 " + sql + " order by Create_Date desc");
                List<LTracNghiem> iitem = db.ExecuteQuery<LTracNghiem>(@"SELECT * FROM TracNghiem where 1=1 " + sql + " order by Create_Date desc").ToList();
                if (iitem.Count() > 0)
                {
                    Tongsobanghi = iitem.Count();
                }
                int PageIndex = (pages - 1);
                int Tongpage = Tongsotrang;
                int StartRecord = PageIndex * Tongpage;
                int EndRecord = StartRecord + Tongpage + 1;
                List<LTracNghiem> dt = db.ExecuteQuery<LTracNghiem>(@"SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY Create_Date DESC) AS rowindex ,*  FROM  TracNghiem  where 1=1 " + sql + " ) AS A WHERE  ( A.rowindex >  " + StartRecord.ToString() + " AND A.rowindex < " + EndRecord + ")").ToList();
                if (dt.Count >= 1)
                {
                    ViewBag.Show = dt;
                }

                if (Tongsobanghi % Tongsotrang > 0)
                {
                    Tongsobanghi = (Tongsobanghi / Tongsotrang) + 1;
                }
                else
                {
                    Tongsobanghi = Tongsobanghi / Tongsotrang;
                }
                ViewBag.MonHoc = ShowMonHoc(Category);
                ViewBag.TuNgays = TuNgay;
                ViewBag.DenNgays = DenNgay;
                ViewBag.Phantrang = Commond.Phantrang_loc("/danh-sach-TracNghiem.html", "&Category=" + Category + "&MonHoc=" + MonHoc + "&TrinhDo=" + TrinhDo + "&TuNgay=" + TuNgay + "&DenNgay=" + DenNgay + "", Tongsobanghi, pages);
            }
            return View();
        }