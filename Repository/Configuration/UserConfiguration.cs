using HishabNikash.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    ID = 1,
                    FirstName = "Seed",
                    LastName = "User",
                    UserName = "test_user",
                    Email = "initial-data-seed@test.user",
                    HashedPassword = "initial-user-password"
                }
            );
        }
    }
}
