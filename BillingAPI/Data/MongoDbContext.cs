using BillingAPI.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using System.Numerics;
using System.Reflection.Emit;

namespace BillingAPI.Data
{
    public class MongoDbContext : DbContext
    {
        public IConfiguration _configuration;

        public MongoDbContext(DbContextOptions<MongoDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Billing> Billings { get; set; }
        public DbSet<BillingLine> BillingLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().ToCollection("customers");
            modelBuilder.Entity<Product>().ToCollection("products");
            modelBuilder.Entity<Billing>().ToCollection("billings");
            modelBuilder.Entity<BillingLine>().ToCollection("billingLines");
        }
    }
}
