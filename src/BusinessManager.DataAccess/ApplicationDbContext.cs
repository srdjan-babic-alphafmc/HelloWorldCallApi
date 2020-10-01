using BusinessManager.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagerApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Products> Products { get; set; }
        public DbSet<Clients> Clients { get; set; }
        public DbSet<Configuration> Configuration { get; set; }
        public DbSet<Provider> Provider { get; set; }
        public DbSet<Sale> Sale { get; set; }
    }
}
