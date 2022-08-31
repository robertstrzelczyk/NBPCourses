using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPBCourses.Persistence.Models
{
    public class CoursesDbContext : DbContext
    {
        private string connectionString = "Server=DESKTOP-OIISHMB;Database=NBPCourses;Trusted_Connection=True;";
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .Property(x => x.CurrencyName)
                .HasMaxLength(50);

            modelBuilder.Entity<Course>()
                .Property(x => x.Code)
                .HasMaxLength(5);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(connectionString);
        }
    }
}
