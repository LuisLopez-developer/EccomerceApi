using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EccomerceApi.Entity.Seeders
{
    public static class AppUserRoleSeedData
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            // Sembrar datos para la tabla AspNetUserRoles
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = "1", RoleId = "1" }, // Asignar rol "admin" al usuario "admin"
                new IdentityUserRole<string> { UserId = "2", RoleId = "2" }, // Asignar rol "user" al usuario "user"
                new IdentityUserRole<string> { UserId = "3", RoleId = "3" }  // Asignar rol "managed" al usuario "managed"
                // Puedes asignar más roles a más usuarios aquí según sea necesario
            );
        }
    }
}
