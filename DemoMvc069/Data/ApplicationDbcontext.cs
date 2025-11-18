using Microsoft.EntityFrameworkCore;
using DemoMvc069.Models;

namespace DemoMvc069.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Person> Persons { get; set; }
    }
}