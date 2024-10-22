using Microsoft.EntityFrameworkCore;
using Models;

namespace Data.Entity.Seeders
{
    public static class EntryTypeSeedData
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EntryTypeModel>().HasData(
                new EntryTypeModel { Id = 1, Type = "Compra" },
                new EntryTypeModel { Id = 2, Type = "Transferencia de inventario" },
                new EntryTypeModel { Id = 3, Type = "Devolución de cliente" },
                new EntryTypeModel { Id = 4, Type = "Donación recibida" },
                new EntryTypeModel { Id = 5, Type = "Muestra gratuita" }
                // Puedes agregar más tipos de entrada aquí según sea necesario
            );
        }
    }
}
