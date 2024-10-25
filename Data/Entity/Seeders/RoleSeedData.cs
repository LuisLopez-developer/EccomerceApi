using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Data.Entity.Seeders
{
    public static class RoleSeedData
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "user", NormalizedName = "USER" },
                new IdentityRole { Id = "3", Name = "managed", NormalizedName = "MANAGED" },
                new IdentityRole { Id = "4", Name = "customer", NormalizedName = "CUSTOMER" }
            );
        }
    }
}
