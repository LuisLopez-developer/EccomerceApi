using Microsoft.EntityFrameworkCore;

namespace EccomerceApi.Entity.Seeders
{
    public static class StateSeedData
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<State>().HasData(
                new State { Id = 1, Name = "Activo" },
                new State { Id = 2, Name = "Inactivo" },
                new State { Id = 3, Name = "En espera" },
                new State { Id = 4, Name = "En proceso" }
            );
        }
    }
}
