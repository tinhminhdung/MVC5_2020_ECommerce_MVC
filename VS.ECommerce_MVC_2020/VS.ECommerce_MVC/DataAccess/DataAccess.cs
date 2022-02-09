using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VS.ECommerce_MVC.Models;

public class DataAccess
{
    private static string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

    public static List<API_News> Sget_New_Detail(string id)
    {
        List<API_News> it_r = new List<API_News>();
        using (var con = new SqlConnection(strConn))
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("select * from News where inid=" + id + "", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("inid", id));
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    API_News it = new API_News
                    {
                        inid = Convert.ToInt32(reader["inid"].ToString()),
                        icid = Convert.ToInt32(reader["icid"].ToString()),
                        Title = reader["Title"].ToString(),
                        Brief = reader["Brief"].ToString(),
                        Images = reader["Images"].ToString(),
                        ImagesSmall = reader["ImagesSmall"].ToString(),
                        Create_Date = reader["Create_Date"].ToString(),
                        Views = Convert.ToInt32(reader["Views"].ToString()),
                        Contents = reader["Contents"].ToString(),
                    };
                    it_r.Add(it);
                }
                con.Close();
                return it_r;
            }
            catch (Exception ex)
            {
                con.Close();
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "Sget_New_Detail");
                return it_r;
            }
        }
    }
}
