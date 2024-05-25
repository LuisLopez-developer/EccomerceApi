using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EccomerceApi.Entity;
using Microsoft.AspNetCore.Identity;
using EccomerceApi.Entity.Configurations;

namespace EccomerceApi.Data
{
    public class IdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        // Entidades existentes
        public virtual DbSet<Entry> Entries { get; set; }
        public virtual DbSet<EntryDetail> EntryDetails { get; set; }
        public virtual DbSet<Loss> Losses { get; set; }
        public virtual DbSet<LostDetail> LostDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<SaleDetail> SaleDetails { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductBrand> ProductBrands { get; set; } // Agrega esta línea para ProductBrand

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración para la entidad Entry
            modelBuilder.Entity<Entry>()
                .Property(e => e.Total)
                .HasColumnType("decimal(18,2)");

            // Configuración para la entidad EntryDetail
            modelBuilder.Entity<EntryDetail>()
                .Property(ed => ed.UnitCost)
                .HasColumnType("decimal(18,2)");

            // Configuración para la entidad Loss
            modelBuilder.Entity<Loss>()
                .Property(l => l.Total)
                .HasColumnType("decimal(18,2)");

            // Configuración para la entidad LostDetail
            modelBuilder.Entity<LostDetail>()
                .Property(ld => ld.UnitCost)
                .HasColumnType("decimal(18,2)");

            // Configuración para la entidad Product
            modelBuilder.Entity<Product>()
                .Property(p => p.Cost)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            // Configuración para la entidad Sale
            modelBuilder.Entity<Sale>()
                .Property(s => s.Total)
                .HasColumnType("decimal(18,2)");

            // Configuración para la entidad SaleDetail
            modelBuilder.Entity<SaleDetail>()
                .Property(sd => sd.UnitCost)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<SaleDetail>()
                .Property(sd => sd.UnitPrice)
                .HasColumnType("decimal(18,2)");

            // Configuración para la entidad ProductCategory
            modelBuilder.Entity<ProductCategory>()
                .Property(pc => pc.Name)
                .IsRequired();
            modelBuilder.Entity<ProductCategory>()
                .HasMany(pc => pc.Products)
                .WithOne(p => p.ProductCategory)
                .HasForeignKey(p => p.IdProductCategory)
                .IsRequired(false);

            // Configuración para la entidad ProductBrand
            modelBuilder.Entity<ProductBrand>()
                .Property(pb => pb.Name)
                .IsRequired();

            // Aplicar la configuración de semillas para ProductBrand
            modelBuilder.ApplyConfiguration(new ProductBrandConfiguration());
        }
    }
}
