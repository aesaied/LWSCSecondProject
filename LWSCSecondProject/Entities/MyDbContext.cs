using Microsoft.EntityFrameworkCore;

namespace LWSCSecondProject.Entities
{
    public class MyDbContext: DbContext
    {


        // to call it from service registration 
        public MyDbContext(DbContextOptions<MyDbContext> options):base(options) {
        
        }
        
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Product> Products { get;set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<ProductCategory>()
                .HasMany(p => p.Products)
                .WithOne(s=>s.Category).OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<ProductCategory>().HasData(

                new ProductCategory() {  Id=1 , Name= "Category 1"},
                new ProductCategory() { Id = 2, Name = "Category 2" },
                new ProductCategory() { Id = 3, Name = "Category 3" }


                );
        }
    }
}
