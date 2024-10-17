using Microsoft.EntityFrameworkCore;

namespace Data.Entity.Seeders
{
    public static class ProductBrandSeedData
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductBrand>().HasData(
                new ProductBrand { Id = 1, Name = "Samsung" },
                new ProductBrand { Id = 2, Name = "Apple" },
                new ProductBrand { Id = 3, Name = "Huawei" },
                new ProductBrand { Id = 4, Name = "Xiaomi" },
                new ProductBrand { Id = 5, Name = "Motorola" },
                new ProductBrand { Id = 6, Name = "Lg" },
                new ProductBrand { Id = 7, Name = "Sony" }
            // Puedes agregar más marcas de productos aquí según sea necesario
            );
        }
    }

}
