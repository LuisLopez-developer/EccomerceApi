using Microsoft.EntityFrameworkCore;
using Models;

namespace Data.Entity.Seeders
{
    public class OrderStatusSeedData
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderStatusModel>().HasData(
                new OrderStatusModel { Id = 1, Name = "En espera" },
                new OrderStatusModel { Id = 2, Name = "En proceso" },
                new OrderStatusModel { Id = 3, Name = "Enviado" },
                new OrderStatusModel { Id = 4, Name = "Entregado" },
                new OrderStatusModel { Id = 5, Name = "Cancelado" }
            );
        }
    }
}
