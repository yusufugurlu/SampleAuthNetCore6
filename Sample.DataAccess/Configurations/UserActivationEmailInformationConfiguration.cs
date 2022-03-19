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
    public class UserActivationEmailInformationConfiguration : IEntityTypeConfiguration<UserActivationEmailInformation>
    {
        public void Configure(EntityTypeBuilder<UserActivationEmailInformation> builder)
        {
            builder.HasOne(x => x.User).WithMany(p => p.UserActivationEmailInformation).HasForeignKey(x => x.UserId).HasConstraintName("FK_User_UserActivationEmailInformations_UserId");
            builder.HasIndex(x => x.GuidKey).HasName("IX_UserActivationEmailInformation_GuidKey");
        }
    }
}
