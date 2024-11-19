using Microsoft.EntityFrameworkCore;

namespace Customer.API.Persistence
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {
            
        }

        public DbSet<Entities.CustomerEntity> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Entities.CustomerEntity>().HasIndex(x => x.UserName).IsUnique();
            modelBuilder.Entity<Entities.CustomerEntity>().HasIndex(x => x.EmailAddress).IsUnique();
        }
    }
}
