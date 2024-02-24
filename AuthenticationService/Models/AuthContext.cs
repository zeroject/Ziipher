using AuthenticationService.BE;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {
        }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TODO move connection string to appsettings.json
            optionsBuilder.UseSqlServer("Server=localhost;Database=auth;User Id=sa;Password=Hyy89xjw!;Trusted_Connection=True");
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual DbSet<LoginBe> logins { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual DbSet<TokenBe> tokens { get; set; }

/// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginBe>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Username).IsRequired();
                entity.Property(e => e.Password).IsRequired();
            });

            modelBuilder.Entity<TokenBe>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Token).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(e => e.User)
                    .WithMany(e => e.Tokens)
                    .HasForeignKey(e => e.UserId);
            });
        }


    }
}
