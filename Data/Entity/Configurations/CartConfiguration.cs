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

            builder.HasMany(c => c.CartItems)
                .WithOne()
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de la clave foránea para UserId 
            builder.HasOne<IdentityUser>()
                .WithMany()  
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);  

        }
    }
}
