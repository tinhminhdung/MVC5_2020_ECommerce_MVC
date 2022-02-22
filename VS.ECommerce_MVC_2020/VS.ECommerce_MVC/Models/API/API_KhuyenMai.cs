using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhuyenMai
{
 
    public class NewKhuyenKhai
    {
        public int inid { get; set; }
        public int icid { get; set; }
        public string Title { get; set; }
        public string Brief { get; set; }
        public string Images { get; set; }
        public string ImagesSmall { get; set; }
        public string Create_Date;
        public int Views { get; set; }
        public string Contents { get; set; }
    }
    public class Result_NKM
    {
        public static List<NewKhuyenKhai> item { get; set; }
    }

    public class RootKM
    {
        public Result_NKM result { get; set; }
       // public List<NewKhuyenKhai> result { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
    }

}