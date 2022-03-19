using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.DataAccess.Entities
{
    public class UserLoginResponseTimeStamp
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double TimeStamp { get; set; }
        public DateTime? ResponseStart { get; set; }
        public DateTime? ResponseEnd { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
