using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookAdsTool.Entity
{
    public class CookieUsing
    {
        public int id { get; set; }
        public string licenceKey { get; set; }
    }

    public class CookieUsingRespon
    {
        public Boolean success { get; set; }
        public string message { get; set; }
    }
}
