using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.DTOs
{
    public class APIPagedResponse<T> :ResponseDTO
    {
      

        public APIPagedResponse()
        {
        //   this.ResponseStatus = ResponseStatus.Success;
        }

        //public APIPagedResponse(T t) : this()
        //{
        //   this.Data = t;
        //}

        public APIPagedResponse(T t, int totalCount, bool error = false, string msg = null, int currentPage = 1, int pageSize = 10) : this()
        {
            //  if (error) ResponseStatus = ResponseStatus.Error;
            Data = t;
            Message = msg;
            TotalCount = totalCount;
            CurrentPage = (currentPage < 0) ? 1 : currentPage;
            PageSize = pageSize;

        }

        public T Data { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        //     public  ResponseStatus ResponseStatus { get; set; }
        //    public  string message { get; set; }


    }
}
