using Microsoft.EntityFrameworkCore;
using Models;

namespace Data.Entity.Seeders
{
    public static class LossReasonSeedData
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LossReasonModel>().HasData(
                new LossReasonModel { Id = 1, Reason = "Daño durante el transporte" },
                new LossReasonModel { Id = 2, Reason = "Fecha de caducidad vencida" },
                new LossReasonModel { Id = 3, Reason = "Robo" },
                new LossReasonModel { Id = 4, Reason = "Producto dañado en el almacén" },
                new LossReasonModel { Id = 5, Reason = "Devolución del cliente" },
                new LossReasonModel { Id = 6, Reason = "Muestra gratuita" }
                // Puedes agregar más motivos de pérdida aquí según sea necesario
            );
        }
    }
}
