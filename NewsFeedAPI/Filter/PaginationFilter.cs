using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsFeedAPI.Filter
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageRecords { get; set; }
        public PaginationFilter()
        {
            this.PageNumber = 1;
            this.PageRecords = 10;
        }
        public PaginationFilter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageRecords = pageSize > 10 ? 10 : pageSize;
        }
    }
}
