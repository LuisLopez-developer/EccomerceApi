using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using EccomerceApi.Entity;

namespace EccomerceApi.Data
{
    public class IdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
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
        public virtual DbSet<ProductBrand> ProductBrands { get; set; }
        public virtual DbSet<LossReason> LossReasons { get; set; }
        public virtual DbSet<ReasonForExit> ReasonForExits { get; set; }
        public virtual DbSet<ProductOutput> ProductOutputs { get; set; }


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

            modelBuilder.Entity<Product>()
                .Property(p => p.Cost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Sale>()
                .Property(s => s.Total)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SaleDetail>()
                .Property(sd => sd.UnitCost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SaleDetail>()
                .Property(sd => sd.UnitPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ProductCategory>()
                .Property(pc => pc.Name)
                .IsRequired();


            modelBuilder.Entity<ProductBrand>()
                .Property(pb => pb.Name)
                .IsRequired();

        }

        // Método para sembrar roles
        //private void SeedRoles(ModelBuilder modelBuilder)
        //{
        //    var roleAdmin = new IdentityRole { Id = "1", Name = "admin", NormalizedName = "ADMIN" };
        //    var roleUser = new IdentityRole { Id = "2", Name = "user", NormalizedName = "USER" };
        //    var roleManager = new IdentityRole { Id = "3", Name = "manager", NormalizedName = "MANAGER" };

        //    modelBuilder.Entity<IdentityRole>().HasData(
        //        roleAdmin,
        //        roleUser,
        //        roleManager
        //    );
        //}

        //// Método para sembrar usuarios
        //private void SeedUsers(ModelBuilder modelBuilder)
        //{
        //    var hasher = new PasswordHasher<IdentityUser>();

        //    // Usuario Admin
        //    var adminUser = new IdentityUser
        //    {
        //        Id = "1",
        //        UserName = "admin",
        //        NormalizedUserName = "ADMIN",
        //        Email = "admin@gmail.com",
        //        NormalizedEmail = "ADMIN@GMAIL.COM",
        //        EmailConfirmed = true,
        //        PasswordHash = hasher.HashPassword(null, "Admin@123")
        //    };

        //    // Usuario User
        //    var userUser = new IdentityUser
        //    {
        //        Id = "2",
        //        UserName = "user",
        //        NormalizedUserName = "USER",
        //        Email = "user@gmail.com",
        //        NormalizedEmail = "USER@GMAIL.COM",
        //        EmailConfirmed = true,
        //        PasswordHash = hasher.HashPassword(null, "User@123")
        //    };

        //    // Usuario Manager
        //    var managerUser = new IdentityUser
        //    {
        //        Id = "3",
        //        UserName = "manager",
        //        NormalizedUserName = "MANAGER",
        //        Email = "manager@gmail.com",
        //        NormalizedEmail = "MANAGER@GMAIL.COM",
        //        EmailConfirmed = true,
        //        PasswordHash = hasher.HashPassword(null, "Manager@123")
        //    };

        //    modelBuilder.Entity<IdentityUser>().HasData(
        //        adminUser,
        //        userUser,
        //        managerUser
        //    );

        //    modelBuilder.Entity<IdentityUserRole<string>>().HasData(
        //        new IdentityUserRole<string> { RoleId = "1", UserId = "1" }, // Admin es admin
        //        new IdentityUserRole<string> { RoleId = "2", UserId = "2" }, // User es user
        //        new IdentityUserRole<string> { RoleId = "3", UserId = "3" }  // Manager es manager
        //    );
        //}
    }
}
