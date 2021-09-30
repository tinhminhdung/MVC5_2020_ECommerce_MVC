using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanChuyen
{
    public class Root
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<chitiet> data { get; set; }
    }
    public class chitiet
    {
        public int id { get; set; }
        public string title { get; set; }
        public int from_time { get; set; }
        public int to_time { get; set; }

    }
}
