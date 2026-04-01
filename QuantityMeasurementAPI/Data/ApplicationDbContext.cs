using Microsoft.EntityFrameworkCore;
using QuantityMeasurementAPI.Models;

namespace QuantityMeasurementAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets for our entities
        public DbSet<User> Users { get; set; }
        public DbSet<MeasurementRecord> MeasurementRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User table
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.Salt).IsRequired();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            // Configure MeasurementRecord table
            modelBuilder.Entity<MeasurementRecord>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Operation).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Timestamp).HasDefaultValueSql("GETUTCDATE()");
                entity.HasIndex(e => e.Operation);
                entity.HasIndex(e => e.Timestamp);
            });
        }
    }
}