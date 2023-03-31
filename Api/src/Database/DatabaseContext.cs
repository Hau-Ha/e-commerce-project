using System;
using System.Collections.Immutable;
using System.IO.Compression;
using Api.src.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Api.src.Database
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _config;
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }


        public DatabaseContext(DbContextOptions<DatabaseContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

            options.UseNpgsql(_config.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasPostgresEnum<Role>(); // will create a enum type called "role" inside database
            builder.Entity<User>(entity =>
            {
                entity.Property(e => e.Role).HasColumnType("role");
                entity.HasIndex(e => e.Email).IsUnique();
            }); // connect property "Role" to enum type "role"
            builder.Entity<ProductOrder>(entity =>
            {
                entity
                    .HasOne(e => e.Product)
                    .WithMany()
                    .HasForeignKey(entity => entity.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity
                    .HasOne(e => e.Order)
                    .WithMany()
                    .HasForeignKey(e => e.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }


    }
}
