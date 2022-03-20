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
    public class BookTypeConfiguration : IEntityTypeConfiguration<BookType>
    {
        public void Configure(EntityTypeBuilder<BookType> builder)
        {
            builder.HasData(new BookType
            {
                Id = 1,
                Name = "Masal",
                IsDisabled=false,
                CreateDate=DateTime.Now
            });
            builder.HasData(new BookType
            {
                Id = 2,
                Name = "Öykü",
                IsDisabled = false,
                CreateDate = DateTime.Now
            });
            builder.HasData(new BookType
            {
                Id = 3,
                Name = "Roman",
                IsDisabled = false,
                CreateDate = DateTime.Now
            });
        }
    }
}
