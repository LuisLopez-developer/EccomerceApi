using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EccomerceApi.Entity;

namespace EccomerceApi.Entity.Configurations
{
    public class ProductBrandConfiguration : IEntityTypeConfiguration<ProductBrand>
    {
        public void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            builder.HasData(
                new ProductBrand { Id = 1, Name = "Samsung" },
                new ProductBrand { Id = 2, Name = "Apple" },
                new ProductBrand { Id = 3, Name = "Huawei" },
                new ProductBrand { Id = 4, Name = "Xiaomi" },
                new ProductBrand { Id = 5, Name = "Motorola" },
                new ProductBrand { Id = 6, Name = "Lg" },
                new ProductBrand { Id = 7, Name = "Sony" }
            );
        }
    }
}
