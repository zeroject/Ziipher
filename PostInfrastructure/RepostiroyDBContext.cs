﻿using Domain;
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

        public RepositoryDBContext(DbContextOptions<RepositoryDBContext> options, ServiceLifetime service) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          modelBuilder.Entity<Post>()
                .Property(p => p.PostID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Post>()
                .HasKey(p => p.PostID);

            modelBuilder.Entity<Post>()
                .HasOne<Timeline>()
                .WithMany(t => t.Posts) 
                .HasForeignKey(p => p.TimelineID);
        }


        public DbSet<Post> Posts { get; set; }

        public DbSet<Timeline> Timelines { get; set; }

    }
}
