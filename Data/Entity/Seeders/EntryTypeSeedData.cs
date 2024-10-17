using Microsoft.EntityFrameworkCore;

namespace Data.Entity.Seeders
{
    public static class EntryTypeSeedData
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EntryType>().HasData(
                new EntryType { Id = 1, Type = "Compra" },
                new EntryType { Id = 2, Type = "Transferencia de inventario" },
                new EntryType { Id = 3, Type = "Devolución de cliente" },
                new EntryType { Id = 4, Type = "Donación recibida" },
                new EntryType { Id = 5, Type = "Muestra gratuita" }
                // Puedes agregar más tipos de entrada aquí según sea necesario
            );
        }
    }
}
