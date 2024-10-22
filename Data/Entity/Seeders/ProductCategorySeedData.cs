using Microsoft.EntityFrameworkCore;
using Models;

namespace Data.Entity.Seeders
{
    public static class ProductCategorySeedData
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategoryModel>().HasData(
                new ProductCategoryModel { Id = 1, Name = "Gama Baja" },
                new ProductCategoryModel { Id = 2, Name = "Gama Media" },
                new ProductCategoryModel { Id = 3, Name = "Gama Alta" },
                new ProductCategoryModel { Id = 4, Name = "Gama Top" }
                // Puedes agregar más categorías de productos aquí según sea necesario
            );
        }
    }
}
