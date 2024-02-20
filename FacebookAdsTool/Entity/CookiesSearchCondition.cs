using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookAdsTool.Entity
{


    public class CookiesSearchCondition
    {
        public string start { get; set; }
        public string end { get; set; }
        public string[] faceTypes { get; set; }
        public int? organizationId { get; set; }
        public int? employeeId { get; set; }
        public int? page { get; set; }
        public int? size { get; set; }


    }


    public class Sort
    {
        public bool sorted { get; set; }
        public bool unsorted { get; set; }
        public bool empty { get; set; }
    }

    public class Pageable
    {
        public Sort sort { get; set; }
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public int offset { get; set; }
        public bool paged { get; set; }
        public bool unpaged { get; set; }
    }
}
