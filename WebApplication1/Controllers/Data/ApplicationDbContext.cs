using Microsoft.EntityFrameworkCore;
using WebApplication1.Models; //import like method in python

namespace WebApplication1.Controllers.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) //BASIC SYNTAX IN EVERY DATABASES
        {

        }
        public DbSet<Category> Categories { get; set; } // Using this one-line code from Entity Framework will make a Table in SQL Server
    }
}
