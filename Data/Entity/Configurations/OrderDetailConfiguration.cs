using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Data.Entity.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetailModel>
    {
        public void Configure(EntityTypeBuilder<OrderDetailModel> builder)
        {
            builder.Property(od => od.UnitCost)
                .HasColumnType("decimal(18,2)");

            builder.Property(od => od.UnitPrice)
                .HasColumnType("decimal(18,2)");

            builder.Property(od => od.TotalCost)
                .HasColumnType("decimal(18,2)");

            builder.Property(od => od.TotalPrice)
                .HasColumnType("decimal(18,2)");

            builder.HasOne<ProductModel>()
               .WithMany()
               .HasForeignKey(od => od.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
