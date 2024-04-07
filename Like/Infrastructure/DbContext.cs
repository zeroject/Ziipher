using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LikeInfrastructure;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbContext(DbContextOptions<DbContext> options, ServiceLifetime service) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Like>()
            .Property(dm => dm.ID)
            .ValueGeneratedOnAdd();
    }
    public DbSet<Like> Likes { get; set; }

    

}
