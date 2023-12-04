using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsFeedAPI.Wrappers
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }        
        public double TotalPages { get; set; }
                  
        public PagedResponse(T data, int pageNumber, int pageSize, double TotalPages)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.TotalPages = TotalPages;
            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }
    }
}
