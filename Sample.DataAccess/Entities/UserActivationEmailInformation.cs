using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.DataAccess.Entities
{
    public class UserActivationEmailInformation : BaseEntity
    {
        public string GuidKey { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime? UsedDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
