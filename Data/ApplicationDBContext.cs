using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Models;

namespace ProductManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(p => p.Id); //primary key
                entity.Property(p => p.Price).HasColumnType("decimal(18,2)");
                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Products)
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");
                entity.HasKey(c => c.Id); //primary key
            });

            // call seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var now = new DateTime(2026, 1, 22, 0, 0, 0, DateTimeKind.Utc);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics", Description = "Electronic devices and accessories", CreatedAt = now, UpdatedAt = now },
                new Category { Id = 2, Name = "Books", Description = "Printed and electronic books", CreatedAt = now, UpdatedAt = now },
                new Category { Id = 3, Name = "Home Appliances", Description = "Small and large home appliances", CreatedAt = now, UpdatedAt = now },
                new Category { Id = 4, Name = "Clothing", Description = "Men and women apparel", CreatedAt = now, UpdatedAt = now },
                new Category { Id = 5, Name = "Toys", Description = "Toys and games for children", CreatedAt = now, UpdatedAt = now },
                new Category { Id = 6, Name = "Sports", Description = "Sporting goods and accessories", CreatedAt = now, UpdatedAt = now }
            );

            modelBuilder.Entity<Product>().HasData(
                // Electronics (2 products)
                new Product { Id = 1, Name = "Laptop", Description = "15 inch laptop", Price = 999.99m, CategoryId = 1, CreatedAt = now, UpdatedAt = now },
                new Product { Id = 2, Name = "Smartphone", Description = "Android smartphone", Price = 499.99m, CategoryId = 1, CreatedAt = now, UpdatedAt = now },

                // Books (3 products)
                new Product { Id = 3, Name = "Novel", Description = "Fiction novel", Price = 19.99m, CategoryId = 2, CreatedAt = now, UpdatedAt = now },
                new Product { Id = 4, Name = "Cookbook", Description = "Healthy recipes", Price = 29.99m, CategoryId = 2, CreatedAt = now, UpdatedAt = now },
                new Product { Id = 5, Name = "Biography", Description = "Life story", Price = 14.99m, CategoryId = 2, CreatedAt = now, UpdatedAt = now },

                // Home Appliances (2 products)
                new Product { Id = 6, Name = "Blender", Description = "High-power blender", Price = 89.99m, CategoryId = 3, CreatedAt = now, UpdatedAt = now },
                new Product { Id = 7, Name = "Vacuum Cleaner", Description = "Bagless vacuum", Price = 149.99m, CategoryId = 3, CreatedAt = now, UpdatedAt = now },

                // Clothing (3 products)
                new Product { Id = 8, Name = "T-Shirt", Description = "Cotton T-Shirt", Price = 19.99m, CategoryId = 4, CreatedAt = now, UpdatedAt = now },
                new Product { Id = 9, Name = "Jeans", Description = "Denim jeans", Price = 49.99m, CategoryId = 4, CreatedAt = now, UpdatedAt = now },
                new Product { Id = 10, Name = "Jacket", Description = "Waterproof jacket", Price = 89.99m, CategoryId = 4, CreatedAt = now, UpdatedAt = now },

                // Toys (2 products)
                new Product { Id = 11, Name = "Building Blocks", Description = "Educational blocks", Price = 24.99m, CategoryId = 5, CreatedAt = now, UpdatedAt = now },
                new Product { Id = 12, Name = "Remote Car", Description = "Remote controlled car", Price = 39.99m, CategoryId = 5, CreatedAt = now, UpdatedAt = now },

                // Sports (2 products)
                new Product { Id = 13, Name = "Football", Description = "Official size football", Price = 29.99m, CategoryId = 6, CreatedAt = now, UpdatedAt = now },
                new Product { Id = 14, Name = "Tennis Racket", Description = "Lightweight racket", Price = 79.99m, CategoryId = 6, CreatedAt = now, UpdatedAt = now }
            );
        }
    }
}