using Microsoft.EntityFrameworkCore;
using Models;

namespace Data.Entity.Seeders
{
    public static class StateSeedData
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StateModel>().HasData(
                new StateModel { Id = 1, Name = "Activo" },
                new StateModel { Id = 2, Name = "Inactivo" },
                new StateModel { Id = 3, Name = "En espera" },
                new StateModel { Id = 4, Name = "En proceso" }
            );
        }
    }
}
