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
            optionsBuilder.UseSqlServer("Server=Auth-db;Database=Auth;User Id=sa;Password=SuperSecret7!;Trusted_Connection=False;TrustServerCertificate=True;");
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual DbSet<Login> logins { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual DbSet<Token> tokens { get; set; }

/// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id);
                entity.Property(e => e.Username).IsRequired();
                entity.Property(e => e.Password).IsRequired();
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.JwtToken).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(e => e.User)
                    .WithMany(e => e.Tokens)
                    .HasForeignKey(e => e.UserId);
            });
        }


    }
}
