using Microsoft.EntityFrameworkCore;
using WebApplication_temp.Models;

namespace WebApplication_temp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) //BASIC SYNTAX IN EVERY DATABASES
        {

        }
        public DbSet<Category> Categories { get; set; } // Using this one-line code from Entity Framework will make a Table in SQL Server

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
                );
        }
    }
}
