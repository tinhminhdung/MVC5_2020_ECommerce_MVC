using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.ECommerce_MVC.cms.PhatTrien
{
    public partial class Dropdowlisst_selectpicker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Literal1.Text = "";
            Literal1.Text += "<br />Tỉnh thành:" + hdddlCountry.Value;
            Literal1.Text += "<br />Quận huyện :" + hdddlProvince.Value;
            Literal1.Text += "<br />Phường xã:" + hdddlDistrict.Value;
        }
    }
}