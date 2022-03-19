using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Common.Result
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public int Status()
        {
            if (Success)
                return 200;
            else
                return 403;
        }
        public ServiceResult(bool success = false, string message = "", object data = null)
        {
            Success = success;
            Message = message;
            Data = data;
        }


        public bool IsSuccess
        {
            get { return Success; }
        }


    }

    public static class Result
    {
        public static ServiceResult Success( string message = "", object data = null)
        {
            return new ServiceResult(true, message, data);
        }
        public static ServiceResult Fail( string message = "", object data = null)
        {
            return new ServiceResult(false, message, data);
        }

    }
}
