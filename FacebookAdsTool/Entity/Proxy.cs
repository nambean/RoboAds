using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookAdsTool.Entity
{
    public class Proxy
    {
        public string ip { get; set; }
        public string ip_number { get; set; }
        public int ip_version { get; set; }
        public int port { get; set; }
        public string speed { get; set; }
        public string country_name { get; set; }
        public string country_code2 { get; set; }
        public string isp { get; set; }
        public string response_code { get; set; }
        public string response_message { get; set; }
    }
}
