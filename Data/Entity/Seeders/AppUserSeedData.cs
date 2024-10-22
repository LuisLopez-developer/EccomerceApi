using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data.Entity.Seeders
{
    public static class AppUserSeedData
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            // Agregar los seeders para People y State, siempre y cuando no esten ya en el contexto
            //PeopleSeedData.SeedData(modelBuilder);
            //StateSeedData.SeedData(modelBuilder);

            // Sembrar datos para la entidad AppUser
            modelBuilder.Entity<UserModel>().HasData(
                new UserModel
                {
                    Id = "1",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<UserModel>().HashPassword(null, "Admin@123"),
                    SecurityStamp = string.Empty,
                    StateId = 1, // Estado activo
                    PeopleId = 1 // Luis Lopez
                },
                new UserModel
                {
                    Id = "2",
                    UserName = "user",
                    NormalizedUserName = "USER",
                    Email = "user@example.com",
                    NormalizedEmail = "USER@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<UserModel>().HashPassword(null, "User@123"),
                    SecurityStamp = string.Empty,
                    StateId = 1, // Estado activo
                    PeopleId = 2 // Jeampierre Muñoz
                },
                new UserModel
                {
                    Id = "3",
                    UserName = "managed",
                    NormalizedUserName = "MANAGED",
                    Email = "managed@example.com",
                    NormalizedEmail = "MANAGED@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<UserModel>().HashPassword(null, "Managed@123"),
                    SecurityStamp = string.Empty,
                    StateId = 1, // Estado activo
                    PeopleId = 3 // Jean Benedicto
                }
                // Puedes agregar más usuarios aquí según sea necesario
            );
        }
    }
}
