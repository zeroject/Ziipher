using AuthenticationService.BE;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Models
{
    public class AuthContext : DbContext
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {
        }

        public DbSet<LoginBe> Users { get; set; }

        public DbSet<TokenBe> tokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginBe>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired();
                entity.Property(e => e.Password).IsRequired();
            });

            modelBuilder.Entity<TokenBe>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Token).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();

                // Define the one-to-one relationship
                entity.HasOne(e => e.User) // Navigation property
                    .WithOne() // No navigation property on the other side (one-to-one)
                    .HasForeignKey<TokenBe>(e => e.UserId) // Foreign key property
                    .IsRequired(); // Make it required
            });
        }


    }
}
