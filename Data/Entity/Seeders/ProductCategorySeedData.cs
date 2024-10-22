using Microsoft.EntityFrameworkCore;
using Models;

namespace Data.Entity.Seeders
{
    public static class ProductCategorySeedData
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { Id = 1, Name = "Gama Baja" },
                new ProductCategory { Id = 2, Name = "Gama Media" },
                new ProductCategory { Id = 3, Name = "Gama Alta" },
                new ProductCategory { Id = 4, Name = "Gama Top" }
                // Puedes agregar más categorías de productos aquí según sea necesario
            );
        }
    }
}
