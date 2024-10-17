using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Data.Entity.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<CartModel>
    {
        public void Configure(EntityTypeBuilder<CartModel> builder)
        {
            // Configuración de la clave foránea para ProductId 
            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configuración de la clave foránea para UserId 
            builder.HasOne<IdentityUser>()
                .WithMany()  
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);  

            // Otras configuraciones de propiedades
            builder.Property(c => c.Quantity).IsRequired();
            builder.Property(c => c.CreatedAt).IsRequired();
            builder.Property(c => c.UpdatedAt).IsRequired();
        }
    }
}
