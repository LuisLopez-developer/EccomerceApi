using Microsoft.EntityFrameworkCore;
using Models;

namespace Data.Entity.Seeders
{
    public class PaymentMethodSeedData
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentMethodModel>().HasData(
                new PaymentMethodModel { Id = 1, Name = "Efectivo", Description = "Pago en efectivo" },
                new PaymentMethodModel { Id = 2, Name = "Tarjeta de crédito", Description = "Pago con tarjeta de crédito" },
                new PaymentMethodModel { Id = 3, Name = "Tarjeta de débito", Description = "Pago con tarjeta de débito" },
                new PaymentMethodModel { Id = 4, Name = "Transferencia bancaria", Description = "Pago por transferencia bancaria" }
            );
        }
    }
}
