using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LWSCSecondProject.Entities
{
    public class MyDbContext: IdentityDbContext<AppUser,AppRole,string>
    {


        // to call it from service registration 
        public MyDbContext(DbContextOptions<MyDbContext> options):base(options) {
        
        }
        
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Product> Products { get;set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppRole>().ToTable("AppRoles");

            modelBuilder.Entity<ProductCategory>()
                .HasMany(p => p.Products)
                .WithOne(s=>s.Category).OnDelete(DeleteBehavior.Restrict);


             

            //modelBuilder.Entity<Product>().Property(p => p.Name).has;
            modelBuilder.Entity<ProductCategory>().HasData(

                new ProductCategory() {  Id=1 , Name= "Category 1"},
                new ProductCategory() { Id = 2, Name = "Category 2" },
                new ProductCategory() { Id = 3, Name = "Category 3" }


                );

            modelBuilder.Entity<Product>().HasData(
                new Product() {Id=1,  CategoryId=1, Name="Product 1" , Description="Product Description" ,  Price=10 },
                new Product() { Id = 2, CategoryId = 2, Name = "Product 2", Description = "Product Description", Price = 50 }

                );
        }
    }
}
