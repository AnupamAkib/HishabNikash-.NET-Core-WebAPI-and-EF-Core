using HishabNikash.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HishabNikash.Context
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options){}

        public DbSet<User> Users { get; set; }
        public DbSet<Hishab> Hishabs { get; set; }
    }
}