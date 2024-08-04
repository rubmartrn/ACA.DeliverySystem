using ACA.DeliverySystem.Data.Configurations;
using ACA.DeliverySystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ACA.DeliverySystem.Data
{
    public class DeliveryDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }

        public DeliveryDbContext(DbContextOptions<DeliveryDbContext> options) : base(options)
        {

        }

        public DeliveryDbContext() 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }

}
