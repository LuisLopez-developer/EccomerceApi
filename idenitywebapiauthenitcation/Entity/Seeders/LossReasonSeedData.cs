using Microsoft.EntityFrameworkCore;

namespace EccomerceApi.Entity.Seeders
{
    public static class LossReasonSeedData
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LossReason>().HasData(
                new LossReason { Id = 1, Reason = "Daño durante el transporte" },
                new LossReason { Id = 2, Reason = "Fecha de caducidad vencida" },
                new LossReason { Id = 3, Reason = "Robo" },
                new LossReason { Id = 4, Reason = "Producto dañado en el almacén" },
                new LossReason { Id = 5, Reason = "Devolución del cliente" },
                new LossReason { Id = 6, Reason = "Muestra gratuita" }
                // Puedes agregar más motivos de pérdida aquí según sea necesario
            );
        }
    }
}
