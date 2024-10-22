using Microsoft.EntityFrameworkCore;
using Models;

namespace Data.Entity.Seeders
{
    public static class ProductBrandSeedData
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductBrandModel>().HasData(
                new ProductBrandModel { Id = 1, Name = "Samsung" },
                new ProductBrandModel { Id = 2, Name = "Apple" },
                new ProductBrandModel { Id = 3, Name = "Huawei" },
                new ProductBrandModel { Id = 4, Name = "Xiaomi" },
                new ProductBrandModel { Id = 5, Name = "Motorola" },
                new ProductBrandModel { Id = 6, Name = "Lg" },
                new ProductBrandModel { Id = 7, Name = "Sony" }
            // Puedes agregar más marcas de productos aquí según sea necesario
            );
        }
    }

}
