using Data.Entity;
using Data.Entity.Configurations;
using Data.Entity.Seeders;
using Data.Entity.Triggers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        private DbSet<Entity.Entry> entries;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Entidades existentes
        public virtual DbSet<Entry> Entries { get => entries; set => entries = value; }
        public virtual DbSet<EntryDetail> EntryDetails { get; set; }
        public virtual DbSet<Loss> Losses { get; set; }
        public virtual DbSet<LostDetail> LostDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<SaleDetail> SaleDetails { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductBrand> ProductBrands { get; set; }
        public virtual DbSet<LossReason> LossReasons { get; set; }
        public virtual DbSet<People> Peoples { get; set; }
        public virtual DbSet<ProductSpecification> ProductSpecifications { get; set; }
        public virtual DbSet<ProductPhoto> ProductPhotos { get; set; }
        public virtual DbSet<Batch> Batches { get; set; }

        // Nuevas entidades
        public virtual DbSet<CartModel> Carts { get; set; }
        public virtual DbSet<CartItemModel> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración para las entidades con tipo decimal
            modelBuilder.Entity<Entry>()
                .Property(e => e.Total)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<EntryDetail>()
                .Property(ed => ed.UnitCost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Loss>()
                .Property(l => l.Total)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<LostDetail>()
                .Property(ld => ld.UnitCost)
                .HasColumnType("decimal(18,2)");

            // Aplicar configuraciones adicionales a una entidad desde la carpeta "Configurations"
            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            modelBuilder.Entity<Sale>()
                .Property(s => s.Total)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SaleDetail>()
                .Property(sd => sd.UnitCost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SaleDetail>()
                .Property(sd => sd.UnitPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SaleDetail>()
                .Property(sd => sd.Subtotal)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ProductCategory>()
                .Property(pc => pc.Name)
                .IsRequired();


            modelBuilder.Entity<ProductBrand>()
                .Property(pb => pb.Name)
                .IsRequired();

            modelBuilder.Entity<Batch>()
                .Property(b => b.Cost)
                .HasColumnType("decimal(18,2)");

            //Configuraciones
            modelBuilder.ApplyConfiguration(new CartConfiguration());

            // Sembrar datos para la entidad AppUser y sus derivados
            PeopleSeedData.SeedData(modelBuilder);
            StateSeedData.SeedData(modelBuilder);
            AppUserSeedData.SeedData(modelBuilder);
            RoleSeedData.SeedData(modelBuilder);
            AppUserRoleSeedData.SeedData(modelBuilder);

            //Sembrar datos para las marcas yc ategorias de los productos
            ProductCategorySeedData.SeedData(modelBuilder);
            ProductBrandSeedData.SeedData(modelBuilder);

            //Sembrar datos para las razones y tipos
            LossReasonSeedData.SeedData(modelBuilder);
            EntryTypeSeedData.SeedData(modelBuilder);

        }

        // Usar la nueva convencion para crear triggers ficticios en las tablas, para manejar correctamente las opereaciones de los triggers reales
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }
    }
}
