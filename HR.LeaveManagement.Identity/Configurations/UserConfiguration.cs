using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData(
                new ApplicationUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "ADMIN@LOCALHOST.COM",
                    FirstName = "System",
                    LastName = "Admin",
                    UserName = "admin@localhost.com",
                    NormalizedUserName = "ADMIN@LOCALHOST.COM",
                    PasswordHash = "AQAAAAIAAYagAAAAEHl6r86NL3eyubd96ZuanN9DI/IyBqnbSYQprWzJged/i9LcIuc4+egZK51jpmKHtg==", // Password is "Qwerty123!"
                    EmailConfirmed = true,
                    ConcurrencyStamp = "57f43447-804a-424b-adbb-5104518848a7",
                    SecurityStamp = "e70807cb-17d2-44d5-9a8c-9b5185223b88"
                },
                new ApplicationUser
                {
                    Id = "9e224968-33e4-4652-b7b7-8574d048cdb9",
                    Email = "user@localhost.com",
                    NormalizedEmail = "USER@LOCALHOST.COM",
                    FirstName = "System",
                    LastName = "User",
                    UserName = "user@localhost.com",
                    NormalizedUserName = "USER@LOCALHOST.COM",
                    PasswordHash = "AQAAAAIAAYagAAAAEHl6r86NL3eyubd96ZuanN9DI/IyBqnbSYQprWzJged/i9LcIuc4+egZK51jpmKHtg==", // Password is "Qwerty123!"
                    EmailConfirmed = true,
                    ConcurrencyStamp = "072e0b52-011a-4db4-845d-2d088b2c2f93",
                    SecurityStamp = "bac58039-a347-4b5e-98dc-4696d71b57c7"
                }
            );
        }
    }
}
