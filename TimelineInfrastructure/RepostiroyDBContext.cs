using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostInfrastructure
{
    public class RepositoryDBContext : DbContext
    {

        public RepositoryDBContext(DbContextOptions<RepositoryDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


          modelBuilder.Entity<Timeline>()
                .Property(t => t.TimelineID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Timeline>()
                .HasKey(t => t.TimelineID);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("TimelineDB");
        }

        public DbSet<Timeline> Timelines { get; set; }

    }
}
