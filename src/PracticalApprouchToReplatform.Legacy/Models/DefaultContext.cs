using Microsoft.EntityFrameworkCore;

namespace PracticalApprouchToReplatform.Legacy.Models
{
    public class DefaultContext : DbContext
    {
        
        public DefaultContext (DbContextOptions<DefaultContext> options)
            : base(options)
        {
        }
        
        public DbSet<Package> Packages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Package>().ToTable("Packages");
            
            base.OnModelCreating(modelBuilder);
        }
    }
}