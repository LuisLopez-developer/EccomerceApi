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
        private DbSet<EntryModel> entries;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Entidades existentes
        public virtual DbSet<EntryModel> Entries { get => entries; set => entries = value; }
        public virtual DbSet<EntryDetailModel> EntryDetails { get; set; }
        public virtual DbSet<LossModel> Losses { get; set; }
        public virtual DbSet<LostDetailModel> LostDetails { get; set; }
        public virtual DbSet<ProductModel> Products { get; set; }
        public virtual DbSet<PaymentMethodModel> PaymentMethods { get; set; }
        public virtual DbSet<OrderStatusModel> OrderStatuses { get; set; }
        public virtual DbSet<OrderModel> Orders { get; set; }
        public virtual DbSet<OrderDetailModel> OrderDetails{ get; set; }
        public virtual DbSet<StateModel> States { get; set; }
        public virtual DbSet<ProductCategoryModel> ProductCategories { get; set; }
        public virtual DbSet<ProductBrandModel> ProductBrands { get; set; }
        public virtual DbSet<LossReasonModel> LossReasons { get; set; }
        public virtual DbSet<PeopleModel> Peoples { get; set; }
        public virtual DbSet<ProductSpecificationModel> ProductSpecifications { get; set; }
        public virtual DbSet<ProductPhotoModel> ProductPhotos { get; set; }
        public virtual DbSet<BatchModel> Batches { get; set; }

        // Nuevas entidades
        public virtual DbSet<CartModel> Carts { get; set; }
        public virtual DbSet<CartItemModel> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configuración para las entidades con tipo decimal
            modelBuilder.Entity<EntryModel>()
                .Property(e => e.Total)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<EntryDetailModel>()
                .Property(ed => ed.UnitCost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<LossModel>()
                .Property(l => l.Total)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<LostDetailModel>()
                .Property(ld => ld.UnitCost)
                .HasColumnType("decimal(18,2)");

            // Aplicar configuraciones adicionales a una entidad desde la carpeta "Configurations"
            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());

            modelBuilder.Entity<ProductCategoryModel>()
                .Property(pc => pc.Name)
                .IsRequired();


            modelBuilder.Entity<ProductBrandModel>()
                .Property(pb => pb.Name)
                .IsRequired();

            modelBuilder.Entity<BatchModel>()
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

            //Sembrar datos para los estados de los pedidos
            PaymentMethodSeedData.SeedData(modelBuilder);
            OrderStatusSeedData.SeedData(modelBuilder);

        }

        // Usar la nueva convencion para crear triggers ficticios en las tablas, para manejar correctamente las opereaciones de los triggers reales
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }
    }
}
