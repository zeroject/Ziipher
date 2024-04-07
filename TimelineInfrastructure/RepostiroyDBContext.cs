using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimelineInfrastructure
{
    public class RepositoryDBContext : DbContext
    {

        public RepositoryDBContext(DbContextOptions<RepositoryDBContext> options, ServiceLifetime service) : base(options)
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

        public DbSet<Timeline> Timelines { get; set; }

    }
}
