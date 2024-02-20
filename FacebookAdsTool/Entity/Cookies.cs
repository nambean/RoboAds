using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookAdsTool.Entity
{
    

    public class Cookie
    {
        public DateTime createdAt { get; set; }
        public int? createdBy { get; set; }
        public DateTime updateAt { get; set; }
        public int? updateBy { get; set; }
        public int id { get; set; }
        public string ipAddress { get; set; }
        public string cookie { get; set; }
        public string actId { get; set; }
        public string fbUserId { get; set; }
        public string fbUserName { get; set; }
        public int? adTrustDsl { get; set; }
        public int? amountSpent { get; set; }
        public int? useStatus { get; set; }
        public int? useStatusid { get; set; }
        public bool isRunning { get; set; }
        public string licenceKey { get; set; }
    }

    public class Meta
    {
        public int totalItems { get; set; }
        public int itemCount { get; set; }
        public int itemsPerPage { get; set; }
        public int totalPages { get; set; }
        public int currentPage { get; set; }
    }

    public class Cookies
    {
        public List<Cookie> items { get; set; }
        public Meta meta { get; set; }
    }
}
