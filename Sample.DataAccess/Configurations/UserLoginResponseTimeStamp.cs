using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.DataAccess.Configurations
{
    public class UserLoginResponseTimeStamp
    {
        public int Id { get; set; }
        public long TimeStamp { get; set; }
        public DateTime ResponseStart { get; set; }
        public DateTime ResponseEnd { get; set; }
    }
}
