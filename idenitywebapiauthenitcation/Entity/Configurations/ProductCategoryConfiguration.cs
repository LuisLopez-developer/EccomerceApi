using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EccomerceApi.Entity.Configurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasData(
                new ProductCategory { Id = 1, Name = "Gama Baja" },
                new ProductCategory { Id = 2, Name = "Gama Media" },
                new ProductCategory { Id = 3, Name = "Gama Alta" },
                new ProductCategory { Id = 4, Name = "Gama Top" }
            );
        }
    }
}
