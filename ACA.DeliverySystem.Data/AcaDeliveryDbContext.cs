using ACA.DeliverySystem.Data.Configurations;
using ACA.DeliverySystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACA.DeliverySystem.Data
{
    public class AcaDeliveryDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AcaDeliveryDbContext(DbContextOptions<AcaDeliveryDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
