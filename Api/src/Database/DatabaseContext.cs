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
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<ProductOrder> ProductOrders { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        static DatabaseContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            /* NpgsqlConnection.GlobalTypeMapper.MapEnum<Role>(); */
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
            var builder = new NpgsqlDataSourceBuilder(
                _config.GetConnectionString("DefaultConnection")
            );
            builder.MapEnum<Role>("role");
            options
                .UseNpgsql(builder.Build())
                .AddInterceptors(new DbContextSaveChangesInterceptor())
                .UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasPostgresEnum<Role>(); // will create a enum type called "role" inside database
            builder.Entity<User>(entity =>
            {
                entity.Property(e => e.Role).HasColumnType("role");
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
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
            builder.Entity<Category>(entity =>
            {
                entity
                    .HasMany(c => c.Products)
                 .WithOne(p => p.Category)
                 .HasForeignKey(p => p.CategoryId)
                 .OnDelete(DeleteBehavior.Cascade);

            });

            builder.Entity<Product>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
        }
    }
}
