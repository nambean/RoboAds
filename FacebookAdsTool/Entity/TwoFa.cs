using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookAdsTool.Entity
{
    public class TwoFa
    {
       
        public string fbUserId { get; set; }
        public string fbUserName { get; set; }
        public string fbPassword { get; set; }
        public string secretCode { get; set; }       
        public string licenceKey { get; set; }
        public string twoFaName { get; set; }
    }

    public class TwoFaRespon
    {
        public Boolean success { get; set; }
        public string message { get; set; }
    }
}
