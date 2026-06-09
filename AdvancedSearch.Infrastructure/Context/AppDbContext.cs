using Microsoft.EntityFrameworkCore;
using ShopSage.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedSearch.Infrastructure.Context
{
    public class AppDbContext:DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .OwnsOne(o => o.Address, a =>
                {
                    a.Property(p => p.Street).HasMaxLength(200);
                    a.Property(p => p.City).HasMaxLength(100);
                    a.Property(p => p.District).HasMaxLength(100);
                    a.Property(p => p.BuildingNumber).HasMaxLength(20);
                    a.Property(p => p.ZipCode).HasMaxLength(20);
                    a.Ignore(p => p.FullAddress);
                });

            modelBuilder.Entity<Customer>()
                .OwnsOne(c => c.Address, a =>
                {
                    a.Property(p => p.Street).HasMaxLength(200);
                    a.Property(p => p.City).HasMaxLength(100);
                    a.Property(p => p.District).HasMaxLength(100);
                    a.Property(p => p.BuildingNumber).HasMaxLength(20);
                    a.Property(p => p.ZipCode).HasMaxLength(20);
                    a.Ignore(p => p.FullAddress);
                });

            
        }

    }
}
