using HishabNikash.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;

namespace Repository
{
    public class RepositoryContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Hishab> Hishabs { get; set; }

        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hishab>()
                .HasMany(h => h.Histories)
                .WithOne(history => history.Hishab)
                .HasForeignKey(history => history.HishabID)
                .OnDelete(DeleteBehavior.Cascade);  // Enable cascade delete

            //initialize data seed for user
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
