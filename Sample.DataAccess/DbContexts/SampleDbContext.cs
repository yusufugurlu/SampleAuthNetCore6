using Microsoft.EntityFrameworkCore;
using Sample.DataAccess.Configurations;
using Sample.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.DataAccess.DbContexts
{
    public class SampleDbContext : DbContext
    {
        public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configuration yaptığımız classları burada tanımlanır.
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserActivationEmailInformationConfiguration());
            modelBuilder.ApplyConfiguration(new UserLoginResponseTimeStampConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new BookTypeConfiguration());     
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserActivationEmailInformation> UserActivationEmailInformations { get; set; }
        public DbSet<UserLoginResponseTimeStamp> UserLoginResponseTimeStamps { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookType> BookTypes { get; set; }

    }
}
