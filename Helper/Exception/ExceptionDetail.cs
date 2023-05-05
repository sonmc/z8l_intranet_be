using System;
using System.Text.Json;

namespace z8l_intranet_be.Helper.Exception
{
    public class ExceptionDetail
    {
        public string message { get; set; }
        public string Message { get; set; }
        public string? Title { get; set; }
        public Object? Details { get; set; }

        public ExceptionDetail()
        {
            Title = "";
            message = "";
            Message = "";
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}