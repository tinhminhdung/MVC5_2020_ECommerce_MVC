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
				
select FORMAT(Create_Date ,'dd-MM-yyyy') as ToDate from News 
 --convert sang dinh dang dd-MM-yyyy
select convert(varchar,Create_Date, 103) as [Expected Receipt Date] from News
 --convert sang dinh dang (/) Ví dụ : 15/01/2020
select ISNULL(REPLACE(CONVERT(varchar(200), (CAST(PR_Qtty AS int)), 1), '', ''),0) as PR_Qtty,PR_Qtty from Demo
 --convert những trường nul thành số 0

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
    background: url("data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHhtbG5zOnhsaW5rPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5L3hsaW5rIiB2aWV3Qm94PSIwIDAgNjAgNjAiPjxwYXRoIGQ9Ik03LjEwNCAxNC4wMzJsMTUuNTg2IDEuOTg0YzAgMC0wLjAxOSAwLjUgMCAwLjk1M2MwLjAyOSAwLjc1Ni0wLjI2IDEuNTM0LTAuODA5IDIuMSBsLTQuNzQgNC43NDJjMi4zNjEgMy4zIDE2LjUgMTcuNCAxOS44IDE5LjhsMTYuODEzIDEuMTQxYzAgMCAwIDAuNCAwIDEuMSBjLTAuMDAyIDAuNDc5LTAuMTc2IDAuOTUzLTAuNTQ5IDEuMzI3bC02LjUwNCA2LjUwNWMwIDAtMTEuMjYxIDAuOTg4LTI1LjkyNS0xMy42NzRDNi4xMTcgMjUuMyA3LjEgMTQgNy4xIDE0IiBmaWxsPSIjMDA2NzAwIi8+PHBhdGggZD0iTTcuMTA0IDEzLjAzMmw2LjUwNC02LjUwNWMwLjg5Ni0wLjg5NSAyLjMzNC0wLjY3OCAzLjEgMC4zNWw1LjU2MyA3LjggYzAuNzM4IDEgMC41IDIuNTMxLTAuMzYgMy40MjZsLTQuNzQgNC43NDJjMi4zNjEgMy4zIDUuMyA2LjkgOS4xIDEwLjY5OWMzLjg0MiAzLjggNy40IDYuNyAxMC43IDkuMSBsNC43NC00Ljc0MmMwLjg5Ny0wLjg5NSAyLjQ3MS0xLjAyNiAzLjQ5OC0wLjI4OWw3LjY0NiA1LjQ1NWMxLjAyNSAwLjcgMS4zIDIuMiAwLjQgMy4xMDVsLTYuNTA0IDYuNSBjMCAwLTExLjI2MiAwLjk4OC0yNS45MjUtMTMuNjc0QzYuMTE3IDI0LjMgNy4xIDEzIDcuMSAxMyIgZmlsbD0iI2Z
