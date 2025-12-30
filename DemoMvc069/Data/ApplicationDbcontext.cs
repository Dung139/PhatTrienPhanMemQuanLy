using Microsoft.EntityFrameworkCore;
using DemoMvc069.Models;

namespace DemoMvc069.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<HeThongPhanPhoi> HeThongPhanPhoi { get; set; }
        public DbSet<DaiLy> DaiLy { get; set; }
    }
}