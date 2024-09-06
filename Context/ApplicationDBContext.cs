using HishabNikash.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HishabNikash.Context
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Hishab> Hishabs { get; set; }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options){}

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hishab>()
                .HasMany(h => h.Histories)
                .WithOne(history => history.Hishab)
                .HasForeignKey(history => history.HishabID)
                .OnDelete(DeleteBehavior.Cascade);  // Enable cascade delete
        }

    }
}