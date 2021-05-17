using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class ResponseDTO
    {
        public string ResponseStatus { get; set; }
        public string Message { get; set; }
    }

    public enum ResponseStatusEnum
    {
        Success,
        Error
    }
}
