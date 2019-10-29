using Microsoft.EntityFrameworkCore;

namespace PracticalApprouchToReplatform.Legacy.Models
{
    public class PackageContext : DbContext
    {
        public PackageContext(DbContextOptions<PackageContext> options)
            : base(options)
        {
        }

        public DbSet<Package> Packages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Package>()
                .ToTable("Packages")
                .HasIndex(p => p.Barcode)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}