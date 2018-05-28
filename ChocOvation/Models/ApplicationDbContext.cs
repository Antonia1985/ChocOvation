using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ChocOvation.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Material> Materials { get; set; }
        //public DbSet<Offer> Offers { get; set; }
        public DbSet<Choco> Chocos { get; set; }
        public DbSet<DosePerMaterial> DosesPerMaterials { get; set; }
        public DbSet<OfferPerMaterial> OffersPerMaterials { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ChocoStore> ChocoStores { get; set; }
        //public DbSet<Factory> Factories { get; set; }
        public DbSet<Production> Productions { get; set; }
        public DbSet<SoldProduct> SoldProducts { get; set; }
        public DbSet<OrderPerMaterial> OrderPerMaterials { get; set; }





        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
            modelBuilder
            .Entity<Employee>()
            .HasOptional(d => d.Department)
            .WithMany(e => e.Employees);

            modelBuilder
                .Entity<Department>()
                .HasMany(e => e.Employees)
                .WithOptional()
                .WillCascadeOnDelete(false);

            modelBuilder
                .Entity<Supplier>()
                .HasMany(o => o.Offers)
                .WithRequired(s => s.Supplier)
                .WillCascadeOnDelete(true);

            modelBuilder
               .Entity<Offer>()
               .HasRequired(s => s.Supplier)
               .WithMany();



            modelBuilder
                .Entity<OfferPerMaterial>()
                .HasRequired(o => o.Material);




            //modelBuilder
            //   .Entity<Offer>()
            //   .

            //modelBuilder
            //    .Entity<Offer>()
            //    .HasRequired(o => o.OffersPerMaterial).WithRequiredPrincipal();

            //modelBuilder
            //    .Entity<OfferPerMaterial>()
            //    .HasRequired(o => o.Offer).WithRequiredDependent();


            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<ChocOvation.Models.Offer> Offers { get; set; }

        public System.Data.Entity.DbSet<ChocOvation.Models.Order> Orders { get; set; }
    }
}
