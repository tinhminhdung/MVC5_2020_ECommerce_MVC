using MoreAll;
using Newtonsoft.Json;
using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using VS.ECommerce_MVC;

public class GiaoHangTietKiemWebFrom
{

    public static string sendPostNews(string url, string requestData, string method)
    {
        string result = "";
        byte[] byteData = null;
        if (requestData != null)
        {
            byteData = Encoding.GetEncoding("UTF-8").GetBytes(requestData.ToString());
        }
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Timeout = 30 * 1000;
            request.Method = method;
            request.Headers["Authorization"] = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJob3NjbyIsImF6cCI6ImFkbWluIiwianRpIjoiNTZkNjkwNDctNmI1Yi00NWJkLTk5M2YtMWRhYWI4MzBmMTFkIiwibmFtZWlkIjoiMSIsInNpZCI6IiIsIm5iZiI6MTYyNjg0OTQ4MSwiZXhwIjoxNjMyMDMzNDgxLCJpc3MiOiJob3Njby52biIsImF1ZCI6Imhvc2NvLnZuIn0.3DxbhbROU-s7izt5-xQy72Y5xIkkRVT1iEagPWtYSSI";
            if (byteData != null)
            {
                Stream stream = request.GetRequestStream();
                stream.Write(byteData, 0, byteData.Length);
                stream.Flush();
                stream.Close();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream backStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(backStream, Encoding.GetEncoding("UTF-8"));
            result = sr.ReadToEnd();
            sr.Close();
            backStream.Close();
            response.Close();
            request.Abort();
        }
        catch (Exception ex)
        {
            result = ex.Message;
        }
        return result;
    }
    public static string sendPost(string url, string requestData, string method)
    {
        string result = "";
        byte[] byteData = null;
        if (requestData != null)
        {
            byteData = Encoding.GetEncoding("UTF-8").GetBytes(requestData.ToString());
        }
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Timeout = 30 * 1000;
            request.Method = method;
            request.Headers["Token"] = "89ceA31EC615B829085cDe3f6f155bFcFc8987Af";
            if (byteData != null)
            {
                Stream stream = request.GetRequestStream();
                stream.Write(byteData, 0, byteData.Length);
                stream.Flush();
                stream.Close();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream backStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(backStream, Encoding.GetEncoding("UTF-8"));
            result = sr.ReadToEnd();
            sr.Close();
            backStream.Close();
            response.Close();
            request.Abort();
        }
        catch (Exception ex)
        {
            result = ex.Message;
        }
        return result;
    }

    public static string TaoDonHangAPIBanHang()
    {
        // Tạo đơn hàng sang bên giao hàng tiết kiệm
        string requestData = "{\"productName\":\"\",\"productCode\":\"\",\"description\":\"\",\"visible\":true,\"instock\":0,\"productGroup\":\"\",\"pageSize\":10,\"pageIndex\":10}";
        string ReqURL = "http://api.phanmembanhang.com/api/Category/ProductList";
        return sendPostNews(ReqURL, requestData, "POST");
        // sau khi có kết quả thì lại gen ra dạng   var httpContent = new StringContent(json, Encoding.UTF8, "application/json"); để đọc kết quả nhé
    }



    public static string TaoDonHang()
    {
        // Tạo đơn hàng sang bên giao hàng tiết kiệm
        string requestData = "{ \"products\":[ { \"name\":\"Bàn kệ tivi 7777777777777\", \"weight\":0.1, \"quantity\":37, \"product_code\":74 } ], \"order\":{ \"id\":\"2244f\", \"pick_name\":\"HCM-nội thành\", \"pick_address\":\"590 CMT8 P.11\", \"pick_province\":\"TP. Hồ Chí Minh\", \"pick_district\":\"Quận 3\", \"pick_ward\":\"Phường 1\", \"pick_tel\":\"0911222333\", \"tel\":\"0911222333\", \"name\":\"GHTK - HCM - Noi Thanh\", \"address\":\"123 nguyễn chí thanh\", \"province\":\"TP. Hồ Chí Minh\", \"district\":\"Quận 1\", \"ward\":\"Phường Bến Nghé\", \"hamlet\":\"Khác\", \"is_freeship\":\"1\", \"pick_date\":\"2016-09-30\", \"pick_money\":47000, \"note\":\"Khối lượng tính cước tối đa: 1.00 kg\", \"value\":3000000, \"transport\":\"fly\", \"pick_option\":\"cod\", \"deliver_option\":\"xteam\", \"pick_session\":2 } }";
        string ReqURL = "https://services.giaohangtietkiem.vn/services/shipment/order/?ver=1.5";
        return sendPost(ReqURL, requestData, "POST");
        // sau khi có kết quả thì lại gen ra dạng   var httpContent = new StringContent(json, Encoding.UTF8, "application/json"); để đọc kết quả nhé
    }
    public static string HuyDonHang(string ID)
    {
        // Hủy đơn hàng 
        string ReqURL = "https://services.giaohangtietkiem.vn//services/shipment/cancel/" + ID;
        return sendPost(ReqURL, "", "POST");
        // sau khi có kết quả thì lại gen ra dạng   var httpContent = new StringContent(json, Encoding.UTF8, "application/json"); để đọc kết quả nhé
    }

    //Học các của trang https://www.tracktry.com/api-csharp.html

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    // Cách 1 thông qua dạng chưa fomat json
    //    // Học các của trang https://www.tracktry.com/api-csharp.html
    //    string requestData = "{ \"products\":[ { \"name\":\"Bàn kệ tivi 7777777777777\", \"weight\":0.1, \"quantity\":37, \"product_code\":74 } ], \"order\":{ \"id\":\"677001\", \"pick_name\":\"HCM-nội thành\", \"pick_address\":\"590 CMT8 P.11\", \"pick_province\":\"TP. Hồ Chí Minh\", \"pick_district\":\"Quận 3\", \"pick_ward\":\"Phường 1\", \"pick_tel\":\"0911222333\", \"tel\":\"0911222333\", \"name\":\"GHTK - HCM - Noi Thanh\", \"address\":\"123 nguyễn chí thanh\", \"province\":\"TP. Hồ Chí Minh\", \"district\":\"Quận 1\", \"ward\":\"Phường Bến Nghé\", \"hamlet\":\"Khác\", \"is_freeship\":\"1\", \"pick_date\":\"2016-09-30\", \"pick_money\":47000, \"note\":\"Khối lượng tính cước tối đa: 1.00 kg\", \"value\":3000000, \"transport\":\"fly\", \"pick_option\":\"cod\", \"deliver_option\":\"xteam\", \"pick_session\":2 } }";
    //    string ReqURL = "https://services.giaohangtietkiem.vn/services/shipment/order/?ver=1.5";
    //    sendPost(ReqURL, requestData, "POST");
    //    //  sau khi có kết quả thì lại gen ra dạng   var httpContent = new StringContent(json, Encoding.UTF8, "application/json"); để đọc kết quả nhé
    //}

    //public static string getOrderTracesByJson(string requestData, string urlStr, string method)
    //{
    //    string result = null;
    //    if (method.Equals("post"))
    //    {

    //        string ReqURL = "http://api.tracktry.com/v1/trackings/post";
    //        string RelUrl = ReqURL + urlStr;
    //        result = sendPost(ReqURL, requestData, "POST");

    //    }
    //    else if (method.Equals("get"))
    //    {

    //        string ReqURL = "http://api.tracktry.com/v1/trackings/get";
    //        string RelUrl = ReqURL + urlStr;
    //        //Console.WriteLine("RelUrl:" + RelUrl);
    //        result = sendPost(RelUrl, requestData, "GET");

    //    }
    //    else if (method.Equals("batch"))
    //    {

    //        string ReqURL = "http://api.tracktry.com/v1/trackings/batch";
    //        string RelUrl = ReqURL + urlStr;
    //        //Console.WriteLine("RelUrl:" + RelUrl);
    //        result = sendPost(RelUrl, requestData, "POST");

    //    }
    //    else if (method.Equals("codeNumberGet"))
    //    {

    //        string ReqURL = "http://api.tracktry.com/v1/trackings";
    //        string RelUrl = ReqURL + urlStr;
    //        //Console.WriteLine("RelUrl:" + RelUrl);
    //        result = sendPost(RelUrl, requestData, "GET");

    //    }
    //    else if (method.Equals("codeNumberPut"))
    //    {

    //        string ReqURL = "http://api.tracktry.com/v1/trackings";
    //        string RelUrl = ReqURL + urlStr;
    //        //Console.WriteLine("RelUrl:" + RelUrl);
    //        result = sendPost(RelUrl, requestData, "PUT");

    //    }
    //    else if (method.Equals("codeNumberDel"))
    //    {

    //        string ReqURL = "http://api.tracktry.com/v1/trackings";
    //        string RelUrl = ReqURL + urlStr;
    //        //Console.WriteLine("RelUrl:" + RelUrl);
    //        result = sendPost(RelUrl, requestData, "DELETE");

    //    }
    //    else if (method.Equals("realtime"))
    //    {

    //        string ReqURL = "http://api.tracktry.com/v1/trackings/realtime";
    //        string RelUrl = ReqURL + urlStr;
    //        //Console.WriteLine("RelUrl:" + RelUrl);
    //        result = sendPost(RelUrl, requestData, "POST");

    //    }
    //    else if (method.Equals("carriers"))
    //    {

    //        string ReqURL = "http://api.tracktry.com/v1/carriers";
    //        string RelUrl = ReqURL + urlStr;
    //        //Console.WriteLine("RelUrl:" + RelUrl);
    //        result = sendPost(RelUrl, requestData, "GET");

    //    }
    //    else if (method.Equals("carriers/detect"))
    //    {

    //        string ReqURL = "http://api.tracktry.com/v1/carriers/detect";
    //        string RelUrl = ReqURL + urlStr;
    //        //Console.WriteLine("RelUrl:" + RelUrl);
    //        result = sendPost(RelUrl, requestData, "POST");

    //    }
    //    else if (method.Equals("update"))
    //    {

    //        string ReqURL = "http://api.tracktry.com/v1/trackings/update";
    //        string RelUrl = ReqURL + urlStr;
    //        //Console.WriteLine("RelUrl:" + RelUrl);
    //        result = sendPost(RelUrl, requestData, "POST");

    //    }
    //    else if (method.Equals("getuserinfo"))
    //    {

    //        string ReqURL = "http://api.tracktry.com/v1/trackings/getuserinfo";
    //        string RelUrl = ReqURL + urlStr;
    //        //Console.WriteLine("RelUrl:" + RelUrl);
    //        result = sendPost(RelUrl, requestData, "GET");

    //    }
    //    else if (method.Equals("getstatusnumber"))
    //    {

    //        string ReqURL = "http://api.tracktry.com/v1/trackings/getstatusnumber";
    //        string RelUrl = ReqURL + urlStr;
    //        //Console.WriteLine("RelUrl:" + RelUrl);
    //        result = sendPost(RelUrl, requestData, "GET");

    //    }
    //    else if (method.Equals("notupdate"))
    //    {

    //        string ReqURL = "http://api.tracktry.com/v1/trackings/notupdate";
    //        string RelUrl = ReqURL + urlStr;
    //        //Console.WriteLine("RelUrl:" + RelUrl);
    //        result = sendPost(RelUrl, requestData, "POST");

    //    }
    //    else if (method.Equals("remote"))
    //    {

    //        string ReqURL = "http://api.tracktry.com/v1/trackings/remote";
    //        string RelUrl = ReqURL + urlStr;
    //        Console.WriteLine("RelUrl:" + RelUrl);
    //        result = sendPost(RelUrl, requestData, "POST");

    //    }
    //    else if (method.Equals("costtime"))
    //    {

    //        string ReqURL = "http://api.tracktry.com/v1/trackings/costtime";
    //        string RelUrl = ReqURL + urlStr;
    //        //Console.WriteLine("RelUrl:" + RelUrl);
    //        result = sendPost(RelUrl, requestData, "POST");

    //    }
    //    else if (method.Equals("delete"))
    //    {

    //        string ReqURL = "http://api.tracktry.com/v1/trackings/delete";
    //        string RelUrl = ReqURL + urlStr;
    //        //Console.WriteLine("RelUrl:" + RelUrl);
    //        result = sendPost(RelUrl, requestData, "POST");

    //    }
    //    else if (method.Equals("updatemore"))
    //    {

    //        string ReqURL = "http://api.tracktry.com/v1/trackings/updatemore";
    //        string RelUrl = ReqURL + urlStr;
    //        //Console.WriteLine("RelUrl:" + RelUrl);
    //        result = sendPost(RelUrl, requestData, "POST");

    //    }
    //    return result;

    //}
}
