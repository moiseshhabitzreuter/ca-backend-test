using BillingAPI.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

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
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                      .HasElementName("name");
                entity.Property(e => e.Email)
                      .HasElementName("email");
                entity.Property(e => e.Address)
                      .HasElementName("address");
                entity.Property(e => e.IsDeleted)
                      .HasElementName("isDeleted");

                modelBuilder.Entity<BillingLine>().ToCollection("billingLines");
            });

            modelBuilder.Entity<Product>().ToCollection("products");
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ProductName)
                      .HasElementName("productName");
                entity.Property(e => e.IsDeleted)
                      .HasElementName("isDeleted");
            });

            modelBuilder.Entity<Billing>().ToCollection("billings");
            modelBuilder.Entity<Billing>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.InvoiceNumber)
                      .HasElementName("invoiceNumber");
                entity.Property(e => e.Date)
                      .HasElementName("date");
                entity.Property(e => e.DueDate)
                      .HasElementName("dueDate");
                entity.Property(e => e.TotalAmount)
                      .HasElementName("totalAmount");
                entity.Property(e => e.Currency)
                      .HasElementName("currency");
                entity.Property(e => e.CustomerId)
                      .HasElementName("customerId");
                entity.Property(e => e.IsDeleted)
                      .HasElementName("isDeleted");

                entity.HasOne(e => e.Customer)
                      .WithMany()
                      .HasForeignKey(e => e.CustomerId);
            });
            modelBuilder.Entity<BillingLine>().ToCollection("billingLines");
            modelBuilder.Entity<BillingLine>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.BillingId)
                      .HasElementName("billingId");
                entity.Property(e => e.ProductId)
                      .HasElementName("productId");
                entity.Property(e => e.Description)
                      .HasElementName("description");
                entity.Property(e => e.Quantity)
                      .HasElementName("quantity");
                entity.Property(e => e.UnitPrice)
                      .HasElementName("unitPrice");
                entity.Property(e => e.Subtotal)
                      .HasElementName("subtotal");
                entity.Property(e => e.IsDeleted)
                      .HasElementName("isDeleted");
            });
        }
    }
}
