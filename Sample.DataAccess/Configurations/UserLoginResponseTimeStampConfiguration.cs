using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.DataAccess.Configurations
{
    public class UserLoginResponseTimeStampConfiguration : IEntityTypeConfiguration<UserLoginResponseTimeStamp>
    {
        public void Configure(EntityTypeBuilder<UserLoginResponseTimeStamp> builder)
        {

            builder.HasOne(x => x.User).WithMany(p => p.UserLoginResponseTimeStamp).HasForeignKey(x => x.UserId).HasConstraintName("FK_User_UserLoginResponseTimeStamps_UserId");
        }
    }
}
