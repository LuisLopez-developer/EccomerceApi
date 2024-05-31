using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EccomerceApi.Entity.Seeders
{
    public static class PeopleSeedData
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<People>().HasData(
                new People { Id = 1, Name = "Luis", LastName = "Lopez", Address = "123 Elm St" },
                new People { Id = 2, Name = "Jeampierre", LastName = "Muñoz", Address = "431 Elm St" },
                new People { Id = 3, Name = "Jean", LastName = "benedicto", Address = "233 Elm St" },
                new People { Id = 4, Name = "Fabrizzio", LastName = "Zambrano", Address = "432 Elm St" },
                new People { Id = 5, Name = "Fabian", LastName = "Ambrosio", Address = "32 Elm St" }
            );
        }
    }
}
