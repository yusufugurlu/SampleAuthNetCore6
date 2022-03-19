using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Common.Response
{
    public class ResponseModel
    {
        public bool HasData { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public int Status { get; set; }
    }
}
