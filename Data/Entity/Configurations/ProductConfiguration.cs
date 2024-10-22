using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Data.Entity.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductModel>
    {
        // Aqui se añadira las restricciones que no se puedan colocar por anotaciones de datos, en la entidad misma
        public void Configure(EntityTypeBuilder<ProductModel> builder)
        {
            builder.HasIndex(p => p.SKU)
                .IsUnique();

            builder.HasIndex(p => p.BarCode)
                .IsUnique();

            builder.Property(p => p.Cost)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
