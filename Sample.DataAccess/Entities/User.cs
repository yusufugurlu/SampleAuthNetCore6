using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.DataAccess.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            UserActivationEmailInformation = new HashSet<UserActivationEmailInformation>();
        }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public bool IsActivationEmail { get; set; }
        public bool IsLogin { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryDate { get; set; }
        public IEnumerable<UserActivationEmailInformation> UserActivationEmailInformation { get; set; }
    }
}
